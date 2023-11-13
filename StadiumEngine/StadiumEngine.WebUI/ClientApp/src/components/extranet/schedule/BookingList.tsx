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
import moment from "moment";
import {DateRangeSelect} from "../common/DateRangeSelect";

const AgGrid = require('ag-grid-react');
const {AgGridReact} = AgGrid;

export const BookingList = () => {
    const [scheduleService] = useInject<IScheduleService>('ScheduleService');
    
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
    
    const [period, setPeriod] = useState<Date[]>([new Date(), moment(new Date()).add(1, 'M').toDate()]);
    
    const changePeriod = (event: any, dates: any) => {
        let dateEnd = dates.value[1];
        const maxEndDate = moment(period[0]).add(1, 'M').toDate();
        if (dateEnd > maxEndDate) {
            dateEnd = maxEndDate; 
        }
        setPeriod([dates.value[0], dateEnd])
    }
    
    const filterDate = (date: Date) => {
        return date <= moment(period[0]).add(1, 'M').toDate();
    }
    
    useEffect(() => {
        fetchBookings();
    }, [stadium])
    
    const fetchBookings = () => {
        setIsLoading(true);
        scheduleService.getBookingList(period[0], period[1], useSearch ? searchString : null).then((response) => {
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
                    scheduleService.getBookingForm(new Date(selectedBooking.day), stadium.token, null, null, false).then((formResponse) => {
                        const field = formResponse.fields.find(f => f.data.id == selectedBooking.originalData.field.id);
                        if (field) {
                            const slot = field.slots.find(s => s.hour === selectedBooking.originalData.startHour);

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

    const SourceRenderer = (obj: any) => {
        return <span>{t("schedule:list:sources:" + obj.data.source)}</span>;
    }

    const StatusRenderer = (obj: any) => {
        return <span>{t("schedule:list:statuses:" + obj.data.status)}</span>;
    }
    
    const columnDefs = [
        {field: 'number', headerName: t("schedule:list:grid:number"), width: 120, cellRenderer: NumberRenderer},
        {
            field: 'isWeekly',
            cellClass: "grid-center-cell",
            headerName: t("schedule:list:grid:is_weekly"),
            width: 140,
            cellRenderer: (params: any) => {
                return <input type='checkbox' checked={params.value} />;
            }
        },
        {field: 'source', cellClass: "grid-center-cell", headerName: t("schedule:list:grid:source"), width: 180, cellRenderer: SourceRenderer},
        {
            field: 'day',
            cellClass: "grid-center-cell",
            headerName: t("schedule:list:grid:day"),
            width: 110,
            valueFormatter: dateFormatterWithoutTime
        },
        {
            field: 'time',
            cellClass: "grid-center-cell",
            headerName: t("schedule:list:grid:time"),
            width: 130
        },
        {
            field: 'duration',
            cellClass: "grid-center-cell",
            headerName: t("schedule:list:grid:duration"),
            width: 130
        },
        {
            field: 'fieldName',
            cellClass: "grid-center-cell",
            headerName: t("schedule:list:grid:field"),
            width: 200
        },
        {
            field: 'tariffName',
            cellClass: "grid-center-cell",
            headerName: t("schedule:list:grid:tariff"),
            width: 200
        },
        {
            field: 'lockerRoomName',
            cellClass: "grid-center-cell",
            headerName: t("schedule:list:grid:locker_room"),
            width: 200
        },
        {
            field: 'customerName',
            cellClass: "grid-center-cell",
            headerName: t("schedule:list:grid:customer_name"),
            width: 200
        },
        {
            field: 'promoCode',
            cellClass: "grid-center-cell",
            headerName: t("schedule:list:grid:promo_code"),
            width: 130
        },
        {
            field: 'promoValue',
            cellClass: "grid-center-cell",
            headerName: t("schedule:list:grid:promo_value"),
            width: 180
        },
        {
            field: 'manualDiscount',
            cellClass: "grid-center-cell",
            headerName: t("schedule:list:grid:manual_discount"),
            width: 120
        },
        {
            field: 'totalAmountBeforeDiscount',
            cellClass: "grid-center-cell",
            headerName: t("schedule:list:grid:total_amount_before_discount"),
            width: 120
        },
        {
            field: 'totalAmountAfterDiscount',
            cellClass: "grid-center-cell",
            headerName: t("schedule:list:grid:total_amount_after_discount"),
            width: 190
        },
        {
            field: 'status',
            cellClass: "grid-center-cell",
            headerName: t("schedule:list:grid:status"),
            width: 130,
            cellRenderer: StatusRenderer
        },
        {
            field: 'closedDay',
            cellClass: "grid-center-cell",
            headerName: t("schedule:list:grid:closed_day"),
            width: 200,
            valueFormatter: dateFormatterWithoutTime,
            hide: !useSearch
        },
    ];

    const getRowStyle = (params: any) => {
        const status = params.data.status;
        if (status === BookingStatus.Finished ||
            status === BookingStatus.WeeklyFinished ||
            status === BookingStatus.WeeklyItemFinished
        ) {
            return { background: 'rgba(0,210,255, 0.2)' };
        }
        
        return {background: 'rgba(60,179,113, 0.3)'}
    };
    
    
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
        <div className="booking-list-mode">
            <Checkbox
                onChange={(e, data) => setUseSearch(data.checked||false)}
                checked={useSearch}
                label={t('schedule:list:use_search')} />
        </div>
        <div className="booking-list-messages">
            <label className="box-shadow" style={{padding: '8px', marginBottom: 0, marginLeft: '8px', width: 'calc(100% - 16px)', borderRadius: '10px', backgroundColor: 'white'}}>
                <i style={{ color: '#00d2ff', marginRight: 3}} className="fa fa-exclamation-circle" aria-hidden="true"/>
                {useSearch && t('schedule:list:use_search_message')}
                {!useSearch && t('schedule:list:not_use_search_message')}
            </label>
        </div>
        <div className="booking-list-filters">
            {useSearch && <Input icon='search'
                                 value={searchString}
                                 placeholder={t('schedule:list:search_placeholder')}
                                 onChange={(e) => setSearchString(e.target.value)}
            />}
            {!useSearch &&
                <DateRangeSelect clearable={false} value={period} filterDate={filterDate} onChange={changePeriod}/>}
            
            <Button disabled={(useSearch && searchString.length < 3) || (!useSearch && period[1] === undefined)} style={{ marginLeft: 10}} onClick={fetchBookings}>{t('common:search_button')}</Button>
            {data.length === 0 && !isLoading && <span>{t('schedule:list:no_rows')}</span>}
        </div>
        <div className="grid-container ag-theme-alpine" style={{height: 'calc(100% - 111px)'}}>
            { isLoading ? <GridLoading columns={columnDefs}/> : <AgGridReact
                ref={gridRef}
                rowData={data}
                columnDefs={columnDefs}
                getRowStyle={getRowStyle}
                overlayNoRowsTemplate={getOverlayNoRowsTemplate()}
            />}
        </div>
    </div>
}