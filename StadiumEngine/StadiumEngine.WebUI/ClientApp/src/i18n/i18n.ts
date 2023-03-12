// @ts-nocheck

import i18next from "i18next";
import {reactI18nextModule} from "react-i18next";


import portalRU from '../i18n/portal/portal.json';
import portalEN from '../i18n/portal/portal.en.json';


import commonRU from '../i18n/common/common.json';
import commonEN from '../i18n/common/common.en.json';

import adminRU from '../i18n/admin/admin.json';
import adminEN from '../i18n/admin/admin.en.json';

import accountsRU from '../i18n/accounts/accounts.json';
import accountsEN from '../i18n/accounts/accounts.en.json';

import errorsRU from '../i18n/errors/errors.json';
import errorsEN from '../i18n/errors/errors.en.json';

import offersRU from '../i18n/offers/offers.json';
import offersEN from '../i18n/offers/offers.en.json';

import ratesRU from '../i18n/rates/rates.json';
import ratesEN from '../i18n/rates/rates.en.json';

import settingsRU from '../i18n/settings/settings.json';
import settingsEN from '../i18n/settings/settings.en.json';

import {LocaleOptions} from "react-semantic-ui-datepickers/dist/types";


const resources = {
    ru: {
        accounts: accountsRU,
        common: commonRU,
        portal: portalRU,
        admin: adminRU,
        errors: errorsRU,
        offers: offersRU,
        rates: ratesRU,
        settings: settingsRU
    },
    en: {
        accounts: accountsEN,
        common: commonEN,
        portal: portalEN,
        admin: adminEN,
        errors: errorsEN,
        offers: offersEN,
        rates: ratesEN,
        settings: settingsEN
    }
};


i18next
    .use(reactI18nextModule) // passes i18n down to react-i18next
    .init({
        fallbackLng: "ru",
        ns: ['portal', 'common', 'accounts', 'admin', 'errors', 'offers', 'rates', 'settings'],
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
