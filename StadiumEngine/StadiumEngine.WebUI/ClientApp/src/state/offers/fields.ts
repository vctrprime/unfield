import {atom} from 'recoil';
import {FieldDto} from "../../models/dto/offers/FieldDto";

const fieldsAtom = atom({
    key: 'fields',
    default: [] as FieldDto[]
});

export {fieldsAtom};