import React, {useEffect, useRef, useState} from 'react';
import {getInventoryBasicFormData, getOverlayNoRowsTemplate, getTitle} from "../../../helpers/utils";
import {Button, Checkbox} from "semantic-ui-react";
import {t} from "i18next";
import {GridLoading} from "../common/GridLoading";
import {useRecoilValue, useSetRecoilState} from "recoil";
import {stadiumAtom} from "../../../state/stadium";
import {useInject} from "inversify-hooks";
import {IOffersService} from "../../../services/OffersService";
import {useNavigate} from "react-router-dom";
import {GridCellWithTitleRenderer} from "../../common/GridCellWithTitleRenderer";
import {dateFormatter} from "../../../helpers/date-formatter";
import {InventoryDto} from "../../../models/dto/offers/InventoryDto";
import {Currency} from "../../../models/dto/offers/enums/Currency";
import {SportKindsRenderer} from "../common/GridRenderers";
import {PermissionsKeys} from "../../../static/PermissionsKeys";
import {permissionsAtom} from "../../../state/permissions";
import {inventoriesAtom} from "../../../state/offers/inventories";

const AgGrid = require('ag-grid-react');
const {AgGridReact} = AgGrid;

export const Inventories = () => {
    document.title = getTitle("offers:inventories_tab")

    const stadium = useRecoilValue(stadiumAtom);
    const permissions = useRecoilValue(permissionsAtom);
    const setInventories = useSetRecoilState<InventoryDto[]>(inventoriesAtom);

    const [data, setData] = useState<InventoryDto[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const [offersService] = useInject<IOffersService>('OffersService');

    const gridRef = useRef<any>();

    const navigate = useNavigate();

    const onNameClick = (id: number) => {
        navigate("/extranet/offers/inventories/" + id);
    }

    const NameRenderer = (obj: any) => {
        return <span className="link-cell" onClick={() => onNameClick(obj.data.id)}>{obj.data.name}</span>;
    }

    const PriceRenderer = (obj: any) => {
        const currency = Currency[obj.data.currency];

        return <span>{obj.data.price} {t("offers:currencies:" + currency.toLowerCase())}</span>;
    }


    const IsActiveRenderer = (obj: any) => {
        return <Checkbox onChange={() => toggleIsActive(obj.node.id, obj.data)} toggle checked={obj.data.isActive}/>;
    }


    const toggleIsActive = (nodeId: number, data: InventoryDto) => {
        data.isActive = !data.isActive;

        const form = getInventoryBasicFormData(data);

        for (let i = 0; i < data.images.length; i++) {
            form.append('images[' + i + '].path', data.images[i]);
            form.append('images[' + i + '].formFile', '');
        }

        offersService.updateInventory(form).then(() => {
            const rowNode = gridRef.current.api.getRowNode(nodeId);
            rowNode.setDataValue('isActive', data.isActive);
        });
    }

    const columnDefs = [
        {
            field: 'isActive',
            cellClass: "grid-center-cell grid-vcenter-cell",
            headerName: '',
            width: 90,
            cellRenderer: IsActiveRenderer
        },
        {field: 'name', headerName: t("offers:inventories_grid:name"), width: 250, cellRenderer: NameRenderer},
        {
            field: 'quantity',
            cellClass: "grid-center-cell",
            headerName: t("offers:inventories_grid:quantity"),
            width: 120
        },
        {
            field: 'price',
            cellClass: "grid-center-cell",
            headerName: t("offers:inventories_grid:price"),
            width: 200,
            cellRenderer: PriceRenderer
        },
        {field: 'sportKinds', headerName: t("offers:sports:title"), width: 500, cellRenderer: SportKindsRenderer},
        {
            field: 'description',
            headerName: t("offers:inventories_grid:description"),
            width: 500,
            cellRenderer: (obj: any) => <GridCellWithTitleRenderer value={obj.data.description}/>
        },
        {
            field: 'userCreated',
            cellClass: "grid-center-cell",
            headerName: t("offers:inventories_grid:user_created"),
            width: 200
        },
        {
            field: 'dateCreated',
            cellClass: "grid-center-cell",
            headerName: t("offers:inventories_grid:date_created"),
            width: 170,
            valueFormatter: dateFormatter
        },
    ];

    const fetchInventories = () => {
        setIsLoading(true);
        offersService.getInventories().then((result: InventoryDto[]) => {
            setTimeout(() => {
                setData(result);
                setInventories(JSON.parse(JSON.stringify(result)));
                setIsLoading(false);
            }, 500);
        })
    }

    useEffect(() => {
        fetchInventories();
    }, [stadium])

    return (<div className="inventories-container">
        <div className="inventories-btns">
            <Button
                disabled={permissions.filter(p => p.name === PermissionsKeys.InsertInventory).length === 0}
                onClick={() => navigate("/extranet/offers/inventories/new")}>{t('offers:inventories_grid:add')}</Button>
            {data.length === 0 && !isLoading && <span>{t('offers:inventories_grid:no_rows')}</span>}
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