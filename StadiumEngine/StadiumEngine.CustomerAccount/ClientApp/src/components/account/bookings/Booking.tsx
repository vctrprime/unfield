import React from "react";
import {useParams} from "react-router-dom";

export const Booking = () => {
    let { stadiumToken, number } = useParams();
    
    return <div>Booking {number}, {stadiumToken}</div>;
}