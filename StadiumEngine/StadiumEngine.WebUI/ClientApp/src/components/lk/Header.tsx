import React, {useEffect, useState} from 'react';
import {Bell, GearFill, PersonCircle} from 'react-bootstrap-icons';
import {Dropdown} from "semantic-ui-react";
import {useRecoilState, useRecoilValue, useSetRecoilState} from "recoil";
import {stadiumAtom} from "../../state/stadium";
import {UserStadiumDto} from "../../models/dto/accounts/UserStadiumDto";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../services/AccountsService";
import {t} from "i18next";
import ReactFlagsSelect from "react-flags-select";
import {authAtom} from "../../state/auth";
import i18n from "../../i18n/i18n";
import {AuthorizeUserDto} from "../../models/dto/accounts/AuthorizeUserDto";
import {loadingAtom} from "../../state/loading";

interface StadiumDropDownData {
    key: number,
    value: number,
    text: string
}

export const Header = () => {
    const [stadiums, setStadiums] = useState<StadiumDropDownData[]>([])
    const [stadium, setStadium] = useRecoilState<number | null>(stadiumAtom);

    const [auth, setAuth] = useRecoilState<AuthorizeUserDto | null>(authAtom);
    const setLoading = useSetRecoilState(loadingAtom);

    const [accountsService] = useInject<IAccountsService>('AccountsService');
    
    useEffect(() => {
        loadStadiums();
    }, []);

    const loadStadiums = () => {
        accountsService.getCurrentUserStadiums()
            .then((result: UserStadiumDto[]) => {
                setStadiums(result.map((s) => {
                    return { key: s.id, value: s.id, text: s.address }
                }));
                const currentStadium: UserStadiumDto|undefined = result.find(s => s.isCurrent);
                if (currentStadium !== undefined) {
                    setStadium(currentStadium.id);
                }
                
        })
    }


    const changeStadium = (e : any, { value }: any) => {
        accountsService.changeCurrentStadium(value)
            .then(() => {
            setStadium(value);
        })
    }

    const customLabelsLanguages = {
        RU: "RU",
        US: "EN",
    };
    const getSelectedLanguage = ():string => {
        const language = auth?.language || 'en';
        if (language === 'en') return 'US';
        
        return language.toUpperCase();
    }
    const changeLanguage = (code: string) => {
        const currentLanguage = auth?.language || 'en';
        code = code === 'US' ? 'en' : code.toLowerCase();
        
        if (currentLanguage !== code) {
            accountsService.changeLanguage(code).then(() => {
                const user = Object.assign({}, auth);
                user.language = code;
                localStorage.setItem('user', JSON.stringify(user));
                setAuth(user);
                i18n.changeLanguage(user.language).then(() => setLoading(false));
            });
        }
        
    }
    
    return (
        <div className="border-bottom navbar navbar-light box-shadow lk-header">
            {stadium !== null && 
                <div className="stadium-list">
                    <div style={window.location.pathname.startsWith("/lk/accounts") ? {display: "none"} : {}}>
                        {t("common:lk_header:current_stadium_title")}:  &nbsp;
                        <Dropdown
                            onChange={changeStadium}
                            inline
                            options={stadiums}
                            defaultValue={stadium}
                        />
                    </div>
                    <div className="header-warning" style={window.location.pathname.startsWith("/lk/accounts") ? {} : {display: "none"}}>
                        <i className="fa fa-exclamation-circle" aria-hidden="true" />
                        <div className="header-warning-text">
                            <span title={t("accounts:header_notification_line1")|| ''}>{t("accounts:header_notification_line1")}</span>
                            <span title={t("accounts:header_notification_line2")|| ''}>{t("accounts:header_notification_line2")}</span>
                        </div>
                    </div>
                </div>}

            {stadium !== null && <div className="header-icons">
                    <div className="settings-icon">
                        <GearFill color="#354650" size={22}/>
                    </div>
                    <div className="bell-icon">
                        <Bell color="#354650" size={22}/>
                    </div>
                    <div className="profile-icon">
                        <PersonCircle color="#354650" size={22}/>
                    </div>
                    <div className="language-container">
                        <ReactFlagsSelect
                            selected={getSelectedLanguage()}
                            onSelect={changeLanguage}
                            showSelectedLabel={true}
                            //showSecondarySelectedLabel={true}
                            //showOptionLabel={true}
                            //showSecondaryOptionLabel={true}
                            customLabels={customLabelsLanguages}
                            countries={["US", "RU"]}
                        />
                    </div>
                    {/*<span>{auth.fullName}</span>*/}
                </div>}
        </div>)
};