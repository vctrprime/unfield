import React, {useEffect, useRef, useState} from 'react';
import {getOverlayNoRowsTemplate, getTitle} from "../../../helpers/utils";
import {useInject} from "inversify-hooks";
import {IOffersService} from "../../../services/OffersService";
import {t} from "i18next";
import {GridCellWithTitleRenderer} from "../../common/GridCellWithTitleRenderer";
import {dateFormatter} from "../../../helpers/date-formatter";
import {useNavigate} from "react-router-dom";
import {LockerRoomDto} from "../../../models/dto/offers/LockerRoomDto";
import {GridLoading} from "../common/GridLoading";
import {Button, Input} from "semantic-ui-react";
import {useRecoilValue} from "recoil";
import {stadiumAtom} from "../../../state/stadium";
import { Checkbox } from 'semantic-ui-react'
import {UpdateLockerRoomCommand} from "../../../models/command/offers/UpdateLockerRoomCommand";
import {LockerRoomGender} from "../../../models/dto/offers/enums/LockerRoomGender";
import {PermissionsKeys} from "../../../static/PermissionsKeys";
import {permissionsAtom} from "../../../state/permissions";

const AgGrid = require('ag-grid-react');
const { AgGridReact } = AgGrid;

export const LockerRooms = () => {
    document.title = getTitle("offers:locker_rooms_tab")

    const stadium = useRecoilValue(stadiumAtom);
    const permissions = useRecoilValue(permissionsAtom);

    const [data, setData] = useState<LockerRoomDto[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const [offersService] = useInject<IOffersService>('OffersService');

    const gridRef = useRef<any>();
    
    const navigate = useNavigate();

    const onNameClick = (id: number) => {
        navigate("/lk/offers/locker-rooms/" + id);
    }

    const NameRenderer = (obj : any) => {
        return <span className="link-cell" onClick={() => onNameClick(obj.data.id)}>{obj.data.name}</span>;
    }
    
    const IsActiveRenderer = (obj: any) => {
        return <Checkbox onChange={() => toggleIsActive(obj.node.id, obj.data)} toggle checked={obj.data.isActive}/>;
    }
    
    const GenderRenderer = (obj: any) => {
        if (obj.data.gender == LockerRoomGender.Male) {
            return <i className="fa fa-male" aria-hidden="true"/>
        }
        else if (obj.data.gender == LockerRoomGender.Female) {
            return <i className="fa fa-female" aria-hidden="true"/>
        }
        else {
            return <div>
                <i className="fa fa-male" aria-hidden="true"/>
                <i className="fa fa-female" aria-hidden="true"/>
            </div>;
        }
    }
    
    const toggleIsActive = (nodeId: number, data: LockerRoomDto) => {
      const command: UpdateLockerRoomCommand = {
          id: data.id,
          name: data.name,
          description: data.description,
          gender: data.gender,
          isActive: !data.isActive
      }
      offersService.updateLockerRoom(command).then(() => {
          const rowNode = gridRef.current.api.getRowNode(nodeId);
          rowNode.setDataValue('isActive', command.isActive);
      });
    }

    const columnDefs = [
        {field: 'isActive', cellClass: "grid-center-cell grid-vcenter-cell", headerName: '', width: 90, cellRenderer: IsActiveRenderer},
        {field: 'name', headerName: t("offers:locker_rooms_grid:name"), width: 200, cellRenderer: NameRenderer },
        {field: 'gender', cellClass: "grid-center-cell", headerName: t("offers:locker_rooms_grid:gender"), width: 90, cellRenderer: GenderRenderer },
        {field: 'description', headerName: t("offers:locker_rooms_grid:description"), width: 500, cellRenderer: (obj: any) => <GridCellWithTitleRenderer value={obj.data.description}/> },
        {field: 'userCreated', cellClass: "grid-center-cell", headerName: t("offers:locker_rooms_grid:user_created"), width: 200},
        {field: 'dateCreated', cellClass: "grid-center-cell", headerName: t("offers:locker_rooms_grid:date_created"), width: 170, valueFormatter: dateFormatter},
    ];

    const fetchLockerRooms = () => {
        setIsLoading(true);
        offersService.getLockerRooms().then((result: LockerRoomDto[]) => {
            setTimeout(() => {
                setData(result);
                setIsLoading(false);
            }, 500);
        })
    }

    useEffect(() => {
        fetchLockerRooms();
    }, [stadium])

    return (<div className="locker-rooms-container">
        <div className="locker-rooms-btns">
            <Button
                disabled={permissions.filter(p => p.name === PermissionsKeys.InsertLockerRoom).length === 0}
                onClick={() => navigate("/lk/offers/locker-rooms/new")}>{t('offers:locker_rooms_grid:add')}</Button>
            {data.length === 0 && !isLoading && <span>{t('offers:locker_rooms_grid:no_rows')}</span>}
        </div>
        <div className="grid-container ag-theme-alpine" style={{height: 'calc(100% - 36px)'}}>
            {isLoading ? <GridLoading columns={columnDefs}/> : <AgGridReact
                ref={gridRef}
                rowData={data}
                columnDefs={columnDefs}
                overlayNoRowsTemplate={getOverlayNoRowsTemplate()}
            />}
        </div>
    </div>)
}