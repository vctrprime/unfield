import React, {useEffect, useRef, useState} from 'react';
import {useNavigate, useParams} from "react-router-dom";
import {useInject} from "inversify-hooks";
import {IOffersService} from "../../../services/OffersService";
import {FieldDto} from "../../../models/dto/offers/FieldDto";
import {getDataTitle, getTitle, StringFormat} from "../../../helpers/utils";
import {ActionButtons} from "../../common/actions/ActionButtons";
import {t} from "i18next";
import {Button, Checkbox, Dropdown, Form} from "semantic-ui-react";
import {FieldCoveringType} from "../../../models/dto/offers/enums/FieldCoveringType";
import {FieldSportKind} from "../../../models/dto/offers/enums/FieldSportKind";

export const Field = () => {
    let { id } = useParams();

    const [data, setData] = useState<FieldDto|null>(null);
    const [isError, setIsError] = useState<boolean>(false);
    const [isActive, setIsActive] = useState<boolean>(false)
    const [fieldId, setFieldId] = useState(parseInt(id||"0"))
    
    const [parentFields, setParentFields] = useState<FieldDto[]>([]);

    const [offersService] = useInject<IOffersService>('OffersService');

    const navigate = useNavigate();

    const fetchField = () => {
        if (fieldId > 0) {
            offersService.getField(fieldId).then((result: FieldDto) => {
                setData(result);
                setIsActive(result.isActive);
            }).catch(() => setIsError(true));
        }
        else {
            setIsActive(true);
        }
    }
    
    const fetchParentFields = () => {
        offersService.getFields().then((result: FieldDto[]) => {
            setParentFields(result.filter(f => f.parentFieldId === null && f.id !== fieldId));
        })
    }

    useEffect(() => {
        fetchField();
        fetchParentFields();
    }, [])

    useEffect(() => {
        if (data !== null) {
            document.title = getDataTitle(data.name);
        }
        else {
            document.title = getTitle("offers:fields_tab")
        }
    }, [data])
    
    const coveringTypes = () => {
        const result = [];
        for (let item in FieldCoveringType) {
            if (!isNaN(Number(item))) {
                const value = FieldCoveringType[item];
                const text = t("offers:coverings:" + value.toLowerCase());

                result.push({
                    key: item,
                    value: Number(item),
                    text: text
                })
            }
        }
        return result;
    }

    const sportKinds = () => {
        const result = [];
        for (let item in FieldSportKind) {
            if (!isNaN(Number(item))) {
                const value = FieldSportKind[item];
                const text = t("offers:sports:" + value.toLowerCase());

                result.push({
                    key: item,
                    value: Number(item),
                    text: text
                })
            }
        }
        
        return result;
    }

    const nameInput = useRef<any>();
    const descriptionInput = useRef<any>();
    const widthInput = useRef<any>();
    const lengthInput = useRef<any>();

    const validate = (): boolean => {
        if (!nameInput.current?.value) {
            nameInput.current.style.border = "1px solid red";
            setTimeout(() => {
                nameInput.current.style.border = "";
            }, 2000);
            return false;
        }
        else {
            return true;
        }
    }

    const saveField = () => {
        if (validate()) {
            /*const command: AddLockerRoomCommand = {
                name: nameInput.current?.value,
                description: descriptionInput.current?.value,
                isActive: isActive,
                gender: editingGender
            }
            offersService.addLockerRoom(command).then(() => {
                navigate("/lk/offers/locker-rooms");
            })*/
        }
    }

    const updateField = () => {
        if (validate()) {
            /*const command: UpdateLockerRoomCommand = {
                id: lockerRoomId,
                name: nameInput.current?.value,
                description: descriptionInput.current?.value,
                isActive: isActive,
                gender: editingGender
            }
            offersService.updateLockerRoom(command).then(() => {
                navigate("/lk/offers/locker-rooms");
            })*/
        }
    }

    const deleteField = () => {
        offersService.deleteField(fieldId).then(() => {
            navigate("/lk/offers/fields");
        })
    }

    return isError ? <span/> : (<div>
        <ActionButtons
            title={id === "new" ? t('offers:fields_grid:adding') : t('offers:fields_grid:editing')}
            saveAction={id === "new" ? saveField : updateField}
            deleteAction={id === "new" ? null : deleteField}
            deleteHeader={id === "new" ? null : t('offers:fields_grid:delete:header')}
            deleteQuestion={id === "new" ? null : StringFormat(t('offers:fields_grid:delete:question'), data?.name||'')}
        />
        <Form className="field-form">
            <Form.Field style={{marginBottom: 0}}>
                <label>{t("offers:fields_grid:is_active")}</label>
                <Checkbox toggle checked={isActive} onChange={() => setIsActive(!isActive)}/>
            </Form.Field>
            <Form.Field>
                <label>{t("offers:fields_grid:name")}</label>
                <input id="name-input" ref={nameInput} placeholder={t("offers:fields_grid:name")||''} defaultValue={data?.name || ''}/>
            </Form.Field>
            <Form.Field >
                <label>{t("offers:fields_grid:size")}</label>
                <div className="field-size-cont">
                    <input className="field-size-input" type="number" id="length-input" ref={lengthInput} placeholder={t("offers:fields_grid:length")||''} defaultValue={data?.length || ''}/>
                    X
                    <input className="field-size-input" type="number" id="width-input" ref={widthInput} placeholder={t("offers:fields_grid:width")||''} defaultValue={data?.width || ''}/>
                </div>
            </Form.Field>
            <Form.Field>
                <label>{t("offers:fields_grid:description")}</label>
                <textarea id="description-input" ref={descriptionInput} rows={4} placeholder={t("offers:fields_grid:description")||''} defaultValue={data?.description || ''}/>
            </Form.Field>
            <Form.Field >
                <label>{t("offers:fields_grid:covering")}</label>
                <Dropdown
                    placeholder={t("offers:fields_grid:covering")||''}
                    fluid
                    search
                    style={{width: "300px"}}
                    selection
                    value={data?.coveringType||1}
                    options={coveringTypes()}
                />
            </Form.Field>
            <Form.Field >
                <label>{t("offers:fields_grid:available_sport_kinds")}</label>
                <Dropdown
                    placeholder={t("offers:fields_grid:available_sport_kinds")||''}
                    fluid
                    search
                    multiple
                    style={{width: "500px"}}
                    selection
                    value={data?.sportKinds||[]}
                    options={sportKinds()}
                />
            </Form.Field>
            {parentFields.length > 0 && <Form.Field >
                <label>{t("offers:fields_grid:parent_field")}</label>
                <div style={{fontSize: '12px', lineHeight: '12px'}}>{t("offers:fields_grid:parent_field_hint")}</div>
                <Dropdown
                    placeholder={t("offers:fields_grid:parent_field")||''}
                    clearable
                    style={{width: "300px", marginTop: '10px'}}
                    selection
                    value={data?.parentFieldId||''}
                    options={parentFields.map((f) => {
                        return {
                            key: f.id.toString(),
                            value: f.id,
                            text: f.name
                        }
                    })}
                />
            </Form.Field>}
            <Form.Field>
                <label>{t("offers:fields_grid:images")}</label>
                <Button>{t('offers:fields_grid:upload_images')}</Button>
                <div className="field-images">
                    {data?.images.map((img, index) => {
                        return <div key={index} className="field-image">
                            <div className="tools"></div>
                            <img alt="" src={"/legal-images/" + img}/>
                        </div>
                    })}
                </div>
            </Form.Field>
            
        </Form>
    </div>);
}