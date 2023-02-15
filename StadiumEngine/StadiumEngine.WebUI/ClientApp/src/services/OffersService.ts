import {LockerRoomDto} from "../models/dto/offers/LockerRoomDto";
import {BaseService} from "./BaseService";
import {UpdateLockerRoomCommand} from "../models/command/offers/UpdateLockerRoomCommand";
import {t} from "i18next";
import {AddLockerRoomCommand} from "../models/command/offers/AddLockerRoomCommand";
import {FieldDto} from "../models/dto/offers/FieldDto";

export interface IOffersService {
    getLockerRooms(): Promise<LockerRoomDto[]>;
    getLockerRoom(id:number): Promise<LockerRoomDto>;
    updateLockerRoom(command: UpdateLockerRoomCommand): Promise<void>;
    addLockerRoom(command: AddLockerRoomCommand): Promise<void>;
    deleteLockerRoom(lockerRoomId: number): Promise<void>;

    getFields(): Promise<FieldDto[]>;
    getField(id:number): Promise<FieldDto>;
    updateField(command: FormData): Promise<void>;
    addField(command: FormData): Promise<void>;
    deleteField(fieldId: number): Promise<void>;
}

export class OffersService extends BaseService implements IOffersService {
    constructor() {
        super("api/offers");
    }

    getLockerRooms(): Promise<LockerRoomDto[]> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/locker-rooms`,
            withSpinner: false,
            hideSpinner: false
        })
    }

    getLockerRoom(id: number): Promise<LockerRoomDto> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/locker-rooms/${id}`,
        })
    }

    updateLockerRoom(command: UpdateLockerRoomCommand): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/locker-rooms`,
            body: command,
            successMessage: t('common:success_request')
        })
    }

    addLockerRoom(command: AddLockerRoomCommand): Promise<void> {
        return this.fetchWrapper.post({
            url: `${this.baseUrl}/locker-rooms`,
            body: command,
            successMessage: t('common:success_request')
        })
    }

    deleteLockerRoom(lockerRoomId: number): Promise<void> {
        return this.fetchWrapper.delete({
            url: `${this.baseUrl}/locker-rooms/${lockerRoomId}`,
            successMessage: t('common:success_request'),
        })
    }

    getFields(): Promise<FieldDto[]> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/fields`,
            withSpinner: false,
            hideSpinner: false
        })
    }

    getField(id: number): Promise<FieldDto> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/fields/${id}`,
        })
    }

    updateField(command: FormData): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/fields`,
            body: command,
            contentType: 'multipart/form-data',
            successMessage: t('common:success_request')
        })
    }

    addField(command: FormData): Promise<void> {
        return this.fetchWrapper.post({
            url: `${this.baseUrl}/locker-rooms`,
            body: command,
            contentType: 'multipart/form-data',
            successMessage: t('common:success_request')
        })
    }

    deleteField(fieldId: number): Promise<void> {
        return this.fetchWrapper.delete({
            url: `${this.baseUrl}/fields/${fieldId}`,
            successMessage: t('common:success_request'),
        })
    }
}