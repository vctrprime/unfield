import {LockerRoomDto} from "../models/dto/offers/LockerRoomDto";
import {BaseService} from "./BaseService";
import {UpdateLockerRoomCommand} from "../models/command/offers/UpdateLockerRoomCommand";
import {t} from "i18next";
import {AddLockerRoomCommand} from "../models/command/offers/AddLockerRoomCommand";

export interface IOffersService {
    getLockerRooms(): Promise<LockerRoomDto[]>;
    getLockerRoom(id:number): Promise<LockerRoomDto>;
    updateLockerRoom(command: UpdateLockerRoomCommand): Promise<void>;
    saveLockerRoom(command: AddLockerRoomCommand): Promise<void>;
    deleteLockerRoom(lockerRoomId: number): Promise<void>;
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

    saveLockerRoom(command: AddLockerRoomCommand): Promise<void> {
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
}