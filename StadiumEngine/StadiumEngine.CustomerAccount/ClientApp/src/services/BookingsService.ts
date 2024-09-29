import {BaseService} from "./BaseService";
import {BookingListItemDto} from "../models/dto/bookings/BookingListItemDto";
import {CancelBookingByCustomerCommand} from "../models/command/bookings/CancelBookingByCustomerCommand";
import {t} from "i18next";

export interface IBookingsService {
    getBooking(number: string, day?: string): Promise<BookingListItemDto>;
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

    cancelBooking(command: CancelBookingByCustomerCommand): Promise<void> {
        return this.fetchWrapper.delete({
            url: `${this.baseUrl}`,
            body: command,
            successMessage: t('booking:success_cancel')
        })
    }
}

