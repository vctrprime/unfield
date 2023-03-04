import { atom } from 'recoil';
import {LockerRoomDto} from "../../models/dto/offers/LockerRoomDto";

const lockerRoomsAtom = atom({
    key: 'lockerRooms',
    default: [] as LockerRoomDto[]
});

export { lockerRoomsAtom };