import ReactFlagsSelect from "react-flags-select";
import React from "react";
import i18n from "../../i18n/i18n";
import {useRecoilState, useSetRecoilState} from "recoil";
import {AuthorizeUserDto} from "../../models/dto/accounts/AuthorizeUserDto";
import {authAtom} from "../../state/auth";
import {loadingAtom} from "../../state/loading";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../services/AccountsService";

export const LanguageSelect = () => {
    const [auth, setAuth] = useRecoilState<AuthorizeUserDto | null>(authAtom);
    const setLoading = useSetRecoilState(loadingAtom);

    const [accountsService] = useInject<IAccountsService>('AccountsService');
    
    const customLabelsLanguages = {
        RU: "RU",
        US: "EN",
    };
    const getSelectedLanguage = ():string => {
        const language = auth?.language || 'en';
        if (language === 'en') return 'US';

        return language.toUpperCase();
    }
    const changeLanguage = (code: string) => {
        const currentLanguage = auth?.language || 'en';
        code = code === 'US' ? 'en' : code.toLowerCase();

        if (currentLanguage !== code) {
            accountsService.changeLanguage(code).then(() => {
                const user = Object.assign({}, auth);
                user.language = code;
                localStorage.setItem('user', JSON.stringify(user));
                setAuth(user);
                i18n.changeLanguage(user.language).then(() => setLoading(false));
            });
        }

    }
    
    
    return (
        <div className="language-container">
        <ReactFlagsSelect
            selected={getSelectedLanguage()}
            onSelect={changeLanguage}
            showSelectedLabel={true}
            customLabels={customLabelsLanguages}
            countries={["US", "RU"]}
        />
    </div>)
}