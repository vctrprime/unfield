import React, {useEffect} from 'react';
import {NavLink, Outlet, useNavigate} from "react-router-dom";
import {t} from "i18next";

export interface TabData {
    route: string,
    resourcePath: string
}
interface TabsProps {
    tabsData: TabData[]
    leftNavRoute: string
}

export const Tabs = ({ tabsData, leftNavRoute } : TabsProps) => {
    const navigate = useNavigate();

    useEffect(() => {
        if (tabsData.filter(t => `${leftNavRoute}/${t.route}` === window.location.pathname).length === 0) {
            navigate(tabsData[0].route);
        }
    }, [])
    
    
    
    return (
        <div className="tabs-container">
            <div className="tabs-links">
                {tabsData.map((tab, i) => {
                    return (
                        <NavLink key={i} to={tab.route} end>
                            <div className="tabs-link">
                                {t(tab.resourcePath)}
                            </div>
                        </NavLink>
                    )
                })}
            </div>
            <div className="tabs-content">
                <Outlet />
            </div>

        </div>)
}