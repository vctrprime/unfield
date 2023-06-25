import {MainSettingsDto} from "../models/dto/settings/MainSettingsDto";
import {UpdateMainSettingsCommand} from "../models/command/settings/UpdateMainSettingsCommand";
import {BaseService} from "./BaseService";
import {t} from "i18next";
import {BreakDto} from "../models/dto/settings/BreakDto";
import {TariffDto} from "../models/dto/rates/TariffDto";

export interface ISettingsService {
    getMainSettings(): Promise<MainSettingsDto>;
    updateMainSettings(command: UpdateMainSettingsCommand): Promise<void>;
    
    getBreaks(): Promise<BreakDto[]>;
}

export class SettingsService extends BaseService implements ISettingsService {
    constructor() {
        super("api/settings");
    }

    getMainSettings(): Promise<MainSettingsDto> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/main`,
        })
    }

    updateMainSettings(command: UpdateMainSettingsCommand): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/main`,
            body: command,
            successMessage: t('common:success_request')
        })
    }

    getBreaks(): Promise<BreakDto[]> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/breaks`,
            withSpinner: false,
            hideSpinner: false
        })
    }
}