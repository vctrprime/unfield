import React from 'react';
import {Dropdown, Icon} from "semantic-ui-react";
import {t} from "i18next";


export interface TariffIntervalProps {
    interval: string[];
    setInterval: any;
    index: number;
    points: string[]
}

export const TariffInterval = (props: TariffIntervalProps) => {
    
    const startIndex = props.points.indexOf(props.interval[0]);
    const endIndex = props.points.indexOf(props.interval[1]);
    
    const dropdownDataStart = () => {
        return props.points.map((v, i) => {
           return {key: i, value: v, text: v, disabled: i >= endIndex};
       });
    }

    const dropdownDataEnd = () => {
        return props.points.map((v, i) => {
            return {key: i, value: v, text: v, disabled: i <= startIndex};
        });
    }
    
    const changeStart = (e: any, {value}: any) => {
        props.setInterval(value, props.interval[1], props.interval)
    }
    
    const changeEnd = (e: any, {value}: any) => {
        props.setInterval(props.interval[0], value, props.interval);
    }
    
    const remove = () => {
        props.setInterval("", "", props.interval, true);
    }
    
    
    return <div style={{marginTop: "10px"}}>
        <div style={{paddingBottom: "5px", marginTop: "5px", display: 'flex', alignItems: "center"}}>{t("rates:tariffs_grid:interval")} {props.index + 1}
            <Icon style={{ marginLeft: '5px', cursor: 'pointer'}} name='close' title={t('rates:tariffs_grid:remove_interval')} onClick={remove}/>
        </div>
        <div style={{float: "left"}}>
            <Dropdown
                style={{ marginLeft: '5px'}}
                inline
                scrolling
                onChange={changeStart}
                options={dropdownDataStart()}
                value={props.interval[0]}
            />
            <label style={{ marginLeft: '5px'}}>-</label>
            <Dropdown
                style={{ marginLeft: '5px'}}
                inline
                scrolling
                onChange={changeEnd}
                options={dropdownDataEnd()}
                value={props.interval[1]}
            />
        </div>
        <div className="tariff-interval-timeline">
            {props.points.map((time, index) => {
                return <>
                    <div className="tariff-interval-cont">
                        <div className="tariff-interval-point-cont">
                            <div style={index >= startIndex && index <= endIndex ? { backgroundColor: "#00d2ff"} : {}} className={index % 2 == 0 ? "tariff-interval-point" : "tariff-interval-point small"} />
                            <div style={index % 2 == 0 ? {} : { opacity: 0, fontSize: '4px'}} className="tariff-interval-point-text">{time}</div>
                        </div>
                    </div>
                    <div className="tariff-interval-cont" style={{flexDirection: 'column'}}>
                        {index !== props.points.length - 1 && <div style={index >= startIndex && index < endIndex ? { backgroundColor: "#00d2ff"} : {}} className="tariff-interval-point-line" />}
                        <div style={{ opacity: 0, fontSize: '6px'}} className="tariff-interval-point-text">{time}</div>
                    </div>
                </>
                    })}
                
        </div>
    </div> 
}