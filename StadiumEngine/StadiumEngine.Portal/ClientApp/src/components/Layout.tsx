import React from 'react';
import cells from "../img/cells.jpeg";


interface LayoutProps {
    children: React.ReactNode
}

export const Layout = (props: LayoutProps) => {
    
    return (
        <div style={{
            height: '100%',
            overflow: "auto",
            background: `linear-gradient( rgba(255, 255, 255, 0.9), rgba(255, 255, 255, 0.9) ), repeat center / cover url(${cells})`}}>
            {props.children}
        </div>
    );
}

