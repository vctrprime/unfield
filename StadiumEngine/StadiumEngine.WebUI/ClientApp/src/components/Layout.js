import React from 'react';
import {useRecoilValue} from "recoil";
import {loadingAtom} from "../state/loading";
import {HashLoader} from "react-spinners";

export const Layout = (props) => {
  const loading = useRecoilValue(loadingAtom)
  
  
  return (
    <div>
        {props.children}
        {loading && <div className="d-flex justify-content-center align-items-center" 
                          style={{ backgroundColor: 'rgba(53,70,80, 0.2)',height: "100vh", width: "100vw", position: "absolute", top: 0}}>
            <HashLoader color="#00d2ff"/>
        </div>}
    </div>
  );
}

