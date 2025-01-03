import React from "react";
import {Navigate, useParams} from "react-router-dom";
import {useRecoilValue} from "recoil";
import {authAtom} from "../../state/auth";


function ProtectedRoute({component: Component, ...restOfProps}: any) {
    const { stadiumToken } = useParams();
    
    const auth = useRecoilValue(authAtom);

    return auth?.phoneNumber ? <Component {...restOfProps}/> : 
        <Navigate to={"/" + stadiumToken + "/sign-in"} />

}

export default ProtectedRoute;