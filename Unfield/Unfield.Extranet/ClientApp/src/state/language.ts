import {atom} from 'recoil';
import {AuthorizeUserDto} from "../models/dto/accounts/AuthorizeUserDto";

const languageAtom = atom({
    key: 'language',
    default: localStorage.getItem('language') || 'ru'
});

export {languageAtom};