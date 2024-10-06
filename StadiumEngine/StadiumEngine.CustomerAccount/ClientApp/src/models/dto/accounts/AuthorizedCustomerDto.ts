import {StadiumDto} from "./StadiumDto";

export interface AuthorizedCustomerDto {
    firstName?: string;
    lastName?: string;
    phoneNumber: string;
    language: string;
    displayName?: string;
    stadiums: StadiumDto[]
}