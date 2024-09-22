import React, {useEffect} from 'react';
import {Layout} from './components/Layout';
import {Route, Routes} from "react-router-dom";
import {withNamespaces} from 'react-i18next';
import 'react-notifications/lib/notifications.css';
import {BookingForm} from "./components/booking/BookingForm";
import {BookingCheckout} from "./components/booking/checkout/BookingCheckout";
import {BookingCheckoutConfirm} from "./components/booking/checkout/BookingCheckoutConfirm";
import './custom.css'
import './css/common.scss'
import {useSetRecoilState} from "recoil";
import {useInject} from "inversify-hooks";
import {ICommonService} from "./services/CommonService";
import {envAtom} from "./state/env";

const ReactNotifications = require('react-notifications');
const {NotificationContainer} = ReactNotifications;

const App = () => {
    const setEnv = useSetRecoilState(envAtom);
    const [commonService] = useInject<ICommonService>('CommonService');

    useEffect(() => {
        commonService.getEnvData().then((response) => {
            setEnv(response)
        })
    }, [])
    
    return (
        <>
            <Layout>
                <NotificationContainer/>
                <Routes>
                    <Route path=":token?" element={<BookingForm/>}/>
                    <Route path="checkout/:bookingNumber?" element={<BookingCheckout/>}/>
                    <Route path="confirm/:bookingNumber?" element={<BookingCheckoutConfirm/>}/>
                </Routes>
            </Layout>
        </>
    );
}


export default withNamespaces()(App);