import React, {useEffect, useState} from "react";
import {CustomerBookingListMode} from "../../../models/common/enums/CustomerBookingListMode";
import {useInject} from "inversify-hooks";
import {IBookingsService} from "../../../services/BookingsService";
import {CustomerBookingListItemDto} from "../../../models/dto/bookings/CustomerBookingListItemDto";
import {t} from "i18next";
import {Container} from "reactstrap";
import {BookingListCard} from "./BookingListCard";

export interface BookingsListProps {
    mode: CustomerBookingListMode;
}

export const BookingList = (props: BookingsListProps) => {
    const [data, setData] = useState<CustomerBookingListItemDto[]|null>(null);

    const [bookingsService] = useInject<IBookingsService>('BookingsService');
    
    const header = t("booking:list:header_" + props.mode);

    useEffect(() => {
        bookingsService.getBookings(props.mode).then((result) => {
            setData(result);
        });
    }, []);
    
    return data ? <Container className="booking-list-container">
        {data.length > 0 && <div className="booking-list-header">{header}</div>}
            <div className="booking-list-cards">
                {data.length === 0 ?
                    <div className="booking-list-no-bookings">{t('booking:list:no_bookings')}</div> : data.map((b, i) => {
                        return <BookingListCard key={props.mode.toString() + i} data={b}/>
                    })}
            </div>
        </Container>
        : <span/>;
}
