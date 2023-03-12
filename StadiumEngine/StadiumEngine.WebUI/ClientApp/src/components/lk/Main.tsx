import React, {useEffect, useRef, useState} from 'react';
import {useRecoilValue} from "recoil";
import {stadiumAtom} from "../../state/stadium";
import {getTitle, StringFormat, validateInputs} from "../../helpers/utils";
import {TariffDto} from "../../models/dto/rates/TariffDto";
import {StadiumMainSettingsDto} from "../../models/dto/settings/StadiumMainSettingsDto";
import {useInject} from "inversify-hooks";
import {ISettingsService} from "../../services/SettingsService";
import {Button, Checkbox, Dropdown, Form, Icon} from "semantic-ui-react";
import {t} from "i18next";
import {DateRangeSelect} from "./common/DateRangeSelect";
import {TariffInterval} from "./rates/TariffInterval";
import {PermissionsKeys} from "../../static/PermissionsKeys";
import {ActionButtons} from "../common/actions/ActionButtons";
import {UpdateTariffCommand} from "../../models/command/rates/UpdateTariffCommand";
import {UpdateStadiumMainSettingsCommand} from "../../models/command/settings/UpdateStadiumMainSettingsCommand";

export const Main = () => {
    document.title = getTitle("common:lk_navbar:main_settings")

    const stadium = useRecoilValue(stadiumAtom);

    const [data, setData] = useState<StadiumMainSettingsDto>({
        openTime: 8,
        closeTime: 23
    } as StadiumMainSettingsDto);
    const [isError, setIsError] = useState<boolean>(false);

    const [settingsService] = useInject<ISettingsService>('SettingsService');

    const fetchMainSettings = () => {
        settingsService.getStadiumMainSettings().then((result) => {
            setData(result);
        }).catch(() => setIsError(true));
    }
    
    useEffect(() => {
        fetchMainSettings();
    }, [stadium])

    const nameInput = useRef<any>();
    const descriptionInput = useRef<any>();
    
    const points = [0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24];

    const dropdownDataOpen = () => {
        return points.map((v, i) => {
            return {key: i, value: v, text: v, disabled: v >= data.closeTime};
        });
    }

    const dropdownDataClose = () => {
        return points.map((v, i) => {
            return {key: i, value: v, text: v, disabled: v <= data.openTime};
        });
    }

    const changeOpen = (e: any, {value}: any) => {
        setData({
            ...data,
            openTime: value
        });
    }

    const changeClose = (e: any, {value}: any) => {
        setData({
            ...data,
            closeTime: value
        });
    }

    const updateMainSettings = () => {
        const command: UpdateStadiumMainSettingsCommand = {
            name: nameInput.current?.value,
            description: descriptionInput.current?.value,
            closeTime: data.closeTime,
            openTime: data.openTime
        }
        settingsService.updateStadiumMainSettings(command).then(() => {
            
        })
    }

    return isError ? <span/> : (<div>
        <ActionButtons
            savePermission={PermissionsKeys.UpdateMainSettings}
            deletePermission={''}
            title={t('settings:main:editing')}
            saveAction={updateMainSettings}
            deleteAction={null}
            deleteHeader={null}
            deleteQuestion={null}
        />
        <Form className="main-settings-form">
            <Form.Field>
                <label>{t("settings:main:name")}</label>
                <input id="name-input" ref={nameInput} placeholder={t("settings:main:name") || ''}
                       defaultValue={data?.name || ''}/>
            </Form.Field>
            <Form.Field>
                <label>{t("settings:main:description")}</label>
                <textarea id="description-input" ref={descriptionInput} rows={4}
                          placeholder={t("settings:main:description") || ''}
                          defaultValue={data?.description || ''}/>
            </Form.Field>
            <Form.Field>
                <div style={{paddingBottom: "5px", fontWeight: 'bold', marginTop: "5px", display: 'flex', alignItems: "center"}}>{t("settings:main:working_mode")}</div>
                <div style={{float: "left", width: '100%'}}>
                    <label>{t("settings:main:open_time")}: </label>
                    <Dropdown
                        style={{ marginLeft: '5px'}}
                        inline
                        scrolling
                        onChange={changeOpen}
                        options={dropdownDataOpen()}
                        value={data.openTime}
                    />
                   
                </div>
                <div style={{float: "left", width: '100%'}}>
                    <label>{t("settings:main:close_time")}: </label>
                    <Dropdown
                        style={{ marginLeft: '5px'}}
                        inline
                        scrolling
                        onChange={changeClose}
                        options={dropdownDataClose()}
                        value={data.closeTime}
                    />
                </div>
            </Form.Field>
        </Form>
    </div>);
}