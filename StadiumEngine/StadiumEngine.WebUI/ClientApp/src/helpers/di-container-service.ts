import {container} from "inversify-hooks";
import {AccountsService, IAccountsService} from "../services/AccountsService";
import {AdminService, IAdminService} from "../services/AdminService";
import {IOffersService, OffersService} from "../services/OffersService";

export function registerServices() {
    container.addRequest<IAccountsService>(AccountsService, 'AccountsService');
    container.addRequest<IAdminService>(AdminService, 'AdminService');
    container.addRequest<IOffersService>(OffersService, 'OffersService');
}