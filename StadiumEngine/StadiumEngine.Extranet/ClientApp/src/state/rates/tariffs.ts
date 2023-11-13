import {atom} from 'recoil';
import {TariffDto} from "../../models/dto/rates/TariffDto";

const tariffsAtom = atom({
    key: 'tariffs',
    default: [] as TariffDto[]
});

export {tariffsAtom};