import {BookingFormDto} from "../models/dto/booking/BookingFormDto";
import {BaseService} from "./BaseService";

export interface IBookingFormService {
    getBookingForm(): Promise<BookingFormDto>;
}

export class BookingFormService extends BaseService implements IBookingFormService {
    constructor() {
        super("api/booking");
    }

    getBookingForm(): Promise<BookingFormDto> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/form`,
        })
    }
}