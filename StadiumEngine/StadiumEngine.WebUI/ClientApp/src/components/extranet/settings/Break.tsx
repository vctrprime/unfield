import React, {useEffect, useRef, useState} from 'react';
import {useNavigate, useParams} from "react-router-dom";
import {getDataTitle, getTitle, StringFormat, validateInputs} from "../../../helpers/utils";
import {BreakDto} from "../../../models/dto/settings/BreakDto";
import {useInject} from "inversify-hooks";
import {ISettingsService} from "../../../services/SettingsService";
import {IOffersService} from "../../../services/OffersService";
import {FieldDto} from "../../../models/dto/offers/FieldDto";
import {MainSettingsDto} from "../../../models/dto/settings/MainSettingsDto";
import {ActionButtons} from "../../common/actions/ActionButtons";
import {PermissionsKeys} from "../../../static/PermissionsKeys";
import {t} from "i18next";
import {AddBreakCommand} from "../../../models/command/settings/AddBreakCommand";
import {UpdateBreakCommand} from "../../../models/command/settings/UpdateBreakCommand";
import {useRecoilState} from "recoil";
import {fieldsAtom} from "../../../state/offers/fields";
import {Checkbox, Dropdown, Form} from "semantic-ui-react";
import {DateRangeSelect} from "../common/DateRangeSelect";
import {parseNumber} from "../../../helpers/time-point-parser";

