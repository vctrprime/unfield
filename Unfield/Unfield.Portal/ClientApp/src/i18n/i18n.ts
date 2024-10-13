// @ts-nocheck

import i18next from "i18next";
import {reactI18nextModule} from "react-i18next";


import portalRU from '../i18n/portal/portal.json';
import portalEN from '../i18n/portal/portal.en.json';


const resources = {
    ru: {
        portal: portalRU
    },
    en: {
        portal: portalEN
    }
};


i18next
    .use(reactI18nextModule) // passes i18n down to react-i18next
    .init({
        fallbackLng: "ru",
        ns: ['portal'],
        resources,
        lng: localStorage.getItem('language') || 'ru',
        keySeparator: false, // we do not use keys in form messages.welcome
        interpolation: {
            escapeValue: false // react already safes from xss
        }
    });

interface Locales {
    ru: LocaleOptions;
    en: LocaleOptions
}

const locales : Locales = {
    ru: "ru-RU",
    en: "en-US"
}


export const getLocale = (): LocaleOptions => {
    return locales[i18next.language as keyof Locales];
}


export default i18next;
