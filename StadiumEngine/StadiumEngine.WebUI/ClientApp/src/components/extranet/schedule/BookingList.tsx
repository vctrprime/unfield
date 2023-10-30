import React, {useEffect, useRef, useState} from 'react';
import {useInject} from "inversify-hooks";
import {IScheduleService} from "../../../services/ScheduleService";
import {BookingListItemDto} from "../../../models/dto/booking/BookingListItemDto";
import {t} from "i18next";
import {useRecoilValue} from "recoil";
import {stadiumAtom} from "../../../state/stadium";
import {GridLoading} from "../common/GridLoading";
import {getOverlayNoRowsTemplate} from "../../../helpers/utils";
import {Button, Checkbox, Icon, Input} from "semantic-ui-react";
import {SchedulerReadonlyBooking} from "../../booking/scheduler/SchedulerReadonlyBooking";
import {SchedulerBookingSkeleton} from "./SchedulerBookingSkeleton";
import {SchedulerBookingError} from "./SchedulerBookingError";
import {SchedulerBooking} from "../../booking/scheduler/SchedulerBooking";
import {ProcessedEvent} from "react-scheduler/types";
import {IBookingService} from "../../../services/BookingService";
import {BookingFormFieldSlotPriceDto} from "../../../models/dto/booking/BookingFormDto";
import {BookingStatus} from "../../../models/dto/booking/enums/BookingStatus";
import Dialog from '@mui/material/Dialog';
import {dateFormatterWithoutTime} from "../../../helpers/date-formatter";

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
    
    const [useSearch, setUseSearch] = useState(false);
    const [searchString, setSearchString] = useState<string>('');
    
    useEffect(() => {
        fetchBookings();
    }, [stadium])
    
    const fetchBookings = () => {
        setIsLoading(true);
        scheduleService.getBookingList(null, null, useSearch ? searchString : null).then((response) => {
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
        {
            field: 'day', 
            cellClass: "grid-center-cell", 
            headerName: t("schedule:list:grid:day"), 
            width: 200,
            valueFormatter: dateFormatterWithoutTime },
    ];
    
    
    return <div className="booking-list-container">
        <Dialog
            open={selectedBooking !== null}>
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
                    { selectedBooking ? isBookingReadonly ? 
                        <SchedulerReadonlyBooking 
                            booking={selectedBooking?.originalData} 
                            fromSearch={useSearch}
                    /> : isBookingLoading ? <SchedulerBookingSkeleton /> : error ? <SchedulerBookingError message={error} /> :
                        <SchedulerBooking
                            bookingData={selectedBooking?.originalData}
                            scheduler={null}
                            slotPrices={slotPrices}
                            updateEvents={() =>  {
                                setSelectedBooking(null);
                                fetchBookings();
                            }}
                            event={{
                                start: new Date(selectedBooking.start)
                            } as ProcessedEvent}
                        /> : <span/>}
                </div>
        </Dialog>
        <div className="booking-list-messages">
            <label className="box-shadow" style={{padding: '8px', marginBottom: 0, marginLeft: '8px', width: 'calc(100% - 16px)', borderRadius: '10px', backgroundColor: 'white'}}>
                <i style={{ color: '#00d2ff', marginRight: 3}} className="fa fa-exclamation-circle" aria-hidden="true"/>
                {useSearch && t('schedule:list:use_search_message')}
                {!useSearch && t('schedule:list:not_use_search_message')}
            </label>
        </div>
        <div className="booking-list-filters">
            <Checkbox
                onChange={(e, data) => setUseSearch(data.checked||false)}
                checked={useSearch}
                label={t('schedule:list:use_search')} />
            {useSearch && <Input icon='search'
                                 value={searchString}
                                 style={{ marginLeft: 10}}
                                 placeholder={t('schedule:list:search_placeholder')}
                                 onChange={(e) => setSearchString(e.target.value)}
            />}
            {!useSearch && <span>456</span>}
            
            <Button style={{ marginLeft: 10}} onClick={fetchBookings}>{t('common:search_button')}</Button>
            {data.length === 0 && !isLoading && <span>{t('schedule:list:no_rows')}</span>}
        </div>
        <div className="grid-container ag-theme-alpine" style={{height: 'calc(100% - 83px)'}}>
            { isLoading ? <GridLoading columns={columnDefs}/> : <AgGridReact
                ref={gridRef}
                rowData={data}
                columnDefs={columnDefs}
                overlayNoRowsTemplate={getOverlayNoRowsTemplate()}
            />}
        </div>
    </div>
}