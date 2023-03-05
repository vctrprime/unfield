import {atom} from 'recoil';

const stadiumAtom = atom({
    key: 'currentStadium',
    default: null as number | null
});

export {stadiumAtom};