import React, {useEffect, useRef} from 'react';
import {Layout} from './components/Layout';
import {Route, Routes} from "react-router-dom";
import {Home} from "./components/portal/Home";
import {Layout as LkLayout} from "./components/extranet/Layout";
import {Layout as AdminLayout} from "./components/admin/Layout";
import {Offers} from "./components/extranet/offers/Offers";
import {Schedule} from "./components/extranet/schedule/Schedule";
import {SignIn} from "./components/extranet/auth/SignIn";
import ProtectedRoute from "./components/common/ProtectedRoute";

import {withNamespaces} from 'react-i18next';

import './custom.css'
import './css/extranet/SignIn.scss'
import './css/common.scss'

import {Employees} from "./components/extranet/employees/Employees";
import {Accounts} from "./components/extranet/accounts/Accounts";
import {Permissions} from "./components/extranet/accounts/Permissions";
import {Users} from "./components/extranet/accounts/Users";
import {Roles} from "./components/extranet/accounts/Roles";
import {Admin} from "./components/admin/Admin";


import 'react-notifications/lib/notifications.css';
import {Legals} from "./components/admin/legals/Legals";
import {Reports} from "./components/extranet/reports/Reports";
import {useLocalStorage} from "usehooks-ts";
import {AuthorizeUserDto} from "./models/dto/accounts/AuthorizeUserDto";
import {Button, Modal} from "semantic-ui-react";
import {useRecoilState, useSetRecoilState} from "recoil";
import {logoutModalAtom} from "./state/logoutModal";
import {stadiumAtom} from "./state/stadium";
import {UserPermissionDto} from "./models/dto/accounts/UserPermissionDto";
import {permissionsAtom} from "./state/permissions";
import {authAtom} from "./state/auth";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "./services/AccountsService";
import {t} from "i18next";
import {LockerRooms} from "./components/extranet/offers/LockerRooms";
import {LockerRoom} from "./components/extranet/offers/LockerRoom";
import {Fields} from "./components/extranet/offers/Fields";
import {Field} from "./components/extranet/offers/Field";
import {Inventory} from "./components/extranet/offers/Inventory";
import {Inventories} from "./components/extranet/offers/Inventories";
import {Rates} from "./components/extranet/rates/Rates";
import {PriceGroups} from "./components/extranet/rates/PriceGroups";
import {PriceGroup} from "./components/extranet/rates/PriceGroup";
import {Tariffs} from "./components/extranet/rates/Tariffs";
import {Tariff} from "./components/extranet/rates/Tariff";
import {Prices} from "./components/extranet/rates/Prices";
import {Forbidden} from "./components/extranet/Forbidden";
import {MainSettings} from "./components/extranet/settings/MainSettings";
import {Settings} from "./components/extranet/settings/Settings";
import {Breaks} from "./components/extranet/settings/Breaks";
import {getStartLkRoute} from "./helpers/utils";
import {Break} from "./components/extranet/settings/Break";
import {UserStadiumDto} from "./models/dto/accounts/UserStadiumDto";

const ReactNotifications = require('react-notifications');
const {NotificationContainer} = ReactNotifications;

const App = () => {
    const [user, setUser] = useLocalStorage<AuthorizeUserDto | null>('user', null);
    const prevUserRef = useRef<AuthorizeUserDto | null>(user);

    const [logoutModal, setLogoutModal] = useRecoilState<boolean>(logoutModalAtom);

    const setStadium = useSetRecoilState<UserStadiumDto | null>(stadiumAtom);
    const setPermissions = useSetRecoilState<UserPermissionDto[]>(permissionsAtom);
    const setAuth = useSetRecoilState(authAtom);

    const [accountsService] = useInject<IAccountsService>('AccountsService');


    useEffect(() => {
        const prev = JSON.stringify(prevUserRef.current);
        const current = JSON.stringify(user)
        if (prev !== current) {
            if (window.location.pathname.startsWith("/extranet")) {
                prevUserRef.current = user;
                if (window.location.pathname === "/extranet/sign-in" && user?.isAdmin) {
                    window.location.href = `/admin`;
                } else {
                    window.location.href = getStartLkRoute();
                }
            }
            if (window.location.pathname.startsWith("/admin")) {
                if (user === null) {
                    prevUserRef.current = user;
                    window.location.reload();
                }
            }
        }
    }, [user])

    const logout = () => {
        setLogoutModal(false);
        accountsService.logout()
            .finally(() => {
                setUser(null);
                setAuth(null);
                setStadium(null);
                setPermissions([]);
                window.location.href = "/extranet/sign-in";
            });
    }

    return (
        <>
            <Layout>
                <NotificationContainer/>
                <Routes>
                    <Route path="/" element={<Home/>}/>
                    <Route path="/extranet/sign-in" element={<SignIn/>}/>
                    <Route path="/extranet" element={<ProtectedRoute component={LkLayout}/>}>
                        <Route path="forbidden" element={<Forbidden/>}/>
                        <Route path="settings" element={<Settings/>}>
                            <Route path="main" element={<MainSettings/>}/>
                            <Route path="breaks" element={<Breaks/>}/>
                        </Route>
                        <Route path="settings/breaks/:id" element={<Break/>}/>
                        
                        <Route path="offers" element={<Offers/>}>
                            <Route path="fields" element={<Fields/>}/>
                            <Route path="locker-rooms" element={<LockerRooms/>}/>
                            <Route path="inventories" element={<Inventories/>}/>
                        </Route>
                        <Route path="offers/locker-rooms/:id" element={<LockerRoom/>}/>
                        <Route path="offers/fields/:id" element={<Field/>}/>
                        <Route path="offers/inventories/:id" element={<Inventory/>}/>

                        <Route path="rates" element={<Rates/>}>
                            <Route path="price-groups" element={<PriceGroups/>}/>
                            <Route path="tariffs" element={<Tariffs/>}/>
                            <Route path="prices" element={<Prices/>}/>
                        </Route>
                        <Route path="rates/price-groups/:id" element={<PriceGroup/>}/>
                        <Route path="rates/tariffs/:id" element={<Tariff/>}/>

                        <Route path="schedule/:viewMode?" element={<Schedule/>}/>
                        <Route path="employees" element={<Employees/>}/>
                        <Route path="reports" element={<Reports/>}/>
                        <Route path="accounts" element={<Accounts/>}>
                            <Route path="users" element={<Users/>}/>
                            <Route path="roles" element={<Roles/>}/>
                            <Route path="permissions" element={<Permissions/>}/>
                        </Route>
                    </Route>
                    <Route path="/admin" element={<ProtectedRoute component={AdminLayout}/>}>
                        <Route path="" element={<Admin/>}>
                            <Route path="" element={<Legals/>}/>
                        </Route>
                    </Route>
                </Routes>
            </Layout>
            <Modal
                dimmer='blurring'
                size='small'
                basic
                open={logoutModal}>
                <Modal.Header>{t('accounts:logout:header')}</Modal.Header>
                <Modal.Content>
                    <p>{t('accounts:logout:question')}</p>
                </Modal.Content>
                <Modal.Actions>
                    <Button style={{backgroundColor: '#CD5C5C', color: 'white'}}
                            onClick={() => setLogoutModal(false)}>{t('common:no_button')}</Button>
                    <Button style={{backgroundColor: '#3CB371', color: 'white'}}
                            onClick={logout}>{t('common:yes_button')}</Button>
                </Modal.Actions>
            </Modal>
        </>
    );
}


export default withNamespaces()(App);