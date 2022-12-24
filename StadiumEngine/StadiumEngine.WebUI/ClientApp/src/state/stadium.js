import { atom } from 'recoil';

const stadiumAtom = atom({
    key: 'currentStadium',
    default: null
});

export { stadiumAtom };