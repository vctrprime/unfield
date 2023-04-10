import {BookingFormDto} from "../models/dto/booking/BookingFormDto";
import {BaseService} from "./BaseService";

export interface IBookingFormService {
    getBookingForm(token: string|null): Promise<BookingFormDto>;
}

export class BookingFormService extends BaseService implements IBookingFormService {
    constructor() {
        super("api/booking");
    }

    getBookingForm(token: string|null): Promise<BookingFormDto> {
        let params = '';
        if (token !== null) {
            if (params.length === 0) {
                params += "?"
            }
            else {
                params += "&"
            }
            params += `token=${token}`
        }
        
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/form${params}`,
        })
    }
}