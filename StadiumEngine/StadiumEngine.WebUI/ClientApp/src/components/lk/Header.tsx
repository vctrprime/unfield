import React, {useEffect, useState} from 'react';
import {Bell, GearFill, PersonCircle} from 'react-bootstrap-icons';
import {Dropdown} from "semantic-ui-react";
import {useRecoilState} from "recoil";
import {stadiumAtom} from "../../state/stadium";
import {UserStadiumDto} from "../../models/dto/accounts/UserStadiumDto";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../services/AccountsService";

interface StadiumDropDownData {
    key: number,
    value: number,
    text: string
}

export const Header = () => {
    const [stadiums, setStadiums] = useState<StadiumDropDownData[]>([])
    const [stadium, setStadium] = useRecoilState<number | null>(stadiumAtom);

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
    
    return (
        <div className="border-bottom navbar navbar-light box-shadow lk-header">
            {stadium !== null && 
                <div className="stadium-list">
                    <div style={window.location.pathname.startsWith("/lk/accounts") ? {display: "none"} : {}}>
                        Текущий объект:  &nbsp;
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
                            <span>Просим внимательно относиться к изменениям настроек в этом модуле.</span>
                            <span>Некорректная конфигурация может вызвать проблемы с доступом к возможностям системы у сотрудников Вашей организации.</span>
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
                    {/*<span>{auth.fullName}</span>*/}
                </div>}
        </div>)
};