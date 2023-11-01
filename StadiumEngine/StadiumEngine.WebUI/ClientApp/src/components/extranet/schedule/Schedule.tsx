import React, {useState} from 'react';
import {getTitle} from "../../../helpers/utils";
import {FieldsScheduler} from "./FieldsScheduler";
import {NavLink, Outlet, useParams} from "react-router-dom";
import {t} from "i18next";
import {Button, Card, Icon, Popup, Rating} from 'semantic-ui-react';
import {BookingList} from "./BookingList";

export const Schedule = () => {
    document.title = getTitle("common:lk_navbar:schedule")
    const params = useParams();
    
    const [mode, setMode] = useState<string>(params["viewMode"] || "default");
    const [view, setView] = useState<string>("day");
    
    const [refresh, setRefresh] = useState<boolean|null>(null);

    return (
        <div className="tabs-container" style={{ backgroundColor: 'white'}}>
            <div className="tabs-links box-shadow" style={{zIndex: 1001, display: 'flex', 
                justifyContent: 'space-between',
                alignItems: 'center'}}>
                <div>
                    <a className={ mode === "default" ? "active": "" } onClick={() => {
                        window.history.pushState(null, '', "extranet/schedule");
                        setMode("default")
                    }}>
                        <div className="tabs-link">
                            {t('schedule:tabs:all')}
                        </div>
                    </a>
                    <a className={ mode === "tabs" ? "active": "" } onClick={() => {
                        window.history.pushState(null, '', "extranet/schedule/tabs");
                        setMode("tabs")
                    }}>
                        <div className="tabs-link">
                            {t('schedule:tabs:one')}
                        </div>
                    </a>
                    <a className={ mode === "list" ? "active": "" } onClick={() => {
                        window.history.pushState(null, '', "extranet/schedule/list");
                        setMode("list")
                    }}>
                        <div className="tabs-link">
                            {t('schedule:tabs:list')}
                        </div>
                    </a>
                </div>
                {mode !== 'list' && <div style={{ display: 'flex', alignItems: 'center'}}>
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
                    <Icon style={{ fontSize: '18px', marginRight: '10px', cursor: 'pointer', color: '#354650'}} onClick={() => {
                        if (refresh === null) {
                            setRefresh(true);
                        }
                        else {
                            setRefresh(!refresh);
                        }}} name="refresh"/>
                </div>}
            </div>
            <div className={"tabs-content " + view}>
                {mode !== 'list' && <FieldsScheduler refresh={refresh} setView={setView} mode={mode}/>}
                {mode === 'list' && <BookingList/>}
            </div>
        </div>);
}