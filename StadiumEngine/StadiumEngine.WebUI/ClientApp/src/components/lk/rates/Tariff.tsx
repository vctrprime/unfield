import React, {useEffect, useRef, useState} from 'react';
import {useNavigate, useParams} from "react-router-dom";
import {TariffDto} from "../../../models/dto/rates/TariffDto";
import {useInject} from "inversify-hooks";
import {IRatesService} from "../../../services/RatesService";
import {getDataTitle, getTitle, StringFormat, validateInputs} from "../../../helpers/utils";
import {ActionButtons} from "../../common/actions/ActionButtons";
import {PermissionsKeys} from "../../../static/PermissionsKeys";
import {t} from "i18next";
import {Checkbox, Form} from "semantic-ui-react";

export const Tariff = () => {
    let {id} = useParams();

    const [data, setData] = useState<TariffDto>({
        isActive: true
    } as TariffDto);
    const [isError, setIsError] = useState<boolean>(false);
    const [tariffId, setTariffId] = useState(parseInt(id || "0"))

    const [ratesService] = useInject<IRatesService>('RatesService');

    const navigate = useNavigate();

    const fetchTariff = () => {
        if (tariffId > 0) {
            ratesService.getTariff(tariffId).then((result: TariffDto) => {
                setData(result);
            }).catch(() => setIsError(true));
        }
    }

    useEffect(() => {
        fetchTariff();
    }, [tariffId])

    useEffect(() => {
        setTariffId(parseInt(id || "0"));
    }, [id])

    useEffect(() => {
        if (data.name !== undefined && data.name !== null) {
            document.title = getDataTitle(data.name);
        } else {
            document.title = getTitle("rates:tariffs_tab")
        }
    }, [data])

    const nameInput = useRef<any>();
    const descriptionInput = useRef<any>();


    const saveTariff = () => {
        if (validateInputs([nameInput])) {
            /*const command: AddTariffCommand = {
                name: nameInput.current?.value,
                description: descriptionInput.current?.value,
                isActive: data.isActive,
            }
            ratesService.addTariff(command).then(() => {
                navigate("/lk/rates/tariffs");
            })*/
        }
    }

    const updateTariff = () => {
        if (validateInputs([nameInput])) {
            /*const command: UpdateTariffCommand = {
                id: tariffId,
                name: nameInput.current?.value,
                description: descriptionInput.current?.value,
                isActive: data.isActive
            }
            ratesService.updateTariff(command).then(() => {
                navigate("/lk/rates/tariffs");
            })*/
        }
    }

    const deleteTariff = () => {
        ratesService.deleteTariff(tariffId).then(() => {
            navigate("/lk/rates/tariffs");
        })
    }

    const changeIsActive = () => {
        setData({
            ...data,
            isActive: !data.isActive
        });
    }

    const toggleDay = (day: string) => {
        setData({
            ...data,
            [day]: !(data[day as keyof TariffDto] as boolean)
        });
    }


    return isError ? <span/> : (<div>
        <ActionButtons
            savePermission={id === "new" ? PermissionsKeys.InsertTariff : PermissionsKeys.UpdateTariff}
            deletePermission={PermissionsKeys.DeleteTariff}
            title={id === "new" ? t('rates:tariffs_grid:adding') : t('rates:tariffs_grid:editing')}
            saveAction={id === "new" ? saveTariff : updateTariff}
            deleteAction={id === "new" ? null : deleteTariff}
            deleteHeader={id === "new" ? null : t('rates:tariffs_grid:delete:header')}
            deleteQuestion={id === "new" ? null : StringFormat(t('rates:tariffs_grid:delete:question'), data?.name || '')}
        />
        <Form className="tariff-form">
            <Form.Field style={{marginBottom: 0}}>
                <label>{t("rates:tariffs_grid:is_active")}</label>
                <Checkbox toggle checked={data.isActive} onChange={() => changeIsActive()}/>
            </Form.Field>
            <Form.Field>
                <label>{t("rates:tariffs_grid:name")}</label>
                <input id="name-input" ref={nameInput} placeholder={t("rates:tariffs_grid:name") || ''}
                       defaultValue={data?.name || ''}/>
            </Form.Field>
            <Form.Field>
                <label>{t("rates:tariffs_grid:description")}</label>
                <textarea id="description-input" ref={descriptionInput} rows={4}
                          placeholder={t("rates:tariffs_grid:description") || ''}
                          defaultValue={data?.description || ''}/>
            </Form.Field>
            <Form.Field>
                <label>{t("rates:tariffs_grid:period")}</label>
            </Form.Field>
            <Form.Field>
                <label>{t("rates:tariffs_grid:days")}</label>
                {['monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday', 'sunday'].map((day) => {
                    return <Checkbox
                        key={day}
                        checked={data[day as keyof TariffDto] as boolean}
                        onChange={(e, data) => toggleDay(day)}
                        label={t(`rates:tariffs_grid:${day}`)}/>
                })}
            </Form.Field>
            <Form.Field>
                <label>{t("rates:tariffs_grid:intervals")}</label>
            </Form.Field>
        </Form>
    </div>);
}