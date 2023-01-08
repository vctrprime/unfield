import React, {useRef, useState, useEffect} from 'react';
import {useFetchWrapper} from "../../../helpers/fetch-wrapper";
import {AgGridReact} from "ag-grid-react";
import {GridLoading} from "../common/GridLoading";
import {dateFormatter} from "../../../helpers/date-formatter";

export const StadiumsGrid = ({selectedRole}) => {
    const [data, setData] = useState(null);
    const gridRef = useRef();

    const [columnDefs, setColumnDefs] = useState([
        {field: 'name', headerName: "Название", width: 300},
        {field: 'isRoleBound', headerName: "Связан", width: 300},
        {field: 'country', headerName: "Страна", width: 300},
        {field: 'region', headerName: "Регион", width: 300},
        {field: 'city', headerName: "Город", width: 300},
        {field: 'address', headerName: "Адрес", width: 300},
        {field: 'description', headerName: "Описание", width: 300},
        {field: 'dateCreated', headerName: "Дата добавления", width: 300, valueFormatter: dateFormatter}
    ]);

    const fetchWrapper = useFetchWrapper();
    
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
        fetchWrapper.get({url: `api/accounts/stadiums/${selectedRole.id}`}).then((result) => {
            setData(result);
        })
    }

    return (
        <div className="stadiums-container">
            <div className="grid-title">Объекты</div>

            <div className="grid-container ag-theme-alpine">
                {data === null ? <GridLoading columns={columnDefs}/> : <AgGridReact
                    ref={gridRef}
                    rowData={data}
                    columnDefs={columnDefs}
                    overlayNoRowsTemplate={selectedRole === null ? "<span>Выберите роль в верхней таблице, чтобы получить информацию по связям объектов</span>" : "<span>Объектов не найдено</span>"}
                />}
            </div>
        </div>)
}