import React, {useRef, useState, useEffect} from 'react';
import {GridLoading} from "../common/GridLoading";
import {dateFormatter} from "../../../helpers/date-formatter";
import {RoleDto} from "../../../models/dto/accounts/RoleDto";
import {StadiumDto} from "../../../models/dto/accounts/StadiumDto";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";

const AgGrid = require('ag-grid-react');
const { AgGridReact } = AgGrid;

interface StadiumsGridProps {
    selectedRole: RoleDto | null
}

export const StadiumsGrid = ({selectedRole} : StadiumsGridProps) => {
    const [data, setData] = useState<StadiumDto[] | null>(null);
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