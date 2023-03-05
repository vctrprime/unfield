import {PriceGroupDto} from "../models/dto/rates/PriceGroupDto";
import {BaseService} from "./BaseService";
import {UpdatePriceGroupCommand} from "../models/command/rates/UpdatePriceGroupCommand";
import {AddPriceGroupCommand} from "../models/command/rates/AddPriceGroupCommand";
import {t} from "i18next";

export interface IRatesService {
    getPriceGroups(): Promise<PriceGroupDto[]>;

    getPriceGroup(id: number): Promise<PriceGroupDto>;

    updatePriceGroup(command: UpdatePriceGroupCommand): Promise<void>;

    addPriceGroup(command: AddPriceGroupCommand): Promise<void>;

    deletePriceGroup(priceGroupId: number): Promise<void>;
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
}