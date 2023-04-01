import React, {useEffect, useState} from 'react';
import {Dropdown} from "semantic-ui-react";
import {useRecoilState} from "recoil";
import {stadiumAtom} from "../../state/stadium";
import {UserStadiumDto} from "../../models/dto/accounts/UserStadiumDto";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../services/AccountsService";
import {t} from "i18next";
import {authAtom} from "../../state/auth";
import {AuthorizedUserInfo} from "../common/AuthorizedUserInfo";
import {LanguageSelect} from "../common/LanguageSelect";
import {useNavigate} from "react-router-dom";
import {AuthorizeUserDto} from "../../models/dto/accounts/AuthorizeUserDto";
import {ProfileModal} from "./accounts/ProfileModal";
import {HeaderNavigation} from "./common/HeaderNavigation";

interface StadiumDropDownData {
    key: number,
    value: number,
    text: string
}

export const Header = () => {
    const [stadiums, setStadiums] = useState<StadiumDropDownData[]>([])
    const [stadium, setStadium] = useRecoilState<number | null>(stadiumAtom);

    const [profileModal, setProfileModal] = useState(false)

    const [auth, setAuth] = useRecoilState(authAtom);

    const [accountsService] = useInject<IAccountsService>('AccountsService');

    const navigate = useNavigate();

    useEffect(() => {
        loadStadiums();
    }, []);

    const loadStadiums = () => {
        accountsService.getCurrentUserStadiums()
            .then((result: UserStadiumDto[]) => {
                setStadiums(result.map((s) => {
                    return {key: s.id, value: s.id, text: s.address}
                }));
                const currentStadium: UserStadiumDto | undefined = result.find(s => s.isCurrent);
                if (currentStadium !== undefined) {
                    setStadium(currentStadium.id);
                }

            })
    }

    const changeStadium = (e: any, {value}: any) => {
        accountsService.changeCurrentStadium(value)
            .then((result: AuthorizeUserDto) => {
                setAuth(result);
                setStadium(value);
                localStorage.setItem('user', JSON.stringify(result));
                localStorage.setItem('language', result.language);
            })
    }

    const toAdmin = () => {
        navigate("/admin")
    }

    const routesWithBackButton = ["locker-rooms", "fields", "inventories", "price-groups", "tariffs"];

    const routeWithoutStadiumList = (): boolean => {
        return routesWithBackButton.filter(r => window.location.pathname.indexOf(r + "/") !== -1).length > 0;
    }

    return (
        <div className="border-bottom navbar navbar-light box-shadow lk-header">
            <ProfileModal open={profileModal} setOpen={setProfileModal}/>
            {stadium !== null &&
                <div className="stadium-list">
                    <div style={routeWithoutStadiumList() || window.location.pathname.startsWith("/lk/accounts") ? {display: "none"} : {}}>
                        {t("common:lk_header:current_stadium_title")}:  &nbsp;
                        <Dropdown
                            onChange={changeStadium}
                            inline
                            options={stadiums}
                            defaultValue={stadium}
                        />
                    </div>
                    <div className="header-warning"
                         style={window.location.pathname.startsWith("/lk/accounts") ? {} : {display: "none"}}>
                        <i className="fa fa-exclamation-circle" aria-hidden="true"/>
                        <div className="header-warning-text">
                            <span
                                title={t("accounts:header_notification_line1") || ''}>{t("accounts:header_notification_line1")}</span>
                            <span
                                title={t("accounts:header_notification_line2") || ''}>{t("accounts:header_notification_line2")}</span>
                        </div>
                    </div>
                    {routeWithoutStadiumList() && <HeaderNavigation routesWithBackButton={routesWithBackButton}/>}
                </div>}

            {stadium !== null && <div className="header-right-container">
                <div className="header-icons">
                    <LanguageSelect/>
                </div>
                <AuthorizedUserInfo setProfileModal={setProfileModal}/>
                {auth !== null && auth.isAdmin &&
                    <i onClick={toAdmin} title={t('common:lk_header:to_admin_title') || ""}
                       style={{marginLeft: "10px", cursor: "pointer"}} className="fa fa-font"/>}
            </div>}
        </div>)
};