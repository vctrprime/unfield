import React, {useEffect, useRef, useState} from 'react';
import {useRecoilValue} from "recoil";
import {useInject} from "inversify-hooks";
import {Button, Checkbox, Dropdown, Form, Icon} from "semantic-ui-react";
import {t} from "i18next";
import {getTitle} from "../../../helpers/utils";
import {stadiumAtom} from "../../../state/stadium";
import {MainSettingsDto} from "../../../models/dto/settings/MainSettingsDto";
import {ISettingsService} from "../../../services/SettingsService";
import {UpdateMainSettingsCommand} from "../../../models/command/settings/UpdateMainSettingsCommand";
import {ActionButtons} from "../../common/actions/ActionButtons";
import {PermissionsKeys} from "../../../static/PermissionsKeys";

export const MainSettings = () => {
    document.title = getTitle("settings:main_tab")

    const stadium = useRecoilValue(stadiumAtom);

    const [data, setData] = useState<MainSettingsDto>({
        openTime: 8,
        closeTime: 23
    } as MainSettingsDto);
    const [isError, setIsError] = useState<boolean>(false);

    const [settingsService] = useInject<ISettingsService>('SettingsService');

    const fetchMainSettings = () => {
        settingsService.getMainSettings().then((result) => {
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
        const command: UpdateMainSettingsCommand = {
            name: nameInput.current?.value,
            description: descriptionInput.current?.value,
            closeTime: data.closeTime,
            openTime: data.openTime
        }
        settingsService.updateMainSettings(command).then(() => {
            
        })
    }

    return isError ? <span/> : (<div style={{marginTop: '-3px'}}>
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