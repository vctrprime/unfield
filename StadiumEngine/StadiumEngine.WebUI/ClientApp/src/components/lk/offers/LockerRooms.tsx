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

const AgGrid = require('ag-grid-react');
const { AgGridReact } = AgGrid;

export const LockerRooms = () => {
    document.title = getTitle("offers:locker_rooms_tab")

    const stadium = useRecoilValue(stadiumAtom);

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

    const columnDefs = [
        {field: 'name', headerName: t("offers:locker_rooms_grid:name"), width: 200, cellRenderer: NameRenderer },
        {field: 'description', headerName: t("offers:locker_rooms_grid:description"), width: 300, cellRenderer: (obj: any) => <GridCellWithTitleRenderer value={obj.data.description}/> },
        {field: 'dateCreated', cellClass: "grid-center-cell without-border", headerName: t("offers:locker_rooms_grid:date_created"), width: 170, valueFormatter: dateFormatter},
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
            <Button>{t('offers:locker_rooms_grid:add')}</Button>
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