import React, {useEffect, useRef, useState} from 'react';
import {useNavigate, useParams} from "react-router-dom";
import {SportKind} from "../../../models/dto/offers/enums/SportKind";
import {InventoryDto} from "../../../models/dto/offers/InventoryDto";
import {Currency} from "../../../models/dto/offers/enums/Currency";
import {ImageFile} from "../../../models/common/ImageFile";
import {useInject} from "inversify-hooks";
import {IOffersService} from "../../../services/OffersService";
import {getDataTitle, getInventoryBasicFormData, getTitle, StringFormat, validateInputs} from "../../../helpers/utils";
import {ActionButtons} from "../../common/actions/ActionButtons";
import {t} from "i18next";
import {Checkbox, Form} from "semantic-ui-react";
import {SportKindSelect} from "../common/SportKindSelect";
import {ImagesForm} from "../common/ImagesForm";
import {CurrencySelect} from "../common/CurrencySelect";
import {PermissionsKeys} from "../../../static/PermissionsKeys";

export const Inventory = () => {
    let {id} = useParams();

    const [data, setData] = useState<InventoryDto>({
        images: [] as string[],
        isActive: true,
        currency: Currency.Rub,
        sportKinds: [] as SportKind[]
    } as InventoryDto);

    const [passedImages, setPassedImages] = useState<ImageFile[]>([])
    const [isError, setIsError] = useState<boolean>(false);
    const [inventoryId, setInventoryId] = useState(parseInt(id || "0"));

    const [offersService] = useInject<IOffersService>('OffersService');

    const navigate = useNavigate();

    const fetchInventory = () => {
        if (inventoryId > 0) {
            offersService.getInventory(inventoryId).then((result: InventoryDto) => {
                setData(result);
                setPassedImages(result.images.map((image) => {
                    return {
                        path: image
                    } as ImageFile
                }));
            }).catch(() => setIsError(true));
        }
    }

    useEffect(() => {
        fetchInventory();
    }, [inventoryId])

    useEffect(() => {
        setInventoryId(parseInt(id || "0"));
    }, [id])

    useEffect(() => {
        if (data.name !== undefined && data.name !== null) {
            document.title = getDataTitle(data.name);
        } else {
            document.title = getTitle("offers:inventories_tab")
        }
    }, [data])

    const changeIsActive = () => {
        setData({
            ...data,
            isActive: !data.isActive
        });
    }

    const nameInput = useRef<any>();
    const descriptionInput = useRef<any>();
    const priceInput = useRef<any>();
    const quantityInput = useRef<any>();

    const saveAction = () => {
        if (validateInputs([nameInput, priceInput, quantityInput])) {
            data.name = nameInput.current?.value;
            data.description = descriptionInput.current?.value;
            data.price = priceInput.current?.value;
            data.quantity = quantityInput.current?.value;

            const form = getInventoryBasicFormData(data);

            const actualImages = passedImages.filter(i => !i.isDeleted);

            for (let i = 0; i < actualImages.length; i++) {
                if (actualImages[i].formFile === undefined) {
                    form.append('images[' + i + '].path', actualImages[i].path || '');
                    form.append('images[' + i + '].formFile', '');
                } else {
                    form.append('images[' + i + '].path', '');
                    form.append('images[' + i + '].formFile', actualImages[i].formFile || '');
                }
            }
            if (id === "new") {
                offersService.addInventory(form).then(() => {
                    navigate("/offers/inventories");
                });
            } else {
                offersService.updateInventory(form).then(() => {
                    navigate("/offers/inventories");
                });
            }

        }
    }

    const deleteInventory = () => {
        offersService.deleteInventory(inventoryId).then(() => {
            navigate("/offers/inventories");
        })
    }

    return isError ? <span/> : (<div>
        <ActionButtons
            savePermission={id === "new" ? PermissionsKeys.InsertInventory : PermissionsKeys.UpdateInventory}
            deletePermission={PermissionsKeys.DeleteInventory}
            title={id === "new" ? t('offers:inventories_grid:adding') : t('offers:inventories_grid:editing')}
            saveAction={saveAction}
            deleteAction={id === "new" ? null : deleteInventory}
            deleteHeader={id === "new" ? null : t('offers:inventories_grid:delete:header')}
            deleteQuestion={id === "new" ? null : StringFormat(t('offers:inventories_grid:delete:question'), data.name || '')}
        />
        <Form className="inventory-form">
            <Form.Field style={{marginBottom: 0}}>
                <label>{t("offers:inventories_grid:is_active")}</label>
                <Checkbox toggle checked={data.isActive} onChange={() => changeIsActive()}/>
            </Form.Field>
            <Form.Field>
                <label>{t("offers:inventories_grid:name")}</label>
                <input id="name-input" ref={nameInput} placeholder={t("offers:inventories_grid:name") || ''}
                       defaultValue={data.name || ''}/>
            </Form.Field>

            <div className="price-qty-container">
                <Form.Field>
                    <label>{t("offers:inventories_grid:price")}</label>
                    <input id="price-input" type="number" ref={priceInput}
                           placeholder={t("offers:inventories_grid:price") || ''} defaultValue={data.price || ''}/>
                </Form.Field>
                <div className="delimiter"/>
                <CurrencySelect data={data} setData={setData}/>
            </div>

            <Form.Field>
                <label>{t("offers:inventories_grid:quantity")}</label>
                <input id="quantity-input" style={{width: '200px'}} type="number" ref={quantityInput}
                       placeholder={t("offers:inventories_grid:quantity") || ''} defaultValue={data.quantity || ''}/>
            </Form.Field>

            <Form.Field>
                <label>{t("offers:inventories_grid:description")}</label>
                <textarea id="description-input" ref={descriptionInput} rows={4}
                          placeholder={t("offers:inventories_grid:description") || ''}
                          defaultValue={data.description || ''}/>
            </Form.Field>

            <SportKindSelect data={data} setData={setData}/>
            <ImagesForm passedImages={passedImages} setPassedImages={setPassedImages}/>
        </Form>
    </div>);
}