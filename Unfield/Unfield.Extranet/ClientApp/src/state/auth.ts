import {atom} from 'recoil';
import {AuthorizeUserDto} from "../models/dto/accounts/AuthorizeUserDto";

const authAtom = atom({
    key: 'auth',
    // get initial state from local storage to enable user to stay logged in
    default: JSON.parse(localStorage.getItem('user') || '{}') as AuthorizeUserDto | null
});

export {authAtom};