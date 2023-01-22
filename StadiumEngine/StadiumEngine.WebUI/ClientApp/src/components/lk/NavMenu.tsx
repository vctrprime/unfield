import React, {useEffect } from 'react';
import {NavLink, useNavigate} from "react-router-dom";
import {useRecoilState, useSetRecoilState} from "recoil";
import {authAtom} from "../../state/auth";
import logo from "../../img/logo/logo_icon_with_title_white.png";
import '../../css/lk/NavMenu.scss';
import {stadiumAtom} from "../../state/stadium";
import {permissionsAtom} from "../../state/permissions";
import {UserPermissionDto} from "../../models/dto/accounts/UserPermissionDto";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../services/AccountsService";
import {t} from "i18next";


const cdbreact = require('cdbreact');
const {
    CDBSidebar,
    CDBSidebarContent,
    CDBSidebarFooter,
    CDBSidebarHeader,
    CDBSidebarMenu,
    CDBSidebarMenuItem
} = cdbreact;



export const NavMenu = () => {
    const [stadium, setStadium] = useRecoilState<number | null>(stadiumAtom);
    const [permissions, setPermissions] = useRecoilState<UserPermissionDto[]>(permissionsAtom);
    const setAuth = useSetRecoilState(authAtom);
    
    const [accountsService] = useInject<IAccountsService>('AccountsService');
    
    const navigate = useNavigate();
    
    
    useEffect(() => {
        if (permissions.length === 0) {
                accountsService.getCurrentUserPermissions().then((result: UserPermissionDto[] ) => {
                    setPermissions(result);
            })
        }
    }, [stadium])

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
    
    const viewNavLink = (key: string) => {
        return permissions.filter(p => p.groupKey === key).length > 0;
    }
    
    const getNavLinkClassName = ({ isActive }: any) => {
        return isActive ? "activeClicked" : undefined;
    }
    
    return (
            
                <CDBSidebar textColor="#fff" backgroundColor="#354650">
                    <CDBSidebarHeader prefix={<i className="fa fa-bars fa-large" />}>
                            <img style={{height: '26px'}} className={"logo"} alt={"Stadium Engine"} src={logo}/>
                            <div className={"lk-version-title"}>{process.env.REACT_APP_VERSION}</div>
                    </CDBSidebarHeader>

                    <CDBSidebarContent className="sidebar-content">
                        <CDBSidebarMenu>

                            {permissions.length > 0 ? <NavLink to="/lk" end className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title="Основные настройки"  icon="columns">{t('common:lk_navbar:main_settings')}</CDBSidebarMenuItem>
                            </NavLink> : <span/>}

                            {viewNavLink('schedule') ? <NavLink to="/lk/schedule" className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title="Шахматка броней" icon="table">{t('common:lk_navbar:schedule')}</CDBSidebarMenuItem>
                            </NavLink> : <span/>}

                            {viewNavLink('actives') ? <NavLink to="/lk/actives"  className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title="Активы" icon="object-ungroup">{t('common:lk_navbar:actives')}</CDBSidebarMenuItem>
                            </NavLink> : <span/>}

                            {viewNavLink('employees') ? <NavLink to="/lk/employees"  className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title="Персонал" icon="user-tie">{t('common:lk_navbar:employees')}</CDBSidebarMenuItem>
                            </NavLink> : <span/>}

                            {viewNavLink('reports') ? <NavLink to="/lk/reports"  className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title="Отчетность" icon="chart-line">{t('common:lk_navbar:reports')}</CDBSidebarMenuItem>
                            </NavLink> : <span/>}

                            {viewNavLink('accounts') ? <NavLink  to="/lk/accounts" className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title="Аккаунты и роли" icon="users">{t('common:lk_navbar:accounts')}</CDBSidebarMenuItem>
                            </NavLink> : <span/>}
                            
                        </CDBSidebarMenu>
                    </CDBSidebarContent>

                   <CDBSidebarFooter>
                       {stadium !== null && <div onClick={logout}>
                            <CDBSidebarMenuItem icon="power-off">{t('common:lk_navbar:logout')}</CDBSidebarMenuItem>
                        </div>}
                    </CDBSidebarFooter>
                </CDBSidebar>)
}
    
    