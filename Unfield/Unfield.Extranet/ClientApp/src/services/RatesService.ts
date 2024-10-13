import {PriceGroupDto} from "../models/dto/rates/PriceGroupDto";
import {BaseService} from "./BaseService";
import {UpdatePriceGroupCommand} from "../models/command/rates/UpdatePriceGroupCommand";
import {AddPriceGroupCommand} from "../models/command/rates/AddPriceGroupCommand";
import {t} from "i18next";
import {TariffDto} from "../models/dto/rates/TariffDto";
import {UpdateTariffCommand} from "../models/command/rates/UpdateTariffCommand";
import {AddTariffCommand} from "../models/command/rates/AddTariffCommand";
import {PriceDto} from "../models/dto/rates/PriceDto";
import {SetPricesCommand} from "../models/command/rates/SetPricesCommand";

export interface IRatesService {
    getPriceGroups(): Promise<PriceGroupDto[]>;

    getPriceGroup(id: number): Promise<PriceGroupDto>;

    updatePriceGroup(command: UpdatePriceGroupCommand): Promise<void>;

    addPriceGroup(command: AddPriceGroupCommand): Promise<void>;

    deletePriceGroup(priceGroupId: number): Promise<void>;

    getTariffs(): Promise<TariffDto[]>;

    getTariff(id: number): Promise<TariffDto>;

    updateTariff(command: UpdateTariffCommand): Promise<void>;

    addTariff(command: AddTariffCommand): Promise<void>;

    deleteTariff(tariffId: number): Promise<void>;
    
    getPrices(): Promise<PriceDto[]>;
    
    setPrices(command: SetPricesCommand): Promise<void>;
}

export class RatesService extends BaseService implements IRatesService {
    constructor() {
        super("api/rates");
    }

    getPriceGroups(): Promise<PriceGroupDto[]> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/price-groups`,
            withSpinner: false,
            hideSpinner: false
        })
    }

    getPriceGroup(id: number): Promise<PriceGroupDto> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/price-groups/${id}`,
        })
    }

    updatePriceGroup(command: UpdatePriceGroupCommand): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/price-groups`,
            body: command,
            successMessage: t('common:success_request')
        })
    }

    addPriceGroup(command: AddPriceGroupCommand): Promise<void> {
        return this.fetchWrapper.post({
            url: `${this.baseUrl}/price-groups`,
            body: command,
            successMessage: t('common:success_request')
        })
    }

    deletePriceGroup(priceGroupId: number): Promise<void> {
        return this.fetchWrapper.delete({
            url: `${this.baseUrl}/price-groups/${priceGroupId}`,
            successMessage: t('common:success_request'),
        })
    }

    getTariffs(): Promise<TariffDto[]> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/tariffs`,
            withSpinner: false,
            hideSpinner: false
        })
    }

    getTariff(id: number): Promise<TariffDto> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/tariffs/${id}`,
        })
    }

    updateTariff(command: UpdateTariffCommand): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/tariffs`,
            body: command,
            successMessage: t('common:success_request')
        })
    }

    addTariff(command: AddTariffCommand): Promise<void> {
        return this.fetchWrapper.post({
            url: `${this.baseUrl}/tariffs`,
            body: command,
            successMessage: t('common:success_request')
        })
    }

    deleteTariff(tariffId: number): Promise<void> {
        return this.fetchWrapper.delete({
            url: `${this.baseUrl}/tariffs/${tariffId}`,
            successMessage: t('common:success_request'),
        })
    }

    getPrices(): Promise<PriceDto[]> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/prices`,
            withSpinner: false,
            hideSpinner: false
        })
    }

    setPrices(command: SetPricesCommand): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/prices`,
            body: command,
            successMessage: t('common:success_request')
        })
    }
}