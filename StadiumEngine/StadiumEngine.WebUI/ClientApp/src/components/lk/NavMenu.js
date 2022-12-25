import React from 'react';
import {NavLink, useNavigate} from "react-router-dom";
import {useFetchWrapper} from "../../helpers/fetch-wrapper";
import {useRecoilValue, useSetRecoilState} from "recoil";
import {authAtom} from "../../state/auth";
import {
    CDBSidebar,
    CDBSidebarContent,
    CDBSidebarFooter,
    CDBSidebarHeader,
    CDBSidebarMenu,
    CDBSidebarMenuItem,
} from 'cdbreact';
import logo from "../../img/logo/logo_icon_with_title_white.png";
import '../../css/lk/NavMenu.scss';
import {stadiumAtom} from "../../state/stadium";



export const NavMenu = () => {
    const fetchWrapper = useFetchWrapper();
    const setAuth = useSetRecoilState(authAtom);
    const navigate = useNavigate();

    const stadium = useRecoilValue(stadiumAtom);
    
    const logout = () => {
        fetchWrapper.delete({url: `api/account/logout`})
            .finally(() => {
                localStorage.removeItem('user');
                setAuth(null);
                navigate("/lk/sign-in");
                //window.location.pathname = ;
            });
    }
    
    
    return (
            
                <CDBSidebar textColor="#fff" backgroundColor="#354650">
                    <CDBSidebarHeader prefix={<i className="fa fa-bars fa-large"></i>}>
                            <img style={{height: '26px'}} className={"logo"} alt={"Stadium Engine"} src={logo}/>
                            <div className={"lk-version-title"}>{process.env.REACT_APP_VERSION}</div>
                    </CDBSidebarHeader>

                    <CDBSidebarContent className="sidebar-content">
                        {stadium !== null && <CDBSidebarMenu>
                            <NavLink exact="true" to="/lk" end className={({isActive}) => //(isActive) --> ({isActive})
                                isActive ? "activeClicked" : undefined
                            }>
                                <CDBSidebarMenuItem title="Основные настройки"  icon="columns">Основные настройки</CDBSidebarMenuItem>
                            </NavLink>
                            <NavLink exact="true" to="/lk/schedule" end className={({isActive}) => //(isActive) --> ({isActive})
                                isActive ? "activeClicked" : undefined
                            }>
                                <CDBSidebarMenuItem title="Шахматка броней" icon="table">Шахматка броней</CDBSidebarMenuItem>
                            </NavLink>
                            <NavLink exact="true" to="/lk/actives" end className={({isActive}) => //(isActive) --> ({isActive})
                                isActive ? "activeClicked" : undefined
                            }>
                                <CDBSidebarMenuItem title="Активы" icon="object-ungroup">Активы</CDBSidebarMenuItem>
                            </NavLink>
                            <NavLink exact="true" to="/lk/employees" end className={({isActive}) => //(isActive) --> ({isActive})
                                isActive ? "activeClicked" : undefined
                            }>
                                <CDBSidebarMenuItem title="Персонал" icon="users">Персонал</CDBSidebarMenuItem>
                            </NavLink>
                            
                        </CDBSidebarMenu>}
                    </CDBSidebarContent>

                   <CDBSidebarFooter>
                       {stadium !== null && <div onClick={logout}>
                            <CDBSidebarMenuItem icon="power-off">Выйти</CDBSidebarMenuItem>
                        </div>}
                    </CDBSidebarFooter>
                </CDBSidebar>)
}
    
    