import React, {useEffect, useRef, useState} from 'react';
import {getOverlayNoRowsTemplate, getTitle} from "../../../helpers/utils";
import {t} from "i18next";
import {dateFormatter, dateFormatterWithoutTime} from "../../../helpers/date-formatter";
import {BreakDto} from "../../../models/dto/settings/BreakDto";
import {Button, Checkbox} from "semantic-ui-react";
import {useNavigate} from "react-router-dom";
import {useInject} from "inversify-hooks";
import {ISettingsService} from "../../../services/SettingsService";
import {useRecoilValue, useSetRecoilState} from "recoil";
import {stadiumAtom} from "../../../state/stadium";
import {PermissionsKeys} from "../../../static/PermissionsKeys";
import {GridLoading} from "../common/GridLoading";
import {permissionsAtom} from "../../../state/permissions";
import {UpdateBreakCommand} from "../../../models/command/settings/UpdateBreakCommand";
import {breaksAtom} from "../../../state/settings/breaks";
import {parseNumber} from "../../../helpers/time-point-parser";

const AgGrid = require('ag-grid-react');
const {AgGridReact} = AgGrid;

export const Breaks = () => {
    document.title = getTitle("settings:breaks_tab")

    const stadium = useRecoilValue(stadiumAtom);
    const permissions = useRecoilValue(permissionsAtom);
    const setBreaks = useSetRecoilState<BreakDto[]>(breaksAtom);

    const [data, setData] = useState<BreakDto[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const [settingsService] = useInject<ISettingsService>('SettingsService');

    const gridRef = useRef<any>();

    const navigate = useNavigate();

    const onNameClick = (id: number) => {
        navigate("/settings/breaks/" + id);
    }

    const toggleIsActive = (nodeId: number, data: BreakDto) => {
        const command: UpdateBreakCommand = {
            id: data.id,
            name: data.name,
            description: data.description,
            isActive: !data.isActive,
            dateStart: data.dateStart,
            dateEnd: data.dateEnd,
            startHour: data.startHour,
            endHour: data.endHour,
            selectedFields: data.selectedFields,
        }
        settingsService.updateBreak(command).then(() => {
            const rowNode = gridRef.current.api.getRowNode(nodeId);
            rowNode.setDataValue('isActive', command.isActive);
        });
    }


    const NameRenderer = (obj: any) => {
        return <span className="link-cell" onClick={() => onNameClick(obj.data.id)}>{obj.data.name}</span>;
    }

    const IsActiveRenderer = (obj: any) => {
        return <Checkbox onChange={() => toggleIsActive(obj.node.id, obj.data)} toggle checked={obj.data.isActive}/>;
    }
    
    const PeriodRenderer = (obj: any) => {
        const row = obj.data as BreakDto;

        if (row.dateEnd === null) {
            return <span>{dateFormatterWithoutTime({value: row.dateStart})} - {t("settings:breaks_grid:no_date_end")}</span>
        }

        return <span>{dateFormatterWithoutTime({value: row.dateStart})} - {dateFormatterWithoutTime({value: row.dateEnd})}</span>;
    }

    const HoursPeriodRenderer = (obj: any) => {
        const row = obj.data as BreakDto;
        return <span>{parseNumber(row.startHour)} - {parseNumber(row.endHour)}</span>;
    }

    const columnDefs = [
        {
            field: 'isActive',
            cellClass: "grid-center-cell grid-vcenter-cell",
            headerName: '',
            width: 90,
            cellRenderer: IsActiveRenderer
        },
        {field: 'name', headerName: t("settings:breaks_grid:name"), width: 200, cellRenderer: NameRenderer},
        {
            field: 'date_start',
            headerName: t("settings:breaks_grid:period"),
            width: 200,
            cellRenderer: PeriodRenderer
        },
        {
            field: 'start_hour',
            headerName: t("settings:breaks_grid:hours_period"),
            width: 200,
            cellRenderer: HoursPeriodRenderer,
            cellClass: "grid-center-cell"
        },
        {
            field: 'fieldsCount',
            headerName: t("settings:breaks_grid:fields_count"),
            width: 200,
            cellClass: "grid-center-cell"
        },
        {
            field: 'userCreated',
            cellClass: "grid-center-cell",
            headerName: t("settings:breaks_grid:user_created"),
            width: 200
        },
        {
            field: 'dateCreated',
            cellClass: "grid-center-cell",
            headerName: t("settings:breaks_grid:date_created"),
            width: 170,
            valueFormatter: dateFormatter
        },
    ];

    const fetchBreaks = () => {
        setIsLoading(true);
        settingsService.getBreaks().then((result: BreakDto[]) => {
            setTimeout(() => {
                setData(result);
                setBreaks(JSON.parse(JSON.stringify(result)));
                setIsLoading(false);
            }, 500);
        })
    }

    useEffect(() => {
        fetchBreaks();
    }, [stadium])

    return (<div className="breaks-container">
        <div className="breaks-btns">
            <Button
                disabled={permissions.filter(p => p.name === PermissionsKeys.InsertBreak).length === 0}
                onClick={() => navigate("/settings/breaks/new")}>{t('settings:breaks_grid:add')}</Button>
            {data.length === 0 && !isLoading && <span>{t('settings:breaks_grid:no_rows')}</span>}
        </div>
        <div className="grid-container ag-theme-alpine" style={{height: 'calc(100% - 36px)'}}>
            {isLoading ? <GridLoading columns={columnDefs}/> : <AgGridReact
                ref={gridRef}
                rowData={data}
                columnDefs={columnDefs}
                overlayNoRowsTemplate={getOverlayNoRowsTemplate()}
            />}
        </div>
    </div>)
}