import React from "react";
import '../../css/NotFound.scss'
import {t} from "i18next";

export const NotFound = () => {
    return <div className="not-found">
        <div className="not-found-cont">
            <div className="bubble"></div>
            <div className="bubble"></div>
            <div className="bubble"></div>
            <div className="bubble"></div>
            <div className="bubble"></div>
            <div className="not-found-main">
                <h1>404</h1>
                <p className="p-404">{t('common:not_found:first')}<br/>{t('common:not_found:second')}</p>
            </div>
        </div>
    </div>;
}