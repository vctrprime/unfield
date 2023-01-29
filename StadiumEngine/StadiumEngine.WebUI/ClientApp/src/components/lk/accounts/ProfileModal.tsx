import {Button, Modal} from "semantic-ui-react";
import {t} from "i18next";
import React from "react";
import {useRecoilValue} from "recoil";
import {authAtom} from "../../../state/auth";
import {ChangePasswordForm} from "./ChangePasswordForm";

export const ProfileModal = ({open, setOpen} : any) => {
    const auth = useRecoilValue(authAtom);

    
    return (auth !== null ? <Modal
        dimmer='blurring'
        size='small'
        open={open}>
        <Modal.Header>{t('accounts:profile:title')}</Modal.Header>
        <Modal.Content className="profile-container">
            <div className="profile-row">{t('accounts:profile:full_name')}: <span>{auth.fullName}</span>
                {auth.isAdmin &&
                    <sup><i title={t('accounts:profile:admin_title')||""} className="fa fa-font" /></sup>
                }
            </div>
            <div className="profile-row">{t('accounts:profile:legal_name')}: <span>{auth.legalName}</span>
                   {auth.isSuperuser &&
                        <sup><i title={t('accounts:profile:superuser_title')||""} className="fa fa-star" /></sup>
                    }
            </div>
            {!auth.isSuperuser &&
                <div className="profile-row">{t('accounts:profile:role_name')}: <span>{auth.roleName}</span></div>
            }
            <ChangePasswordForm setOpen={setOpen}/>
            
            
        </Modal.Content>
        <Modal.Actions>
            <Button onClick={() => setOpen(false)}>{t('common:close_button')}</Button>
        </Modal.Actions>
    </Modal> : <span/>)
}