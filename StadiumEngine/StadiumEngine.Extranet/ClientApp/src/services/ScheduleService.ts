import {BaseService} from "./BaseService";
import {SchedulerEventDto} from "../models/dto/schedule/SchedulerEventDto";
import {SchedulerFieldsDto} from "../models/dto/schedule/SchedulerFieldsDto";
import {BookingListItemDto} from "../models/dto/booking/BookingListItemDto";
import {BookingFormDto} from "../models/dto/booking/BookingFormDto";
import {AddBookingDraftCommand} from "../models/command/booking/AddBookingDraftCommand";
import {AddBookingDraftDto} from "../models/dto/booking/AddBookingDraftDto";
import {BookingCheckoutDto} from "../models/dto/booking/BookingCheckoutDto";
import {CancelSchedulerBookingCommand} from "../models/command/schedule/CancelSchedulerBookingCommand";
import {t} from "i18next";
import {SaveSchedulerBookingDataCommand} from "../models/command/schedule/SaveSchedulerBookingDataCommand";

export interface IScheduleService {
    getFields(): Promise<SchedulerFieldsDto>;
    getEvents(start: Date, end: Date): Promise<SchedulerEventDto[]>;
    getBookingList(start?: Date|null, end?: Date|null, bookingNumber?: string|null): Promise<BookingListItemDto[]>;
    getBookingForm(date: Date, token: string|null, cityId: number|null, q: string|null, hideSpinner?: boolean): Promise<BookingFormDto>;
    getBookingFormForMove(date: Date, token: string, bookingNumber: string): Promise<BookingFormDto>;
    getBookingCheckout(bookingNumber: string, isConfirmed: boolean, tariffId?: number, day?: Date, fieldId?: number, startHour?: number): Promise<BookingCheckoutDto>;
    addSchedulerBookingDraft(command: AddBookingDraftCommand): Promise<AddBookingDraftDto>;
    cancelSchedulerBooking(command: CancelSchedulerBookingCommand): Promise<void>;
    saveSchedulerBookingData(command: SaveSchedulerBookingDataCommand): Promise<void>;
}

export class ScheduleService extends BaseService implements IScheduleService {
    constructor() {
        super("api/schedule");
    }

    getFields(): Promise<SchedulerFieldsDto> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/fields`
        })
    }

    getEvents(start: Date, end: Date): Promise<SchedulerEventDto[]> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/events?start=${start.toJSON()}&end=${end.toJSON()}&language=${localStorage.getItem('language') || 'ru'}`
        })
    }
    
    getBookingList(start?: Date|null, end?: Date|null, bookingNumber?: string|null): Promise<BookingListItemDto[]> {
        let params = `?language=${localStorage.getItem('language') || 'ru'}`;
        
        if (bookingNumber) {
            params += `&bookingNumber=${bookingNumber}`;
        }
        
        if (start && end) {
            params += `&start=${start.toDateString()}&end=${end.toDateString()}`
        }
        
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/list${params}`,
            withSpinner: false
        })
    }

    getBookingForm(date: Date, token: string|null, cityId: number|null, q: string|null, hideSpinner?: boolean): Promise<BookingFormDto> {
        let params = `?day=${date.toDateString()}`;
        if (token !== null) {
            params += `&stadiumToken=${token}`
        }
        if (cityId !== null) {
            params += `&cityId=${cityId}`
        }
        if (q !== null && q !== '') {
            params += `&q=${q}`
        }

        return this.fetchWrapper.get({
            url: `${this.baseUrl}/booking/form${params}`,
            hideSpinner: hideSpinner === undefined ? true : hideSpinner
        })
    }

    getBookingFormForMove(date: Date, token: string, bookingNumber: string): Promise<BookingFormDto> {
        let params = `?day=${date.toDateString()}&bookingNumber=${bookingNumber}&stadiumToken=${token}`;

        return this.fetchWrapper.get({
            url: `${this.baseUrl}/booking/form/moving/${params}`,
        })
    }

    addSchedulerBookingDraft(command: AddBookingDraftCommand): Promise<AddBookingDraftDto> {
        return this.fetchWrapper.post({
            url: `${this.baseUrl}/booking/draft`,
            body: command,
            hideSpinner: false
        })
    }

    getBookingCheckout(bookingNumber: string, isConfirmed: boolean, tariffId?: number, day?: Date, fieldId?: number, startHour?: number): Promise<BookingCheckoutDto> {
        let params = `?bookingNumber=${bookingNumber}&isConfirmed=${isConfirmed}`;

        if (tariffId) {
            params += `&tariffId=${tariffId}`
        }

        if (day) {
            params += `&day=${day.toDateString()}`
        }

        if (fieldId) {
            params += `&fieldId=${fieldId}`
        }

        if (startHour) {
            params += `&startHour=${startHour}`
        }

        return this.fetchWrapper.get({
            url: `${this.baseUrl}/booking/checkout${params}`,
        })
    }

    cancelSchedulerBooking(command: CancelSchedulerBookingCommand): Promise<void> {
        return this.fetchWrapper.delete({
            url: `${this.baseUrl}/booking/cancel`,
            body: command,
            successMessage: t('booking:success_cancel')
        })
    }

    saveSchedulerBookingData(command: SaveSchedulerBookingDataCommand): Promise<void> {
        if ( command.isNew ) {
            return this.fetchWrapper.post({
                url: `${this.baseUrl}/booking/save`,
                body: command
            })
        }

        return this.fetchWrapper.put({
            url: `${this.baseUrl}/booking/update`,
            body: command
        })
    }
}