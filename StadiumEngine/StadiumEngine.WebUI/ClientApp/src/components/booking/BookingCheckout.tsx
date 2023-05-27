import React, {useEffect, useState} from "react";
import {useLocation, useNavigate, useParams} from "react-router-dom";
import {BookingCheckoutDto} from "../../models/dto/booking/BookingCheckoutDto";
import {useInject} from "inversify-hooks";
import {IBookingFormService} from "../../services/BookingFormService";
import {Container} from "reactstrap";
import '../../css/booking/BookingCheckout.scss';

type CheckoutLocationState = {
    bookingNumber: string;
}

export const BookingCheckout = () => {
    const location = useLocation();
    const params = useParams();
    
    let bookingNumber = (location.state as CheckoutLocationState)?.bookingNumber || params["bookingNumber"]
    
    //let {bookingNumber} = (location.state as CheckoutLocationState).bookingNumber || useParams();
    
    const [bookingFormService] = useInject<IBookingFormService>('BookingFormService');
    
    const [data, setData] = useState<BookingCheckoutDto|null>(null);
    const navigate = useNavigate();
    
    useEffect(() => {
        bookingFormService.getBookingCheckout(bookingNumber as string).then((response) => {
            setData(response);
        })
            .catch((error) => {
                navigate("/booking");
            })
    }, [])
    
    return data === null ? null :  <Container className="booking-checkout-container">
        <div className="booking-checkout-header">
            Бронирование № {data.bookingNumber}
        </div>
        <div className="booking-checkout-field">
            {data.field.name}
        </div>
        {data.inventories.length > 0 && 
            <div className="booking-checkout-inventories">
                {data.inventories.map((inv, i) => {
                    return <div>{inv.name}</div>
                    })}
        </div>}
        
    </Container>
}