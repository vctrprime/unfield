import React, {useEffect, useRef, useState} from "react";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";
import {useRecoilState, useRecoilValue} from "recoil";
import {authAtom} from "../../../state/auth";
import {t} from "i18next";
import {Button, Form} from "semantic-ui-react";
import {AuthorizeCustomerDto} from "../../../models/dto/accounts/AuthorizeCustomerDto";

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
                    <label>{t("accounts:customer:first_name")}</label>
                    <input type="text" ref={firstNameInput}
                           placeholder={t("accounts:customer:first_name") || ''}/>
                </Form.Field>
                <Form.Field>
                    <label>{t("accounts:customer:last_name")}</label>
                    <input type="text" ref={lastNameInput}
                           placeholder={t("accounts:customer:last_name") || ''} />
                </Form.Field>
                {error !== null && <div className="error-message">{error}</div>}
                <Button style={{backgroundColor: '#3CB371', color: 'white'}}
                        onClick={save}>{t('common:save_button')}</Button>
            </Form>
        </div>
    )
}