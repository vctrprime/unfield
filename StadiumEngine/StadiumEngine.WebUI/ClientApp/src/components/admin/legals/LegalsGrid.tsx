import React, {useCallback, useEffect, useRef, useState} from "react";
import {LegalDto} from "../../../models/dto/admin/LegalDto";
import {t} from "i18next";
import {dateFormatter} from "../../../helpers/date-formatter";
import {useInject} from "inversify-hooks";
import {IAdminService} from "../../../services/AdminService";
import {GridLoading} from "../../lk/common/GridLoading";
import {useNavigate} from "react-router-dom";
import {AuthorizeUserDto} from "../../../models/dto/accounts/AuthorizeUserDto";
import {useSetRecoilState} from "recoil";
import {authAtom} from "../../../state/auth";
import {Input} from "semantic-ui-react";
import {stadiumAtom} from "../../../state/stadium";
import {UserPermissionDto} from "../../../models/dto/accounts/UserPermissionDto";
import {permissionsAtom} from "../../../state/permissions";

const AgGrid = require('ag-grid-react');
const { AgGridReact } = AgGrid;

export const LegalsGrid = () => {
    const [data, setData] = useState<LegalDto[]>([]);
    const [searchString, setSearchString] = useState<string|null>(null);

    const setStadium = useSetRecoilState<number | null>(stadiumAtom);
    const setPermissions = useSetRecoilState<UserPermissionDto[]>(permissionsAtom);
    
    const setAuth = useSetRecoilState(authAtom);

    const gridRef = useRef<any>();

    const navigate = useNavigate();
    
    const columnDefs = [
        {field: 'id', headerName: "ID", width: 300 },
        {field: 'name', headerName: t("admin:legals_grid:name"), width: 300 },
        {field: 'headName', headerName: t("admin:legals_grid:head_name"), width: 300 },
        {field: 'inn', headerName: t("admin:legals_grid:inn"), width: 300 },
        {field: 'description', headerName: t("admin:legals_grid:description"), width: 300 },
        {field: 'country', headerName: t("admin:legals_grid:country"), width: 300 },
        {field: 'region', headerName: t("admin:legals_grid:region"), width: 300 },
        {field: 'city', headerName: t("admin:legals_grid:city"), width: 300 },
        {field: 'usersCount', headerName: t("admin:legals_grid:users_count"), width: 300},
        {field: 'stadiumsCount', headerName: t("admin:legals_grid:stadiums_count"), width: 300},
        {field: 'dateCreated', headerName: t("admin:legals_grid:date_created"), width: 300, valueFormatter: dateFormatter},
    ];

    const [adminService] = useInject<IAdminService>('AdminService');

    const fetchLegals = () => {
        adminService.getLegals(searchString).then((result: LegalDto[]) => {
            setTimeout(() => {
                setData(result);
            }, 500);
        })
    }

    const onSelectionChanged = useCallback(() => {
        if (gridRef.current !== undefined) {
            const selectedRows = gridRef.current.api.getSelectedRows();
            if (selectedRows.length > 0) {
                adminService.changeLegal(selectedRows[0].id).then((result: AuthorizeUserDto) => {
                   setStadium(null);
                   setPermissions([]);
                   setAuth(result);
                   navigate("/lk");
                })
            }
        }

    }, []);
    
    useEffect(() => {
        if (searchString !== null && searchString.length > 2) {
            fetchLegals();
        }
        else {
            setData([]);
        }
    }, [searchString])

    const getOverlayNoRowsTemplate = () => {
        if (searchString === null || searchString.length === 0) {
            return `<span>${t('admin:legals_grid:search_input_notification')}</span>`;
        }

        return `<span>${t('admin:legals_grid:no_rows')}</span>`;
    }
    
    return (<div className="legals-container">
        <Input icon='search'
               placeholder={t('admin:legals_grid:search_placeholder')}
               onChange={(e) => setSearchString(e.target.value)}
        />
        <div className="grid-container ag-theme-alpine" style={{height: 'calc(100% - 40px'}}>
            {data === null ? <GridLoading columns={columnDefs}/> : <AgGridReact
                ref={gridRef}
                rowData={data}
                rowSelection={'single'}
                columnDefs={columnDefs}
                rowDeselection={true}
                onSelectionChanged={onSelectionChanged}
                overlayNoRowsTemplate={getOverlayNoRowsTemplate()}
            />}
        </div>
    </div>)
}