export const Break = () => {
    let {id} = useParams();

    const [data, setData] = useState<BreakDto>({
        isActive: true,
        dateStart: new Date(),
        startHour: 12,
        endHour: 13
    } as BreakDto);

    const [fields, setFields] = useRecoilState<FieldDto[]>(fieldsAtom);
    
    const [mainSettings, setMainSettings] = useState<MainSettingsDto|null>(null);
    
    const [isError, setIsError] = useState<boolean>(false);
    const [breakId, setBreakId] = useState(parseInt(id || "0"))

    const [settingsService] = useInject<ISettingsService>('SettingsService');
    const [offersService] = useInject<IOffersService>('OffersService');

    const fetchBreak = () => {
        if (breakId > 0) {
            settingsService.getBreak(breakId).then((result: BreakDto) => {
                setData(result);
                fetchFields();
                fetchMainSettings();
                
            }).catch(() => setIsError(true));
        }
        else {
            fetchFields();
            fetchMainSettings();
        }
    }
    
    const fetchFields = () => {
        if (fields.length === 0) {
            offersService.getFields().then((result: FieldDto[]) => {
                setFields(result);
            })
        }
    }
    
    const fetchMainSettings = () => {
        settingsService.getMainSettings().then((result: MainSettingsDto) => {
            setMainSettings(result);
        });
    }
    
    useEffect(() => {
        fetchBreak();
    }, [breakId])

    useEffect(() => {
        setBreakId(parseInt(id || "0"));
    }, [id])
    
    useEffect(() => {
        if (data.name !== undefined && data.name !== null) {
            document.title = getDataTitle(data.name);
        } else {
            document.title = getTitle("settings:breaks_tab")
        }
    }, [data])

    const nameInput = useRef<any>();
    const descriptionInput = useRef<any>();

    const navigate = useNavigate();

    const saveBreak = () => {
        if (validateInputs([nameInput])) {
            //const tzDifference = data.dateStart.getTimezoneOffset();
            
            const command: AddBreakCommand = {
                name: nameInput.current?.value,
                description: descriptionInput.current?.value,
                isActive: data.isActive,
                selectedFields: data.selectedFields,
                startHour: data.startHour,
                endHour: data.endHour,
                dateStart: data.dateStart,//new Date(data.dateStart.getTime() - tzDifference * 60 * 1000),
                dateEnd: data.dateEnd// ? new Date(data.dateEnd.getTime() - tzDifference * 60 * 1000) : data.dateEnd,
            }
            settingsService.addBreak(command).then(() => {
                navigate("/extranet/settings/breaks");
            })
        }
    }

    const updateBreak = () => {
        if (validateInputs([nameInput])) {
            const command: UpdateBreakCommand = {
                id: breakId,
                name: nameInput.current?.value,
                description: descriptionInput.current?.value,
                isActive: data.isActive,
                selectedFields: data.selectedFields,
                startHour: data.startHour,
                endHour: data.endHour,
                dateStart: data.dateStart,
                dateEnd: data.dateEnd,
            }
            settingsService.updateBreak(command).then(() => {
                navigate("/extranet/settings/breaks");
            })
        }
    }

    const deleteBreak = () => {
        settingsService.deleteBreak(breakId).then(() => {
            navigate("/extranet/settings/breaks");
        })
    }

    const changeIsActive = () => {
        setData({
            ...data,
            isActive: !data.isActive
        });
    }

    const periodValue = () => {
        let result = [];
        if (data?.dateEnd) {
            result = [new Date(data?.dateStart), new Date(data?.dateEnd)]
        }
        else {
            result = [new Date(data?.dateStart)];
        }
        return result;
    }
    const changePeriod = (event: any, dates: any) => {
        let startDate = new Date();
        if (dates.value !== null && dates.value.length > 0) {
            startDate = dates.value[0];
        }
        let endDate = null;
        if (dates.value !== null && dates.value.length > 1) {
            endDate = dates.value[1]
        }

        setData({
            ...data,
            dateStart: startDate,
            dateEnd: endDate
        });
    }
    
    const changeSelectedFields = (e: any, {value}: any) => {
        setData({
            ...data,
            selectedFields: value
        });
    }

    const dropdownDataStart = () => {
        const result = [];
        let i = mainSettings?.openTime || 0;
        
        while ( i <= (mainSettings?.closeTime || i)) {
            result.push({key: i, value: i, text: parseNumber(i), disabled: i >= data.endHour})
            i += 0.5;
        }
        return result;
    }

    const dropdownDataEnd = () => {
        const result = [];
        let i = mainSettings?.openTime || 0;
        
        while ( i <= (mainSettings?.closeTime || i)) {
            result.push({key: i, value: i, text: parseNumber(i), disabled: i <= data.startHour})
            i += 0.5;
        }
        return result;
    }

    const changeStart = (e: any, {value}: any) => {
        setData({
            ...data,
            startHour: value
        });
    }

    const changeEnd = (e: any, {value}: any) => {
        setData({
            ...data,
            endHour: value
        });
    }

    return isError || mainSettings === null ? <span/> : (
        <div>
            <ActionButtons
                savePermission={id === "new" ? PermissionsKeys.InsertBreak : PermissionsKeys.UpdateBreak}
                deletePermission={PermissionsKeys.DeleteBreak}
                title={id === "new" ? t('settings:breaks_grid:adding') : t('settings:breaks_grid:editing')}
                saveAction={id === "new" ? saveBreak : updateBreak}
                deleteAction={id === "new" ? null : deleteBreak}
                deleteHeader={id === "new" ? null : t('settings:breaks_grid:delete:header')}
                deleteQuestion={id === "new" ? null : StringFormat(t('settings:breaks_grid:delete:question'), data?.name || '')}
            />
            <Form className="break-form">
                <Form.Field style={{marginBottom: 0}}>
                    <label>{t("settings:breaks_grid:is_active")}</label>
                    <Checkbox toggle checked={data.isActive} onChange={() => changeIsActive()}/>
                </Form.Field>
                <Form.Field>
                    <label>{t("settings:breaks_grid:name")}</label>
                    <input id="name-input" ref={nameInput} placeholder={t("settings:breaks_grid:name") || ''}
                           defaultValue={data?.name || ''}/>
                </Form.Field>
                <Form.Field>
                    <label>{t("settings:breaks_grid:description")}</label>
                    <textarea id="description-input" ref={descriptionInput} rows={4}
                              placeholder={t("settings:breaks_grid:description") || ''}
                              defaultValue={data?.description || ''}/>
                </Form.Field>
                <Form.Field>
                    <label>{t("settings:breaks_grid:period")}</label>
                    <DateRangeSelect value={periodValue()} onChange={changePeriod}/>
                </Form.Field>
                <Form.Field>
                    <label style={{paddingBottom: "5px", marginTop: "5px", display: 'flex', alignItems: "center"}}>{t("settings:breaks_grid:hours_period")}</label>
                    <div style={{float: "left", width: '100%'}}>
                        <label>{t("common:from")} </label>
                        <Dropdown
                            style={{ marginLeft: '10px'}}
                            inline
                            scrolling
                            onChange={changeStart}
                            options={dropdownDataStart()}
                            value={data.startHour}
                        />
                        <label style={{ marginLeft: '5px'}}>{t("common:to")} </label>
                        <Dropdown
                            style={{ marginLeft: '10px'}}
                            inline
                            scrolling
                            onChange={changeEnd}
                            options={dropdownDataEnd()}
                            value={data.endHour}
                        />
                    </div>
                </Form.Field>
                <Form.Field>
                    <label>{t("settings:breaks_grid:fields")}</label>
                    <Dropdown
                        placeholder={t("settings:breaks_grid:fields") || ''}
                        fluid
                        search
                        multiple
                        style={{width: "500px"}}
                        selection
                        onChange={changeSelectedFields}
                        value={data.selectedFields}
                        options={fields.map((f: FieldDto) => {
                            return {
                                key: f.id,
                                value: f.id,
                                text: f.name
                            }
                        })}
                    />
                    <a style={{cursor: "pointer", textDecoration: 'underline'}} onClick={() => {
                        setData({
                            ...data,
                            selectedFields: fields.map((f) => f.id)
                        });
                    }}>{t("settings:breaks_grid:select_all_fields")}</a>
                </Form.Field>
            </Form>
        </div>)
}
