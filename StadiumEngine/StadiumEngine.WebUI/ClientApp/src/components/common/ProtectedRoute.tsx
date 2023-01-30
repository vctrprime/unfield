import React from "react";
import { Navigate } from "react-router-dom";
import {useRecoilValue} from "recoil";
import {authAtom} from "../../state/auth";


function ProtectedRoute({ component: Component, ...restOfProps } : any) {
    const auth = useRecoilValue(authAtom);
    
    return auth?.fullName ? <Component {...restOfProps}/> : <Navigate to="/lk/sign-in" />
    
}

export default ProtectedRoute;