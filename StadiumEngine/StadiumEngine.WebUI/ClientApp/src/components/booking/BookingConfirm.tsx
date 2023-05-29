import React, {useRef} from "react";
import {useLocation, useNavigate, useParams} from "react-router-dom";
import {getTitle} from "../../helpers/utils";
import {useInject} from "inversify-hooks";
import {IBookingFormService} from "../../services/BookingFormService";
import {Button} from "semantic-ui-react";

type ConfirmLocationState = {
    bookingNumber: string;
}

export const BookingConfirm = () => {
    document.title = getTitle("booking:confirm_title");
    
    const location = useLocation();
    const params = useParams();
    const navigate = useNavigate();
    
    let bookingNumber = (location.state as ConfirmLocationState)?.bookingNumber  || params["bookingNumber"];

    const [bookingFormService] = useInject<IBookingFormService>('BookingFormService');

    const codeInput = useRef<any>();
    
    return <div>{bookingNumber}
        <input
            placeholder='Введите код...'
            ref={codeInput}/>

        <Button style={{backgroundColor: '#3CB371', color: 'white'}} onClick={() => {
            bookingFormService.confirmBooking({
                bookingNumber: bookingNumber||'',
                accessCode: codeInput.current.value
            }).then(() => {
                navigate("/booking");
            })
        }}>Подтвердить</Button>
        <Button style={{backgroundColor: '#CD5C5C', color: 'white'}} onClick={() => {
            bookingFormService.cancelBooking({
                bookingNumber: bookingNumber||''
            }).finally(() => {
                navigate("/booking");
            });
        }}>Отменить</Button>
    </div>
}