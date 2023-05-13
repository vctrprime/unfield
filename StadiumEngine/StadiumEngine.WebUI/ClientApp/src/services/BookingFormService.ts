import {BookingFormDto} from "../models/dto/booking/BookingFormDto";
import {BaseService} from "./BaseService";
import {AddBookingDraftCommand} from "../models/command/booking/AddBookingDraftCommand";
import {AddBookingDraftDto} from "../models/dto/booking/AddBookingDraftDto";
import {BookingCheckoutDto} from "../models/dto/booking/BookingCheckoutDto";

export interface IBookingFormService {
    getBookingForm(date: Date, token: string|null, cityId: number|null, q: string|null): Promise<BookingFormDto>;
    addBookingDraft(command: AddBookingDraftCommand): Promise<AddBookingDraftDto>;
    getBookingCheckout(bookingNumber: string): Promise<BookingCheckoutDto>;
}

export class BookingFormService extends BaseService implements IBookingFormService {
    constructor() {
        super("api/booking");
    }

    getBookingForm(date: Date, token: string|null, cityId: number|null, q: string|null): Promise<BookingFormDto> {
        let params = `?day=${date.toDateString()}&currentHour=${new Date().getHours()}`;
        if (token !== null) {
            params += `&token=${token}`
        }
        if (cityId !== null) {
            params += `&cityId=${cityId}`
        }
        if (q !== null && q !== '') {
            params += `&q=${q}`
        }
        
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/form${params}`,
        })
    }

    addBookingDraft(command: AddBookingDraftCommand): Promise<AddBookingDraftDto> {
        return this.fetchWrapper.post({
            url: `${this.baseUrl}/draft`,
            body: command
        })
    }

    getBookingCheckout(bookingNumber: string): Promise<BookingCheckoutDto> {
        let params = `?bookingNumber=${bookingNumber}&currentHour=${new Date().getHours()}`;
        
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/checkout${params}`,
        })
    }
}