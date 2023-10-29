import {BaseService} from "./BaseService";
import {SchedulerEventDto} from "../models/dto/schedule/SchedulerEventDto";
import {SchedulerFieldsDto} from "../models/dto/schedule/SchedulerFieldsDto";
import {BookingListItemDto} from "../models/dto/booking/BookingListItemDto";

export interface IScheduleService {
    getFields(): Promise<SchedulerFieldsDto>;
    getEvents(start: Date, end: Date): Promise<SchedulerEventDto[]>;
    getBookingList(start?: Date|null, end?: Date|null, bookingNumber?: string|null): Promise<BookingListItemDto[]>;
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
            url: `${this.baseUrl}/list${params}`
        })
    }
}