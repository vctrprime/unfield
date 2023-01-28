import { atom } from 'recoil';

const changeBindingStadiumAtom = atom({
    key: 'changeBindingStadium',
    default: null as boolean | null
});

export { changeBindingStadiumAtom };