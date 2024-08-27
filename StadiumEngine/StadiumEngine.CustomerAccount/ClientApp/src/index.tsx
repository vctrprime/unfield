import 'bootstrap/dist/css/bootstrap.css';
import './fonts/custom-fonts.css';
import React from 'react';
import ReactDOM from 'react-dom';
import {BrowserRouter} from 'react-router-dom';
import App from './App';
import * as registerServiceWorker from './registerServiceWorker';
import {RecoilRoot} from "recoil";
import 'reflect-metadata';
import {registerServices} from "./helpers/di-container-service";

import './i18n/i18n';


const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

const styleLink = document.createElement("link");
styleLink.rel = "stylesheet";
styleLink.href = "https://cdn.jsdelivr.net/npm/semantic-ui/dist/semantic.min.css";
document.head.appendChild(styleLink);
registerServices();

ReactDOM.render(
    <BrowserRouter basename={baseUrl || ''}>
        <RecoilRoot>
            <App/>
        </RecoilRoot>

    </BrowserRouter>,
    rootElement);

registerServiceWorker.unregister();

