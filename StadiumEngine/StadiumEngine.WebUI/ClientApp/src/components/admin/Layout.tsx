import React from 'react';
import {Outlet} from "react-router-dom";
import {Header} from "./Header";
import '../../css/admin/Legals.scss';
import {isMobile} from 'react-device-detect';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import {MobileNotification} from "../common/MobileNotification";

export const Layout = () => {
    
    if(isMobile) {
        return (
            <MobileNotification/>
        )
    }
    
    return (
        <div
            style={{ display: 'flex', height: '100vh', overflow: 'scroll initial' }}
        >
            <div className="right-container">
                <Header />
                <div className="content-container">
                    <Outlet />
                </div>
            </div>
           
        </div>
    );
}