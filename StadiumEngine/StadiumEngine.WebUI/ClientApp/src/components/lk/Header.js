import React, {useEffect, useState} from 'react';
import {Bell, GearFill, PersonCircle} from 'react-bootstrap-icons';
import {Dropdown} from "semantic-ui-react";
import {useFetchWrapper} from "../../helpers/fetch-wrapper";
import {useRecoilState} from "recoil";
//import {authAtom} from "../../state/auth";
import {stadiumAtom} from "../../state/stadium";


export const Header = () => {
    //const auth = useRecoilValue(authAtom);
    const [stadiums, setStadiums] = useState([])
    const [stadium, setStadium] = useRecoilState(stadiumAtom);

    const fetchWrapper = useFetchWrapper();
    
    useEffect(() => {
        loadStadiums();
    }, []);

    const loadStadiums = () => {
        fetchWrapper.get({url: 'api/accounts/user-stadiums', withSpinner: true, hideSpinner: false}).then((result) => {
            setStadiums(result.map((s) => {
                return { key: s.id, value: s.id, text: s.address }
            }));
            setStadium(result.find(s => s.isCurrent).id);
        })
    }


    const changeStadium = (e, { value }) => {
        fetchWrapper.put({url: 'api/accounts/change-stadium/' + value}).then((result) => {
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