// @ts-nocheck

import i18next from "i18next";
import {reactI18nextModule} from "react-i18next";


import bookingRU from '../i18n/booking/booking.json';
import bookingEN from '../i18n/booking/booking.en.json';

import commonRU from '../i18n/common/common.json';
import commonEN from '../i18n/common/common.en.json';

import errorsRU from '../i18n/errors/errors.json';
import errorsEN from '../i18n/errors/errors.en.json';

import {LocaleOptions} from "react-semantic-ui-datepickers/dist/types";

import fnsRu from "date-fns/locale/ru";
import fnsEn from "date-fns/locale/en-US";


const resources = {
    ru: {
        booking: bookingRU,
        common: commonRU,
        errors: errorsRU,
    },
    en: {
        booking: bookingEN,
        common: commonEN,
        errors: errorsEN,
    }
};


i18next
    .use(reactI18nextModule) // passes i18n down to react-i18next
    .init({
        fallbackLng: "ru",
        ns: ['common', 'errors', 'booking'],
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

export const getDateFnsLocale = () : Locale => {
    switch (i18next.language) {
        case "en":
            return fnsEn;
        default:
            return fnsRu
    }
}

export default i18next;
