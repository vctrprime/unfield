import React, {useEffect, useRef, useState} from "react";
import {Dialog} from "@mui/material";
import {Button} from "semantic-ui-react";
import {t} from "i18next";
import {BookingFormDto} from "../../../models/dto/booking/BookingFormDto";
import {useInject} from "inversify-hooks";
import {IBookingService} from "../../../services/BookingService";
import {useRecoilValue} from "recoil";
import {stadiumAtom} from "../../../state/stadium";

export interface SchedulerBookingMoveModalProps {
    open: boolean;
    setOpen: any;
    bookingNumber: string;
    startDay: Date;
}

export const SchedulerBookingMoveModal = ({ open, setOpen, bookingNumber, startDay }: SchedulerBookingMoveModalProps) => {
    const stadium = useRecoilValue(stadiumAtom);
    
    const [data, setData] = useState<BookingFormDto|null>(null);

    const [date, setDate] = useState(startDay);

    const onChange = (event: any, data: any) => {
        setDate(data.value);
    }

    const [bookingService] = useInject<IBookingService>('BookingService');
    
    useEffect(() => {
        if (open) {
            setData(null);
            bookingService.getBookingFormForMove(date, stadium?.token ?? '' , bookingNumber).then((formResponse) => {
                setData(formResponse);
                console.log(formResponse);
            })
        }
    }, [date, open])
    
    return <Dialog open={open}>
        <div style={{width: 580, minHeight: 500}} className="move-booking-container">
            
            <div className="move-booking-buttons">
                <Button style={{backgroundColor: '#CD5C5C', color: 'white'}} onClick={() => {
                    setOpen(false);
                }}>{t("common:back")}</Button>
            </div>
        </div>
    </Dialog>
}