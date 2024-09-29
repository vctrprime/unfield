import {container} from "inversify-hooks";
import {AccountsService, IAccountsService} from "../services/AccountsService";
import {BookingsService, IBookingsService} from "../services/BookingsService";

export function registerServices() {
    container.addRequest<IAccountsService>(AccountsService, 'AccountsService');
    container.addRequest<IBookingsService>(BookingsService, 'BookingsService');
}