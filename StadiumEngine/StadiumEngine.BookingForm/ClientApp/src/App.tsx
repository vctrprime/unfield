import React from 'react';
import {Layout} from './components/Layout';
import {Route, Routes} from "react-router-dom";
import {withNamespaces} from 'react-i18next';
import 'react-notifications/lib/notifications.css';
import {BookingForm} from "./components/booking/BookingForm";
import {BookingCheckout} from "./components/booking/checkout/BookingCheckout";
import {BookingCheckoutConfirm} from "./components/booking/checkout/BookingCheckoutConfirm";

const ReactNotifications = require('react-notifications');
const {NotificationContainer} = ReactNotifications;

const App = () => {
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