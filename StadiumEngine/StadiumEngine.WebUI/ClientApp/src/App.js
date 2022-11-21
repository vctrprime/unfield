import React, { Component } from 'react';
import { Layout } from './components/Layout';
import './custom.css'
import {Routes, Route} from "react-router-dom";
import {Home} from "./components/landing/Home";
import {Layout as LkLayout} from "./components/lk/Layout";
import {Actives} from "./components/lk/actives/Actives";
import {Schedule} from "./components/lk/schedule/Schedule";
import {Main} from "./components/lk/Main";


export default class App extends Component {
  static displayName = App.name;
  
  
  

  render () {
      
    return (
      <Layout>
          <Routes>
              <Route path="/" element={<Home/>} />
              <Route path="/lk" element={<LkLayout />}>
                  <Route path="" element={<Main />} />
                  <Route path="actives" element={<Actives />} />
                  <Route path="schedule" element={<Schedule />} />
              </Route>
          </Routes>
      </Layout>
    );
  }
}
