import { atom } from 'recoil';
import {PriceGroupDto} from "../../models/dto/rates/PriceGroupDto";

const priceGroupsAtom = atom({
    key: 'priceGroups',
    default: [] as PriceGroupDto[]
});

export { priceGroupsAtom };