import React, {useEffect} from 'react';
import {Layout} from './components/Layout';
import {Route, Routes} from "react-router-dom";
import {Home} from "./components/portal/Home";

import {withNamespaces} from 'react-i18next';

import './custom.css'
import './css/common.scss'
import {useSetRecoilState} from "recoil";
import {useInject} from "inversify-hooks";
import {ICommonService} from "./services/CommonService";
import {envAtom} from "./state/env";


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
                <Routes>
                    <Route path="/" element={<Home/>}/>
                </Routes>
            </Layout>
        </>
    );
}


export default withNamespaces()(App);