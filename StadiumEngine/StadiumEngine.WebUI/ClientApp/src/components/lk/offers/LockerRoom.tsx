import React, {useEffect, useRef, useState} from 'react';
import {getDataTitle, getTitle, StringFormat, validateInputs} from "../../../helpers/utils";
import {useNavigate, useParams} from "react-router-dom";
import {LockerRoomDto} from "../../../models/dto/offers/LockerRoomDto";
import {useInject} from "inversify-hooks";
import {IOffersService} from "../../../services/OffersService";
import {Checkbox, Form} from 'semantic-ui-react'
import {ActionButtons} from "../../common/actions/ActionButtons";
import {t} from "i18next";
import {UpdateLockerRoomCommand} from "../../../models/command/offers/UpdateLockerRoomCommand";
import {LockerRoomGender} from "../../../models/dto/offers/enums/LockerRoomGender";
import {AddLockerRoomCommand} from "../../../models/command/offers/AddLockerRoomCommand";
import {PermissionsKeys} from "../../../static/PermissionsKeys";

export const LockerRoom = () => {
    let {id} = useParams();

    const [data, setData] = useState<LockerRoomDto>({
        gender: LockerRoomGender.Male,
        isActive: true
    } as LockerRoomDto);
    const [isError, setIsError] = useState<boolean>(false);
    const [lockerRoomId, setLockerRoomId] = useState(parseInt(id || "0"))

    const [offersService] = useInject<IOffersService>('OffersService');

    const navigate = useNavigate();

    const fetchLockerRoom = () => {
        if (lockerRoomId > 0) {
            offersService.getLockerRoom(lockerRoomId).then((result: LockerRoomDto) => {
                setData(result);
            }).catch(() => setIsError(true));
        }
    }

    useEffect(() => {
        fetchLockerRoom();
    }, [lockerRoomId])

    useEffect(() => {
        setLockerRoomId(parseInt(id || "0"));
    }, [id])

    useEffect(() => {
        if (data.name !== undefined && data.name !== null) {
            document.title = getDataTitle(data.name);
        } else {
            document.title = getTitle("offers:locker_rooms_tab")
        }
    }, [data])

    const nameInput = useRef<any>();
    const descriptionInput = useRef<any>();


    const saveLockerRoom = () => {
        if (validateInputs([nameInput])) {
            const command: AddLockerRoomCommand = {
                name: nameInput.current?.value,
                description: descriptionInput.current?.value,
                isActive: data.isActive,
                gender: data.gender
            }
            offersService.addLockerRoom(command).then(() => {
                navigate("/lk/offers/locker-rooms");
            })
        }
    }

    const updateLockerRoom = () => {
        if (validateInputs([nameInput])) {
            const command: UpdateLockerRoomCommand = {
                id: lockerRoomId,
                name: nameInput.current?.value,
                description: descriptionInput.current?.value,
                isActive: data.isActive,
                gender: data.gender
            }
            offersService.updateLockerRoom(command).then(() => {
                navigate("/lk/offers/locker-rooms");
            })
        }
    }

    const deleteLockerRoom = () => {
        offersService.deleteLockerRoom(lockerRoomId).then(() => {
            navigate("/lk/offers/locker-rooms");
        })
    }

    const changeIsActive = () => {
        setData({
            ...data,
            isActive: !data.isActive
        });
    }

    const changeGender = (value: LockerRoomGender) => {
        setData({
            ...data,
            gender: value
        });
    }

    return isError ? <span/> : (<div>
        <ActionButtons
            savePermission={id === "new" ? PermissionsKeys.InsertLockerRoom : PermissionsKeys.UpdateLockerRoom}
            deletePermission={PermissionsKeys.DeleteLockerRoom}
            title={id === "new" ? t('offers:locker_rooms_grid:adding') : t('offers:locker_rooms_grid:editing')}
            saveAction={id === "new" ? saveLockerRoom : updateLockerRoom}
            deleteAction={id === "new" ? null : deleteLockerRoom}
            deleteHeader={id === "new" ? null : t('offers:locker_rooms_grid:delete:header')}
            deleteQuestion={id === "new" ? null : StringFormat(t('offers:locker_rooms_grid:delete:question'), data?.name || '')}
        />
        <Form className="locker-room-form">
            <Form.Field style={{marginBottom: 0}}>
                <label>{t("offers:locker_rooms_grid:is_active")}</label>
                <Checkbox toggle checked={data.isActive} onChange={() => changeIsActive()}/>
            </Form.Field>
            <Form.Field>
                <label>{t("offers:locker_rooms_grid:name")}</label>
                <input id="name-input" ref={nameInput} placeholder={t("offers:locker_rooms_grid:name") || ''}
                       defaultValue={data?.name || ''}/>
            </Form.Field>
            <Form.Field>
                <label>{t("offers:locker_rooms_grid:gender")}</label>
                <div className="gender-radio-container">
                    <div className="gender-radio">
                        <Checkbox
                            radio
                            label=''
                            name='genderRadioGroup'
                            value={2}
                            checked={data.gender === LockerRoomGender.Male}
                            onChange={() => changeGender(LockerRoomGender.Male)}
                        />
                        <i className="fa fa-male" aria-hidden="true"/>
                    </div>
                    <div className="gender-radio">
                        <Checkbox
                            radio
                            label=''
                            name='genderRadioGroup'
                            value={1}
                            checked={data.gender === LockerRoomGender.Female}
                            onChange={() => changeGender(LockerRoomGender.Female)}
                        />
                        <i className="fa fa-female" aria-hidden="true"/>
                    </div>
                    <div className="gender-radio">
                        <Checkbox
                            radio
                            label=''
                            name='genderRadioGroup'
                            value={3}
                            checked={data.gender === LockerRoomGender.All}
                            onChange={() => changeGender(LockerRoomGender.All)}
                        />
                        <i className="fa fa-male" aria-hidden="true"/>
                        <i style={{marginLeft: 0}} className="fa fa-female" aria-hidden="true"/>
                    </div>
                </div>
            </Form.Field>
            <Form.Field>
                <label>{t("offers:locker_rooms_grid:description")}</label>
                <textarea id="description-input" ref={descriptionInput} rows={4}
                          placeholder={t("offers:locker_rooms_grid:description") || ''}
                          defaultValue={data?.description || ''}/>
            </Form.Field>
        </Form>
    </div>);
}