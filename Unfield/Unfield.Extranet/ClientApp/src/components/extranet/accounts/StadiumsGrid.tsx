import React, {useEffect, useRef, useState} from 'react';
import {GridLoading} from "../common/GridLoading";
import {dateFormatter} from "../../../helpers/date-formatter";
import {RoleDto} from "../../../models/dto/accounts/RoleDto";
import {StadiumDto} from "../../../models/dto/accounts/StadiumDto";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";
import {t} from "i18next";
import {GridCellWithTitleRenderer} from "../../common/GridCellWithTitleRenderer";
import {useRecoilValue, useSetRecoilState} from "recoil";
import {changeBindingStadiumAtom} from "../../../state/changeBindingStadium";
import {UserDto} from "../../../models/dto/accounts/UserDto";

const AgGrid = require('ag-grid-react');
const {AgGridReact} = AgGrid;

interface StadiumsGridProps {
    selectedUser: UserDto | null
}

export const StadiumsGrid = ({selectedUser}: StadiumsGridProps) => {
    const [data, setData] = useState<StadiumDto[] | null>(null);
    const gridRef = useRef<any>();

    const setChangeBindingStadium = useSetRecoilState(changeBindingStadiumAtom);

    const toggleBindStadium = (nodeId: string, id: number, value: boolean) => {
        setChangeBindingStadium(null);
        if (selectedUser !== null) {
            accountsService.toggleUserStadium({
                userId: selectedUser.id,
                stadiumId: id
            })
                .then(() => {
                    if (gridRef.current !== undefined) {
                        const rowNode = gridRef.current.api.getRowNode(nodeId);
                        rowNode.setDataValue('isUserBound', value);
                        setChangeBindingStadium(value);
                    }
                })

        }

    }

    const BindingButtonRenderer = (obj: any) => {
        const value = obj.data.isUserBound;
        const title = value ? t('accounts:stadiums_grid:is_user_bound') : t('accounts:stadiums_grid:is_user_unbound');

        return <i title={title} style={value ? {color: '#00d2ff'} : {}}
                  onClick={() => toggleBindStadium(obj.node.id, obj.data.id, !value)} className="fa fa-link"
                  aria-hidden="true"/>;
    }
    
    
    const columnDefs = [
        {
            field: 'isUserBound',
            cellClass: "grid-center-cell",
            headerName: '',
            cellRenderer: BindingButtonRenderer,
            width: 60
        },
        {field: 'name', 
            headerName: t("accounts:stadiums_grid:name"), 
            width: 250},
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
        if (selectedUser === null) {
            setTimeout(() => {
                setData([]);
            }, 500);
        } else {
            fetchStadiums();
        }
    }, [selectedUser])

    const fetchStadiums = () => {
        if (selectedUser !== null) {
            accountsService.getStadiums(selectedUser.id)
                .then((result: StadiumDto[]) => {
                    setData(result);
                })
        }
    }

    const getOverlayNoRowsTemplate = () => {
        if (selectedUser === null) {
            return `<span>${t('accounts:stadiums_grid:select_user_notification')}</span>`;
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