import { atom } from 'recoil';

const rolesAtom = atom({
    key: 'roles',
    default: []
});

export { rolesAtom };