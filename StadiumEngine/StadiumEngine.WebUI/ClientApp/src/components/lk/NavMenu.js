import React from 'react';
import {NavLink, useNavigate} from "react-router-dom";
import {useFetchWrapper} from "../../helpers/fetch-wrapper";
import {useRecoilState} from "recoil";
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



export const NavMenu = () => {
    const fetchWrapper = useFetchWrapper();
    const [auth, setAuth] = useRecoilState(authAtom);
    const navigate = useNavigate();
    
    const logout = () => {
        fetchWrapper.delete(`api/account/logout`)
            .finally(() => {
                localStorage.removeItem('user');
                setAuth(null);
                navigate("/lk/sign-in");
                //window.location.pathname = ;
            });
    }
    
    
    return (
            
                <CDBSidebar textColor="#fff" backgroundColor="#354650">
                    <CDBSidebarHeader prefix={<i style={{marginTop: '8px'}} className="fa fa-bars fa-large"></i>}>
                            <img style={{height: '26px'}} className={"logo"} alt={"Stadium Engine"} src={logo}/>
                            <div className={"lk-version-title"}>{process.env.REACT_APP_VERSION}</div>
                    </CDBSidebarHeader>

                    <CDBSidebarContent className="sidebar-content">
                        <CDBSidebarMenu>
                            <NavLink exact="true" to="/lk" end className={({isActive}) => //(isActive) --> ({isActive})
                                isActive ? "activeClicked" : undefined
                            }>
                                <CDBSidebarMenuItem title="Dashboard"  icon="columns">Dashboard</CDBSidebarMenuItem>
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
                            
                            {/*<NavLink exact to="/analytics" activeClassName="activeClicked">
                                <CDBSidebarMenuItem icon="chart-line">
                                    Analytics
                                </CDBSidebarMenuItem>
                            </NavLink>

                            <NavLink
                                exact
                                to="/hero404"
                                target="_blank"
                                activeClassName="activeClicked"
                            >
                                <CDBSidebarMenuItem icon="exclamation-circle">
                                    404 page
                                </CDBSidebarMenuItem>
                            </NavLink>*/}
                        </CDBSidebarMenu>
                    </CDBSidebarContent>

                    <CDBSidebarFooter>
                        <div onClick={logout}>
                            <CDBSidebarMenuItem icon="power-off">Выйти</CDBSidebarMenuItem>
                        </div>
                    </CDBSidebarFooter>
                </CDBSidebar>)
}
    
    