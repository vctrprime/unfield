import React, {useEffect, useState} from 'react';
import {useParams} from "react-router-dom";
import {useInject} from "inversify-hooks";
import {IBookingFormService} from "../../services/BookingFormService";
import {BookingFormDto} from "../../models/dto/booking/BookingFormDto";
import {FieldCard} from "./FieldCard";
import {Container} from "reactstrap";
import '../../css/booking/BookingForm.scss';


export const BookingForm = () => {
    let {token} = useParams();
    
    document.title = 'Форма бронирования';
    
    const [data, setData] = useState<BookingFormDto|null>(null);

    const [bookingFormService] = useInject<IBookingFormService>('BookingFormService');
    
    useEffect(() => {
        bookingFormService.getBookingForm(token === undefined ? null : token).then((result: BookingFormDto) => {
            setData(result);
        })
    }, [])
    

    return <div>{data === null ? <span/> : 
            <Container className="booking-form-container">
                {data.fields.map((f, i) => {
                return <FieldCard 
                    key={i}
                    field={f}/>
            })}
            </Container>
        }
    </div>
}