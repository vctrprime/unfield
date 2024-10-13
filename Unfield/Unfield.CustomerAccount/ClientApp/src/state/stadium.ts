import {atom} from 'recoil';
import {StadiumDto} from "../models/dto/accounts/StadiumDto";

const stadiumAtom = atom({
    key: 'currentStadium',
    default: null as StadiumDto | null
});

export {stadiumAtom};