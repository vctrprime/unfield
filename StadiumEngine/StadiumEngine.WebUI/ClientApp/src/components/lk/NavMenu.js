import React, {useEffect, useState} from 'react';
import {NavLink, useNavigate} from "react-router-dom";
import {useFetchWrapper} from "../../helpers/fetch-wrapper";
import {useRecoilState, useRecoilValue, useSetRecoilState} from "recoil";
import {authAtom} from "../../state/auth";
import {
    CDBSidebar,
    CDBSidebarContent,
    CDBSidebarFooter,
    CDBSidebarHeader,
    CDBSidebarMenu,
    CDBSidebarMenuItem
} from 'cdbreact';
import logo from "../../img/logo/logo_icon_with_title_white.png";
import '../../css/lk/NavMenu.scss';
import {stadiumAtom} from "../../state/stadium";
import {permissionsAtom} from "../../state/permissions";



export const NavMenu = () => {
    const fetchWrapper = useFetchWrapper();
    const setAuth = useSetRecoilState(authAtom);
    const navigate = useNavigate();
    
    const [stadium, setStadium] = useRecoilState(stadiumAtom);
    const [permissions, setPermissions] = useRecoilState(permissionsAtom);
    
    const logout = () => {
        fetchWrapper.delete({url: `api/accounts/logout`})
            .finally(() => {
                localStorage.removeItem('user');
                setAuth(null);
                setStadium(null);
                setPermissions([]);
                navigate("/lk/sign-in");
                //window.location.pathname = ;
            });
    }
    
    useEffect(() => {
        if (permissions.length === 0) {
            fetchWrapper.get({url: `api/accounts/user-permissions`, withSpinner: true, hideSpinner: true}).then((result) => {
                setPermissions(result);
            })
        }
    }, [stadium])
    
    const viewNavLink = (key) => {
        return permissions.filter(p => p.groupKey === key).length > 0;
    }
    
    const getNavLinkClassName = ({ isActive }) => {
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

                            {permissions.length > 0 ? <NavLink exact="true" to="/lk" end className={getNavLinkClassName}>
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
    
    