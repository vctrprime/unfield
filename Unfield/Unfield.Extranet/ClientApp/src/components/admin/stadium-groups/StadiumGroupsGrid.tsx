import React, {useEffect, useRef, useState} from "react";
import {StadiumGroupDto} from "../../../models/dto/admin/StadiumGroupDto";
import {t} from "i18next";
import {dateFormatter} from "../../../helpers/date-formatter";
import {useInject} from "inversify-hooks";
import {IAdminService} from "../../../services/AdminService";
import {GridLoading} from "../../extranet/common/GridLoading";
import {AuthorizeUserDto} from "../../../models/dto/accounts/AuthorizeUserDto";
import {useRecoilState, useSetRecoilState} from "recoil";
import {authAtom} from "../../../state/auth";
import {Button, Input} from "semantic-ui-react";
import {stadiumAtom} from "../../../state/stadium";
import {UserPermissionDto} from "../../../models/dto/accounts/UserPermissionDto";
import {permissionsAtom} from "../../../state/permissions";
import {stadiumGroupsSearchValue} from "../../../state/admin/stadiumGroupsSearchValue";
import {useLocalStorage} from "usehooks-ts";
import {getOverlayNoRowsTemplate, getStartLkRoute} from "../../../helpers/utils";
import {GridCellWithTitleRenderer} from "../../common/GridCellWithTitleRenderer";
import {UserStadiumDto} from "../../../models/dto/accounts/UserStadiumDto";

const AgGrid = require('ag-grid-react');
const {AgGridReact} = AgGrid;

export const StadiumGroupsGrid = () => {
    const [user, setUser] = useLocalStorage<AuthorizeUserDto | null>('user', null);

    const [data, setData] = useState<StadiumGroupDto[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const [searchString, setSearchString] = useRecoilState<string>(stadiumGroupsSearchValue);
    const setStadium = useSetRecoilState<UserStadiumDto | null>(stadiumAtom);
    const [permissions, setPermissions] = useRecoilState<UserPermissionDto[]>(permissionsAtom);

    const [adminService] = useInject<IAdminService>('AdminService');

    const setAuth = useSetRecoilState(authAtom);

    const gridRef = useRef<any>();

    const onNameClick = (id: number) => {
        adminService.changeStadiumGroup(id).then((result: AuthorizeUserDto) => {
            setStadium(null);
            setPermissions([]);
            setAuth(result);
            setUser(result);
            window.open(`${window.location.origin}${getStartLkRoute(permissions)}`);
        })
    }

    const changeSearchString = (value: string) => {
        setSearchString(value);
        localStorage.setItem('stadiumGroupsSearchValue', value);
    }

    const NameRenderer = (obj: any) => {
        return <span className="link-cell" onClick={() => onNameClick(obj.data.id)}>{obj.data.name}</span>;
    }

    const columnDefs = [
        {field: 'id', cellClass: "grid-center-cell", headerName: "ID", width: 70},
        {field: 'name', headerName: t("admin:stadium_groups_grid:name"), width: 200, cellRenderer: NameRenderer},
        {
            field: 'description',
            headerName: t("admin:stadium_groups_grid:description"),
            width: 300,
            cellRenderer: (obj: any) => <GridCellWithTitleRenderer value={obj.data.description}/>
        },
        {
            field: 'usersCount',
            cellClass: "grid-center-cell",
            headerName: t("admin:stadium_groups_grid:users_count"),
            width: 200
        },
        {
            field: 'stadiumsCount',
            cellClass: "grid-center-cell",
            headerName: t("admin:stadium_groups_grid:stadiums_count"),
            width: 200
        },
        {
            field: 'dateCreated',
            cellClass: "grid-center-cell",
            headerName: t("admin:stadium_groups_grid:date_created"),
            width: 170,
            valueFormatter: dateFormatter
        },
    ];

    const fetchStadiumGroups = () => {
        setIsLoading(true);
        adminService.getStadiumGroups(searchString).then((result: StadiumGroupDto[]) => {
            setTimeout(() => {
                setData(result);
                setIsLoading(false);
            }, 500);
        })
    }

    useEffect(() => {
        fetchStadiumGroups();
    }, [])


    return (<div className="stadium-groups-container">
        <div className="stadium-groups-container-filter">
            <Input icon='search'
                   value={searchString}
                   placeholder={t('admin:stadium_groups_grid:search_placeholder')}
                   onChange={(e) => changeSearchString(e.target.value)}
            />
            <Button onClick={fetchStadiumGroups}>{t('common:search_button')}</Button>
            {data.length === 0 && !isLoading && <span>{t('admin:stadium_groups_grid:no_rows')}</span>}
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