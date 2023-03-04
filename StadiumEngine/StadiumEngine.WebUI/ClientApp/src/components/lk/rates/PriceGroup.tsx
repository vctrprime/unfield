import React, {useEffect, useRef, useState} from 'react';
import {getDataTitle, getTitle, StringFormat, validateInputs} from "../../../helpers/utils";
import {useNavigate, useParams} from "react-router-dom";
import {PriceGroupDto} from "../../../models/dto/rates/PriceGroupDto";
import {useInject} from "inversify-hooks";
import {Checkbox, Form} from 'semantic-ui-react'
import {ActionButtons} from "../../common/actions/ActionButtons";
import {t} from "i18next";
import {UpdatePriceGroupCommand} from "../../../models/command/rates/UpdatePriceGroupCommand";
import {AddPriceGroupCommand} from "../../../models/command/rates/AddPriceGroupCommand";
import {IRatesService} from "../../../services/RatesService";
import {PermissionsKeys} from "../../../static/PermissionsKeys";

export const PriceGroup = () => {
    let { id } = useParams();
    
    const [data, setData] = useState<PriceGroupDto>({
        isActive: true
    } as PriceGroupDto);
    const [isError, setIsError] = useState<boolean>(false);
    const [priceGroupId, setPriceGroupId] = useState(parseInt(id||"0"))
    
    const [ratesService] = useInject<IRatesService>('RatesService');
    
    const navigate = useNavigate();

    const fetchPriceGroup = () => {
        if (priceGroupId > 0) {
            ratesService.getPriceGroup(priceGroupId).then((result: PriceGroupDto) => {
                setData(result);
            }).catch(() => setIsError(true));
        }
    }

    useEffect(() => {
        fetchPriceGroup();
    }, [])
    
    useEffect(() => {
        if (data.name !== undefined && data.name !== null) {
            document.title = getDataTitle(data.name);
        }
        else {
            document.title = getTitle("rates:price_groups_tab")
        }
    }, [data])

    const nameInput = useRef<any>();
    const descriptionInput = useRef<any>();

    
    const savePriceGroup = () => {
        if (validateInputs([nameInput])) {
            const command: AddPriceGroupCommand = {
                name: nameInput.current?.value,
                description: descriptionInput.current?.value,
                isActive: data.isActive,
            }
            ratesService.addPriceGroup(command).then(() => {
                navigate("/lk/rates/price-groups");
            })
        }
    }
    
    const updatePriceGroup = () => {
        if (validateInputs([nameInput])) {
            const command: UpdatePriceGroupCommand = {
                id: priceGroupId,
                name: nameInput.current?.value,
                description: descriptionInput.current?.value,
                isActive: data.isActive
            }
            ratesService.updatePriceGroup(command).then(() => {
                navigate("/lk/rates/price-groups");
            })
        }
    }
    
    const deletePriceGroup = () => {
        ratesService.deletePriceGroup(priceGroupId).then(() => {
            navigate("/lk/rates/price-groups");
        })
    }

    const changeIsActive = () => {
        setData({
            ...data,
            isActive: !data.isActive
        });
    }

    
    return isError ? <span/> : (<div>
            <ActionButtons
                savePermission={id === "new" ? PermissionsKeys.InsertPriceGroup : PermissionsKeys.UpdatePriceGroup}
                deletePermission={PermissionsKeys.DeletePriceGroup}
                title={id === "new" ? t('rates:price_groups_grid:adding') : t('rates:price_groups_grid:editing')}
                saveAction={id === "new" ? savePriceGroup : updatePriceGroup}
                deleteAction={id === "new" ? null : deletePriceGroup}
                deleteHeader={id === "new" ? null : t('rates:price_groups_grid:delete:header')}
                deleteQuestion={id === "new" ? null : StringFormat(t('rates:price_groups_grid:delete:question'), data?.name||'')}
            />
             <Form className="price-group-form">
                 <Form.Field style={{marginBottom: 0}}>
                     <label>{t("rates:price_groups_grid:is_active")}</label>
                     <Checkbox toggle checked={data.isActive} onChange={() => changeIsActive()}/>
                 </Form.Field>
                <Form.Field>
                    <label>{t("rates:price_groups_grid:name")}</label>
                    <input id="name-input" ref={nameInput} placeholder={t("rates:price_groups_grid:name")||''} defaultValue={data?.name || ''}/>
                </Form.Field>
                <Form.Field>
                    <label>{t("rates:price_groups_grid:description")}</label>
                    <textarea id="description-input" ref={descriptionInput} rows={4} placeholder={t("rates:price_groups_grid:description")||''} defaultValue={data?.description || ''}/>
                </Form.Field>
                 {data?.fieldNames?.length > 0 &&
                     <div style={{marginBottom: '1em'}}>{t("rates:price_groups_grid:fields_list")}: <br/><span style={{ fontWeight: 'bold'}}>{data.fieldNames.join(', ')}</span></div>}
            </Form>
        </div>);
}