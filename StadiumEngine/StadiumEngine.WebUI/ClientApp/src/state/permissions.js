import { atom } from 'recoil';

const permissionsAtom = atom({
    key: 'permissions',
    default: []
});

export { permissionsAtom };