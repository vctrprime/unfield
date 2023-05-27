import React from 'react';
import {useRecoilValue} from "recoil";
import {loadingAtom} from "../state/loading";
import {HashLoader} from "react-spinners";
import cells from "../img/cells.jpeg";


interface LayoutProps {
    children: React.ReactNode
}

export const Layout = (props: LayoutProps) => {
    const loading = useRecoilValue(loadingAtom)

    return (
        <div style={{height: '100%',
            background: `linear-gradient( rgba(255, 255, 255, 0.9), rgba(255, 255, 255, 0.9) ), no-repeat center / cover url(${cells})`}}>
            {props.children}
            {loading && <div className="d-flex justify-content-center align-items-center"
                             style={{
                                 backgroundColor: 'rgba(53,70,80, 0.2)',
                                 height: "100vh",
                                 width: "100vw",
                                 zIndex: 10000,
                                 position: "absolute",
                                 top: 0
                             }}>
                <HashLoader color="#00d2ff"/>
            </div>}
        </div>
    );
}

