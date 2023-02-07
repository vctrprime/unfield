import React from 'react';
import { Outlet} from "react-router-dom";
import {NavMenu} from "./NavMenu";
import {Header} from "./Header";
import '../../css/lk/Main.scss';
import '../../css/lk/Accounts.scss';
import '../../css/lk/Offers.scss';
import {isMobile} from 'react-device-detect';
import {useRecoilValue} from "recoil";
import {stadiumAtom} from "../../state/stadium";

import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import {MobileNotification} from "../common/MobileNotification";

export const Layout = () => {
    const stadium = useRecoilValue(stadiumAtom);


    if(isMobile) {
        return (
            <MobileNotification/>
        )
    }
    
    return (
        <div
            style={{ display: 'flex', height: '100vh', overflow: 'scroll initial' }}
        >
            <NavMenu />
            <div className="right-container">
                <Header />
                {stadium !== null && <div className="content-container">
                    <Outlet />
                </div>}
            </div>
           
        </div>
    );
}