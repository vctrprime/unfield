import React, { Component } from 'react';
import { Layout } from './components/Layout';
import {Routes, Route} from "react-router-dom";
import {Home} from "./components/portal/Home";
import {Layout as LkLayout} from "./components/lk/Layout";
import {Actives} from "./components/lk/actives/Actives";
import {Schedule} from "./components/lk/schedule/Schedule";
import {Main} from "./components/lk/Main";
import {SignIn} from "./components/lk/auth/SignIn";
import ProtectedRoute from "./components/common/ProtectedRoute";

import './custom.css'
import './css/lk/SignIn.scss'
import {Employees} from "./components/lk/employees/Employees";
import {Accounts} from "./components/lk/accounts/Accounts";
import {Permissions} from "./components/lk/accounts/Permissions";
import {Users} from "./components/lk/accounts/Users";
import {Roles} from "./components/lk/accounts/Roles";

import 'react-notifications/lib/notifications.css';
const ReactNotifications = require('react-notifications');
const { NotificationContainer } = ReactNotifications;
/*NotificationManager.info('Info message');
NotificationManager.success('Success message', 'Title here');
NotificationManager.warning('Warning message', 'Close after 3000ms', 3000);
NotificationManager.error('Error message', 'Click me!', 5000, () => {
    alert('callback');
});*/


export default class App extends Component {
  static displayName = App.name;

  render () {
      
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
                  <Route path="reports" element={<Main />} />
                  <Route path="accounts" element={<Accounts />} >
                      <Route path="" element={<Users />} />
                      <Route path="roles" element={<Roles />} />
                      <Route path="permissions" element={<Permissions />} />
                  </Route>
              </Route>
          </Routes>
      </Layout>
    );
  }
}
