import React, {useEffect, useRef, useState} from "react";
import {useLocation, useNavigate, useParams} from "react-router-dom";
import {
    BookingCheckoutDto,
    BookingCheckoutDurationAmountDto,
    BookingCheckoutInventoryDto
} from "../../models/dto/booking/BookingCheckoutDto";
import {useInject} from "inversify-hooks";
import {IBookingFormService} from "../../services/BookingFormService";
import {Col, Container} from "reactstrap";
import '../../css/booking/BookingCheckout.scss';
import {PromoCodeDto} from "../../models/dto/rates/TariffDto";
import {PromoCodeType} from "../../models/dto/rates/enums/PromoCodeType";
import {getDurationText, getTitle} from "../../helpers/utils";
import {Button, Dropdown, Form, Icon, Input} from "semantic-ui-react";
import {t} from "i18next";
import {SportKind} from "../../models/dto/offers/enums/SportKind";
import noImage from "../../img/no-image.png";
import {
    FillBookingDataCommandCost,
    FillBookingDataCommandInventory
} from "../../models/command/booking/FillBookingDataCommand";
import i18n from "../../i18n/i18n";
import PhoneInput from "react-phone-input-2";
import ru from 'react-phone-input-2/lang/ru.json'
import {BookingCancelModal} from "./BookingCancelModal";

type CheckoutLocationState = {
    bookingNumber: string;
    backPath: string;
}

type CheckoutDiscount = {
    duration: number,
    value: number
}

type PromoMessage = {
    success: boolean;
    message: string;
}

interface InventoryRowProps {
    inventory: BookingCheckoutInventoryDto;
    selectedInventories: SelectedInventory[];
    setSelectedInventories: any;
    
}

interface SelectedInventory {
    id: number;
    quantity: number;
    price: number;
}

const InventoryRow = (props: InventoryRowProps) => {
    let selectedInventory = props.selectedInventories.find( x => x.id === props.inventory.id);
    
    const addInventory = () => {
        let copy: SelectedInventory[] = [];
        
        if ( selectedInventory) {
            if (selectedInventory.quantity < props.inventory.quantity) {
                selectedInventory.quantity++;
                copy = [...props.selectedInventories.filter( i => i !== selectedInventory)]
            }
            else {
                return;
            }
        }
        else {
            selectedInventory = {
                id: props.inventory.id,
                quantity: 1,
                price: props.inventory.price
            }
            copy = [...props.selectedInventories];
        }
        
        copy.push(selectedInventory);
        props.setSelectedInventories(copy);
    }
    
    const removeInventory = () => {
        if (selectedInventory) {
            selectedInventory.quantity -= 1;
            const copy = [...props.selectedInventories.filter( i => i !== selectedInventory)]
            if (selectedInventory.quantity > 0) {
                copy.push(selectedInventory);
            }
            props.setSelectedInventories(copy);
        }
    }
    
    return <div
        style={selectedInventory ? {backgroundColor: 'rgba(245, 245, 245, 1)'} : {} }
        className="booking-checkout-inventory-row">
        <div className="booking-checkout-inventory-row-block" style={{justifyContent: 'flex-start'}}>
            {props.inventory.image ?
                <img src={"/legal-images/" + props.inventory.image}/> :
                <img src={noImage}/>
            }
            <div className="booking-checkout-inventory-row-name" title={t("common:available_qty")||''}>{props.inventory.name} ({props.inventory.quantity})</div>
        </div>
        <div className="booking-checkout-inventory-row-block" style={{justifyContent: 'flex-end'}}>
            <div className="booking-checkout-inventory-row-price">{props.inventory.price} {t("booking:checkout:rub")}/{t("common:hour_long")}</div>
            
            <div className="booking-checkout-inventory-row-button">
                <Icon onClick={() => removeInventory()} name='minus circle' style={{ color: '#CD5C5C', opacity: selectedInventory ? 1 : 0.5}} /> 
                <span>{selectedInventory?.quantity || 0}</span>
                <Icon onClick={() => addInventory()} name='add circle' style={{ color: '#3CB371', marginLeft: '5px', opacity: (selectedInventory?.quantity||0) < props.inventory.quantity ? 1 : 0.5}} />
            </div>
        </div>
    </div>
}

