import React, {useEffect} from "react";
import {useInject} from "inversify-hooks";
import {IBookingsService} from "../../../services/BookingsService";
import {CustomerBookingListMode} from "../../../models/common/enums/CustomerBookingListMode";

export const PastBookings = () => {

    const [bookingsService] = useInject<IBookingsService>('BookingsService');

    useEffect(() => {
        bookingsService.getBookings(CustomerBookingListMode.Previous).then((result) => {
            console.log(result);
        });
    }, []);
    
    
    return <div>PastBookings</div>;
}