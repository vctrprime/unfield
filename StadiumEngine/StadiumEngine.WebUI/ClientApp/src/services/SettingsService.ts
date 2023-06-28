import {MainSettingsDto} from "../models/dto/settings/MainSettingsDto";
import {UpdateMainSettingsCommand} from "../models/command/settings/UpdateMainSettingsCommand";
import {BaseService} from "./BaseService";
import {t} from "i18next";
import {BreakDto} from "../models/dto/settings/BreakDto";
import {AddBreakCommand} from "../models/command/settings/AddBreakCommand";
import {UpdateBreakCommand} from "../models/command/settings/UpdateBreakCommand";

export interface ISettingsService {
    getMainSettings(): Promise<MainSettingsDto>;
    updateMainSettings(command: UpdateMainSettingsCommand): Promise<void>;
    
    getBreaks(): Promise<BreakDto[]>;
    getBreak(id: number): Promise<BreakDto>;
    updateBreak(command: UpdateBreakCommand): Promise<void>;
    addBreak(command: AddBreakCommand): Promise<void>;
    deleteBreak(breakId: number): Promise<void>;
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

    getBreak(id: number): Promise<BreakDto> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/breaks/${id}`,
            hideSpinner: false
        })
    }

    updateBreak(command: UpdateBreakCommand): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/breaks`,
            body: command,
            successMessage: t('common:success_request')
        })
    }

    addBreak(command: AddBreakCommand): Promise<void> {
        return this.fetchWrapper.post({
            url: `${this.baseUrl}/breaks`,
            body: command,
            successMessage: t('common:success_request')
        })
    }

    deleteBreak(breakId: number): Promise<void> {
        return this.fetchWrapper.delete({
            url: `${this.baseUrl}/breaks/${breakId}`,
            successMessage: t('common:success_request'),
        })
    }
}