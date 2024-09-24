import {ContainerLoading} from "../../common/ContainerLoading";
import {t} from "i18next";
import {Button, Form} from "semantic-ui-react";
import i18n from "../../../i18n/i18n";
import React, {useEffect, useState} from "react";
import {LanguageSelect} from "../../common/LanguageSelect";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";
import PhoneInput from 'react-phone-input-2'
import ru from 'react-phone-input-2/lang/ru.json'
import 'react-phone-input-2/lib/style.css'
import {useNavigate, useParams, useSearchParams} from "react-router-dom";
import {getAuthUrl, getTitle} from "../../../helpers/utils";
import {StadiumDto} from "../../../models/dto/accounts/StadiumDto";
import {useRecoilState} from "recoil";
import {stadiumAtom} from "../../../state/stadium";

export const ResetPassword = () => {
    let { stadiumToken } = useParams();

    const [searchParams, setSearchParams] = useSearchParams();
    const withoutName = searchParams.get('withoutName');
    const lng = searchParams.get('lng');
    const source = searchParams.get('source');
    
    const navigate = useNavigate();
    const [accountsService] = useInject<IAccountsService>('AccountsService');
    
    const [resetPasswordPhoneNumber, setResetPasswordPhoneNumber] = useState<string | undefined>();
    const [resetPasswordLoading, setResetPasswordLoading] = useState<boolean>(false)

    const [resetPasswordError, setResetPasswordError] = useState<string | null>(null);

    const [stadium, setStadium] = useRecoilState(stadiumAtom);
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

    const resetPassword = () => {
        setResetPasswordLoading(true);
        setResetPasswordError(null);
        accountsService.resetPassword({
            phoneNumber: resetPasswordPhoneNumber || ''
        }).then(() => {
            navigate(`/${stadiumToken}/sign-in?withoutName=${withoutName}&lng=${lng}&source=${source}`);
        })
            .catch((error) => {
                setResetPasswordError(error);
            })
            .finally(() => setResetPasswordLoading(false))
    }
    
    return <div className={"reset-password-container " + (source == "booking-form" ? "no-padding-top" : "")}>
        <ContainerLoading show={resetPasswordLoading}/>
        {withoutName && stadium ? '' : <div className="stadium-name">{t("accounts:sign_in:name_prefix")} {stadium?.name}</div>}
        
        <p className="reset-password-description">{t('accounts:reset_password:description')}</p>
        <Form style={{width: '100%', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>
            <PhoneInput
                onlyCountries={['ru']}
                country='ru'
                containerStyle={{width: '200px'}}
                inputStyle={{width: '100%', height: 38, paddingLeft: '42px', fontFamily: 'inherit'}}
                placeholder={'+7 (123) 456-78-90'}
                value={resetPasswordPhoneNumber}
                onChange={(phone) => setResetPasswordPhoneNumber(phone)}
                localization={i18n.language === 'ru' ? ru : undefined}
                countryCodeEditable={false}
            />
        </Form>
        {resetPasswordError !== null && <div className="error-message">{resetPasswordError}</div>}
        <div className="reset-password-buttons">
            <button
                type="submit"
                onClick={resetPassword}
            >
                {t('accounts:reset_password:button')}
            </button>
        </div>
        <div className="back-to-login" onClick={() => {
            navigate(getAuthUrl(`/${stadiumToken}/sign-in`, withoutName, lng, source));
        }}>{t('accounts:reset_password:back_to_login')}</div>

        {lng ? <span/> : <LanguageSelect alignOptionsToRight={false} withRequest={false}
                                         style={{position: 'absolute', top: 10, left: 20}}/>}
    </div>
}