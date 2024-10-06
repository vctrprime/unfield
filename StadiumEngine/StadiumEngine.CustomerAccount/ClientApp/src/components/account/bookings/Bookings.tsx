import React from "react";
import {Outlet} from "react-router-dom";

export const Bookings = () => {
    return <div style={{ paddingTop: '48px'}}>
        <Outlet/>
    </div>;
}