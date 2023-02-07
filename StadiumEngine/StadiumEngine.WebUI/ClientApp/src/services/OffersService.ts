import {LockerRoomDto} from "../models/dto/offers/LockerRoomDto";
import {BaseService} from "./BaseService";

export interface IOffersService {
    getLockerRooms(): Promise<LockerRoomDto[]>;
    getLockerRoom(id:number): Promise<LockerRoomDto>;
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
}