import React from 'react';
import {AuthorizedUserInfo} from "../common/AuthorizedUserInfo";
import {LanguageSelect} from "../common/LanguageSelect";
import logo from "../../img/logo/logo_icon_with_title.png";
import {t} from "i18next";
import {useSetRecoilState} from "recoil";
import {authAtom} from "../../state/auth";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../services/AccountsService";
import {useNavigate} from "react-router-dom";
import {stadiumAtom} from "../../state/stadium";
import {UserPermissionDto} from "../../models/dto/accounts/UserPermissionDto";
import {permissionsAtom} from "../../state/permissions";


export const Header = () => {
    const setAuth = useSetRecoilState(authAtom);
    const setStadium = useSetRecoilState<number | null>(stadiumAtom);
    const setPermissions = useSetRecoilState<UserPermissionDto[]>(permissionsAtom);
    
    const [accountsService] = useInject<IAccountsService>('AccountsService');

    const navigate = useNavigate();
    
    const logout = () => {
        accountsService.logout()
            .finally(() => {
                localStorage.removeItem('user');
                setAuth(null);
                setStadium(null);
                setPermissions([]);
                navigate("/lk/sign-in");
            });
    }
    
    return (
        <div className="border-bottom navbar navbar-light box-shadow lk-header">
            <div className="header-left-container">
                <img style={{height: '26px'}} className={"logo"} alt={"Stadium Engine"} src={logo}/>
                {/*<div className="panel-admin-title">{t("admin:header:panel_admin_title")}</div>*/}
            </div>
            <div className="header-right-container">
                <div className="header-icons">
                    
                        <LanguageSelect />
                    </div>
                <AuthorizedUserInfo withLegalName={false} />
                <i onClick={logout} title={t('common:lk_navbar:logout')||""} style={{marginLeft: "10px", cursor: "pointer"}} className="fa fa-power-off" />
            </div>
        </div>)
};