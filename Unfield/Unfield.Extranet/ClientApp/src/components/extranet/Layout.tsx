import React from 'react';
import {Outlet, useNavigate} from "react-router-dom";
import {NavMenu} from "./NavMenu";
import {Header} from "./Header";
import '../../css/extranet/Main.scss';
import '../../css/extranet/Accounts.scss';
import '../../css/extranet/Offers.scss';
import '../../css/extranet/Rates.scss';
import '../../css/extranet/Schedule.scss';
import '../../css/extranet/Settings.scss';
import '../../css/extranet/Notifications.scss';
import {isMobile} from 'react-device-detect';
import {useRecoilValue} from "recoil";
import {stadiumAtom} from "../../state/stadium";

import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import {MobileNotification} from "../common/MobileNotification";
import {t} from "i18next";
import {getStartLkRoute} from "../../helpers/utils";
import {permissionsAtom} from "../../state/permissions";
import {UserPermissionDto} from "../../models/dto/accounts/UserPermissionDto";

export const Layout = () => {
    const stadium = useRecoilValue(stadiumAtom);
    const permissions = useRecoilValue(permissionsAtom);
    
    const navigate = useNavigate();

    if (isMobile) {
        return (
            <MobileNotification/>
        )
    }
    
    if (window.location.pathname.length < 3) {
        navigate(getStartLkRoute(permissions));
    }
    

    return (
        <div
            style={{display: 'flex', height: '100vh', overflow: 'scroll initial'}}
        >
            <NavMenu/>
            <div className="right-container">
                <Header/>
                {stadium !== null && <div className="content-container">
                    <Outlet/>
                </div>}
                <div className="lk-footer">{t('common:footer')}</div>
            </div>

        </div>
    );
}