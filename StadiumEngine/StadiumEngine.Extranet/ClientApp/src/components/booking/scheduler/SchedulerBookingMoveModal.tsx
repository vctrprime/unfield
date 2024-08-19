import React, {useEffect, useRef, useState} from "react";
import {Dialog} from "@mui/material";
import {Button, Checkbox} from "semantic-ui-react";
import {t} from "i18next";
import {BookingFormDto, BookingFormFieldSlotDto} from "../../../models/dto/booking/BookingFormDto";
import {useInject} from "inversify-hooks";
import {useRecoilValue} from "recoil";
import {stadiumAtom} from "../../../state/stadium";
import SemanticDatepicker from "react-semantic-ui-datepickers";
import {getLocale} from "../../../i18n/i18n";
import {
    SaveSchedulerBookingDataCommandCost,
    SaveSchedulerBookingDataCommandMoveData
} from "../../../models/command/schedule/SaveSchedulerBookingDataCommand";
import {SportKind} from "../../../models/dto/offers/enums/SportKind";
import {FieldDto} from "../../../models/dto/offers/FieldDto";
import {BookingDto} from "../../../models/dto/booking/BookingDto";
import {SimpleAlert} from "../../common/SimpleAlert";
import {IScheduleService} from "../../../services/ScheduleService";

export interface SchedulerBookingMoveModalProps {
    open: boolean;
    setOpen: any;
    booking: BookingDto;
    startDay: Date;
    setMoveData: any;
    setOneInRow: any;
    setMoveCosts: any;
    oneInRow: boolean;
}

export const SchedulerBookingMoveModal = ({ open, setOpen, booking, startDay, setMoveData, oneInRow, setOneInRow, setMoveCosts }: SchedulerBookingMoveModalProps) => {
    const stadium = useRecoilValue(stadiumAtom);
    
    const [data, setData] = useState<BookingFormDto|null>(null);

    const [date, setDate] = useState(startDay);

    const onChange = (event: any, data: any) => {
        setDate(data.value);
    }
    
    const [selectedFieldId, setSelectedFieldId] = useState<number|null>(null);
    const [selectedStartHour, setStartHour] = useState<number|null>(null);

    const [scheduleService] = useInject<IScheduleService>('ScheduleService');
    
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
            scheduleService.getBookingFormForMove(date, stadium?.token ?? '' , booking.number).then((formResponse) => {
                setData(formResponse);
            })
        }

        setStartHour(null);
        setSelectedFieldId(null);
        setNewAmount(null);
        
    }, [date, open])
    
    const [newAmount, setNewAmount] = useState<number|null>(null);
    
    const selectSlot = (field: FieldDto , slot: BookingFormFieldSlotDto ) => {
        setStartHour(slot.hour);
        setSelectedFieldId(field.id);
        scheduleService.getBookingCheckout(booking.number, true, booking.tariff.id, date, field.id, slot.hour).then((response) => {
            const costs = response.pointPrices.slice(0, booking.hoursCount/0.5).map((p) => {
                return {
                    startHour: p.start,
                    endHour: p.end,
                    cost: p.value
                } as SaveSchedulerBookingDataCommandCost
            });
            setMoveCosts(costs);
            setNewAmount(costs.reduce((accumulator, object) => {
                return accumulator + object.cost;
            }, 0) + booking.inventoryAmount)
        });
    }
    
    useEffect(() => {
        if (!oneInRow) {
            setDate(new Date(booking.day));
        }
    }, [oneInRow])
    
    return <Dialog maxWidth={"md"} open={open} onClose={() => {
        setMoveCosts(null);
    }}>
        <div className="move-booking-container">
            <div className="move-booking-notification">
                <SimpleAlert message={"schedule:scheduler:booking:move:notifications:common"}/>
            </div>
            <div className="move-booking-filters">
                <SemanticDatepicker
                    className="booking-form-date-picker"
                    firstDayOfWeek={1}
                    datePickerOnly={true}
                    locale={getLocale()}
                    value={date}
                    disabled={booking.isWeekly && !oneInRow}
                    format={'DD.MM.YYYY'}
                    minDate={new Date()}
                    onChange={onChange}
                    clearable={false}
                    pointing="left"
                />
                {booking.isWeekly &&
                    <Checkbox label={t("schedule:scheduler:booking:move:one_in_row")} checked={oneInRow} onChange={() => setOneInRow(!oneInRow)}  />
                }
            </div>
            <div className="move-booking-fields">
                {data ? data.fields.length === 0 ? <div className="no-fields">{t('schedule:scheduler:booking:move:no_fields')}</div> : data.fields.map((field) => {
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
                                            setNewAmount(null);
                                        }
                                        else {
                                            selectSlot(field.data, slot);
                                        }
                                    }}>{slot.name}</div>;
                                    })}
                            </div>
                        </div>
                    }) : <span/>}
            </div>
            <div className="move-booking-notification">
                {booking.isWeekly && !oneInRow && <SimpleAlert message={"schedule:scheduler:booking:move:notifications:weekly_move_description"}/>}
                {newAmount && newAmount !== booking.totalAmountBeforeDiscount && 
                    <div style={{display: 'flex', flexDirection: 'column', marginTop: 15}}>
                        <SimpleAlert style={{ fontSize: '14px', paddingBottom: 10}} message={"schedule:scheduler:booking:move:notifications:change_amount"}/>
                        <span>{t("schedule:scheduler:booking:move:notifications:current_amount")} <b>{booking.totalAmountBeforeDiscount}</b></span>
                        <span>{t("schedule:scheduler:booking:move:notifications:future_amount")} <b>{newAmount}</b></span>
                    </div>
                }
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