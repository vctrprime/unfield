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
                //window.location.pathname = ;
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
                    <CDBSidebarHeader prefix={<i className="fa fa-bars fa-large"></i>}>
                            <img style={{height: '26px'}} className={"logo"} alt={"Stadium Engine"} src={logo}/>
                            <div className={"lk-version-title"}>{process.env.REACT_APP_VERSION}</div>
                    </CDBSidebarHeader>

                    <CDBSidebarContent className="sidebar-content">
                        <CDBSidebarMenu>

                            {permissions.length > 0 ? <NavLink to="/lk" end className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title="Основные настройки"  icon="columns">Основные настройки</CDBSidebarMenuItem>
                            </NavLink> : <span/>}

                            {viewNavLink('schedule') ? <NavLink to="/lk/schedule" className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title="Шахматка броней" icon="table">Шахматка броней</CDBSidebarMenuItem>
                            </NavLink> : <span/>}

                            {viewNavLink('actives') ? <NavLink to="/lk/actives"  className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title="Активы" icon="object-ungroup">Активы</CDBSidebarMenuItem>
                            </NavLink> : <span/>}

                            {viewNavLink('employees') ? <NavLink to="/lk/employees"  className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title="Персонал" icon="user-tie">Персонал</CDBSidebarMenuItem>
                            </NavLink> : <span/>}

                            {viewNavLink('reports') ? <NavLink to="/lk/reports"  className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title="Отчетность" icon="chart-line">Отчетность</CDBSidebarMenuItem>
                            </NavLink> : <span/>}

                            {viewNavLink('accounts') ? <NavLink  to="/lk/accounts" className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title="Аккаунты и роли" icon="users">Аккаунты и роли</CDBSidebarMenuItem>
                            </NavLink> : <span/>}
                            
                        </CDBSidebarMenu>
                    </CDBSidebarContent>

                   <CDBSidebarFooter>
                       {stadium !== null && <div onClick={logout}>
                            <CDBSidebarMenuItem icon="power-off">Выйти</CDBSidebarMenuItem>
                        </div>}
                    </CDBSidebarFooter>
                </CDBSidebar>)
}
    
    