import React, {useEffect, useRef, useState, useCallback} from 'react';
import {GridLoading} from "../common/GridLoading";
import {useSetRecoilState} from "recoil";
import {rolesAtom} from "../../../state/roles";
import {dateFormatter} from "../../../helpers/date-formatter";
import {RoleDto} from "../../../models/dto/accounts/RoleDto";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";

const AgGrid = require('ag-grid-react');
const { AgGridReact } = AgGrid;

export const RolesGrid = ({setSelectedRole} : any) => {
    const [data, setData] = useState<RoleDto[] | null>(null);
    const setRecoilRoles = useSetRecoilState(rolesAtom);
    
    const gridRef = useRef<any>();

    const [columnDefs, setColumnDefs] = useState([
        {field: 'name', headerName: "Название", width: 300 },
        {field: 'description', headerName: "Описание", width: 300 },
        {field: 'usersCount', headerName: "Кол-во пользователей", width: 300},
        {field: 'stadiumsCount', headerName: "Кол-во объектов", width: 300},
        {field: 'userCreated', headerName: "Добавил", width: 300},
        {field: 'dateCreated', headerName: "Дата добавления", width: 300, valueFormatter: dateFormatter},
        {field: 'dateModified', headerName: "Дата изменения", width: 300, valueFormatter: dateFormatter},
    ]);

    const [accountsService] = useInject<IAccountsService>('AccountsService');
    
    useEffect(() => {
        fetchRoles();
    }, [])
    
    const fetchRoles = () => {
        accountsService.getRoles().then((result: RoleDto[]) => {
            setTimeout(() => {
                setData(result);
                setRecoilRoles(result.map((r) => {
                    return { key: r.id, value: r.id, text: r.name }
                }));
            }, 500);
        })
    }

    const onSelectionChanged = useCallback(() => {
        if (gridRef.current !== undefined) {
            const selectedRows = gridRef.current.api.getSelectedRows();
            if (selectedRows.length > 0) {
                setSelectedRole(selectedRows[0]);
            }
            else {
                setSelectedRole(selectedRows[0]);
            }
        }
        
    }, []);
    
    return (
        <div className="roles-container">
            <div className="grid-container ag-theme-alpine" style={{height: '100%'}}>
                {data === null ? <GridLoading columns={columnDefs}/> : <AgGridReact
                    ref={gridRef}
                    rowData={data}
                    rowSelection={'single'}
                    columnDefs={columnDefs}
                    rowDeselection={true}
                    onSelectionChanged={onSelectionChanged}
                />}
            </div>
        </div>)
} ;