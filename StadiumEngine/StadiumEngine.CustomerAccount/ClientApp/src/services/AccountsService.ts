import {AuthorizeCustomerCommand} from "../models/command/accounts/AuthorizeCustomerCommand";
import {BaseService} from "./BaseService";
import {t} from "i18next";
import {
    ChangeCustomerPasswordCommand
} from "../models/command/accounts/ChangeCustomerPasswordCommand";
import {
    ResetCustomerPasswordCommand
} from "../models/command/accounts/ResetCustomerPasswordCommand";
import {AuthorizeCustomerDto} from "../models/dto/accounts/AuthorizeCustomerDto";
import {AuthorizeCustomerByRedirectCommand} from "../models/command/accounts/AuthorizeCustomerByRedirectCommand";

export interface IAccountsService {
    authorizeByRedirect(command: AuthorizeCustomerByRedirectCommand): Promise<AuthorizeCustomerDto>;
    authorize(command: AuthorizeCustomerCommand): Promise<AuthorizeCustomerDto>;
    logout(): Promise<void>;
    changeLanguage(language: string): Promise<void>;
    changePassword(command: ChangeCustomerPasswordCommand): Promise<void>;
    resetPassword(command: ResetCustomerPasswordCommand): Promise<void>;
}

export class AccountsService extends BaseService implements IAccountsService {
    constructor() {
        super("api/accounts");
    }

    authorize(command: AuthorizeCustomerCommand): Promise<AuthorizeCustomerDto> {
        return this.fetchWrapper.post({
            url: `${this.baseUrl}/login`,
            body: command,
            hideSpinner: false
        })
    }

    authorizeByRedirect(command: AuthorizeCustomerByRedirectCommand): Promise<AuthorizeCustomerDto> {
        return this.fetchWrapper.post({
            url: `${this.baseUrl}/login/redirect`,
            body: command
        })
    }

    logout(): Promise<void> {
        return this.fetchWrapper.delete({
            url: `${this.baseUrl}/logout`
        });
    }
    
    changeLanguage(language: string): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/customer-language/${language}`,
            hideSpinner: false
        })
    }
    
    changePassword(command: ChangeCustomerPasswordCommand): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/customer-password/change`,
            body: command,
            showErrorAlert: false,
            withSpinner: false,
            successMessage: t('accounts:change_password:success')
        })
    }

    resetPassword(command: ResetCustomerPasswordCommand): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/customer-password/reset`,
            body: command,
            showErrorAlert: false,
            withSpinner: false,
            successMessage: t('accounts:reset_password:success')
        })
    }


}

