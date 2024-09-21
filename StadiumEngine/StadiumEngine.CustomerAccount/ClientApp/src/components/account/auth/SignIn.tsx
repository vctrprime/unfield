import React, {useEffect, useState} from "react";
import {useNavigate, useParams, useSearchParams} from "react-router-dom";
import {useSetRecoilState} from "recoil";
import {authAtom} from "../../../state/auth";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";
import {StadiumDto} from "../../../models/dto/accounts/StadiumDto";
import {AuthorizeCustomerCommand} from "../../../models/command/accounts/AuthorizeCustomerCommand";
import {AuthorizeCustomerDto} from "../../../models/dto/accounts/AuthorizeCustomerDto";
import {t} from "i18next";
import {LanguageSelect} from "../../common/LanguageSelect";
import i18n from "../../../i18n/i18n";
import PhoneInput from 'react-phone-input-2'
import ru from 'react-phone-input-2/lang/ru.json'
import 'react-phone-input-2/lib/style.css'
import {getTitle} from "../../../helpers/utils";


export const SignIn = () => {
    const [searchParams, setSearchParams] = useSearchParams();
    const withoutName = searchParams.get('withoutName');

    const { stadiumToken } = useParams();
    
    const setAuth = useSetRecoilState(authAtom);
    const [accountsService] = useInject<IAccountsService>('AccountsService');
    const navigate = useNavigate();
    
    const [stadium, setStadium] = useState<StadiumDto|null>(null);
    const [login, setLogin] = useState<string | undefined>();

    useEffect(() => {
        accountsService.getStadium().then((result) => {
            setStadium(result);
            document.title = getTitle(result.name)
        })
    }, []);

    const handleSubmit = (e: any) => {
        e.preventDefault();

        const {password} = document.forms[0];
        if (login !== undefined) {
            const command: AuthorizeCustomerCommand = {login, password: password.value};

            accountsService.authorize(command)
                .then((customer: AuthorizeCustomerDto) => {
                    localStorage.setItem('customer', JSON.stringify(customer));
                    setAuth(customer);

                    const localStorageLanguage = localStorage.getItem('language') || 'ru';

                    if (localStorageLanguage !== customer.language) {
                        accountsService.changeLanguage(localStorageLanguage).then(() => {
                        });
                    }

                    navigate(`/${stadiumToken}/bookings/future`);
                });
        }
    };
    
    return stadium ? <div className="sign-in-container">
        
            <div style={{
                display: 'flex',
                flexDirection: 'column',
                width: '100%',
                justifyContent: 'center',
                alignItems: 'center'
            }}>
                {withoutName ? '' : <div className="stadium-name">{t("accounts:sign_in:name_prefix")} {stadium.name}</div>}
                <form>
                    <div className="form-group">
                        <label className="form-control-label">{t("accounts:sign_in:login")}</label>
                        <PhoneInput
                            onlyCountries={['ru']} // Перчень стран в поиске 
                            country='ru'
                            containerStyle={{marginTop: '10px'}}
                            inputStyle={{width: '100%', height: 40}}
                            searchClass='search-class'
                            searchStyle={{margin: 0, width: '90%', height: '30px', fontFamily: 'inherit'}}
                            enableSearch={false}
                            placeholder={'+7 (123) 456-78-90'}
                            disableSearchIcon
                            value={login}
                            localization={i18n.language === 'ru' ? ru : undefined}
                            countryCodeEditable={false}
                            onChange={(phone) => setLogin(phone)}
                        />
                    </div>

                    <div className="form-group">
                        <label className="form-control-label">{t("accounts:sign_in:password")}</label>
                        <input
                            className="form-control password-input"
                            type="password"
                            name="password"
                        />

                        <div className="reset-button-modal" onClick={() => {
                            navigate(`/${stadiumToken}/reset-password?withoutName=${withoutName}`)
                        }}>{t('accounts:reset_password:title')}</div>
                        <button
                            type="submit"
                            onClick={handleSubmit}
                        >
                            {t("accounts:sign_in:button")}
                        </button>
                    </div>
                </form>
            </div>
            <LanguageSelect alignOptionsToRight={false} withRequest={false} style={{position: 'absolute', top: 10, left: 20}}/>
        </div>
        : <span/>;
}