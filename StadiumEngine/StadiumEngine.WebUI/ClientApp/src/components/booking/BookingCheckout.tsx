import React, {useEffect, useRef, useState} from "react";
import {useLocation, useNavigate, useParams} from "react-router-dom";
import {BookingCheckoutDto, BookingCheckoutDurationAmountDto} from "../../models/dto/booking/BookingCheckoutDto";
import {useInject} from "inversify-hooks";
import {IBookingFormService} from "../../services/BookingFormService";
import {Container} from "reactstrap";
import '../../css/booking/BookingCheckout.scss';
import {PromoCodeDto} from "../../models/dto/rates/TariffDto";
import {PromoCodeType} from "../../models/dto/rates/enums/PromoCodeType";
import {getDurationText, getTitle} from "../../helpers/utils";
import {Button, Dropdown, Form, Icon, Input} from "semantic-ui-react";
import {t} from "i18next";
import {InventoryDto} from "../../models/dto/offers/InventoryDto";
import {SportKind} from "../../models/dto/offers/enums/SportKind";
import noImage from "../../img/no-image.png";
import {
    FillBookingDataCommandCost,
    FillBookingDataCommandInventory
} from "../../models/command/booking/FillBookingDataCommand";

type CheckoutLocationState = {
    bookingNumber: string;
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
    inventory: InventoryDto
    action: any;
    added: boolean;
}

const InventoryRow = (props: InventoryRowProps) => {
    return <div
        style={props.added ? {backgroundColor: 'rgba(245, 245, 245, 1)'} : {} }
        className="booking-checkout-inventory-row">
        <div className="booking-checkout-inventory-row-block" style={{justifyContent: 'flex-start'}}>
            {props.inventory.images.length ?
                <img src={"/legal-images/" + props.inventory.images[0]}/> :
                <img src={noImage}/>
            }
            <div className="booking-checkout-inventory-row-name">{props.inventory.name}</div>
        </div>
        <div className="booking-checkout-inventory-row-block" style={{justifyContent: 'flex-end'}}>
            <div className="booking-checkout-inventory-row-price">{props.inventory.price} руб./час</div>
            <div className="booking-checkout-inventory-row-button" onClick={() => props.action(props.inventory)}>
                {props.added ? <Icon name='remove circle' style={{ color: '#CD5C5C'}} /> : <Icon name='add circle' style={{ color: '#3CB371'}} />}
            </div>
        </div>
    </div>
}

export const BookingCheckout = () => {
    document.title = getTitle("booking:checkout_title");
    
    const location = useLocation();
    const params = useParams();
    
    let bookingNumber = (location.state as CheckoutLocationState)?.bookingNumber || params["bookingNumber"]
    
    const [bookingFormService] = useInject<IBookingFormService>('BookingFormService');
    
    const [data, setData] = useState<BookingCheckoutDto|null>(null);
    
    const [promo, setPromo] = useState<PromoCodeDto|null>(null);
    const [promoMessage, setPromoMessage] = useState<PromoMessage|null>(null);
    const [discounts, setDiscounts] = useState<CheckoutDiscount[]>([]);

    const promoInput = useRef<any>();
    
    const [selectedDuration, setSelectedDuration] = useState<number>(1);
    const [selectedInventories, setSelectedInventories] = useState<InventoryDto[]>([]);
    
    const navigate = useNavigate();
    
    useEffect(() => {
        bookingFormService.getBookingCheckout(bookingNumber as string).then((response: BookingCheckoutDto) => {
            setData(response);
        })
            .catch((error) => {
                navigate("/booking");
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
    
    const addOrRemoveInventory = (inventory: InventoryDto) => {
        if (selectedInventories.find(x => x.id === inventory.id)) {
            setSelectedInventories(selectedInventories.filter( x => x.id !== inventory.id));
        }
        else {
            const copy = [...selectedInventories];
            copy.push(inventory);
            setSelectedInventories(copy);
        }
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
            result += inv.price * (selectedDuration);
        });
        
        return result;
    }
    
    const getTotalAmount = () => {
        return getFieldAmountValue() + getInventoryAmount();
    }
    
    
    return data === null  ? null :  <Container className="booking-checkout-container">
        <Form>
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
                <span style={{ fontSize: '16px'}}>c <span style={{fontWeight: 'bold'}}>{data.pointPrices[0].displayStart}</span> на  &nbsp;</span> <Dropdown
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
            За аренду площадки:&nbsp; {getFieldAmount()}
        </div>
        <Form.Field className="booking-checkout-promo">
                <input
                    placeholder='Введите промокод...'
                    disabled={promo !== null}
                    ref={promoInput}/>
                <Icon
                    name='check'
                    disabled={promo !== null}
                    onClick={() => {

                        if (promoInput.current?.value?.length === 0) {
                            setPromoMessage({
                                message: 'Введите промокод',
                                success: false
                            });
                            return;
                        }

                        const tariffPromo = data?.tariff.promoCodes.find( x => x.code.toLowerCase() === promoInput.current?.value.toLowerCase()) || null;

                        setPromoMessage(tariffPromo ? {
                            message: 'Промокод применен',
                            success: true
                        }: {
                            message: 'Промокод не найден',
                            success: false
                        });

                        setPromo(tariffPromo);

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
            {data.inventories.length > 0 && 
                <div className="booking-checkout-inventories">
                    <div className="booking-checkout-inventories-title">Вы можете добавить аренду инвентаря в заказ:</div>
                    {data.inventories.map((inv, i) => {
                        return <InventoryRow added={selectedInventories.filter(x => x.id === inv.id).length > 0} key={i}  inventory={inv} action={addOrRemoveInventory}/>
                        })}
                    <div className="booking-checkout-amount">За аренду инвентаря: &nbsp;<span style={{fontWeight: 'bold'}}>{getInventoryAmount()} руб.</span></div>
            </div>}
        <div className="booking-checkout-amount" style={{ marginTop: '20px', borderTop: '1px solid #eee'}} >
            Общая стоимость: &nbsp;<span style={{fontWeight: 'bold'}}>{getTotalAmount()} руб.</span>
        </div>
            <div className="booking-checkout-inputs">
                <Form.Field >
                    <input
                        placeholder='ФИО...'/>
                </Form.Field>
                <Form.Field style={{marginLeft: '10px'}}>
                    <input
                        placeholder='Номер телефона...'/>
                </Form.Field>
            </div>
            <div className="booking-checkout-buttons">
                <Button style={{backgroundColor: '#3CB371', color: 'white'}} onClick={() => {
                    bookingFormService.fillBookingData({
                        bookingNumber: bookingNumber||'',
                        hoursCount: selectedDuration,
                        amount: getTotalAmount(),
                        promoCode: promo?.code || null,
                        discount: discounts.find(x => x.duration == selectedDuration)?.value || null,
                        customer: {
                            name: 'name',
                            phoneNumber: 'number'
                        },
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
                                quantity: 1,
                                amount: inv.price * selectedDuration
                            } as FillBookingDataCommandInventory
                        })
                    }).then(() => {
                        navigate("/booking/confirm",  {
                            state: {
                                bookingNumber: bookingNumber
                            }
                        });
                    })
                }}>Забронировать</Button>
                <Button style={{backgroundColor: '#CD5C5C', color: 'white'}} onClick={() => {
                    bookingFormService.cancelBooking({
                        bookingNumber: bookingNumber||''
                    }).finally(() => {
                        navigate("/booking");
                    });
                }}>Отменить</Button>
            </div>
        </Form>
    </Container>
}