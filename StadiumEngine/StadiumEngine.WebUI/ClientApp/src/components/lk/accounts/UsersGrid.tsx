import React, {useRef, useState, useEffect} from 'react';
import {GridLoading} from "../common/GridLoading";
import {dateFormatter} from "../../../helpers/date-formatter";
import {UserDto} from "../../../models/dto/accounts/UserDto";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";
import {t} from "i18next";
import {getOverlayNoRowsTemplate} from "../../../helpers/utils";
import {Button} from "semantic-ui-react";
import {useRecoilValue} from "recoil";
import {permissionsAtom} from "../../../state/permissions";

const AgGrid = require('ag-grid-react');
const { AgGridReact } = AgGrid;

export const UsersGrid = () => {
    const [data, setData] = useState<UserDto[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const permissions = useRecoilValue(permissionsAtom);
    
    const gridRef = useRef<any>();
    
    
    const columnDefs = [
        {field: 'name', headerName: t("accounts:users_grid:name"), width: 150},
        {field: 'lastName', headerName: t("accounts:users_grid:last_name"), width: 170},
        {field: 'phoneNumber', cellClass: "grid-center-cell", headerName: t("accounts:users_grid:phone_number"), width: 150},
        {field: 'roleName', cellClass: "grid-center-cell", headerName: t("accounts:users_grid:role_name"), width: 170},
        {field: 'userCreated', cellClass: "grid-center-cell", headerName: t("accounts:users_grid:user_created"), width: 200},
        {field: 'dateCreated', cellClass: "grid-center-cell", headerName: t("accounts:users_grid:date_created"), width: 170, valueFormatter: dateFormatter},
        {field: 'lastLoginDate', cellClass: "grid-center-cell", headerName: t("accounts:users_grid:last_login_date"), width: 200, valueFormatter: dateFormatter}
    ]

    const [accountsService] = useInject<IAccountsService>('AccountsService');

    useEffect(() => {
        fetchUsers();
    }, [])

    const fetchUsers = () => {
        setIsLoading(true);
        accountsService.getUsers().then((result: UserDto[]) => {
            setTimeout(() => {
                setData(result);
                setIsLoading(false);
            }, 500);
        })
    }
    
    return (
        <div className="users-container">
            <Button disabled={permissions.filter(p => p.name === 'insert-user').length === 0} className="add-user-button">{t('accounts:users_grid:add')}</Button>
            {data.length === 0 && !isLoading && <span className="no-rows-message">{t('accounts:users_grid:no_rows')}</span>}
            <div className="grid-container ag-theme-alpine" style={{height: 'calc(100% - 36px)'}}>
                {isLoading ? <GridLoading columns={columnDefs} /> : <AgGridReact
                    ref={gridRef}
                    rowData={data}
                    columnDefs={columnDefs}
                    overlayNoRowsTemplate={getOverlayNoRowsTemplate()}
                />}
            </div>
        </div>)
}