import React, {useEffect, useRef, useState} from "react";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";
import {useRecoilState, useRecoilValue} from "recoil";
import {authAtom} from "../../../state/auth";
import {t} from "i18next";
import {Button, Form} from "semantic-ui-react";
import {AuthorizeCustomerDto} from "../../../models/dto/accounts/AuthorizeCustomerDto";
import PhoneInput from "react-phone-input-2";
import i18n from "../../../i18n/i18n";
import ru from "react-phone-input-2/lang/ru.json";

export const Profile = () => {
    const [accountsService] = useInject<IAccountsService>('AccountsService');

    const [error, setError] = useState<string | null>(null);
    

    const [auth, setAuth] = useRecoilState(authAtom);

    const firstNameInput = useRef<any>();
    const lastNameInput = useRef<any>();

    useEffect(() => {
        if (auth) {
            firstNameInput.current.value = auth.firstName;
            lastNameInput.current.value = auth.lastName;
        }
    }, [auth]);

    const save = () => {
        setError(null);
        accountsService.update({
            firstName: firstNameInput.current?.value || '',
            lastName: lastNameInput.current?.value || ''
        })
            .then((result) => {
                const customer: AuthorizeCustomerDto = {
                    firstName: result.firstName,
                    lastName: result.lastName,
                    language: result.language,
                    phoneNumber: result.phoneNumber,
                    displayName: result.displayName,
                }
                setAuth(customer);
                localStorage.setItem('customer', JSON.stringify(customer));
            })
            .catch((error) => {
                setError(error);
            })
    }

    return (
        <div className="profile-container">
            <p className="profile-title">{t('accounts:customer:menu:profile')}</p>
            <Form style={{textAlign: 'center'}}>
                <Form.Field>
                    <label className="form-control-label">{t("accounts:sign_in:login")}</label>
                    <PhoneInput
                        onlyCountries={['ru']} // Перчень стран в поиске 
                        country='ru'
                        containerStyle={{marginTop: '10px', pointerEvents: 'none'}}
                        inputStyle={{width: '100%', height: 40, pointerEvents: 'none', paddingLeft: '48px'}}
                        placeholder={'+7 (123) 456-78-90'}
                        disableSearchIcon
                        value={auth?.phoneNumber}
                        localization={i18n.language === 'ru' ? ru : undefined}
                        countryCodeEditable={false}
                    />
                </Form.Field>
                <Form.Field>
                    <label>{t("accounts:customer:first_name")}</label>
                    <input type="text" ref={firstNameInput}
                           placeholder={t("accounts:customer:first_name") || ''}/>
                </Form.Field>
                <Form.Field>
                    <label>{t("accounts:customer:last_name")}</label>
                    <input type="text" ref={lastNameInput}
                           placeholder={t("accounts:customer:last_name") || ''}/>
                </Form.Field>
                {error !== null && <div className="error-message">{error}</div>}
                <Button style={{backgroundColor: '#3CB371', color: 'white'}}
                        onClick={save}>{t('common:save_button')}</Button>
            </Form>
        </div>
    )
}