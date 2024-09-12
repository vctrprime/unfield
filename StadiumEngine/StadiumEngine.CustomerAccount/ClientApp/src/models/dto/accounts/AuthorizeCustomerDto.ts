import {AuthorizeCustomerBookingDto} from "./AuthorizeCustomerBookingDto";

export interface AuthorizeCustomerDto {
    firstName?: string;
    lastName?: string;
    phoneNumber: string;
    language: string;
    booking?: AuthorizeCustomerBookingDto;
}