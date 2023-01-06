import React, {useEffect, useRef, useState, useCallback} from 'react';
import {useFetchWrapper} from "../../../helpers/fetch-wrapper";
import { AgGridReact } from 'ag-grid-react';
import {GridLoading} from "../common/GridLoading";

export const RolesGrid = ({setSelectedRole}) => {
    const [data, setData] = useState(null);
    const gridRef = useRef();

    const [columnDefs, setColumnDefs] = useState([
        {field: 'name', headerName: "Название", width: 300 },
        {field: 'description', headerName: "Описание", width: 300 },
        {field: 'usersCount', headerName: "Кол-во пользователей", width: 300},
        {field: 'stadiumsCount', headerName: "Кол-во объектов", width: 300},
        {field: 'userCreated', headerName: "Добавил", width: 300},
        {field: 'dateCreated', headerName: "Дата добавления", width: 300}
    ]);

    const fetchWrapper = useFetchWrapper();
    
    useEffect(() => {
        fetchRoles();
    }, [])
    
    const fetchRoles = () => {
        fetchWrapper.get({url: 'api/accounts/roles', withSpinner: false, hideSpinner: false}).then((result) => {
            setTimeout(() => {
                setData(result);
            }, 500);
        })
    }

    const onSelectionChanged = useCallback(() => {
        const selectedRows = gridRef.current.api.getSelectedRows();
        if (selectedRows.length > 0) {
            setSelectedRole(selectedRows[0]);
        }
        else {
            setSelectedRole(selectedRows[0]);
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