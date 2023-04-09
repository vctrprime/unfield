import React, {useEffect, useState} from 'react';
import {useParams} from "react-router-dom";
import {useInject} from "inversify-hooks";
import {IBookingFormService} from "../../services/BookingFormService";
import {BookingFormDto, BookingFormFieldSlotDto} from "../../models/dto/booking/BookingFormDto";
import {FieldCard} from "./FieldCard";
import {Container} from "reactstrap";
import '../../css/booking/BookingForm.scss';


export const BookingForm = () => {
    let {token} = useParams();
    
    document.title = 'Форма бронирования';
    
    const [data, setData] = useState<BookingFormDto|null>(null);
    const [selectedSlot, setSelectedSlot] = useState<BookingFormFieldSlotDto|null>(null)

    const [bookingFormService] = useInject<IBookingFormService>('BookingFormService');
    
    useEffect(() => {
        bookingFormService.getBookingForm().then((result: BookingFormDto) => {
            setData(result);
        })
    }, [])
    

    return <div style={{width: '100%', overflowY: "auto"}}>Форма бронирования {token}
        {data === null ? <span/> : 
            <Container>
                {data.fields.map((f, i) => {
                return <FieldCard 
                    key={i} 
                    selectedSlot={selectedSlot}
                    setSelectedSlot={setSelectedSlot}
                    field={f}/>
            })}
            </Container>
        }
    </div>
}