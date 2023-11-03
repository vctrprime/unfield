import React, {useEffect, useRef, useState} from "react";
import {Dialog} from "@mui/material";
import {Button} from "semantic-ui-react";
import {t} from "i18next";
import {BookingFormDto} from "../../../models/dto/booking/BookingFormDto";
import {useInject} from "inversify-hooks";
import {IBookingService} from "../../../services/BookingService";
import {useRecoilValue} from "recoil";
import {stadiumAtom} from "../../../state/stadium";
import SemanticDatepicker from "react-semantic-ui-datepickers";
import {getLocale} from "../../../i18n/i18n";
import {
    SaveSchedulerBookingDataCommandMoveData
} from "../../../models/command/schedule/SaveSchedulerBookingDataCommand";
import {SportKind} from "../../../models/dto/offers/enums/SportKind";

export interface SchedulerBookingMoveModalProps {
    open: boolean;
    setOpen: any;
    bookingNumber: string;
    startDay: Date;
    setMoveData: any;
}

export const SchedulerBookingMoveModal = ({ open, setOpen, bookingNumber, startDay, setMoveData }: SchedulerBookingMoveModalProps) => {
    const stadium = useRecoilValue(stadiumAtom);
    
    const [data, setData] = useState<BookingFormDto|null>(null);

    const [date, setDate] = useState(startDay);

    const onChange = (event: any, data: any) => {
        setDate(data.value);
    }
    
    const [selectedFieldId, setSelectedFieldId] = useState<number|null>(null);
    const [selectedStartHour, setStartHour] = useState<number|null>(null);

    const [bookingService] = useInject<IBookingService>('BookingService');
    
    const confirmMoving = () => {
        setMoveData({
            startHour: selectedStartHour,
            fieldId: selectedFieldId,
            day: date.toDateString()
        } as SaveSchedulerBookingDataCommandMoveData);
        setOpen(false);
    }
    
    useEffect(() => {
        if (open) {
            setData(null);
            bookingService.getBookingFormForMove(date, stadium?.token ?? '' , bookingNumber).then((formResponse) => {
                setData(formResponse);
            })
        }
    }, [date, open])
    
    return <Dialog open={open}>
        <div className="move-booking-container">
            <div className="move-booking-filters">
                <SemanticDatepicker
                    className="booking-form-date-picker"
                    firstDayOfWeek={1}
                    datePickerOnly={true}
                    locale={getLocale()}
                    value={date}
                    format={'DD.MM.YYYY'}
                    minDate={new Date()}
                    onChange={onChange}
                    clearable={false}
                    pointing="left"
                />
            </div>
            <div className="move-booking-fields">
                {data?.fields.map((field) => {
                        return <div className="move-booking-field-card">
                            <div className="move-booking-field">
                                {field.data.images.length ?
                                    <img src={"/legal-images/" + field.data.images[0]}/>  : <span/>
                                }
                                <div className="move-booking-field-text">
                                    <div className="move-booking-field-name">{field.data.name}</div>
                                    <div className="move-booking-field-sports">
                                        {field.data.sportKinds.length === 0 ?
                                            <span style={{paddingLeft: '10px', fontSize: '12px', fontWeight: "bold"}}>{t("booking:field_card:no_sports")}</span> :
                                            field.data.sportKinds.map((s, i) => {
                                                const value = SportKind[s];
                                                const text = t("offers:sports:" + value.toLowerCase());
    
                                                return <div style={ i === 0 ? { marginLeft: 0} : {}} key={`${field.data.id}-${i}`} className="field-sport">{text}</div>;
                                            })}
                                    </div>
                                </div>
                            </div>
                            <div className="move-booking-slots">
                                {field.slots.map((slot) => {
                                    const isActive = selectedFieldId === field.data.id && selectedStartHour === slot.hour;
                                    
                                    return <div className={"move-booking-slot " + ( isActive ? "active" : "")} onClick={() => {
                                        if (isActive) {
                                            setStartHour(null);
                                            setSelectedFieldId(null);
                                        }
                                        else {
                                            setStartHour(slot.hour);
                                            setSelectedFieldId(field.data.id);
                                        }
                                    }}>{slot.name}</div>;
                                    })}
                            </div>
                        </div>
                    })}
            </div>
            <div className="move-booking-buttons">
                <Button
                    disabled={!(selectedFieldId && selectedStartHour)}
                    style={{backgroundColor: '#3CB371', color: 'white'}}
                    onClick={confirmMoving}
                >
                    {t("schedule:scheduler:booking:move:confirm")}
                </Button>
                <Button style={{backgroundColor: '#CD5C5C', color: 'white'}} onClick={() => {
                    setOpen(false);
                }}>{t("common:back")}</Button>
            </div>
        </div>
    </Dialog>
}