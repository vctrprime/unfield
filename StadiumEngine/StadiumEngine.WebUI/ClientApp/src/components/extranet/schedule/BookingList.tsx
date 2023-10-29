import React, {useEffect, useRef, useState} from 'react';
import {useInject} from "inversify-hooks";
import {IScheduleService} from "../../../services/ScheduleService";
import {BookingListItemDto} from "../../../models/dto/booking/BookingListItemDto";
import {t} from "i18next";
import {useRecoilValue} from "recoil";
import {stadiumAtom} from "../../../state/stadium";
import {GridLoading} from "../common/GridLoading";
import {getOverlayNoRowsTemplate} from "../../../helpers/utils";
import {Icon, Modal} from "semantic-ui-react";
import {SchedulerReadonlyBooking} from "../../booking/scheduler/SchedulerReadonlyBooking";
import {SchedulerBookingSkeleton} from "./SchedulerBookingSkeleton";
import {SchedulerBookingError} from "./SchedulerBookingError";
import {SchedulerBooking} from "../../booking/scheduler/SchedulerBooking";
import {ProcessedEvent} from "react-scheduler/types";
import {IBookingService} from "../../../services/BookingService";
import {BookingFormFieldSlotPriceDto} from "../../../models/dto/booking/BookingFormDto";
import {BookingStatus} from "../../../models/dto/booking/enums/BookingStatus";

const AgGrid = require('ag-grid-react');
const {AgGridReact} = AgGrid;

export const BookingList = () => {
    const [scheduleService] = useInject<IScheduleService>('ScheduleService');
    const [bookingService] = useInject<IBookingService>('BookingService');
    
    const stadium = useRecoilValue(stadiumAtom);
    
    const [data, setData] = useState<BookingListItemDto[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    
    const [isBookingLoading, setIsBookingLoading] = useState<boolean>(true);
    const [isBookingReadonly, setIsBookingReadonly] = useState<boolean>(true);
    
    const [selectedBooking, setSelectedBooking] = useState<BookingListItemDto|null>(null);

    const [slotPrices, setSlotPrices] = useState<BookingFormFieldSlotPriceDto[]>([]);
    const [error, setError] = useState<string|null>(null);
    
    useEffect(() => {
        fetchBookings();
    }, [stadium])
    
    const fetchBookings = () => {
        setIsLoading(true);
        scheduleService.getBookingList(null, null, '243').then((response) => {
            setData(response);
            setIsLoading(false);
        })
    };
    
    const gridRef = useRef<any>();

    const onNumberClick = (data: BookingListItemDto) => {
        setIsBookingLoading(true);
        setSelectedBooking(data);
    }

    const raiseError = (message: string) => {
        setError(message);
        setIsBookingLoading(false);
    }
    
    useEffect(() => {
        if (stadium?.token === undefined) {
            raiseError(t('schedule:scheduler:booking:errors:stadium'));
            return;
        }
        
        if (selectedBooking) {
            if (selectedBooking.status !== BookingStatus.Active &&
                selectedBooking.status !== BookingStatus.WeeklyActive &&
                selectedBooking.status !== BookingStatus.WeeklyItemActive) {
                setIsBookingReadonly(true);
                setIsBookingLoading(false);
            }
            else {
                if (selectedBooking.day) {
                    setIsBookingReadonly(false);
                    bookingService.getBookingForm(new Date(selectedBooking.day), stadium.token, null, null, false).then((formResponse) => {
                        const field = formResponse.fields.find(f => f.data.id == selectedBooking.originalData.field.id);
                        if (field) {
                            const slot = field.slots.find(s => s.hour === selectedBooking.startHour);

                            if (slot) {
                                setSlotPrices(slot.prices);

                                if (slot.prices.length > 0) {
                                    setIsBookingLoading(false);
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
                else {
                    setIsBookingReadonly(true);
                    setIsBookingLoading(false);
                }
            }
        }
    }, [selectedBooking])
    
    const NumberRenderer = (obj: any) => {
        return <span className="link-cell" onClick={() => onNumberClick(obj.data)}>{obj.data.number}</span>;
    }
    
    const columnDefs = [
        {field: 'number', headerName: t("schedule:list:grid:number"), width: 200, cellRenderer: NumberRenderer},
        {field: 'day', headerName: 'День', width: 200 },
    ];
    
    
    return <div className="booking-list-container">

        <Modal
            dimmer='blurring'
            size='large'
            open={selectedBooking !== null}>
            <Modal.Content>
                <div style={{width: 580, minHeight: 500}}>
                    <div style={{
                        display: 'flex',
                        justifyContent: 'flex-end',
                        alignItems: 'center',
                        height: 32,
                        paddingRight: '5px',
                        backgroundColor: 'rgba(245, 245, 245, 0.3)'
                    }}>
                        <Icon style={{cursor: 'pointer'}} name='close' onClick={() => setSelectedBooking(null)} />
                    </div>
                    { selectedBooking ? isBookingReadonly ? <SchedulerReadonlyBooking booking={selectedBooking?.originalData} /> : isBookingLoading ? <SchedulerBookingSkeleton /> : error ? <SchedulerBookingError message={error} /> :
                        <SchedulerBooking
                            bookingData={selectedBooking?.originalData}
                            scheduler={null}
                            slotPrices={slotPrices}
                            updateEvents={() => null}
                            event={{
                                start: new Date(selectedBooking.start)
                            } as ProcessedEvent}
                        /> : <span/>}
                </div>
            </Modal.Content>
        </Modal>
        
        
        <div className="booking-list-filters">
            
        </div>
        <div className="grid-container ag-theme-alpine" style={{height: 'calc(100% - 36px)'}}>
            { isLoading ? <GridLoading columns={columnDefs}/> : <AgGridReact
                ref={gridRef}
                rowData={data}
                columnDefs={columnDefs}
                overlayNoRowsTemplate={getOverlayNoRowsTemplate()}
            />}
        </div>
    </div>
}