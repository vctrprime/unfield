import React, {useEffect, useRef, useState} from 'react';
import {getOverlayNoRowsTemplate, getTitle} from "../../../helpers/utils";
import {useInject} from "inversify-hooks";
import {t} from "i18next";
import {GridCellWithTitleRenderer} from "../../common/GridCellWithTitleRenderer";
import {dateFormatter, dateFormatterWithoutTime} from "../../../helpers/date-formatter";
import {useNavigate} from "react-router-dom";
import {TariffDto} from "../../../models/dto/rates/TariffDto";
import {GridLoading} from "../common/GridLoading";
import {Button, Checkbox} from "semantic-ui-react";
import {useRecoilValue, useSetRecoilState} from "recoil";
import {stadiumAtom} from "../../../state/stadium";
import {UpdateTariffCommand} from "../../../models/command/rates/UpdateTariffCommand";
import {IRatesService} from "../../../services/RatesService";
import {PermissionsKeys} from "../../../static/PermissionsKeys";
import {permissionsAtom} from "../../../state/permissions";
import {tariffsAtom} from "../../../state/rates/tariffs";

const AgGrid = require('ag-grid-react');
const {AgGridReact} = AgGrid;

export const Tariffs = () => {
    document.title = getTitle("rates:tariffs_tab")

    const stadium = useRecoilValue(stadiumAtom);
    const permissions = useRecoilValue(permissionsAtom);
    const setTariffs = useSetRecoilState<TariffDto[]>(tariffsAtom);

    const [data, setData] = useState<TariffDto[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const [ratesService] = useInject<IRatesService>('RatesService');

    const gridRef = useRef<any>();

    const navigate = useNavigate();

    const onNameClick = (id: number) => {
        navigate("/lk/rates/tariffs/" + id);
    }

    const NameRenderer = (obj: any) => {
        return <span className="link-cell" onClick={() => onNameClick(obj.data.id)}>{obj.data.name}</span>;
    }

    const IsActiveRenderer = (obj: any) => {
        return <Checkbox onChange={() => toggleIsActive(obj.node.id, obj.data)} toggle checked={obj.data.isActive}/>;
    }

    const PeriodRenderer = (obj: any) => {
        const row = obj.data as TariffDto;

        if (row.dateEnd === null) {
            return <span>{dateFormatterWithoutTime({value: row.dateStart})} - {t("rates:tariffs_grid:no_date_end")}</span>
        }

        return <span>{dateFormatterWithoutTime({value: row.dateStart})} - {dateFormatterWithoutTime({value: row.dateEnd})}</span>;
    }

    const DaysRenderer = (obj: any) => {
        const row = obj.data as TariffDto;
        const result = [] as string[];

        if (row.monday) {
            result.push(t("rates:tariffs_grid:monday"))
        }
        if (row.tuesday) {
            result.push(t("rates:tariffs_grid:tuesday"))
        }
        if (row.wednesday) {
            result.push(t("rates:tariffs_grid:wednesday"))
        }
        if (row.thursday) {
            result.push(t("rates:tariffs_grid:thursday"))
        }
        if (row.friday) {
            result.push(t("rates:tariffs_grid:friday"))
        }
        if (row.saturday) {
            result.push(t("rates:tariffs_grid:saturday"))
        }
        if (row.sunday) {
            result.push(t("rates:tariffs_grid:sunday"))
        }

        return <span>{result.join('; ')}</span>
    }

    const toggleIsActive = (nodeId: number, data: TariffDto) => {
        const command: UpdateTariffCommand = {
            id: data.id,
            name: data.name,
            description: data.description,
            isActive: !data.isActive,
            dateStart: data.dateStart,
            dateEnd: data.dateEnd,
            monday: data.monday,
            tuesday: data.tuesday,
            wednesday: data.wednesday,
            thursday: data.thursday,
            friday: data.friday,
            saturday: data.saturday,
            sunday: data.sunday,
            dayIntervals: data.dayIntervals,
            promoCodes: data.promoCodes
        }
        ratesService.updateTariff(command).then(() => {
            const rowNode = gridRef.current.api.getRowNode(nodeId);
            rowNode.setDataValue('isActive', command.isActive);
        });
    }

    const columnDefs = [
        {
            field: 'isActive',
            cellClass: "grid-center-cell grid-vcenter-cell",
            headerName: '',
            width: 90,
            cellRenderer: IsActiveRenderer
        },
        {field: 'name', headerName: t("rates:tariffs_grid:name"), width: 200, cellRenderer: NameRenderer},
        {
            field: 'date_start',
            headerName: t("rates:tariffs_grid:period"),
            width: 200,
            cellRenderer: PeriodRenderer
        },
        {
            field: 'monday',
            headerName: t("rates:tariffs_grid:days"),
            width: 200,
            cellRenderer: DaysRenderer
        },
        {
            field: 'description',
            headerName: t("rates:tariffs_grid:description"),
            width: 500,
            cellRenderer: (obj: any) => <GridCellWithTitleRenderer value={obj.data.description}/>
        },
        {
            field: 'userCreated',
            cellClass: "grid-center-cell",
            headerName: t("rates:tariffs_grid:user_created"),
            width: 200
        },
        {
            field: 'dateCreated',
            cellClass: "grid-center-cell",
            headerName: t("rates:tariffs_grid:date_created"),
            width: 170,
            valueFormatter: dateFormatter
        },
    ];

    const fetchTariffs = () => {
        setIsLoading(true);
        ratesService.getTariffs().then((result: TariffDto[]) => {
            setTimeout(() => {
                setData(result);
                setTariffs(result);
                setIsLoading(false);
            }, 500);
        })
    }

    useEffect(() => {
        fetchTariffs();
    }, [stadium])

    return (<div className="tariffs-container">
        <div className="tariffs-btns">
            <Button
                disabled={permissions.filter(p => p.name === PermissionsKeys.InsertTariff).length === 0}
                onClick={() => navigate("/lk/rates/tariffs/new")}>{t('rates:tariffs_grid:add')}</Button>
            {data.length === 0 && !isLoading && <span>{t('rates:tariffs_grid:no_rows')}</span>}
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