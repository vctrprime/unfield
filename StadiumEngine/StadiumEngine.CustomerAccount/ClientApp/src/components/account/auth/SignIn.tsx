import React, {useEffect, useState} from "react";
import {useNavigate, useParams, useSearchParams} from "react-router-dom";
import {useRecoilState, useSetRecoilState} from "recoil";
import {authAtom} from "../../../state/auth";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";
import {AuthorizeCustomerCommand} from "../../../models/command/accounts/AuthorizeCustomerCommand";
import {AuthorizeCustomerDto} from "../../../models/dto/accounts/AuthorizeCustomerDto";
import {t} from "i18next";
import {LanguageSelect} from "../../common/LanguageSelect";
import i18n from "../../../i18n/i18n";
import PhoneInput from 'react-phone-input-2'
import ru from 'react-phone-input-2/lang/ru.json'
import 'react-phone-input-2/lib/style.css'
import {getAuthUrl, getTitle} from "../../../helpers/utils";
import {stadiumAtom} from "../../../state/stadium";


export const SignIn = () => {
    const [searchParams, setSearchParams] = useSearchParams();
    const withoutName = searchParams.get('withoutName');
    const lng = searchParams.get('lng');
    const source = searchParams.get('source');

    const { stadiumToken } = useParams();
    
    const setAuth = useSetRecoilState(authAtom);
    const [accountsService] = useInject<IAccountsService>('AccountsService');
    const navigate = useNavigate();
    
    const [stadium, setStadium] = useRecoilState(stadiumAtom);
    const [login, setLogin] = useState<string | undefined>();

    useEffect(() => {
        if ( stadium == null ) {
            accountsService.getStadium().then((result) => {
                setStadium(result);
                document.title = getTitle(result.name)
            })
        }
        
        if (lng) {
            i18n.changeLanguage(lng).then(() => {
            });
        }
    }, []);

    const handleSubmit = (e: any) => {
        e.preventDefault();

        const {password} = document.forms[0];
        if (login !== undefined) {
            const command: AuthorizeCustomerCommand = {login, password: password.value};

            accountsService.login(command)
                .then((customer: AuthorizeCustomerDto) => {
                    localStorage.setItem('customer', JSON.stringify(customer));
                    setAuth(customer);

                    const localStorageLanguage = localStorage.getItem('language') || 'ru';

                    if (localStorageLanguage !== customer.language) {
                        accountsService.changeLanguage(localStorageLanguage).then(() => {
                        });
                    }
                    
                    if ( source === 'booking-form') {
                        if (stadium?.bookingFormHost) {
                            window.parent?.postMessage({}, stadium?.bookingFormHost);
                        }
                        else {
                            console.error('Invalid BookingFormHost');
                        }
                    }
                    else {
                        navigate(`/${stadiumToken}/bookings/future`);
                    }
                });
        }
    };
    
    return stadium ? <div className={"sign-in-container " + (source == "booking-form" ? "no-padding-top" : "")}>
        
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

                        <div className="reset-password-button" onClick={() => {
                            navigate(getAuthUrl(`/${stadiumToken}/reset-password`, withoutName, lng, source));
                        }}>{t('accounts:reset_password:title')}</div>
                        
                        <button
                            type="submit"
                            onClick={handleSubmit}
                        >
                            {t("accounts:sign_in:button")}
                        </button>
                        
                        <div className="register-button">
                            {t('accounts:register:title_one')} <span className="register-button-link" onClick={() => {
                            navigate(getAuthUrl(`/${stadiumToken}/register`, withoutName, lng, source));
                        }}>{t('accounts:register:title_two')}</span>
                        </div>
                    </div>
                </form>
            </div>
            {lng ? <span/> : <LanguageSelect alignOptionsToRight={false} withRequest={false}
                                             style={{position: 'absolute', top: 10, left: 20}}/>}
        </div>
        : <span/>;
}