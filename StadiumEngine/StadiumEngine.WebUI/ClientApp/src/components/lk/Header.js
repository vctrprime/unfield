import React from 'react';
import {Bell, GearFill, PersonCircle} from 'react-bootstrap-icons';
import {Dropdown} from "semantic-ui-react";


export const Header = () => {
    
    const data = [
        {key: '1', text: 'Андреевская 2', value: '1'},
        {key: '2', text: 'МонтКлер, 40', value: '2'}
    ];
    
    return (
        <div className="border-bottom navbar navbar-light box-shadow lk-header">
            <div className="stadium-list">
                Текущий объект:  &nbsp;
                <Dropdown
                    inline
                    options={data}
                    defaultValue={data[0].value}
                />
            </div>
            
            <div className="header-icons">
                <div className="settings-icon">
                    <GearFill color="#354650" size={22}/>
                </div>
                <div className="bell-icon">
                    <Bell color="#354650" size={22}/>
                </div>
                <div className="profile-icon">
                    <PersonCircle color="#354650" size={22}/>
                </div>
                
            </div>
            
        </div>)
};