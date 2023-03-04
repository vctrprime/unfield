import React, {useEffect, useRef, useState} from 'react';
import {useNavigate, useParams} from "react-router-dom";
import {useInject} from "inversify-hooks";
import {IOffersService} from "../../../services/OffersService";
import {FieldDto} from "../../../models/dto/offers/FieldDto";
import {getDataTitle, getFieldBasicFormData, getTitle, StringFormat, validateInputs} from "../../../helpers/utils";
import {ActionButtons} from "../../common/actions/ActionButtons";
import {t} from "i18next";
import {Checkbox, Dropdown, Form} from "semantic-ui-react";
import {FieldCoveringType} from "../../../models/dto/offers/enums/FieldCoveringType";
import {SportKind} from "../../../models/dto/offers/enums/SportKind";
import {ImageFile} from "../../../models/common/ImageFile";
import {ImagesForm} from "../common/ImagesForm";
import {SportKindSelect} from "../common/SportKindSelect";
import {IRatesService} from "../../../services/RatesService";
import {PriceGroupDto} from "../../../models/dto/rates/PriceGroupDto";
import {PermissionsKeys} from "../../../static/PermissionsKeys";


export const Field = () => {
    let { id } = useParams();

    const [data, setData] = useState<FieldDto>({
        images: [] as string[],
        isActive: true,
        coveringType: FieldCoveringType.Natural,
        sportKinds: [] as SportKind[],
        parentFieldId: null
    } as FieldDto);
    
    const [passedImages, setPassedImages] = useState<ImageFile[]>([])
    const [isError, setIsError] = useState<boolean>(false);
    const [fieldId, setFieldId] = useState(parseInt(id||"0"));
    
    const [parentFields, setParentFields] = useState<FieldDto[]>([]);
    const [priceGroups, setPriceGroups] = useState<PriceGroupDto[]>([]);

    const [offersService] = useInject<IOffersService>('OffersService');
    const [ratesService] = useInject<IRatesService>('RatesService');

    const navigate = useNavigate();

    const fetchField = () => {
        if (fieldId > 0) {
            offersService.getField(fieldId).then((result: FieldDto) => {
                setData(result);
                setPassedImages(result.images.map((image) => {
                    return {
                        path: image
                    } as ImageFile
                }));
            }).catch(() => setIsError(true));
        }
    }
    
    const fetchParentFields = () => {
        offersService.getFields().then((result: FieldDto[]) => {
            setParentFields(result.filter(f => f.parentFieldId === null && f.id !== fieldId));
        })
    }

    const fetchPriceGroups = () => {
        ratesService.getPriceGroups().then((result: PriceGroupDto[]) => {
            setPriceGroups(result.filter(f => f.isActive));
        })
    }

    useEffect(() => {
        fetchField();
        fetchParentFields();
        fetchPriceGroups();
    }, [])

    useEffect(() => {
        if (data.name !== undefined && data.name !== null) {
            document.title = getDataTitle(data.name);
        }
        else {
            document.title = getTitle("offers:fields_tab")
        }
    }, [data])
    
    const coveringTypesAll = () => {
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
    
    const changeIsActive = () => {
        setData({
            ...data,
            isActive: !data.isActive
        });
    }

    const changeCoveringType = (e : any, { value }: any) => {
        setData({
            ...data,
            coveringType: value
        });
    }
    
    const changeParentFieldId = (e : any, { value }: any) => {
        setData({
            ...data,
            parentFieldId: value
        });
    }

    const changePriceGroupId = (e : any, { value }: any) => {
        setData({
            ...data,
            priceGroupId: value
        });
    }

    const nameInput = useRef<any>();
    const descriptionInput = useRef<any>();
    const widthInput = useRef<any>();
    const lengthInput = useRef<any>();
    
    const saveAction = () => {
        if (validateInputs([nameInput, widthInput, lengthInput])) {
            data.name = nameInput.current?.value;
            data.description = descriptionInput.current?.value;
            data.width = widthInput.current?.value;
            data.length = lengthInput.current?.value;

            const form = getFieldBasicFormData(data);
            
            const actualImages = passedImages.filter(i => !i.isDeleted);

            for (let i = 0; i < actualImages.length; i++) {
                if (actualImages[i].formFile === undefined) {
                    form.append('images['+i+'].path', actualImages[i].path||'');
                    form.append('images['+i+'].formFile', '');
                }
                else {
                    form.append('images['+i+'].path', '');
                    form.append('images['+i+'].formFile', actualImages[i].formFile||'');
                }
            }
            if (id === "new") {
                offersService.addField(form).then(() => {
                    navigate("/lk/offers/fields");
                });
            }
            else {
                offersService.updateField(form).then(() => {
                    navigate("/lk/offers/fields");
                });
            }
            
        }
    }

    const deleteField = () => {
        offersService.deleteField(fieldId).then(() => {
            navigate("/lk/offers/fields");
        })
    }
    
    return isError ? <span/> : (<div>
        <ActionButtons
            savePermission={id === "new" ? PermissionsKeys.InsertField : PermissionsKeys.UpdateField}
            deletePermission={PermissionsKeys.DeleteField}
            title={id === "new" ? t('offers:fields_grid:adding') : t('offers:fields_grid:editing')}
            saveAction={saveAction}
            deleteAction={id === "new" || data?.childNames?.length > 0 ? null : deleteField}
            deleteHeader={id === "new" || data?.childNames?.length > 0 ? null : t('offers:fields_grid:delete:header')}
            deleteQuestion={id === "new" || data?.childNames?.length > 0 ? null : StringFormat(t('offers:fields_grid:delete:question'), data.name||'')}
        />
        <Form className="field-form">
            <Form.Field style={{marginBottom: 0}}>
                <label>{t("offers:fields_grid:is_active")}</label>
                <Checkbox toggle checked={data.isActive} onChange={() => changeIsActive()}/>
            </Form.Field>
            <Form.Field>
                <label>{t("offers:fields_grid:name")}</label>
                <input id="name-input" ref={nameInput} placeholder={t("offers:fields_grid:name")||''} defaultValue={data.name || ''}/>
            </Form.Field>
            <Form.Field >
                <label>{t("offers:fields_grid:size")}</label>
                <div className="field-size-cont">
                    <input className="field-size-input" type="number" id="length-input" ref={lengthInput} placeholder={t("offers:fields_grid:length")||''} defaultValue={data.length || ''}/>
                    X
                    <input className="field-size-input" type="number" id="width-input" ref={widthInput} placeholder={t("offers:fields_grid:width")||''} defaultValue={data.width || ''}/>
                </div>
            </Form.Field>
            <Form.Field>
                <label>{t("offers:fields_grid:description")}</label>
                <textarea id="description-input" ref={descriptionInput} rows={4} placeholder={t("offers:fields_grid:description")||''} defaultValue={data.description || ''}/>
            </Form.Field>
            <Form.Field >
                <label>{t("offers:fields_grid:covering")}</label>
                <Dropdown
                    placeholder={t("offers:fields_grid:covering")||''}
                    fluid
                    search
                    style={{width: "300px"}}
                    selection
                    onChange={changeCoveringType}
                    value={data.coveringType}
                    options={coveringTypesAll()}
                />
            </Form.Field>
            <SportKindSelect data={data} setData={setData} />
            {parentFields.length > 0 && <Form.Field >
                <label>{t("offers:fields_grid:parent_field")}</label>
                <div style={{fontSize: '12px', lineHeight: '12px'}}>{t("offers:fields_grid:parent_field_hint")}</div>
                <Dropdown
                    placeholder={t("offers:fields_grid:parent_field")||''}
                    clearable
                    style={{width: "300px", marginTop: '10px'}}
                    selection
                    onChange={changeParentFieldId}
                    value={data.parentFieldId||undefined}
                    options={parentFields.map((f) => {
                        return {
                            key: f.id.toString(),
                            value: f.id,
                            text: f.name
                        }
                    })}
                />
            </Form.Field>}
            {priceGroups.length > 0 && <Form.Field >
                <label>{t("offers:fields_grid:price_group")}</label>
                <div style={{fontSize: '12px', lineHeight: '12px'}}>{t("offers:fields_grid:price_group_hint")}</div>
                <Dropdown
                    placeholder={t("offers:fields_grid:price_group")||''}
                    clearable
                    style={{width: "300px", marginTop: '10px'}}
                    selection
                    onChange={changePriceGroupId}
                    value={data.priceGroupId||undefined}
                    options={priceGroups.map((f) => {
                        return {
                            key: f.id.toString(),
                            value: f.id,
                            text: f.name
                        }
                    })}
                />
            </Form.Field>}
            {data?.childNames?.length > 0 &&
                <div style={{marginBottom: '1em'}}>{t("offers:fields_grid:child_fields_list")}: <br/><span style={{ fontWeight: 'bold'}}>{data.childNames.join(', ')}</span></div>}
            <ImagesForm passedImages={passedImages} setPassedImages={setPassedImages} />
        </Form>
    </div>);
}