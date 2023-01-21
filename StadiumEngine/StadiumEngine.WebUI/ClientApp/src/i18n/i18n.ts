// @ts-nocheck

import i18next from "i18next";
import { reactI18nextModule }  from "react-i18next";

import portalEN from '../i18n/portal/portal.en.json';
import portalRU from '../i18n/portal/portal.ru.json';

import commonEN from '../i18n/common/common.en.json';
import commonRU from '../i18n/common/common.ru.json';

import accountsEN from '../i18n/accounts/accounts.en.json';
import accountsRU from '../i18n/accounts/accounts.ru.json';

const resources = {
    en: {
        accounts: accountsEN,
        common: commonEN,
        portal: portalEN
    },
    ru: {
        accounts: accountsRU,
        common: commonRU,
        portal: portalRU
    }
};

const user = JSON.parse(localStorage.getItem('user'));
const startLanguage = user === undefined ? "en" : 
    user.language === undefined ||  user.language === null ? "en" :
        user.language;


i18next
    .use(reactI18nextModule) // passes i18n down to react-i18next
    .init({
        fallbackLng: "ru",
        ns: ['portal', 'common','accounts'],
        resources,
        lng: startLanguage,
        keySeparator: false, // we do not use keys in form messages.welcome
        interpolation: {
            escapeValue: false // react already safes from xss
        }
    });

export default i18next;
