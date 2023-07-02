import {BookingFormDto} from "../models/dto/booking/BookingFormDto";
import {BaseService} from "./BaseService";
import {AddBookingDraftCommand} from "../models/command/booking/AddBookingDraftCommand";
import {AddBookingDraftDto} from "../models/dto/booking/AddBookingDraftDto";
import {BookingCheckoutDto} from "../models/dto/booking/BookingCheckoutDto";
import {FillBookingDataCommand} from "../models/command/booking/FillBookingDataCommand";
import {CancelBookingCommand} from "../models/command/booking/CancelBookingCommand";
import {ConfirmBookingCommand} from "../models/command/booking/ConfirmBookingCommand";
import {t} from "i18next";
import {PromoCodeDto} from "../models/dto/rates/TariffDto";

export interface IBookingFormService {
    getBookingForm(date: Date, token: string|null, cityId: number|null, q: string|null): Promise<BookingFormDto>;
    addBookingDraft(command: AddBookingDraftCommand): Promise<AddBookingDraftDto>;
    getBookingCheckout(bookingNumber: string): Promise<BookingCheckoutDto>;
    fillBookingData(command: FillBookingDataCommand) : Promise<void>;
    cancelBooking(command: CancelBookingCommand) : Promise<void>;
    confirmBooking(command: ConfirmBookingCommand) : Promise<void>;
    checkPromoCode(tariffId: number, code: string): Promise<PromoCodeDto|null>;
}

export class BookingFormService extends BaseService implements IBookingFormService {
    constructor() {
        super("api/booking");
    }

    getBookingForm(date: Date, token: string|null, cityId: number|null, q: string|null): Promise<BookingFormDto> {
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
        let params = `?bookingNumber=${bookingNumber}`;
        
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/checkout${params}`,
        })
    }

    cancelBooking(command: CancelBookingCommand): Promise<void> {
        return this.fetchWrapper.delete({
            url: `${this.baseUrl}`,
            body: command,
            successMessage: t('booking:success_cancel')
        })
    }

    confirmBooking(command: ConfirmBookingCommand): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/confirm`,
            body: command,
            successMessage: t('booking:success_confirm')
        })
    }

    fillBookingData(command: FillBookingDataCommand): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}`,
            body: command
        })
    }

    checkPromoCode(tariffId: number, code: string): Promise<PromoCodeDto | null> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/checkout/promo/check?tariffId=${tariffId}&code=${code}`,
        })
    }
}