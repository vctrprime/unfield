import React, {useEffect, useState} from "react";
import {StadiumDto} from "../../../models/dto/accounts/StadiumDto";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";
import {useNavigate, useParams, useSearchParams} from "react-router-dom";
import {getTitle} from "../../../helpers/utils";
import {t} from "i18next";
import PhoneInput from "react-phone-input-2";
import i18n from "../../../i18n/i18n";
import ru from "react-phone-input-2/lang/ru.json";
import {LanguageSelect} from "../../common/LanguageSelect";
import {RegisterCustomerCommand} from "../../../models/command/accounts/RegisterCustomerCommand";

export const Register = () => {
    const [stadium, setStadium] = useState<StadiumDto|null>(null);

    const [searchParams, setSearchParams] = useSearchParams();
    const withoutName = searchParams.get('withoutName');
    const lng = searchParams.get('lng');
    const source = searchParams.get('source');

    const { stadiumToken } = useParams();

    const [accountsService] = useInject<IAccountsService>('AccountsService');
    const navigate = useNavigate();

    const [login, setLogin] = useState<string | undefined>();

    useEffect(() => {
        accountsService.getStadium().then((result) => {
            setStadium(result);
            document.title = getTitle(result.name)

            if (lng) {
                i18n.changeLanguage(lng).then(() => {
                });
            }
        })
    }, []);

    const handleSubmit = (e: any) => {
        e.preventDefault();

        const {firstName, lastName} = document.forms[0];
        if (login !== undefined) {
            const command: RegisterCustomerCommand =  {
                phoneNumber: login,
                firstName: firstName.value,
                lastName: lastName.value,
                language:  localStorage.getItem('language') || 'ru'
            };

            accountsService.register(command)
                .then(() => {
                    navigate(`/${stadiumToken}/sign-in?withoutName=${withoutName}&lng=${lng}&source=${source}`);
                });
        }
    };

    return stadium ? <div className={"register-container " + (source == "booking-form" ? "no-padding-top" : "")}>

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
                        <label className="form-control-label">{t("accounts:register:login")}*</label>
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
                        <label className="form-control-label">{t("accounts:register:first_name")}</label>
                        <input
                            className="form-control text-input"
                            type="text"
                            name="firstName"
                        />
                    </div>

                    <div className="form-group">
                        <label className="form-control-label">{t("accounts:register:last_name")}</label>
                        <input
                            className="form-control  text-input"
                            type="text"
                            name="lastName"
                        />
                        <button
                            type="submit"
                            onClick={handleSubmit}
                        >
                            {t("accounts:register:button")}
                        </button>
                    </div>
                </form>
            </div>

            <div style={{marginTop: 0}} className="back-to-login" onClick={() => {
                navigate(`/${stadiumToken}/sign-in?withoutName=${withoutName}&lng=${lng}&source=${source}`)
            }}>{t('accounts:reset_password:back_to_login')}</div>

        {lng ? <span/> : <LanguageSelect alignOptionsToRight={false} withRequest={false}
                            style={{position: 'absolute', top: 10, left: 20}}/>}
        </div>
        : <span/>;
} 