import React from 'react';
import {Outlet, useNavigate} from "react-router-dom";
import {Header} from "./Header";

import {t} from "i18next";

export const Layout = () => {
    return (
        <div style={{display: 'flex', flexDirection: "column", height: '100vh', overflow: 'scroll initial'}}>
            <Header/>
            <Outlet/>
        </div>
    );
}