import React, {useEffect, useRef, useState} from 'react';
import Skeleton from 'react-loading-skeleton'
import 'react-loading-skeleton/dist/skeleton.css'

export const GridLoading = ({ columns }) => {
    const [height, setHeight] = useState(0);

    const ref = useRef(null);

    useEffect(() => {
        setHeight(ref.current.parentElement.offsetHeight);
    }, []);
    
    function TableRow() {
        return (
            <tr style={{height: '42px'}}>
                {columns.map((c, i) => {
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
                        {columns.map((c, i) => {
                            return <th  key={i} width={c.width}>
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