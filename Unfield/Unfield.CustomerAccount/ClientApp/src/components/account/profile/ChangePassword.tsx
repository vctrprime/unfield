import React, {useRef, useState} from "react";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";
import {Button, Form} from "semantic-ui-react";
import {t} from "i18next";

export const ChangePassword = () => {
    const [accountsService] = useInject<IAccountsService>('AccountsService');

    const [error, setError] = useState<string | null>(null);

    const oldPasswordInput = useRef<any>();
    const newPasswordInput = useRef<any>();
    const newPasswordAgainInput = useRef<any>();

    const validate = (): boolean => {
        let hasErrors = false;
        const inputs = [oldPasswordInput, newPasswordInput, newPasswordAgainInput];
        inputs.forEach((input) => {
            if (!input.current?.value) {
                input.current.style.border = "1px solid red";
                setTimeout(() => {
                    input.current.style.border = "";
                }, 2000);
                hasErrors = true;
            }
        })

        return !hasErrors;
    }

    const changePassword = () => {
        setError(null);
        if (validate()) {
            accountsService.changePassword({
                oldPassword: oldPasswordInput.current?.value || '',
                newPassword: newPasswordInput.current?.value || '',
                newPasswordRepeat: newPasswordAgainInput.current?.value || ''
            })
                .then(() => {
                    oldPasswordInput.current.value = null;
                    newPasswordInput.current.value = null;
                    newPasswordAgainInput.current.value = null;
                })
                .catch((error) => {
                    setError(error);
                })
        }
    }

    return (
        <div className="change-password-container">
            <p className="change-password-title">{t('accounts:change_password:title')}</p>
            <Form style={{textAlign: 'center'}}>
                <Form.Field>
                    <label>{t("accounts:change_password:old_password")}</label>
                    <input type="password" ref={oldPasswordInput}
                           placeholder={t("accounts:change_password:old_password") || ''}/>
                </Form.Field>
                <div className="change-password-description">{t('accounts:change_password:description')}</div>
                <Form.Field>
                    <label>{t("accounts:change_password:new_password")}</label>
                    <input type="password" ref={newPasswordInput}
                           placeholder={t("accounts:change_password:new_password") || ''}/>
                </Form.Field>
                <Form.Field>
                    <label>{t("accounts:change_password:new_password_again")}</label>
                    <input type="password" ref={newPasswordAgainInput}
                           placeholder={t("accounts:change_password:new_password_again") || ''}/>
                </Form.Field>
                {error !== null && <div className="error-message">{error}</div>}
                <Button style={{backgroundColor: '#3CB371', color: 'white'}}
                        onClick={changePassword}>{t('common:apply_button')}</Button>
            </Form>
        </div>
    )
}