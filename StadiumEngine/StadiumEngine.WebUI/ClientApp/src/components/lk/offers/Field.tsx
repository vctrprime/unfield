import React, {useEffect, useRef, useState} from 'react';
import {useNavigate, useParams} from "react-router-dom";
import {useInject} from "inversify-hooks";
import {IOffersService} from "../../../services/OffersService";
import {FieldDto} from "../../../models/dto/offers/FieldDto";
import {getDataTitle, getTitle, StringFormat} from "../../../helpers/utils";
import {ActionButtons} from "../../common/actions/ActionButtons";
import {t} from "i18next";
import {Checkbox, Form} from "semantic-ui-react";

export const Field = () => {
    let { id } = useParams();

    const [data, setData] = useState<FieldDto|null>(null);
    const [isError, setIsError] = useState<boolean>(false);
    const [isActive, setIsActive] = useState<boolean>(false)
    const [fieldId, setFieldId] = useState(parseInt(id||"0"))

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

    useEffect(() => {
        fetchField();
    }, [])

    useEffect(() => {
        if (data !== null) {
            document.title = getDataTitle(data.name);
        }
        else {
            document.title = getTitle("offers:fields_tab")
        }
    }, [data])

    const nameInput = useRef<any>();
    const descriptionInput = useRef<any>();

    const validate = (): boolean => {
        if (
            nameInput.current?.value === undefined ||
            nameInput.current?.value === null ||
            nameInput.current?.value === '') {
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
            <Form.Field>
                <label>{t("offers:fields_grid:description")}</label>
                <textarea id="description-input" ref={descriptionInput} rows={4} placeholder={t("offers:fields_grid:description")||''} defaultValue={data?.description || ''}/>
            </Form.Field>
        </Form>
    </div>);
}