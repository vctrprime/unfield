import React, {useEffect, useRef, useState} from 'react';
import {getFieldBasicFormData, getOverlayNoRowsTemplate, getTitle} from "../../../helpers/utils";
import {Button, Checkbox} from "semantic-ui-react";
import {t} from "i18next";
import {GridLoading} from "../common/GridLoading";
import {useRecoilValue, useSetRecoilState} from "recoil";
import {stadiumAtom} from "../../../state/stadium";
import {useInject} from "inversify-hooks";
import {IOffersService} from "../../../services/OffersService";
import {useNavigate} from "react-router-dom";
import {GridCellWithTitleRenderer} from "../../common/GridCellWithTitleRenderer";
import {dateFormatter} from "../../../helpers/date-formatter";
import {FieldDto} from "../../../models/dto/offers/FieldDto";
import {Parent} from "../../common/tree/Parent";
import {Child} from "../../common/tree/Child";
import {FieldCoveringType} from "../../../models/dto/offers/enums/FieldCoveringType";
import {SportKindsRenderer} from "../common/GridRenderers";
import {PermissionsKeys} from "../../../static/PermissionsKeys";
import {permissionsAtom} from "../../../state/permissions";
import {fieldsAtom} from "../../../state/offers/fields";

const AgGrid = require('ag-grid-react');
const { AgGridReact } = AgGrid;

export const Fields = () => {
    document.title = getTitle("offers:fields_tab")

    const stadium = useRecoilValue(stadiumAtom);
    const permissions = useRecoilValue(permissionsAtom);
    const setFields = useSetRecoilState<FieldDto[]>(fieldsAtom);

    const [data, setData] = useState<FieldDto[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const [offersService] = useInject<IOffersService>('OffersService');

    const gridRef = useRef<any>();

    const navigate = useNavigate();

    const onNameClick = (id: number) => {
        navigate("/lk/offers/fields/" + id);
    }

    const NameRenderer = (obj : any) => {
        return <span className="link-cell" onClick={() => onNameClick(obj.data.id)}>{obj.data.name}</span>;
    }
    
    const SizeRenderer = (obj : any) => {
        return <span>{obj.data.length}x{obj.data.width}</span>;
    }

    const CoveringRenderer = (obj : any) => {
        const value = FieldCoveringType[obj.data.coveringType];
        
        return <span>{ t("offers:coverings:" + value.toLowerCase())}</span>;
    }

    
    const IsActiveRenderer = (obj: any) => {
        return <Checkbox onChange={() => toggleIsActive(obj.node.id, obj.data)} toggle checked={obj.data.isActive}/>;
    }
    
    const TreeColumnRenderer = (obj: any) => {
        return obj.data.parentFieldId ?  <Child isLastChild={obj.data.isLastChild} /> : <Parent hasChild={data.filter(f => f.parentFieldId === obj.data.id).length > 0}/>
    }
    
    const toggleIsActive = (nodeId: number, data: FieldDto) => {
        data.isActive = !data.isActive;
        
        const form = getFieldBasicFormData(data);
        
        for (let i = 0; i < data.images.length; i++) {
            form.append('images['+i+'].path', data.images[i]);
            form.append('images['+i+'].formFile', '');
        }
        
        offersService.updateField(form).then(() => {
            const rowNode = gridRef.current.api.getRowNode(nodeId);
            rowNode.setDataValue('isActive', data.isActive);
        });
    }

    const columnDefs = [
        {field: 'parentFieldId', cellClass: "grid-center-cell grid-vcenter-cell no-vborder", headerName: '', width: 50, cellRenderer: TreeColumnRenderer},
        {field: 'isActive', cellClass: "grid-center-cell grid-vcenter-cell", headerName: '', width: 90, cellRenderer: IsActiveRenderer},
        {field: 'name', headerName: t("offers:fields_grid:name"), width: 250, cellRenderer: NameRenderer },
        {field: 'size', cellClass: "grid-center-cell", headerName: t("offers:fields_grid:size"), width: 120, cellRenderer: SizeRenderer },
        {field: 'priceGroupName', headerName: t("offers:fields_grid:price_group"), width: 200 },
        {field: 'coveringType', cellClass: "grid-center-cell", headerName: t("offers:fields_grid:covering"), width: 200, cellRenderer: CoveringRenderer },
        {field: 'sportKinds', headerName: t("offers:sports:title"), width: 500, cellRenderer: SportKindsRenderer },
        {field: 'description', headerName: t("offers:fields_grid:description"), width: 500, cellRenderer: (obj: any) => <GridCellWithTitleRenderer value={obj.data.description}/> },
        {field: 'userCreated', cellClass: "grid-center-cell", headerName: t("offers:fields_grid:user_created"), width: 200},
        {field: 'dateCreated', cellClass: "grid-center-cell", headerName: t("offers:fields_grid:date_created"), width: 170, valueFormatter: dateFormatter},
    ];

    const fetchFields = () => {
        setIsLoading(true);
        offersService.getFields().then((result: FieldDto[]) => {
            setTimeout(() => {
                setData(result);
                setFields(result);
                setIsLoading(false);
            }, 500);
        })
    }

    useEffect(() => {
        fetchFields();
    }, [stadium])

    return (<div className="fields-container">
        <div className="fields-btns">
            <Button
                disabled={permissions.filter(p => p.name === PermissionsKeys.InsertField).length === 0}
                onClick={() => navigate("/lk/offers/fields/new")}>{t('offers:fields_grid:add')}</Button>
            {data.length === 0 && !isLoading && <span>{t('offers:fields_grid:no_rows')}</span>}
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