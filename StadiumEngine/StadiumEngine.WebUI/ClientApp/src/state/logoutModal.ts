import {atom} from 'recoil';

const logoutModalAtom = atom({
    key: 'logoutModal',
    default: false
});

export {logoutModalAtom};