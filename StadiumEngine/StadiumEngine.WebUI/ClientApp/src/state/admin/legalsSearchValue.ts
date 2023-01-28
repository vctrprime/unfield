import { atom } from 'recoil';

const legalsSearchValue = atom({
    key: 'legalsSearchValue',
    default: localStorage.getItem('legalsSearchValue') || null as string | null
});

export { legalsSearchValue };