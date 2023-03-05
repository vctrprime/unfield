import React, {useEffect, useRef, useState} from "react";
import {LegalDto} from "../../../models/dto/admin/LegalDto";
import {t} from "i18next";
import {dateFormatter} from "../../../helpers/date-formatter";
import {useInject} from "inversify-hooks";
import {IAdminService} from "../../../services/AdminService";
import {GridLoading} from "../../lk/common/GridLoading";
import {AuthorizeUserDto} from "../../../models/dto/accounts/AuthorizeUserDto";
import {useRecoilState, useSetRecoilState} from "recoil";
import {authAtom} from "../../../state/auth";
import {Button, Input} from "semantic-ui-react";
import {stadiumAtom} from "../../../state/stadium";
import {UserPermissionDto} from "../../../models/dto/accounts/UserPermissionDto";
import {permissionsAtom} from "../../../state/permissions";
import {legalsSearchValue} from "../../../state/admin/legalsSearchValue";
import {useLocalStorage} from "usehooks-ts";
import {getOverlayNoRowsTemplate} from "../../../helpers/utils";
import {GridCellWithTitleRenderer} from "../../common/GridCellWithTitleRenderer";

const AgGrid = require('ag-grid-react');
const {AgGridReact} = AgGrid;

export const LegalsGrid = () => {
    const [user, setUser] = useLocalStorage<AuthorizeUserDto | null>('user', null);

    const [data, setData] = useState<LegalDto[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const [searchString, setSearchString] = useRecoilState<string>(legalsSearchValue);
    const setStadium = useSetRecoilState<number | null>(stadiumAtom);
    const setPermissions = useSetRecoilState<UserPermissionDto[]>(permissionsAtom);

    const [adminService] = useInject<IAdminService>('AdminService');

    const setAuth = useSetRecoilState(authAtom);

    const gridRef = useRef<any>();

    const onNameClick = (id: number) => {
        adminService.changeLegal(id).then((result: AuthorizeUserDto) => {
            setStadium(null);
            setPermissions([]);
            setAuth(result);
            setUser(result);
            window.open(`${window.location.origin}/lk`);
        })
    }

    const changeSearchString = (value: string) => {
        setSearchString(value);
        localStorage.setItem('legalsSearchValue', value);
    }

    const NameRenderer = (obj: any) => {
        return <span className="link-cell" onClick={() => onNameClick(obj.data.id)}>{obj.data.name}</span>;
    }

    const columnDefs = [
        {field: 'id', cellClass: "grid-center-cell", headerName: "ID", width: 70},
        {field: 'name', headerName: t("admin:legals_grid:name"), width: 200, cellRenderer: NameRenderer},
        {field: 'city', cellClass: "grid-center-cell", headerName: t("admin:legals_grid:city"), width: 200},
        {field: 'inn', cellClass: "grid-center-cell", headerName: t("admin:legals_grid:inn"), width: 170},
        {field: 'headName', headerName: t("admin:legals_grid:head_name"), width: 300},
        {
            field: 'description',
            headerName: t("admin:legals_grid:description"),
            width: 300,
            cellRenderer: (obj: any) => <GridCellWithTitleRenderer value={obj.data.description}/>
        },
        //{field: 'country', headerName: t("admin:legals_grid:country"), width: 300 },
        //{field: 'region', headerName: t("admin:legals_grid:region"), width: 300 },
        {
            field: 'usersCount',
            cellClass: "grid-center-cell",
            headerName: t("admin:legals_grid:users_count"),
            width: 200
        },
        {
            field: 'stadiumsCount',
            cellClass: "grid-center-cell",
            headerName: t("admin:legals_grid:stadiums_count"),
            width: 200
        },
        {
            field: 'dateCreated',
            cellClass: "grid-center-cell",
            headerName: t("admin:legals_grid:date_created"),
            width: 170,
            valueFormatter: dateFormatter
        },
    ];

    const fetchLegals = () => {
        setIsLoading(true);
        adminService.getLegals(searchString).then((result: LegalDto[]) => {
            setTimeout(() => {
                setData(result);
                setIsLoading(false);
            }, 500);
        })
    }

    useEffect(() => {
        fetchLegals();
    }, [])


    return (<div className="legals-container">
        <div className="legals-container-filter">
            <Input icon='search'
                   value={searchString}
                   placeholder={t('admin:legals_grid:search_placeholder')}
                   onChange={(e) => changeSearchString(e.target.value)}
            />
            <Button onClick={fetchLegals}>{t('common:search_button')}</Button>
            {data.length === 0 && !isLoading && <span>{t('admin:legals_grid:no_rows')}</span>}
        </div>

        <div className="grid-container ag-theme-alpine" style={{height: 'calc(100% - 40px'}}>
            {isLoading ? <GridLoading columns={columnDefs}/> : <AgGridReact
                ref={gridRef}
                rowData={data}
                columnDefs={columnDefs}
                overlayNoRowsTemplate={getOverlayNoRowsTemplate()}
            />}
        </div>
    </div>)
}