import {LockerRoomGender} from "../../dto/offers/enums/LockerRoomGender";

export interface AddLockerRoomCommand {
    name: string,
    description?: string,
    gender: LockerRoomGender,
    isActive: boolean
}