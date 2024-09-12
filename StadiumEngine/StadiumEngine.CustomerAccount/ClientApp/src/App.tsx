import React from 'react';
import {Layout} from './components/Layout';
import {Route, Routes, useParams} from "react-router-dom";
import {withNamespaces} from 'react-i18next';
import 'react-notifications/lib/notifications.css';
import './custom.css'
import './css/common.scss'
import './css/SignIn.scss'
import {Redirect} from "./components/account/auth/Redirect";
import {SignIn} from "./components/account/auth/SignIn";
import {Layout as AccountLayout} from "./components/account/Layout";
import ProtectedRoute from "./components/common/ProtectedRoute";
import {Bookings} from "./components/account/bookings/Bookings";
import {PastBookings} from "./components/account/bookings/PastBookings";
import {FutureBookings} from "./components/account/bookings/FutureBookings";
import {Booking} from "./components/account/bookings/Booking";
import {Profile} from "./components/account/profile/Profile";
import {NotFound} from "./components/common/NotFound";

const ReactNotifications = require('react-notifications');
const {NotificationContainer} = ReactNotifications;

const App = () => {
    return (
        <>
            <Layout>
                <NotificationContainer/>
                <Routes>
                    <Route path="*" element={<NotFound/>}></Route>
                    <Route path="/redirect/:lng/:token" element={<Redirect/>}/>
                    <Route path="/:stadiumId/sign-in" element={<SignIn/>}/>
                    
                    <Route path="/:stadiumId" element={<ProtectedRoute component={AccountLayout}/>}>
                        <Route path="profile" element={<Profile/>}/>
                        <Route path="bookings" element={<Bookings />}>
                            <Route path="past" element={<PastBookings/>}/>
                            <Route path="future" element={<FutureBookings/>}/>
                        </Route>
                        <Route path="bookings/:number" element={<Booking/>}/>
                    </Route>
                </Routes>
            </Layout>
        </>
    );
}



export default withNamespaces()(App);