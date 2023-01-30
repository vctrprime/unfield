import { atom } from 'recoil';

const legalsSearchValue = atom({
    key: 'legalsSearchValue',
    default: localStorage.getItem('legalsSearchValue') || '' as string
});

export { legalsSearchValue };