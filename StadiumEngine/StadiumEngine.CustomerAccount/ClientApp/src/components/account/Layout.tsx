import React from 'react';
import {Outlet, useNavigate} from "react-router-dom";
import {NavMenu} from "./NavMenu";
import {Header} from "./Header";

import {t} from "i18next";

export const Layout = () => {
    return (
        <div style={{display: 'flex', height: '100vh', overflow: 'scroll initial'}}>
            <NavMenu/>
            <div className="right-container">
                <Header/>
                <Outlet/>
                <div className="lk-footer">{t('common:footer')}</div>
            </div>

        </div>
    );
}