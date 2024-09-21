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
import {getTitle} from "../../../helpers/utils";
import {StadiumDto} from "../../../models/dto/accounts/StadiumDto";

export const ResetPassword = () => {
    let { stadiumToken } = useParams();

    const [searchParams, setSearchParams] = useSearchParams();
    const withoutName = searchParams.get('withoutName');
    
    const navigate = useNavigate();
    const [accountsService] = useInject<IAccountsService>('AccountsService');
    
    const [resetPasswordPhoneNumber, setResetPasswordPhoneNumber] = useState<string | undefined>();
    const [resetPasswordLoading, setResetPasswordLoading] = useState<boolean>(false)

    const [resetPasswordError, setResetPasswordError] = useState<string | null>(null);

    const [stadium, setStadium] = useState<StadiumDto|null>(null);
    useEffect(() => {
        accountsService.getStadium().then((result) => {
            setStadium(result);
            document.title = getTitle(result.name)
        })
    }, []);

    const resetPassword = () => {
        setResetPasswordLoading(true);
        setResetPasswordError(null);
        accountsService.resetPassword({
            phoneNumber: resetPasswordPhoneNumber || ''
        }).then(() => {
            navigate(`/${stadiumToken}/sign-in?withoutName=${withoutName}`);
        })
            .catch((error) => {
                setResetPasswordError(error);
            })
            .finally(() => setResetPasswordLoading(false))
    }
    
    return <div className="reset-password-container">
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

        <div className="reset-password-buttons">
            <Button style={{backgroundColor: '#3CB371', color: 'white'}}
                    onClick={resetPassword}>{t('accounts:reset_password:button')}</Button>
        </div>
        <div className="back-to-login" onClick={() => {
            navigate(`/${stadiumToken}/sign-in?withoutName=${withoutName}`)
        }}>{t('accounts:reset_password:back_to_login')}</div>

        <LanguageSelect alignOptionsToRight={false} withRequest={false}
                        style={{position: 'absolute', top: 10, left: 20}}/>
    </div>
}