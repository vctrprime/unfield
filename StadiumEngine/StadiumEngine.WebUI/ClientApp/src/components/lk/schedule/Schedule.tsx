import React, {useState} from 'react';
import {getTitle} from "../../../helpers/utils";
import {FieldsScheduler} from "./FieldsScheduler";
import {NavLink, Outlet, useParams} from "react-router-dom";
import {t} from "i18next";

export const Schedule = () => {
    document.title = getTitle("common:lk_navbar:schedule")
    const params = useParams();
    
    const [mode, setMode] = useState<string>(params["viewMode"] || "default");

    return (
        <div className="tabs-container">
            <div className="tabs-links box-shadow" style={{zIndex: 1001, cursor: 'pointer'}}>
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
            <div className="tabs-content">
                {mode !== 'list' && <FieldsScheduler mode={mode}/>}
                {mode === 'list' && <span>Список</span>}
            </div>
        </div>);
}