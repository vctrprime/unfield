import React, {Component, useEffect, useRef} from 'react';
import { Layout } from './components/Layout';
import {Routes, Route} from "react-router-dom";
import {Home} from "./components/portal/Home";
import {Layout as LkLayout} from "./components/lk/Layout";
import {Layout as AdminLayout} from "./components/admin/Layout";
import {Actives} from "./components/lk/actives/Actives";
import {Schedule} from "./components/lk/schedule/Schedule";
import {Main} from "./components/lk/Main";
import {SignIn} from "./components/lk/auth/SignIn";
import ProtectedRoute from "./components/common/ProtectedRoute";

import { withNamespaces } from 'react-i18next';

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

const ReactNotifications = require('react-notifications');
const { NotificationContainer } = ReactNotifications;

/*NotificationManager.info('Info message');
NotificationManager.success('Success message', 'Title here');
NotificationManager.warning('Warning message', 'Close after 3000ms', 3000);
NotificationManager.error('Error message', 'Click me!', 5000, () => {
    alert('callback');
});*/


const App = () => {
    const [user, setUser] = useLocalStorage<AuthorizeUserDto | null>('user', null);
    const prevUserRef = useRef<AuthorizeUserDto | null>(user);
    
    useEffect(() => {
        const prev = JSON.stringify(prevUserRef.current);
        const current = JSON.stringify(user)
        if (prev !== current) {
            if (window.location.pathname.startsWith("/lk")) {
                prevUserRef.current = user;
                if (window.location.pathname === "/lk/sign-in") {
                    if (user?.isAdmin) {
                        window.location.href = `/admin`;
                    }
                    else {
                        window.location.href = `/lk`;
                    }
                    
                }
                else {
                    window.location.reload();
                }
            }
        }
    }, [user])
    
    
    return (
      <Layout>
          <NotificationContainer/>
          <Routes>
              <Route path="/" element={<Home/>} />
              <Route path="/lk/sign-in" element={<SignIn />} />
              <Route path="/lk" element={<ProtectedRoute component={LkLayout}  />}>
                  <Route path="" element={<Main />} />
                  <Route path="actives" element={<Actives />} />
                  <Route path="schedule" element={<Schedule />} />
                  <Route path="employees" element={<Employees />} />
                  <Route path="reports" element={<Reports />} />
                  <Route path="accounts" element={<Accounts />} >
                      <Route path="" element={<Users />} />
                      <Route path="roles" element={<Roles />} />
                      <Route path="permissions" element={<Permissions />} />
                  </Route>
              </Route>
              <Route path="/admin" element={<ProtectedRoute component={AdminLayout}  />}>
                  <Route path="" element={<Admin />} >
                      <Route path="" element={<Legals />} />
                  </Route>
              </Route>
          </Routes>
      </Layout>
    );
}


export default withNamespaces()(App);