import React from 'react';
import {Layout} from './components/Layout';
import {Route, Routes, useParams} from "react-router-dom";
import {withNamespaces} from 'react-i18next';
import 'react-notifications/lib/notifications.css';
import './custom.css'
import './css/common.scss'

const ReactNotifications = require('react-notifications');
const {NotificationContainer} = ReactNotifications;

const App = () => {
    return (
        <>
            <Layout>
                <NotificationContainer/>
                <Routes>
                    <Route path="account/redirect/:token" element={<Test/>}/>
                </Routes>
            </Layout>
        </>
    );
}

const Test = () => {
    let {token} = useParams();
    
    return <div>{token}</div>;
}


export default withNamespaces()(App);