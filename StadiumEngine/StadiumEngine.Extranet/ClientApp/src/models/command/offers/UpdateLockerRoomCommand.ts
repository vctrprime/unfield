import {LockerRoomGender} from "../../dto/offers/enums/LockerRoomGender";

export interface UpdateLockerRoomCommand {
    id: number,
    name: string,
    description?: string,
    gender: LockerRoomGender,
    isActive: boolean
}