import React from "react";
import {useParams} from "react-router-dom";

export const BookingCheckout = () => {
    let {bookingNumber} = useParams();
    
    return <div>{bookingNumber}</div>
}