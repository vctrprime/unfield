import React, {useEffect, useRef, useState} from 'react';
import {useNavigate, useParams} from "react-router-dom";
import {useInject} from "inversify-hooks";
import {IOffersService} from "../../../services/OffersService";
import {FieldDto} from "../../../models/dto/offers/FieldDto";
import {getDataTitle, getFieldBasicFormData, getTitle, StringFormat} from "../../../helpers/utils";
import {ActionButtons} from "../../common/actions/ActionButtons";
import {t} from "i18next";
import {Button, Checkbox, Dropdown, Form, Icon} from "semantic-ui-react";
import {FieldCoveringType} from "../../../models/dto/offers/enums/FieldCoveringType";
import {FieldSportKind} from "../../../models/dto/offers/enums/FieldSportKind";

interface PassedImage {
    path?: string;
    formFile?: File;
    isDeleted: boolean;
}

//toDo подумать что еще можно вывести в таблицу

export const Field = () => {
    let { id } = useParams();

    const [data, setData] = useState<FieldDto>({
        images: [] as string[],
        isActive: true,
        coveringType: FieldCoveringType.Natural,
        sportKinds: [] as FieldSportKind[],
        parentFieldId: null
    } as FieldDto);
    
    const [passedImages, setPassedImages] = useState<PassedImage[]>([])
    const [isError, setIsError] = useState<boolean>(false);
    const [fieldId, setFieldId] = useState(parseInt(id||"0"));
    
    const [parentFields, setParentFields] = useState<FieldDto[]>([]);

    const [offersService] = useInject<IOffersService>('OffersService');

    const navigate = useNavigate();

    const fetchField = () => {
        if (fieldId > 0) {
            offersService.getField(fieldId).then((result: FieldDto) => {
                setData(result);
                setPassedImages(result.images.map((image) => {
                    return {
                        path: image
                    } as PassedImage
                }));
            }).catch(() => setIsError(true));
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

    const sportKindsAll = () => {
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

    const changeSportKinds = (e : any, { value }: any) => {
        setData({
            ...data,
            sportKinds: value
        });
    }
    
    const changeParentFieldId = (e : any, { value }: any) => {
        setData({
            ...data,
            parentFieldId: value
        });
    }

    const nameInput = useRef<any>();
    const descriptionInput = useRef<any>();
    const widthInput = useRef<any>();
    const lengthInput = useRef<any>();
    const hiddenUploadInput = useRef<any>(null)

    const validate = (): boolean => {
        let hasErrors = false;
        const inputs = [nameInput, widthInput, lengthInput];
        
        inputs.forEach((input) => {
            if (!input.current?.value) {
                input.current.style.border = "1px solid red";
                setTimeout(() => {
                    input.current.style.border = "";
                }, 2000);
                hasErrors = true;
            }
        })
        return !hasErrors;
    }

    const saveField = () => {
        saveAction();
    }

    const updateField = () => {
        saveAction();
    }
    
    const saveAction = () => {
        if (validate()) {
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
    
    const uploadImages = (e: any) => {
        const files = Array.from(e.target.files);
        const newImages = files.map((file: any) => {
            return {
                path: URL.createObjectURL(file),
                formFile: file
            } as PassedImage
        })
        setPassedImages(oldImages => [...oldImages,...newImages] );
    }
    
    const changeImageOrder = (currentIndex: number, direction: number) => {
        const currentImage = passedImages[currentIndex];
        const directionNextImage = passedImages[currentIndex + direction];
        
        setPassedImages(passedImages.map((item,i)=> {
            if(currentIndex === i){
                return directionNextImage;
            }
            if (currentIndex + direction === i) {
                return currentImage;
            }
            return item;
        }));
    }

    const toggleImageDeleted = (index: number) => {
        setPassedImages(passedImages.map((item,i)=> {
            if(index === i){
                item.isDeleted = !item.isDeleted;
            }
            return item;
        }));
    }

    return isError ? <span/> : (<div>
        <ActionButtons
            title={id === "new" ? t('offers:fields_grid:adding') : t('offers:fields_grid:editing')}
            saveAction={id === "new" ? saveField : updateField}
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
            <Form.Field >
                <label>{t("offers:fields_grid:available_sport_kinds")}</label>
                <Dropdown
                    placeholder={t("offers:fields_grid:available_sport_kinds")||''}
                    fluid
                    search
                    multiple
                    style={{width: "500px"}}
                    selection
                    onChange={changeSportKinds}
                    value={data.sportKinds}
                    options={sportKindsAll()}
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
            {data?.childNames?.length > 0 &&
                <div style={{marginBottom: '1em'}}>{t("offers:fields_grid:child_fields_list")}: <br/><span style={{ fontWeight: 'bold'}}>{data.childNames.join(', ')}</span></div>}
            <Form.Field>
                <label>{t("offers:fields_grid:images")}</label>
                <Button onClick={() => hiddenUploadInput?.current?.click()}>{t('offers:fields_grid:upload_images')}</Button>
                <input ref={hiddenUploadInput} style={{display: 'none'}} type="file" multiple onChange={uploadImages} />
                <div className="field-images">
                    {passedImages.map((img, index) => {
                        const src = img.formFile !== undefined ? img.path : "/legal-images/" + img.path;
                        
                        return <div key={index} className="field-image">
                            <div className="tools">
                                <div className="change-order-buttons" title={t("offers:fields_grid:change_images_order")||''}>
                                    {index !== 0 ? <Icon name='angle left' onClick={() => changeImageOrder(index, -1)}/> :
                                        <Icon name='angle left' style={{ opacity: 0.4, pointerEvents: 'none'}}/>
                                    }
                                    {index !== passedImages.length - 1 ? <Icon name='angle right' onClick={() => changeImageOrder(index, 1)}/> :
                                        <Icon name='angle right' style={{ opacity: 0.4, pointerEvents: 'none'}}/>
                                    }
                                 </div>
                                <div className="remove-buttons">
                                    {!img.isDeleted ? <Icon title={t("offers:fields_grid:delete_image")||''} onClick={() => toggleImageDeleted(index)} name='trash alternate' /> 
                                        : <Icon title={t("offers:fields_grid:restore_image")||''} onClick={() => toggleImageDeleted(index)} name='redo' />}
                                    
                                </div>
                            </div>
                            <img style={ img.isDeleted ? { opacity: 0.5} : {}} alt="" src={src}/>
                        </div>
                    })}
                </div>
            </Form.Field>
            
        </Form>
    </div>);
}