import React, {useEffect, useRef, useState} from 'react';
import {GridLoading} from "../common/GridLoading";
import {dateFormatter} from "../../../helpers/date-formatter";
import {RoleDto} from "../../../models/dto/accounts/RoleDto";
import {StadiumDto} from "../../../models/dto/accounts/StadiumDto";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";
import {t} from "i18next";
import {GridCellWithTitleRenderer} from "../../common/GridCellWithTitleRenderer";
import {useSetRecoilState} from "recoil";
import {changeBindingStadiumAtom} from "../../../state/changeBindingStadium";

const AgGrid = require('ag-grid-react');
const {AgGridReact} = AgGrid;

interface StadiumsGridProps {
    selectedRole: RoleDto | null
}

export const StadiumsGrid = ({selectedRole}: StadiumsGridProps) => {
    const [data, setData] = useState<StadiumDto[] | null>(null);
    const gridRef = useRef<any>();

    const setChangeBindingStadium = useSetRecoilState(changeBindingStadiumAtom);

    const toggleBindStadium = (nodeId: string, id: number, value: boolean) => {
        setChangeBindingStadium(null);
        if (selectedRole !== null) {
            accountsService.toggleRoleStadium({
                roleId: selectedRole.id,
                stadiumId: id
            })
                .then(() => {
                    if (gridRef.current !== undefined) {
                        const rowNode = gridRef.current.api.getRowNode(nodeId);
                        rowNode.setDataValue('isRoleBound', value);
                        setChangeBindingStadium(value);
                    }
                })

        }

    }

    const BindingButtonRenderer = (obj: any) => {
        const value = obj.data.isRoleBound;
        const title = value ? t('accounts:stadiums_grid:is_role_bound') : t('accounts:stadiums_grid:is_role_unbound');

        return <i title={title} style={value ? {color: '#00d2ff'} : {}}
                  onClick={() => toggleBindStadium(obj.node.id, obj.data.id, !value)} className="fa fa-link"
                  aria-hidden="true"/>;
    }

    const columnDefs = [
        {
            field: 'isRoleBound',
            cellClass: "grid-center-cell",
            headerName: '',
            cellRenderer: BindingButtonRenderer,
            width: 60
        },
        {field: 'name', headerName: t("accounts:stadiums_grid:name"), width: 250},
        //{field: 'country', headerName: t("accounts:stadiums_grid:country"), width: 300},
        //{field: 'region', headerName: t("accounts:stadiums_grid:region"), width: 300},
        {field: 'city', cellClass: "grid-center-cell", headerName: t("accounts:stadiums_grid:city"), width: 200},
        {field: 'address', cellClass: "grid-center-cell", headerName: t("accounts:stadiums_grid:address"), width: 200},
        {
            field: 'description',
            headerName: t("accounts:stadiums_grid:description"),
            width: 400,
            cellRenderer: (obj: any) => <GridCellWithTitleRenderer value={obj.data.description}/>
        },
        {
            field: 'dateCreated',
            cellClass: "grid-center-cell",
            headerName: t("accounts:stadiums_grid:date_created"),
            width: 170,
            valueFormatter: dateFormatter
        }
    ]

    const [accountsService] = useInject<IAccountsService>('AccountsService');

    useEffect(() => {
        if (selectedRole === null) {
            setTimeout(() => {
                setData([]);
            }, 500);
        } else {
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

    const getOverlayNoRowsTemplate = () => {
        if (selectedRole === null) {
            return `<span>${t('accounts:stadiums_grid:select_role_notification')}</span>`;
        }

        return `<span>${t('accounts:stadiums_grid:no_rows')}</span>`;
    }


    return (
        <div className="stadiums-container">
            <div className="grid-title">{t("accounts:stadiums_grid_title")}</div>

            <div className="grid-container ag-theme-alpine">
                {data === null ? <GridLoading columns={columnDefs}/> : <AgGridReact
                    ref={gridRef}
                    rowData={data}
                    columnDefs={columnDefs}
                    overlayNoRowsTemplate={getOverlayNoRowsTemplate()}
                />}
            </div>
        </div>)
}