import React, {useEffect, useRef, useState} from 'react';
import Skeleton from 'react-loading-skeleton'
import 'react-loading-skeleton/dist/skeleton.css'
import {AgGridReact} from "ag-grid-react";

export const GridLoading = ({columns}: any) => {
    const [height, setHeight] = useState<number>(0);

    const ref = useRef<HTMLDivElement>(null);

    useEffect(() => {
        if (ref.current !== null) {
            setHeight(ref.current.parentElement?.offsetHeight || 0);
        }
    }, []);

    const loadingColumns = () => {
        return columns.map((c: any, i: number) => {
            return {
                field: c.field,
                headerName: c.headerName,
                width: c.width,
                cellRenderer: () => <Skeleton/>
            }
        });
    }

    const data = () => {
        return new Array(Math.floor((height - 59) / 42)).fill({});
    }

    return (
        <div style={{height: '100%', width: '100%'}} ref={ref}>
            {height !== 0 ?
                <AgGridReact
                    rowData={data()}
                    columnDefs={loadingColumns()}
                /> : <span/>}

        </div>);
};