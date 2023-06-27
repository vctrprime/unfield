import {atom} from 'recoil';
import {BreakDto} from "../../models/dto/settings/BreakDto";

const breaksAtom = atom({
    key: 'breaks',
    default: [] as BreakDto[]
});

export {breaksAtom};