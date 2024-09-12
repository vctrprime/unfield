import React from "react";
import {useParams} from "react-router-dom";

export const Booking = () => {
    let { stadiumId, number } = useParams();
    
    return <div>Booking {number}, {stadiumId}</div>;
}