import {AuthorizeUserDto} from "../models/dto/accounts/AuthorizeUserDto";
import {AuthorizeUserCommand} from "../models/command/accounts/AuthorizeUserCommand";
import {useFetchWrapper} from "../helpers/fetch-wrapper";
import { container } from 'inversify-hooks';

export interface IAccountsService {
    authorize(command: AuthorizeUserCommand): Promise<AuthorizeUserDto>;
}

export class AccountsService implements IAccountsService {
    fetchWrapper: any;
    
    constructor() {
        this.fetchWrapper = useFetchWrapper();
    }
    
    
    public authorize(command: AuthorizeUserCommand): Promise<AuthorizeUserDto> {
        return this.fetchWrapper.post({
            url: `api/accounts/login`,
            body: command,
            hideSpinner: false
        })
    }
}

