import {StadiumMainSettingsDto} from "../models/dto/settings/StadiumMainSettingsDto";
import {UpdateStadiumMainSettingsCommand} from "../models/command/settings/UpdateStadiumMainSettingsCommand";
import {BaseService} from "./BaseService";
import {t} from "i18next";

export interface ISettingsService {
    getStadiumMainSettings(): Promise<StadiumMainSettingsDto>;
    updateStadiumMainSettings(command: UpdateStadiumMainSettingsCommand): Promise<void>;
}

export class SettingsService extends BaseService implements ISettingsService {
    constructor() {
        super("api/settings");
    }

    getStadiumMainSettings(): Promise<StadiumMainSettingsDto> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/main`,
        })
    }

    updateStadiumMainSettings(command: UpdateStadiumMainSettingsCommand): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/main`,
            body: command,
            successMessage: t('common:success_request')
        })
    }
}