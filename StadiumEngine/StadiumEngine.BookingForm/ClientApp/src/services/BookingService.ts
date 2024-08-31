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
import {ConfirmBookingDto} from "../models/dto/booking/ConfirmBookingDto";
import {BookingSource} from "../models/dto/booking/enums/BookingSource";

export interface IBookingService {
    getBookingForm(date: Date, token: string|null, cityId: number|null, q: string|null, hideSpinner?: boolean): Promise<BookingFormDto>;
    addBookingDraft(command: AddBookingDraftCommand): Promise<AddBookingDraftDto>;
    getBookingCheckout(bookingNumber: string, isConfirmed: boolean, tariffId?: number, day?: Date, fieldId?: number, startHour?: number): Promise<BookingCheckoutDto>;
    fillBookingData(command: FillBookingDataCommand) : Promise<void>;
    cancelBooking(command: CancelBookingCommand) : Promise<void>;
    confirmBooking(command: ConfirmBookingCommand) : Promise<ConfirmBookingDto>;
    checkPromoCode(tariffId: number, code: string): Promise<PromoCodeDto|null>;
}

export class BookingService extends BaseService implements IBookingService {
    constructor() {
        super("api/booking");
    }

    getBookingForm(date: Date, token: string | null, cityId: number | null, q: string | null, hideSpinner?: boolean): Promise<BookingFormDto> {
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
            hideSpinner: hideSpinner === undefined ? true : hideSpinner
        })
    }

    addBookingDraft(command: AddBookingDraftCommand): Promise<AddBookingDraftDto> {
        return this.fetchWrapper.post({
            url: `${this.baseUrl}/draft`,
            body: command,
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

    confirmBooking(command: ConfirmBookingCommand): Promise<ConfirmBookingDto> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/confirm`,
            body: command,
            // successMessage: t('booking:success_confirm')
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