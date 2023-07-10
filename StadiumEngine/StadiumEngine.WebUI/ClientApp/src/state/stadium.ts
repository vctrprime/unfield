import {atom} from 'recoil';
import {UserStadiumDto} from "../models/dto/accounts/UserStadiumDto";

const stadiumAtom = atom({
    key: 'currentStadium',
    default: null as UserStadiumDto | null
});

export {stadiumAtom};