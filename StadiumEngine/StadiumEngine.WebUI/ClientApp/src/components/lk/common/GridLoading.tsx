import React, {useEffect, useRef, useState} from 'react';
import Skeleton from 'react-loading-skeleton'
import 'react-loading-skeleton/dist/skeleton.css'

export const GridLoading = ({ columns } : any) => {
    const [height, setHeight] = useState<number>(0);

    const ref = useRef<HTMLDivElement>(null);

    useEffect(() => {
        if (ref.current !== null) {
            setHeight(ref.current.parentElement?.offsetHeight || 0);
        }
    }, []);
    
    function TableRow() {
        return (
            <tr style={{height: '42px'}}>
                {columns.map((c: any, i: number) => {
                    return <td key={i} style={{verticalAlign: 'middle'}}>
                        <Skeleton height={20}/>
                    </td>
                })}
            </tr>
        )
    }
    
    
    return ( 
        
        
        <div style={{ height: '100%', width: '100%', display: 'table'}} ref={ref}>

            {height !== 0 ? 
                <table className="table" style={{ position: "absolute", 
                    tableLayout: "fixed",
                backgroundColor: "white"}}>
                    <thead>
                    <tr className="grid-loading-header">
                        {columns.map((c: any, i:number) => {
                            return <th key={i} style={{ width: c.width}}>
                                {c.headerName}
                            </th>
                        })}
                    </tr>
                    </thead>
                    <tbody>
                    {new Array(Math.floor((height - 48)/42)).fill('').map((r, i) => (
                        <TableRow key={i}/>
                    ))}
                    </tbody>
                </table> : <span />}
            
        </div>);
} ;