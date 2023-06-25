import React, {useState} from 'react';
import {getTitle} from "../../../helpers/utils";
import {FieldsScheduler} from "./FieldsScheduler";
import {NavLink, Outlet, useParams} from "react-router-dom";
import {t} from "i18next";
import {Card, Icon, Popup, Rating} from 'semantic-ui-react';

export const Schedule = () => {
    document.title = getTitle("common:lk_navbar:schedule")
    const params = useParams();
    
    const [mode, setMode] = useState<string>(params["viewMode"] || "default");
    const [view, setView] = useState<string>("day");

    return (
        <div className="tabs-container" style={{ backgroundColor: 'white'}}>
            <div className="tabs-links box-shadow" style={{zIndex: 1001, display: 'flex', 
                justifyContent: 'space-between',
                alignItems: 'center'}}>
                <div>
                    <a className={ mode === "default" ? "active": "" } onClick={() => {
                        window.history.pushState(null, '', "lk/schedule");
                        setMode("default")
                    }}>
                        <div className="tabs-link">
                            {t('schedule:tabs:all')}
                        </div>
                    </a>
                    <a className={ mode === "tabs" ? "active": "" } onClick={() => {
                        window.history.pushState(null, '', "lk/schedule/tabs");
                        setMode("tabs")
                    }}>
                        <div className="tabs-link">
                            {t('schedule:tabs:one')}
                        </div>
                    </a>
                    <a className={ mode === "list" ? "active": "" } onClick={() => {
                        window.history.pushState(null, '', "lk/schedule/list");
                        setMode("list")
                    }}>
                        <div className="tabs-link">
                            {t('schedule:tabs:list')}
                        </div>
                    </a>
                </div>
                <Popup
                    trigger={
                        <Icon style={{ fontSize: '18px', marginRight: '10px'}} name='question circle outline'/>
                    }
                >
                    <Popup.Content style={{width: '310px'}}>
                       <div className="scheduler-legend-row">
                           <div className="scheduler-legend-square" style={{backgroundColor: "#3CB371"}}/> <span>&nbsp;  {t('schedule:scheduler:legend:form')}</span>
                       </div>
                        <div className="scheduler-legend-row">
                            <div className="scheduler-legend-square" style={{backgroundColor: "#4682B4"}} /> <span>&nbsp;  {t('schedule:scheduler:legend:schedule')}</span>
                        </div>
                        <div className="scheduler-legend-row">
                            <div className="scheduler-legend-square" style={{backgroundColor: "#20B2AA"}} /> <span>&nbsp;  {t('schedule:scheduler:legend:weekly')}</span>
                        </div>
                        <div className="scheduler-legend-row">
                            <div className="scheduler-legend-square" style={{backgroundColor: "#d0d0d0"}} /> <span>&nbsp;  {t('schedule:scheduler:legend:disabled')}</span>
                        </div>
                    </Popup.Content>
                </Popup>
            </div>
            <div className={"tabs-content " + view}>
                {mode !== 'list' && <FieldsScheduler setView={setView} mode={mode}/>}
                {mode === 'list' && <span>Список</span>}
            </div>
        </div>);
}