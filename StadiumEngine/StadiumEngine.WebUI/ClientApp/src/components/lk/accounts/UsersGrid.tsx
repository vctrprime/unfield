import React, {useRef, useState, useEffect} from 'react';
import {GridLoading} from "../common/GridLoading";
import {dateFormatter} from "../../../helpers/date-formatter";
import {UserDto} from "../../../models/dto/accounts/UserDto";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";
import {t} from "i18next";

const AgGrid = require('ag-grid-react');
const { AgGridReact } = AgGrid;

export const UsersGrid = () => {
    const [data, setData] = useState<UserDto[] | null>(null);
    const gridRef = useRef<any>();

    const [columnDefs, setColumnDefs] = useState([
        {field: 'name', headerName: t("accounts:users_grid:name"), width: 300},
        {field: 'lastName', headerName: t("accounts:users_grid:last_name"), width: 300},
        {field: 'phoneNumber', headerName: t("accounts:users_grid:phone_number"), width: 300},
        {field: 'roleName', headerName: t("accounts:users_grid:role_name"), width: 300},
        {field: 'userCreated', headerName: t("accounts:users_grid:user_created"), width: 300},
        {field: 'dateCreated', headerName: t("accounts:users_grid:date_created"), width: 300, valueFormatter: dateFormatter},
        {field: 'lastLoginDate', headerName: t("accounts:users_grid:last_login_date"), width: 300, valueFormatter: dateFormatter}
    ]);

    const [accountsService] = useInject<IAccountsService>('AccountsService');

    useEffect(() => {
        fetchUsers();
    }, [])

    const fetchUsers = () => {
        accountsService.getUsers().then((result: UserDto[]) => {
            setTimeout(() => {
                setData(result);
            }, 500);
        })
    }

    return (
        <div className="users-container">
            <div className="grid-container ag-theme-alpine" style={{height: '100%'}}>
                {data === null ? <GridLoading columns={columnDefs} /> : <AgGridReact
                    ref={gridRef}
                    rowData={data}
                    columnDefs={columnDefs}
                />}
            </div>
        </div>)
}