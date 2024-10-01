import {BaseService} from "./BaseService";
import {BookingListItemDto} from "../models/dto/bookings/BookingListItemDto";
import {CancelBookingByCustomerCommand} from "../models/command/bookings/CancelBookingByCustomerCommand";
import {t} from "i18next";
import {CustomerBookingListMode} from "../models/common/enums/CustomerBookingListMode";
import {CustomerBookingListItemDto} from "../models/dto/bookings/CustomerBookingListItemDto";

export interface IBookingsService {
    getBooking(number: string, day?: string): Promise<BookingListItemDto>;
    getBookings(mode: CustomerBookingListMode): Promise<CustomerBookingListItemDto[]>;
    cancelBooking(command: CancelBookingByCustomerCommand): Promise<void>;
}

export class BookingsService extends BaseService implements IBookingsService {
    constructor() {
        super("api/bookings");
    }
    
    getBooking(number: string, day?: string): Promise<BookingListItemDto> {
        let params = `?number=${number}`;
        if (day) {
            params += `&day=${day}`
        }
        
        return this.fetchWrapper.get({
            url: `${this.baseUrl}${params}`
        })
    }

    getBookings(mode: CustomerBookingListMode): Promise<CustomerBookingListItemDto[]> {
        let params = `?language=${localStorage.getItem('language') || 'ru'}&mode=${mode}`;
        
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/list${params}`
        })
    }

    cancelBooking(command: CancelBookingByCustomerCommand): Promise<void> {
        return this.fetchWrapper.delete({
            url: `${this.baseUrl}`,
            body: command,
            successMessage: t('booking:success_cancel')
        })
    }
}

