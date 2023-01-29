import React, {useState} from 'react';
import {AuthorizedUserInfo} from "../common/AuthorizedUserInfo";
import {LanguageSelect} from "../common/LanguageSelect";
import logo from "../../img/logo/logo_icon_with_title.png";
import {t} from "i18next";
import {useSetRecoilState} from "recoil";
import {logoutModalAtom} from "../../state/logoutModal";
import {ProfileModal} from "../lk/accounts/ProfileModal";


export const Header = () => {
    const setLogoutModal = useSetRecoilState<boolean>(logoutModalAtom);

    const [profileModal, setProfileModal] = useState(false)
    
    return (
        <div className="border-bottom navbar navbar-light box-shadow lk-header">
            <ProfileModal open={profileModal} setOpen={setProfileModal}/>
            <div className="header-left-container">
                <img style={{height: '26px'}} className={"logo"} alt={"Stadium Engine"} src={logo}/>
                {/*<div className="panel-admin-title">{t("admin:header:panel_admin_title")}</div>*/}
            </div>
            <div className="header-right-container">
                <div className="header-icons">
                    
                        <LanguageSelect />
                    </div>
                <AuthorizedUserInfo setProfileModal={setProfileModal} withLegalName={false} />
                <i onClick={() => setLogoutModal(true)} title={t('common:lk_navbar:logout')||""} style={{marginLeft: "10px", cursor: "pointer"}} className="fa fa-power-off" />
            </div>
        </div>)
};