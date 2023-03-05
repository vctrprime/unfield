import React, {useEffect, useRef, useState} from 'react';
import {getOverlayNoRowsTemplate, getTitle} from "../../../helpers/utils";
import {useInject} from "inversify-hooks";
import {t} from "i18next";
import {GridCellWithTitleRenderer} from "../../common/GridCellWithTitleRenderer";
import {dateFormatter} from "../../../helpers/date-formatter";
import {useNavigate} from "react-router-dom";
import {PriceGroupDto} from "../../../models/dto/rates/PriceGroupDto";
import {GridLoading} from "../common/GridLoading";
import {Button, Input} from "semantic-ui-react";
import {useRecoilValue, useSetRecoilState} from "recoil";
import {stadiumAtom} from "../../../state/stadium";
import {Checkbox} from 'semantic-ui-react'
import {UpdatePriceGroupCommand} from "../../../models/command/rates/UpdatePriceGroupCommand";
import {IRatesService} from "../../../services/RatesService";
import {PermissionsKeys} from "../../../static/PermissionsKeys";
import {permissionsAtom} from "../../../state/permissions";
import {FieldDto} from "../../../models/dto/offers/FieldDto";
import {fieldsAtom} from "../../../state/offers/fields";
import {priceGroupsAtom} from "../../../state/rates/priceGroups";

const AgGrid = require('ag-grid-react');
const {AgGridReact} = AgGrid;

export const PriceGroups = () => {
    document.title = getTitle("rates:price_groups_tab")

    const stadium = useRecoilValue(stadiumAtom);
    const permissions = useRecoilValue(permissionsAtom);
    const setPriceGroups = useSetRecoilState<PriceGroupDto[]>(priceGroupsAtom);

    const [data, setData] = useState<PriceGroupDto[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const [ratesService] = useInject<IRatesService>('RatesService');

    const gridRef = useRef<any>();

    const navigate = useNavigate();

    const onNameClick = (id: number) => {
        navigate("/lk/rates/price-groups/" + id);
    }

    const NameRenderer = (obj: any) => {
        return <span className="link-cell" onClick={() => onNameClick(obj.data.id)}>{obj.data.name}</span>;
    }

    const IsActiveRenderer = (obj: any) => {
        return <Checkbox onChange={() => toggleIsActive(obj.node.id, obj.data)} toggle checked={obj.data.isActive}/>;
    }

    const toggleIsActive = (nodeId: number, data: PriceGroupDto) => {
        const command: UpdatePriceGroupCommand = {
            id: data.id,
            name: data.name,
            description: data.description,
            isActive: !data.isActive
        }
        ratesService.updatePriceGroup(command).then(() => {
            const rowNode = gridRef.current.api.getRowNode(nodeId);
            rowNode.setDataValue('isActive', command.isActive);
        });
    }

    const FieldNamesRenderer = (obj: any) => {
        const names = obj.data.fieldNames;
        return <span>{names.join('; ')}</span>;
    }

    const columnDefs = [
        {
            field: 'isActive',
            cellClass: "grid-center-cell grid-vcenter-cell",
            headerName: '',
            width: 90,
            cellRenderer: IsActiveRenderer
        },
        {field: 'name', headerName: t("rates:price_groups_grid:name"), width: 200, cellRenderer: NameRenderer},
        {
            field: 'fieldNames',
            headerName: t("rates:price_groups_grid:fields"),
            width: 600,
            cellRenderer: FieldNamesRenderer
        },
        {
            field: 'description',
            headerName: t("rates:price_groups_grid:description"),
            width: 500,
            cellRenderer: (obj: any) => <GridCellWithTitleRenderer value={obj.data.description}/>
        },
        {
            field: 'userCreated',
            cellClass: "grid-center-cell",
            headerName: t("rates:price_groups_grid:user_created"),
            width: 200
        },
        {
            field: 'dateCreated',
            cellClass: "grid-center-cell",
            headerName: t("rates:price_groups_grid:date_created"),
            width: 170,
            valueFormatter: dateFormatter
        },
    ];

    const fetchPriceGroups = () => {
        setIsLoading(true);
        ratesService.getPriceGroups().then((result: PriceGroupDto[]) => {
            setTimeout(() => {
                setData(result);
                setPriceGroups(result);
                setIsLoading(false);
            }, 500);
        })
    }

    useEffect(() => {
        fetchPriceGroups();
    }, [stadium])

    return (<div className="price-groups-container">
        <div className="price-groups-btns">
            <Button
                disabled={permissions.filter(p => p.name === PermissionsKeys.InsertPriceGroup).length === 0}
                onClick={() => navigate("/lk/rates/price-groups/new")}>{t('rates:price_groups_grid:add')}</Button>
            {data.length === 0 && !isLoading && <span>{t('rates:price_groups_grid:no_rows')}</span>}
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