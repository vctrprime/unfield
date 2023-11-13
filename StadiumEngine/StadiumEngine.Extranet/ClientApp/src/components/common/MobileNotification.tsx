import {NavbarBrand} from "reactstrap";
import {Link, NavLink} from "react-router-dom";
import logo from "../../img/logo/logo_icon_with_title.png";
import React from "react";
import {t} from "i18next";
import {useRecoilValue} from "recoil";
import {envAtom} from "../../state/env";

export const MobileNotification = () => {
    const env = useRecoilValue(envAtom);
    
    return (<div className="sign-in-container">
        <div className="color-block bottom-color-block" style={{top: 0}}/>
        <div className="color-block top-color-block"/>

        <div className="logo-container">
            <NavbarBrand className={"navbar-brand-ext"} tag={Link} to={env?.portalHost||'/'}>
                <img className={"logo"} alt={"Stadium Engine"} src={logo}/>
            </NavbarBrand>
            <div className={"version-title"}>{process.env.REACT_APP_VERSION}</div>
        </div>

        <div style={{
            width: '100%',
            position: "absolute",
            left: 0,
            top: "150px",
            zIndex: 2,
            padding: '10px 50px',
            textAlign: 'center'
        }}>{t('common:mobile_notification')}</div>

        <NavLink className="portal-button" to={env?.portalHost||'/'}>
            <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" fill="currentColor"
                 className="bi bi-house" viewBox="0 0 16 16">
                <path
                    d="M8.707 1.5a1 1 0 0 0-1.414 0L.646 8.146a.5.5 0 0 0 .708.708L2 8.207V13.5A1.5 1.5 0 0 0 3.5 15h9a1.5 1.5 0 0 0 1.5-1.5V8.207l.646.647a.5.5 0 0 0 .708-.708L13 5.793V2.5a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1.293L8.707 1.5ZM13 7.207V13.5a.5.5 0 0 1-.5.5h-9a.5.5 0 0 1-.5-.5V7.207l5-5 5 5Z"/>
            </svg>
        </NavLink>

    </div>)
}