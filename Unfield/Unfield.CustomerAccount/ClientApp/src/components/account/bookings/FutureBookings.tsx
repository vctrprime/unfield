import React from "react";
import {CustomerBookingListMode} from "../../../models/common/enums/CustomerBookingListMode";
import {BookingList} from "./BookingList";

export const FutureBookings = () => {
    return <BookingList mode={CustomerBookingListMode.Future} />;
}