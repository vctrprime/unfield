import React from 'react';
import {Outlet} from "react-router-dom";
import {Header} from "./Header";
import '../../css/admin/Legals.scss';
import {isMobile} from 'react-device-detect';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import {MobileNotification} from "../common/MobileNotification";
import {getTitle} from "../../helpers/utils";

export const Layout = () => {
    document.title = getTitle("admin:header:panel_admin_title")

    if (isMobile) {
        return (
            <MobileNotification/>
        )
    }

    return (
        <div
            style={{display: 'flex', height: '100vh', overflow: 'scroll initial'}}
        >
            <div className="right-container">
                <Header/>
                <div className="content-container">
                    <Outlet/>
                </div>
            </div>

        </div>
    );
}