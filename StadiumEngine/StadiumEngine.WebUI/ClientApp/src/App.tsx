import React, {useEffect, useRef} from 'react';
import {Layout} from './components/Layout';
import {Route, Routes} from "react-router-dom";
import {Home} from "./components/portal/Home";
import {Layout as LkLayout} from "./components/lk/Layout";
import {Layout as AdminLayout} from "./components/admin/Layout";
import {Offers} from "./components/lk/offers/Offers";
import {Schedule} from "./components/lk/schedule/Schedule";
import {Main} from "./components/lk/Main";
import {SignIn} from "./components/lk/auth/SignIn";
import ProtectedRoute from "./components/common/ProtectedRoute";

import {withNamespaces} from 'react-i18next';

import './custom.css'
import './css/lk/SignIn.scss'
import './css/common.scss'

import {Employees} from "./components/lk/employees/Employees";
import {Accounts} from "./components/lk/accounts/Accounts";
import {Permissions} from "./components/lk/accounts/Permissions";
import {Users} from "./components/lk/accounts/Users";
import {Roles} from "./components/lk/accounts/Roles";
import {Admin} from "./components/admin/Admin";


import 'react-notifications/lib/notifications.css';
import {Legals} from "./components/admin/legals/Legals";
import {Reports} from "./components/lk/reports/Reports";
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
import {LockerRooms} from "./components/lk/offers/LockerRooms";
import {LockerRoom} from "./components/lk/offers/LockerRoom";
import {Fields} from "./components/lk/offers/Fields";
import {Field} from "./components/lk/offers/Field";
import {Inventory} from "./components/lk/offers/Inventory";
import {Inventories} from "./components/lk/offers/Inventories";
import {Rates} from "./components/lk/rates/Rates";
import {PriceGroups} from "./components/lk/rates/PriceGroups";
import {PriceGroup} from "./components/lk/rates/PriceGroup";
import {Tariffs} from "./components/lk/rates/Tariffs";
import {Tariff} from "./components/lk/rates/Tariff";
import {Prices} from "./components/lk/rates/Prices";
import {BookingForm} from "./components/booking/BookingForm";
import {BookingCheckout} from "./components/booking/BookingCheckout";

const ReactNotifications = require('react-notifications');
const {NotificationContainer} = ReactNotifications;

const App = () => {
    const [user, setUser] = useLocalStorage<AuthorizeUserDto | null>('user', null);
    const prevUserRef = useRef<AuthorizeUserDto | null>(user);

    const [logoutModal, setLogoutModal] = useRecoilState<boolean>(logoutModalAtom);

    const setStadium = useSetRecoilState<number | null>(stadiumAtom);
    const setPermissions = useSetRecoilState<UserPermissionDto[]>(permissionsAtom);
    const setAuth = useSetRecoilState(authAtom);

    const [accountsService] = useInject<IAccountsService>('AccountsService');


    useEffect(() => {
        const prev = JSON.stringify(prevUserRef.current);
        const current = JSON.stringify(user)
        if (prev !== current) {
            if (window.location.pathname.startsWith("/lk")) {
                prevUserRef.current = user;
                if (window.location.pathname === "/lk/sign-in" && user?.isAdmin) {
                    window.location.href = `/admin`;
                } else {
                    window.location.href = `/lk`;
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
                window.location.href = "/lk/sign-in";
            });
    }

    return (
        <div>


            <Layout>
                <NotificationContainer/>
                <Routes>
                    <Route path="/" element={<Home/>}/>
                    <Route path="/lk/sign-in" element={<SignIn/>}/>
                    <Route path="/lk" element={<ProtectedRoute component={LkLayout}/>}>
                        <Route path="" element={<Main/>}/>
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

                        <Route path="schedule" element={<Schedule/>}/>
                        <Route path="employees" element={<Employees/>}/>
                        <Route path="reports" element={<Reports/>}/>
                        <Route path="accounts" element={<Accounts/>}>
                            <Route path="" element={<Users/>}/>
                            <Route path="roles" element={<Roles/>}/>
                            <Route path="permissions" element={<Permissions/>}/>
                        </Route>
                    </Route>
                    <Route path="/admin" element={<ProtectedRoute component={AdminLayout}/>}>
                        <Route path="" element={<Admin/>}>
                            <Route path="" element={<Legals/>}/>
                        </Route>
                    </Route>
                    <Route path="booking/:token?" element={<BookingForm/>}/>
                    <Route path="booking-checkout/:bookingNumber?" element={<BookingCheckout/>}/>
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
        </div>
    );
}


export default withNamespaces()(App);