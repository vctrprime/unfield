import React from "react";
import { Navigate } from "react-router-dom";

function ProtectedRoute({ component: Component, ...restOfProps }) {
    const isAuthenticated = localStorage.getItem("isAuthenticated");
    
    return isAuthenticated ? <Component {...restOfProps}/> : <Navigate to="/lk/sign-in" />
    
}

export default ProtectedRoute;