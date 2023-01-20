import {container} from "inversify-hooks";
import {AccountsService, IAccountsService} from "../services/AccountsService";

export function registerServices() {
    container.addSingleton<IAccountsService>(AccountsService, 'AccountsService');
}