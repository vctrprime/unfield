import {atom} from 'recoil';
import {EnvDataDto} from "../models/dto/EnvDataDto";

const envAtom = atom({
    key: 'envData',
    default: null as EnvDataDto | null
});

export {envAtom};