// @ts-nocheck

import i18next from "i18next";
import { reactI18nextModule }  from "react-i18next";


import portalRU from '../i18n/portal/portal.json';
import portalEN from '../i18n/portal/portal.en.json';


import commonRU from '../i18n/common/common.json';
import commonEN from '../i18n/common/common.en.json';

import adminRU from '../i18n/admin/admin.json';
import adminEN from '../i18n/admin/admin.en.json';

import accountsRU from '../i18n/accounts/accounts.json';
import accountsEN from '../i18n/accounts/accounts.en.json';


const resources = {
    ru: {
        accounts: accountsRU,
        common: commonRU,
        portal: portalRU,
        admin: adminRU
    },
    en: {
        accounts: accountsEN,
        common: commonEN,
        portal: portalEN,
        admin: adminEN
    }
};

i18next
    .use(reactI18nextModule) // passes i18n down to react-i18next
    .init({
        fallbackLng: "ru",
        ns: ['portal', 'common','accounts', 'admin'],
        resources,
        lng: localStorage.getItem('language') || 'en',
        keySeparator: false, // we do not use keys in form messages.welcome
        interpolation: {
            escapeValue: false // react already safes from xss
        }
    });

export default i18next;