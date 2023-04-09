import React, {useEffect, useState} from 'react';
import {useParams} from "react-router-dom";
import {useInject} from "inversify-hooks";
import {IBookingFormService} from "../../services/BookingFormService";
import {BookingFormDto} from "../../models/dto/booking/BookingFormDto";
import {FieldCard} from "./FieldCard";


export const BookingForm = () => {
    let {token} = useParams();
    
    document.title = 'Форма бронирования';
    
    const [data, setData] = useState<BookingFormDto|null>(null);

    const [bookingFormService] = useInject<IBookingFormService>('BookingFormService');
    
    useEffect(() => {
        bookingFormService.getBookingForm().then((result: BookingFormDto) => {
            setData(result);
        })
    }, [])
    

    return <div>Форма бронирования {token}
        {data === null ? <span/> : data.fields.map((f, i) => {
            return <FieldCard field={f}/>
        })
        }
    </div>
}