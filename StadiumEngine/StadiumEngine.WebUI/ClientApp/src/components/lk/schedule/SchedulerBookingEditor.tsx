import {SchedulerHelpers} from "react-scheduler/types";
import {SchedulerEventDto} from "../../../models/dto/schedule/SchedulerEventDto";
import {useRecoilValue} from "recoil";
import {permissionsAtom} from "../../../state/permissions";
import {PermissionsKeys} from "../../../static/PermissionsKeys";
import React, {useEffect, useState} from "react";
import {BookingDto} from "../../../models/dto/booking/BookingDto";
import {BookingFormFieldSlotPriceDto} from "../../../models/dto/booking/BookingFormDto";
import {useInject} from "inversify-hooks";
import {IBookingService} from "../../../services/BookingService";
import {parseString} from "../../../helpers/time-point-parser";
import {stadiumAtom} from "../../../state/stadium";
import {BookingSource} from "../../../models/dto/booking/enums/BookingSource";
import {AddBookingDraftCommand} from "../../../models/command/booking/AddBookingDraftCommand";
import {Icon} from "semantic-ui-react";
import {SchedulerBooking} from "../../booking/scheduler/SchedulerBooking";
import {SchedulerBookingSkeleton} from "./SchedulerBookingSkeleton";
import {SchedulerBookingError} from "./SchedulerBookingError";
import {t} from "i18next";

interface SchedulerBookingEditorProps {
    scheduler: SchedulerHelpers;
    events: SchedulerEventDto[];
}

export const SchedulerBookingEditor = ({ scheduler, events }: SchedulerBookingEditorProps) => {
    const event = scheduler.edited;

    const permissions = useRecoilValue(permissionsAtom);

    const hasInsertPermission = permissions.find( x => x.name == PermissionsKeys.InsertBooking) !== undefined;
    const hasUpdatePermission = permissions.find( x => x.name == PermissionsKeys.UpdateBooking) !== undefined;

    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string|null>(null);
    const [data, setData] = useState<BookingDto>({} as BookingDto);
    const [slotPrices, setSlotPrices] = useState<BookingFormFieldSlotPriceDto[]>([]);

    const [bookingService] = useInject<IBookingService>('BookingService');

    const day = scheduler.state.start.value;
    const hour: number = parseString(`${scheduler.state.start.value.getHours()}:${scheduler.state.start.value.getMinutes()}`);
    const fieldId = scheduler.field_id;

    const stadium = useRecoilValue(stadiumAtom);

    const raiseError = (message: string) => {
        setError(message);
        setIsLoading(false);
    }

    useEffect(() => {
        if (isLoading) {

            const hours = (day.getTime() - new Date().getTime()) / 3600000;
            const nextHalfHourEvent = events.find( x => x.field_id === fieldId && x.start.getTime() === scheduler.state.end.value.getTime() );

            const intersectEvents = events.filter( x => x.field_id === fieldId
                && (x.start.getTime() === scheduler.state.start.value.getTime()
                    ||
                    x.end.getTime() === scheduler.state.end.value.getTime()
                    ||
                    (x.start.getTime() < scheduler.state.start.value.getTime() && x.end.getTime() > scheduler.state.start.value.getTime() )
                    ||
                    (x.start.getTime() < scheduler.state.end.value.getTime() && x.end.getTime() > scheduler.state.end.value.getTime() )
                ));

            if (event === undefined && hours < 0) {
                raiseError(t('schedule:scheduler:booking:errors:invalid_time'));
                return;
            }

            if (event === undefined && intersectEvents.length > 0 ) {
                raiseError(t('schedule:scheduler:booking:errors:intersect'));
                return;
            }

            if (event === undefined && nextHalfHourEvent !== undefined) {
                raiseError(t('schedule:scheduler:booking:errors:invalid_time'));
                return;
            }

            if (stadium?.token === undefined) {
                raiseError(t('schedule:scheduler:booking:errors:stadium'));
                return;
            }

            if (event === undefined && !hasInsertPermission ) {
                raiseError(t('schedule:scheduler:booking:errors:forbidden'));
                return;
            }


            bookingService.getBookingForm(day, stadium.token, null, null, false).then((formResponse) => {
                const field = formResponse.fields.find(f => f.data.id == fieldId);
                if (field) {
                    const slot = field.slots.find(s => s.hour === hour);

                    if (slot) {
                        setSlotPrices(slot.prices);

                        if (slot.prices.length > 0) {
                            if (event) {
                                setData(event.data);
                                setIsLoading(false);
                            }
                            else {
                                bookingService.addSchedulerBookingDraft({
                                    fieldId: fieldId,
                                    tariffId: slot.prices[0].tariffId,
                                    hour: hour,
                                    source: BookingSource.Schedule,
                                    day: day.toDateString()
                                } as AddBookingDraftCommand).then((response) => {
                                    setData({
                                        id: 0,
                                        number: response.bookingNumber,
                                        tariff: {
                                            id: slot.prices[0].tariffId
                                        }
                                    } as BookingDto);
                                    setIsLoading(false);
                                })
                                    .catch((error) => {
                                        raiseError(error);
                                    })
                            }
                        }
                        else {
                            raiseError(t('schedule:scheduler:booking:errors:slot_prices'))
                        }
                    }
                    else {
                        raiseError(t('schedule:scheduler:booking:errors:slot'))
                    }
                }
                else {
                    raiseError(t('schedule:scheduler:booking:errors:fields'));
                }
            })
        }
    }, [isLoading])

    return (
        <div style={{width: 580, minHeight: error ? 'auto': 500}}>
            <div style={{
                display: 'flex',
                justifyContent: 'flex-end',
                alignItems: 'center',
                height: 32,
                paddingRight: '5px',
                backgroundColor: 'rgba(245, 245, 245, 0.3)'
            }}>
                <Icon style={{cursor: 'pointer'}} name='close' onClick={scheduler.close} />
            </div>
            {isLoading ? <SchedulerBookingSkeleton /> : error ? <SchedulerBookingError message={error} /> : <SchedulerBooking bookingData={data} slotPrices={slotPrices} />}
        </div>
    );
}