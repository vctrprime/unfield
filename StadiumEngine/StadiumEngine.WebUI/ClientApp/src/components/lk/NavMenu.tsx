import React, {useEffect} from 'react';
import {NavLink} from "react-router-dom";
import {useRecoilState, useRecoilValue, useSetRecoilState} from "recoil";
import logo from "../../img/logo/logo_icon_with_title_white.png";
import '../../css/lk/NavMenu.scss';
import {stadiumAtom} from "../../state/stadium";
import {permissionsAtom} from "../../state/permissions";
import {UserPermissionDto} from "../../models/dto/accounts/UserPermissionDto";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../services/AccountsService";
import {t} from "i18next";
import {logoutModalAtom} from "../../state/logoutModal";


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
    const stadium = useRecoilValue<number | null>(stadiumAtom);
    const [permissions, setPermissions] = useRecoilState<UserPermissionDto[]>(permissionsAtom);
    const setLogoutModal = useSetRecoilState<boolean>(logoutModalAtom);
    
    const [accountsService] = useInject<IAccountsService>('AccountsService');
    
    useEffect(() => {
        if (permissions.length === 0) {
                accountsService.getCurrentUserPermissions().then((result: UserPermissionDto[] ) => {
                    setPermissions(result);
            })
        }
    }, [stadium])

    
    
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
                                <CDBSidebarMenuItem title={t('common:lk_navbar:main_settings')}  icon="columns">{t('common:lk_navbar:main_settings')}</CDBSidebarMenuItem>
                            </NavLink> : <span/>}

                            {viewNavLink('schedule') ? <NavLink to="/lk/schedule" className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title={t('common:lk_navbar:schedule')} icon="table">{t('common:lk_navbar:schedule')}</CDBSidebarMenuItem>
                            </NavLink> : <span/>}

                            {viewNavLink('offers') ? <NavLink to="/lk/offers"  className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title={t('common:lk_navbar:offers')} icon="object-ungroup">{t('common:lk_navbar:offers')}</CDBSidebarMenuItem>
                            </NavLink> : <span/>}

                            {viewNavLink('rates') ? <NavLink to="/lk/rates"  className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title={t('common:lk_navbar:rates')} icon="dollar-sign">{t('common:lk_navbar:rates')}</CDBSidebarMenuItem>
                            </NavLink> : <span/>}

                            {viewNavLink('employees') ? <NavLink to="/lk/employees"  className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title={t('common:lk_navbar:employees')} icon="user-tie">{t('common:lk_navbar:employees')}</CDBSidebarMenuItem>
                            </NavLink> : <span/>}

                            {viewNavLink('reports') ? <NavLink to="/lk/reports"  className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title={t('common:lk_navbar:reports')} icon="chart-line">{t('common:lk_navbar:reports')}</CDBSidebarMenuItem>
                            </NavLink> : <span/>}

                            {viewNavLink('accounts') ? <NavLink  to="/lk/accounts" className={getNavLinkClassName}>
                                <CDBSidebarMenuItem title={t('common:lk_navbar:accounts')} icon="users">{t('common:lk_navbar:accounts')}</CDBSidebarMenuItem>
                            </NavLink> : <span/>}
                            
                        </CDBSidebarMenu>
                    </CDBSidebarContent>

                   <CDBSidebarFooter>
                       {stadium !== null && <div onClick={() => setLogoutModal(true)}>
                            <CDBSidebarMenuItem icon="power-off">{t('common:lk_navbar:logout')}</CDBSidebarMenuItem>
                        </div>}
                    </CDBSidebarFooter>
                </CDBSidebar>)
}
    
    