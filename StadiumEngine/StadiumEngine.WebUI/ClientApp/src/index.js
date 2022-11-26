import 'bootstrap/dist/css/bootstrap.css';
import './fonts/custom-fonts.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import {RecoilRoot} from "recoil";


const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

ReactDOM.render(
  <BrowserRouter basename={baseUrl}>
      <RecoilRoot>
          <App />
      </RecoilRoot>
    
  </BrowserRouter>,
  rootElement);

registerServiceWorker();

