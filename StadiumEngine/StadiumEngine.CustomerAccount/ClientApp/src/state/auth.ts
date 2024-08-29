import {atom} from 'recoil';
import {AuthorizeCustomerDto} from "../models/dto/accounts/AuthorizeCustomerDto";

const authAtom = atom({
    key: 'auth',
    // get initial state from local storage to enable user to stay logged in
    default: JSON.parse(localStorage.getItem('user') || '{}') as AuthorizeCustomerDto | null
});

export {authAtom};