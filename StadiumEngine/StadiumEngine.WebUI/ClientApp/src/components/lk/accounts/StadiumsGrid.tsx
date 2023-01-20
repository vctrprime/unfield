import React, {useRef, useState, useEffect} from 'react';
import {GridLoading} from "../common/GridLoading";
import {dateFormatter} from "../../../helpers/date-formatter";
import {RoleDto} from "../../../models/dto/accounts/RoleDto";
import {StadiumDto} from "../../../models/dto/accounts/StadiumDto";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";
import {t} from "i18next";

const AgGrid = require('ag-grid-react');
const { AgGridReact } = AgGrid;

interface StadiumsGridProps {
    selectedRole: RoleDto | null
}

export const StadiumsGrid = ({selectedRole} : StadiumsGridProps) => {
    const [data, setData] = useState<StadiumDto[] | null>(null);
    const gridRef = useRef();

    const [columnDefs, setColumnDefs] = useState([
        {field: 'name', headerName: t("accounts:stadiums_grid:name"), width: 300},
        {field: 'isRoleBound', headerName: t("accounts:stadiums_grid:is_role_bound"), width: 300},
        {field: 'country', headerName: t("accounts:stadiums_grid:country"), width: 300},
        {field: 'region', headerName: t("accounts:stadiums_grid:region"), width: 300},
        {field: 'city', headerName: t("accounts:stadiums_grid:city"), width: 300},
        {field: 'address', headerName: t("accounts:stadiums_grid:address"), width: 300},
        {field: 'description', headerName: t("accounts:stadiums_grid:description"), width: 300},
        {field: 'dateCreated', headerName: t("accounts:stadiums_grid:date_created"), width: 300, valueFormatter: dateFormatter}
    ]);

    const [accountsService] = useInject<IAccountsService>('AccountsService');
    
    useEffect(() => {
        if (selectedRole === null) 
        {
            setTimeout(() => {
                setData([]);
            }, 500);
        }
        else {
            fetchStadiums();
        }
    }, [selectedRole])

    const fetchStadiums = () => {
        if (selectedRole !== null) {
            accountsService.getStadiums(selectedRole.id)
                .then((result: StadiumDto[]) => {
                setData(result);
            })
        }
    }
    
    const getOverlayNoRowsTemplate = () => {
        if (selectedRole === null) {
            return `<span>${t('accounts:stadiums_grid:select_role_notification')}</span>`;
        }
        
        return `<span>${t('accounts:stadiums_grid:no_rows')}</span>`;
    }

    return (
        <div className="stadiums-container">
            <div className="grid-title">{t("accounts:stadiums_grid_title")}</div>

            <div className="grid-container ag-theme-alpine">
                {data === null ? <GridLoading columns={columnDefs}/> : <AgGridReact
                    ref={gridRef}
                    rowData={data}
                    columnDefs={columnDefs}
                    overlayNoRowsTemplate={getOverlayNoRowsTemplate()}
                />}
            </div>
        </div>)
}