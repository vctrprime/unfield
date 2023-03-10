import React from 'react';
import {Dropdown, Icon} from "semantic-ui-react";
import {t} from "i18next";


export interface TariffIntervalProps {
    interval: string[];
    setInterval: any;
    index: number;
}

export const TariffInterval = (props: TariffIntervalProps) => {
    const startHour = 8;
    const endHour = 24;
    
    const getAllIntervals = (): string[] => {
        const result: string[] = [];
        for (let i = startHour; i <= endHour; i++) {
            if (i === endHour) {
                result.push(i.toString().length === 1 ? `0${i}:00`: `${i}:00`);
            }
            else {
                result.push(i.toString().length === 1 ? `0${i}:00`: `${i}:00`);
                result.push(i.toString().length === 1 ? `0${i}:30`: `${i}:30`);
            }
        }
        
        return result;
    }
    
    const allIntervals = getAllIntervals();

    const startIndex = allIntervals.indexOf(props.interval[0]);
    const endIndex = allIntervals.indexOf(props.interval[1]);
    
    const dropdownDataStart = () => {
        const endIndex = allIntervals.indexOf(props.interval[1]);
        
        return allIntervals.map((v, i) => {
           return {key: i, value: v, text: v, disabled: i >= endIndex};
       });
    }

    const dropdownDataEnd = () => {
        const startIndex = allIntervals.indexOf(props.interval[0]);
        
        return allIntervals.map((v, i) => {
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
            {allIntervals.map((time, index) => {
                return <>
                    <div className="tariff-interval-cont">
                        <div className="tariff-interval-point-cont">
                            <div style={index >= startIndex && index <= endIndex ? { backgroundColor: "#00d2ff"} : {}} className={index % 2 == 0 ? "tariff-interval-point" : "tariff-interval-point small"} />
                            <div style={index % 2 == 0 ? {} : { opacity: 0, fontSize: '4px'}} className="tariff-interval-point-text">{time}</div>
                        </div>
                    </div>
                    <div className="tariff-interval-cont" style={{flexDirection: 'column'}}>
                        {index !== allIntervals.length - 1 && <div style={index >= startIndex && index < endIndex ? { backgroundColor: "#00d2ff"} : {}} className="tariff-interval-point-line" />}
                        <div style={{ opacity: 0, fontSize: '6px'}} className="tariff-interval-point-text">{time}</div>
                    </div>
                </>
                    })}
                
        </div>
    </div> 
}