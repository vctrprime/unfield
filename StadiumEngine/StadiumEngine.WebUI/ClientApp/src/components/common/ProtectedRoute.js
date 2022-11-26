import React from "react";
import { Navigate } from "react-router-dom";
import {useRecoilValue} from "recoil";
import {authAtom} from "../../state/auth";

function ProtectedRoute({ component: Component, ...restOfProps }) {
    const auth = useRecoilValue(authAtom);
    
    console.log(auth);
    
    return auth ? <Component {...restOfProps}/> : <Navigate to="/lk/sign-in" />
    
}

export default ProtectedRoute;