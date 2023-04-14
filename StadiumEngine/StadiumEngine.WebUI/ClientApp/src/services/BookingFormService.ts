import {BookingFormDto} from "../models/dto/booking/BookingFormDto";
import {BaseService} from "./BaseService";

export interface IBookingFormService {
    getBookingForm(date: Date, token: string|null, cityId: number|null): Promise<BookingFormDto>;
}

export class BookingFormService extends BaseService implements IBookingFormService {
    constructor() {
        super("api/booking");
    }

    getBookingForm(date: Date, token: string|null, cityId: number|null): Promise<BookingFormDto> {
        let params = `?date=${date.toDateString()}`;
        if (token !== null) {
            params += `&token=${token}`
        }
        if (cityId !== null) {
            params += `&cityId=${cityId}`
        }
        
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/form${params}`,
        })
    }
}