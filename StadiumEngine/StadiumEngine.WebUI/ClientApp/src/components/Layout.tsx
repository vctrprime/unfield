import React from 'react';
import {useRecoilValue} from "recoil";
import {loadingAtom} from "../state/loading";
import {HashLoader} from "react-spinners";


interface LayoutProps {
  children: React.ReactNode
}

export const Layout = (props: LayoutProps) => {
  const loading = useRecoilValue(loadingAtom)
  
  return (
    <div style={{height: '100%'}}>
        {props.children}
        {loading && <div className="d-flex justify-content-center align-items-center" 
                          style={{ backgroundColor: 'rgba(53,70,80, 0.2)',height: "100vh", width: "100vw", zIndex: 10000, position: "absolute", top: 0}}>
            <HashLoader color="#00d2ff"/>
        </div>}
    </div>
  );
}

