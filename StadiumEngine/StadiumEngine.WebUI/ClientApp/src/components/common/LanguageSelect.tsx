import ReactFlagsSelect from "react-flags-select";
import React from "react";
import i18n from "../../i18n/i18n";
import {useSetRecoilState} from "recoil";
import {loadingAtom} from "../../state/loading";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../services/AccountsService";

export const LanguageSelect = ({ withRequest = true, style} : any) => {
    const setLoading = useSetRecoilState(loadingAtom);

    const [accountsService] = useInject<IAccountsService>('AccountsService');
    
    const customLabelsLanguages = {
        RU: "Русский",
        US: "English",
    };
    const getSelectedLanguage = ():string => {
        const language = localStorage.getItem('language') || 'ru';
        if (language === 'en') return 'US';

        return language.toUpperCase();
    }
    
    const changeLanguage = (code: string) => {
        const currentLanguage = localStorage.getItem('language') || 'ru';
        code = code === 'US' ? 'en' : code.toLowerCase();

        if (currentLanguage !== code) {
            localStorage.setItem('language', code);
            if (withRequest) {
                accountsService.changeLanguage(code).then(() => {
                    i18n.changeLanguage(code).then(() => {
                        setLoading(false);
                    });
                });
            }
            else {
                i18n.changeLanguage(code).then(() => {});
            }
        }

    }
    
    
    return (
        <div className="language-container" style={style === undefined ? { marginRight: '0'} : style}>
        <ReactFlagsSelect
            selected={getSelectedLanguage()}
            onSelect={changeLanguage}
            showSelectedLabel={false}
            customLabels={customLabelsLanguages}
            fullWidth={false}
            countries={["US", "RU"]}
        />
    </div>)
}