export const BookingCheckout = () => {
    document.title = getTitle("booking:checkout_title");
    
    const location = useLocation();
    const params = useParams();
    
    let bookingNumber = (location.state as CheckoutLocationState)?.bookingNumber || params["bookingNumber"]
    let backPath = (location.state as CheckoutLocationState)?.backPath || '/booking';
    
    const [bookingFormService] = useInject<IBookingFormService>('BookingFormService');
    
    const [data, setData] = useState<BookingCheckoutDto|null>(null);
    
    const [promo, setPromo] = useState<PromoCodeDto|null>(null);
    const [promoMessage, setPromoMessage] = useState<PromoMessage|null>(null);
    const [discounts, setDiscounts] = useState<CheckoutDiscount[]>([]);

    const promoInput = useRef<any>();
    
    const [selectedDuration, setSelectedDuration] = useState<number>(1);
    const [selectedInventories, setSelectedInventories] = useState<SelectedInventory[]>([]);
    
    const navigate = useNavigate();
    
    useEffect(() => {
        bookingFormService.getBookingCheckout(bookingNumber as string).then((response: BookingCheckoutDto) => {
            setData(response);
        })
            .catch((error) => {
                navigate(backPath);
            })
    }, [])
    
    useEffect(() => {
        calculateDiscounts();
    }, [data, promo])
    
    
    const calculateDiscounts = () => {
        if (promo == null) {
            setDiscounts([]);
            return;
        }
        
        const calculatedDiscounts = [] as CheckoutDiscount[];
        data?.durationAmounts.map((a: BookingCheckoutDurationAmountDto) => {
            let discountValue = 0;
            switch (promo.type) {
                case PromoCodeType.Fixed:
                    discountValue = promo.value;
                    break;
                case PromoCodeType.Percent:
                    discountValue = a.value * (promo.value/100);
                    break;
            }
            calculatedDiscounts.push({
                duration: a.duration,
                value: discountValue
            });
        })
        
        setDiscounts(calculatedDiscounts);
    }
    
    const getFieldAmountValue = () => {
        if (data) {
            const discount = discounts.find( x=> x.duration == selectedDuration);
            const durationAmount = data.durationAmounts.find( x => x.duration === selectedDuration);

            if (durationAmount) {
                if (discount && discount.value > 0) {
                    return durationAmount.value - discount.value;
                }

                return durationAmount.value;
            }
            
            return 0;
        }
        
        return 0;
    }
    
    const getFieldAmount = () => {
        if (data) {
            const amount = getFieldAmountValue();
            const durationAmount = data.durationAmounts.find( x => x.duration === selectedDuration);
            
            if (durationAmount) {
                if (durationAmount.value > amount) {
                    return <span style={{fontWeight: 'bold'}}>{amount} <span style={{textDecoration: 'line-through'}}>{durationAmount.value}</span> руб.</span>
                }
                
                return <span style={{fontWeight: 'bold'}}>{amount} руб.</span>
            }
            
            return <span/>;
        }
        return <span/>;
    }
    
    const getInventoryAmount = () => {
        let result = 0;
        selectedInventories.map((inv) => {
            result += (inv.price * inv.quantity * selectedDuration);
        });
        
        return result;
    }
    
    const getTotalAmount = () => {
        return getFieldAmountValue() + getInventoryAmount();
    }

    const [phoneNumber, setPhoneNumber] = useState<string | undefined>();
    const [name, setName] = useState<string | undefined>();
    
    const confirmButtonDisabled = () => {
        return !((name?.length || 0 ) > 1 && phoneNumber?.length === 11);
    }

    const [cancelModal, setCancelModal] = useState<boolean>(false)
    
    const inventories = data?.durationInventories.find( x => x.duration === selectedDuration)?.inventories || [];
    
    useEffect(() => {
        const copySelectedInventories: SelectedInventory[] = [];
        selectedInventories.forEach((i) => {
            const inventory = inventories.find( x => x.id === i.id);
            if (inventory) {
                if (i.quantity > inventory.quantity) {
                    i.quantity = inventory.quantity;
                }
                copySelectedInventories.push(i);
            }
        })
        setSelectedInventories(copySelectedInventories);
    }, [selectedDuration])
    
    return data === null  ? null :  <Container className="booking-checkout-container">
        <BookingCancelModal backPath={backPath} openModal={cancelModal} setOpenModal={setCancelModal} bookingNumber={bookingNumber} />
        <Form style={{paddingBottom: '10px', minHeight: 'calc(100vh - 30px)'}}>
            <div className="booking-checkout-header">
                <span>№ {data.bookingNumber}</span>
                <span>{data.day}</span>
            </div>
            <div className="booking-checkout-stadium">
                {data.stadiumName}
            </div>
            <div className="booking-checkout-field">
                {data.field.images.length ? 
                    <img src={"/legal-images/" + data.field.images[0]}/>  : <span/>
                }
                <div className="booking-checkout-field-text">
                    <div className="booking-checkout-field-name">{data.field.name}</div>
                    <div className="booking-checkout-field-description">{data.field.description}</div>
                    <div className="booking-checkout-field-sports">
                        {data.field.sportKinds.length === 0 ?
                            <span style={{paddingLeft: '10px', fontSize: '12px', fontWeight: "bold"}}>{t("booking:field_card:no_sports")}</span> :
                            data.field.sportKinds.map((s, i) => {
                                const value = SportKind[s];
                                const text = t("offers:sports:" + value.toLowerCase());

                                return <div style={ i === 0 ? { marginLeft: 0} : {}} key={i} className="field-sport">{text}</div>;
                            })}
                    </div>
                </div>
            </div>
            <div className="booking-checkout-durations">
                <span style={{ fontSize: '16px'}}>c <span style={{fontWeight: 'bold'}}>{data.pointPrices[0].displayStart}</span> {t("booking:checkout:at")}  &nbsp;</span> <Dropdown
                    fluid
                    style={{width: "115px"}}
                    selection
                    onChange={(e: any, {value}: any) => setSelectedDuration(value)}
                    value={selectedDuration}
                    options={data ? data.durationAmounts.map((a) => {
                        return {
                            key: a.duration,
                            value: a.duration,
                            text: getDurationText(a.duration)
                        }
                    }) : []}
                />
            </div>
        <div className="booking-checkout-amount">
            {t("booking:checkout:amount_field")}&nbsp; {getFieldAmount()}
        </div>
        <Form.Field className="booking-checkout-promo">
                <input
                    placeholder={t("booking:checkout:promo_placeholder")||''}
                    disabled={promo !== null}
                    ref={promoInput}/>
                <Icon
                    name='check'
                    disabled={promo !== null}
                    onClick={() => {

                        if (promoInput.current?.value?.length === 0) {
                            setPromoMessage({
                                message: t("booking:checkout:promo_need"),
                                success: false
                            });
                            return;
                        }

                        bookingFormService.checkPromoCode( data?.tariffId, promoInput.current?.value).then((result) => {
                            const tariffPromo = result;
                            setPromoMessage(tariffPromo ? {
                                message: t("booking:checkout:promo_success"),
                                success: true
                            }: {
                                message: t("booking:checkout:promo_not_found"),
                                success: false
                            });

                            setPromo(tariffPromo ? tariffPromo : null);
                        }).catch(() => {
                            setPromo(null);
                        })
                    }} />
                <Icon
                    name='repeat'
                    disabled={promo === null}
                    onClick={() => {
                        setPromoMessage(null)
                        setPromo(null);
                        promoInput.current.value = '';
                    }} />
            </Form.Field>
            {promoMessage && <label className="booking-checkout-promo-message" style={promoMessage.success ? { color: '#3CB371'} : {color: '#CD5C5C'}}>{promoMessage.message}</label>}
            {inventories.length > 0 && 
                <div className="booking-checkout-inventories">
                    <div className="booking-checkout-inventories-title">{t("booking:checkout:inventory_header")}</div>
                    {inventories.map((inv, i) => {
                        return <InventoryRow selectedInventories={selectedInventories} key={i}  inventory={inv} setSelectedInventories={setSelectedInventories}/>
                        })}
                    <div className="booking-checkout-amount">{t("booking:checkout:amount_inventory")} &nbsp;<span style={{fontWeight: 'bold'}}>{getInventoryAmount()} {t("booking:checkout:rub")}</span></div>
            </div>}
        <div className="booking-checkout-amount" style={{ marginTop: '20px', borderTop: '1px solid #eee'}} >
            {t("booking:checkout:amount_total")} &nbsp;<span style={{fontWeight: 'bold'}}>{getTotalAmount()} {t("booking:checkout:rub")}</span>
        </div>
            <div className="booking-checkout-inputs">
                <label>{t("booking:checkout:inputs_header")}</label>
                <Col xs={12} sm={7} md={7} lg={7} style={{float: 'left', padding: '0 5px'}}>
                    <Form.Field style={{width: '100%'}}>
                        <input
                            value={name||''}
                            onChange={(e) => setName(e.target.value)}
                            placeholder={t("booking:checkout:surname")||''}/>
                    </Form.Field>
                </Col>
                <Col xs={12} sm={5} md={5} lg={5} style={{float: 'left', padding: '0 5px'}}>
                    <Form.Field style={{width: '100%'}}>
                        <PhoneInput
                            onlyCountries={['ru']}
                            country='ru'
                            containerStyle={{width: '100%'}}
                            inputStyle={{width: '100%', height: 38, paddingLeft: '42px', fontFamily: 'inherit'}}
                            placeholder={'+7 (123) 456-78-90'}
                            value={phoneNumber}
                            onChange={(phone) => setPhoneNumber(phone)}
                            localization={i18n.language === 'ru' ? ru : undefined}
                            countryCodeEditable={false}
                        />
                    </Form.Field>
                </Col>
            </div>
            <div className="booking-checkout-buttons">
                <Button 
                    disabled={confirmButtonDisabled()}
                    style={{backgroundColor: '#3CB371', color: 'white'}} onClick={() => {
                    bookingFormService.fillBookingData({
                        bookingNumber: bookingNumber||'',
                        hoursCount: selectedDuration,
                        amount: getTotalAmount(),
                        promoCode: promo?.code || null,
                        discount: discounts.find(x => x.duration == selectedDuration)?.value || null,
                        customer: {
                            name: name||'',
                            phoneNumber: phoneNumber||''
                        },
                        language: localStorage.getItem('language') || 'ru',
                        costs: data.pointPrices.slice(0, selectedDuration/0.5).map((p) => {
                            return {
                                startHour: p.start,
                                endHour: p.end,
                                cost: p.value
                            } as FillBookingDataCommandCost
                        }),
                        inventories: selectedInventories.map((inv, i) => {
                            return {
                                inventoryId: inv.id,
                                price: inv.price,
                                quantity: inv.quantity,
                                amount: inv.price * inv.quantity * selectedDuration,
                            } as FillBookingDataCommandInventory
                        })
                    }).then(() => {
                        navigate("/booking/confirm",  {
                            state: {
                                bookingNumber: bookingNumber,
                                backPath: backPath
                            }
                        });
                    }).catch((error) => {
                        if (error === 'booking:booking_intersection') {
                            navigate(backPath);
                        }
                    })
                }}>{t("booking:checkout:booking_button")}</Button>
                <Button style={{backgroundColor: '#CD5C5C', color: 'white'}} onClick={() => {
                    setCancelModal(true);
                }}>{t("booking:checkout:cancel_button")}</Button>
            </div>
        </Form>
        <div className="booking-form-footer">{t('common:footer')}</div>
    </Container>
}