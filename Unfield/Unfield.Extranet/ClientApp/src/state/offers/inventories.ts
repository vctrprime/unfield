import {atom} from 'recoil';
import {InventoryDto} from "../../models/dto/offers/InventoryDto";

const inventoriesAtom = atom({
    key: 'inventories',
    default: [] as InventoryDto[]
});

export {inventoriesAtom};