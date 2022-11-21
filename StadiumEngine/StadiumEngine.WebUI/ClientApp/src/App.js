import React, { Component } from 'react';
import { Layout } from './components/Layout';
import './custom.css'
import {Routes, Route} from "react-router-dom";
import {Home} from "./components/landing/Home";
import {Layout as LkLayout} from "./components/lk/Layout";
import {Actives} from "./components/lk/Actives";
import {Schedule} from "./components/lk/Schedule";


export default class App extends Component {
  static displayName = App.name;
  
  

  render () {
    return (
      <Layout>
          <Routes>
              <Route path="/" element={<Home/>} />
              <Route path="/lk" element={<LkLayout />}>
                  <Route path="actives" element={<Actives />} />
                  <Route path="schedule" element={<Schedule />} />
              </Route>
          </Routes>
      </Layout>
    );
  }
}
