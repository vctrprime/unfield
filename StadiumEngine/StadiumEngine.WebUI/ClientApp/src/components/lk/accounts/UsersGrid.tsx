import React, {useRef, useState, useEffect} from 'react';
import {GridLoading} from "../common/GridLoading";
import {dateFormatter} from "../../../helpers/date-formatter";
import {UserDto} from "../../../models/dto/accounts/UserDto";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";

const AgGrid = require('ag-grid-react');
const { AgGridReact } = AgGrid;

export const UsersGrid = () => {
    const [data, setData] = useState<UserDto[] | null>(null);
    const gridRef = useRef<any>();

    const [columnDefs, setColumnDefs] = useState([
        {field: 'name', headerName: "Имя", width: 300},
        {field: 'lastName', headerName: "Фамилия", width: 300},
        {field: 'phoneNumber', headerName: "Логин", width: 300},
        {field: 'roleName', headerName: "Роль", width: 300},
        {field: 'userCreated', headerName: "Добавил", width: 300},
        {field: 'dateCreated', headerName: "Дата добавления", width: 300, valueFormatter: dateFormatter},
        {field: 'dateModified', headerName: "Дата изменения", width: 300, valueFormatter: dateFormatter},
        {field: 'lastLoginDate', headerName: "Дата последнего входа", width: 300, valueFormatter: dateFormatter}
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