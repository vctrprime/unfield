import {atom} from 'recoil';

const languageAtom = atom({
    key: 'language',
    default: localStorage.getItem('language') || 'ru'
});

export {languageAtom};