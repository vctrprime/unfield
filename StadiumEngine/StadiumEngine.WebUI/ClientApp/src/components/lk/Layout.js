import React from 'react';
import {Outlet} from "react-router-dom";
import {NavMenu} from "./NavMenu";
import {Header} from "./Header";
import '../../css/lk/Main.scss';

export const Layout = () => {
    return (
        <div
            style={{ display: 'flex', height: '100vh', overflow: 'scroll initial' }}
        >
            <NavMenu />
            <div className="right-container">
                <Header />
                <div className="content-container">
                    <Outlet />
                </div>
            </div>
           
        </div>
    );
}