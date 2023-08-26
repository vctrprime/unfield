import React, {useEffect, useState} from "react";
import {SchedulerBookingDto} from "../../../models/dto/booking/SchedulerBookingDto";
import {Container} from "reactstrap";
import {Checkbox, Dropdown, Form, Input} from "semantic-ui-react";
import {BookingHeader} from "../common/BookingHeader";
import {BookingInventory, SelectedInventory} from "../common/BookingInventory";
import {BookingDto, BookingPromoDto} from "../../../models/dto/booking/BookingDto";
import {useInject} from "inversify-hooks";
import {IBookingService} from "../../../services/BookingService";
import {BookingCheckoutDto} from "../../../models/dto/booking/BookingCheckoutDto";
import {
    getFieldAmount,
    getFieldAmountValueByBooking,
    getInventoryAmount,
    getInventoryAmountByBooking
} from "../../../helpers/booking-utils";
import {BookingFieldAmount} from "../common/BookingFieldAmount";
import {t} from "i18next";
import {BookingTotalAmount} from "../common/BookingTotalAmount";
import {BookingCustomer} from "../common/BookingCustomer";
import {BookingDuration} from "../common/BookingDuration";
import {BookingFormFieldSlotPriceDto} from "../../../models/dto/booking/BookingFormDto";
import {SchedulerBookingTariffButton} from "./SchedulerBookingTariffButton";
import {useRecoilState} from "recoil";
import {LockerRoomDto} from "../../../models/dto/offers/LockerRoomDto";
import {lockerRoomsAtom} from "../../../state/offers/lockerRooms";
import {IOffersService} from "../../../services/OffersService";
import {LockerRoomStatus} from "../../../models/dto/offers/enums/LockerRoomStatus";

export interface SchedulerBooking {
    bookingData: BookingDto;
    slotPrices: BookingFormFieldSlotPriceDto[];
}

export const SchedulerBooking = (props: SchedulerBooking) => {
    const [data, setData] = useState<SchedulerBookingDto|null>(null);
    const [lockerRooms, setLockerRooms] = useRecoilState<LockerRoomDto[]>(lockerRoomsAtom);
    
    const isNew = props.bookingData.id === 0;

    const [promo, setPromo] = useState<BookingPromoDto|null>(isNew ? null : props.bookingData.promo);
    const [currentTariffId, setCurrentTariffId] = useState<number>(props.bookingData.tariff.id);
    const [currentLockerRoom, setCurrentLockerRoom] = useState(props.bookingData.lockerRoom?.id || '');
    const [isWeekly, setIsWeekly] = useState<boolean|undefined>(props.bookingData.isWeekly);
    
    const [selectedDuration, setSelectedDuration] = useState<number>(isNew ? 1 : props.bookingData.hoursCount);
    const [selectedInventories, setSelectedInventories] = useState<SelectedInventory[]>(isNew ? [] : props.bookingData.inventories.map((i) => {
        return {
            id: i.inventory.id,
            quantity: i.quantity,
            price: i.price
        } as SelectedInventory
    }));

    const [phoneNumber, setPhoneNumber] = useState<string | undefined>(isNew ? undefined : props.bookingData.customer?.phoneNumber || undefined);
    const [name, setName] = useState<string | undefined>(isNew ? undefined : props.bookingData.customer?.name || undefined);

    const [bookingService] = useInject<IBookingService>('BookingService');
    const [offersService] = useInject<IOffersService>('OffersService');

    
    const getManualDiscount = (): string => {
        if (props.bookingData.manualDiscount) {
            return props.bookingData.manualDiscount.toString();
        }
        return '';
    }
    const [manualDiscount, setManualDiscount] = useState<string>(getManualDiscount());
    
    useEffect(() => {
        fetchCheckoutData();
        
        if (lockerRooms.length === 0) {
            offersService.getLockerRooms().then((response) => {
                setLockerRooms(response.filter( l => l.status === LockerRoomStatus.Ready && l.isActive));
            })
        }
    }, [])

    const getSchedulerBookingFieldAmount = () => {
        if (!isAvailableEditDuration()) {
            return getFieldAmountValueByBooking(props.bookingData);
        }
        return getFieldAmount(selectedDuration, data?.checkoutData||null);
    }

    const getSchedulerBookingInventoryAmount = () => {
        if (!isAvailableEditInventory()) {
            return getInventoryAmountByBooking(props.bookingData);
        }
        return getInventoryAmount(selectedDuration, selectedInventories);
    }

    const getTotalAmountValue = () => {
        return getSchedulerBookingFieldAmount() + getSchedulerBookingInventoryAmount();
    }
    
    const toggleBookingTariff = (tariffId: number) => {
        if (tariffId !== currentTariffId) {
            fetchCheckoutData(tariffId);
        }
    }
    
    const fetchCheckoutData = (tariffId?: number) => {
        bookingService.getBookingCheckout(props.bookingData.number, !isNew, tariffId).then((response: BookingCheckoutDto) => {
            setData({
                checkoutData: response
            })
            if (tariffId) {
                setCurrentTariffId(tariffId);
            }
        })
    }
    
    const isAvailableEditDuration = () => {
        if ( currentTariffId === props.bookingData.tariff.id) {
            if (selectedDuration !== props.bookingData.hoursCount) {
                return true;
            }
            return props.bookingData.fieldAmount === getFieldAmount(selectedDuration, data?.checkoutData||null)
        }
        return true;
    }
    
    const isAvailableEditInventory = () => {
        if ( currentTariffId === props.bookingData.tariff.id) {
            let hasDiff = false;
            selectedInventories.forEach((inv) => {
                const durationInventory = data?.checkoutData.durationInventories.find( di => di.duration === selectedDuration);
                if (durationInventory) {
                   const inventory = durationInventory.inventories.find(i => i.id === inv.id);
                   if (inventory && !hasDiff) {
                       hasDiff = inventory.price !== inv.price;
                   }
                }
            })
            
            return !hasDiff;
        }

        return true;
    }

    const lockerRoomDropDownRows = () => {
        return lockerRooms.map((l) => {
            return {key: l.id, value: l.id, text: l.name}
        })
    }
    
    return data === null  ? null :  <Container className="booking-checkout-container" style={{minHeight: "auto"}}>
        <Form style={{paddingBottom: '10px'}}>
            <BookingHeader data={data.checkoutData} withStadiumName={false} />
                <div className="scheduler-booking-tariff-buttons">
                    <span>{t("booking:field_card:tariff")}: </span>
                    {(promo === null && isAvailableEditDuration() ? props.slotPrices : props.slotPrices.filter(x => x.tariffId === currentTariffId)).map((p) => {
                    return <SchedulerBookingTariffButton
                        key={p.tariffId}
                        action={() => toggleBookingTariff(p.tariffId)}
                        slotPrice={p}
                        isCurrent={currentTariffId === p.tariffId}
                    />
                })
                }</div>
            {promo && <div style={{marginTop: '5px',
                fontSize: '12px',
                color: '#666'}}><i style={{ color: '#00d2ff'}} className="fa fa-exclamation-circle" aria-hidden="true"/> {t('schedule:scheduler:booking:promo_applied')}: <b style={{color: 'black'}}>{promo.code} (-{promo.value})</b>. {t("common:disable_change_tariff")}.</div>}
            <BookingDuration 
                isEditable={isAvailableEditDuration()}
                data={data.checkoutData} 
                selectedDuration={selectedDuration} 
                setSelectedDuration={setSelectedDuration} />
            <BookingFieldAmount getFieldAmount={getSchedulerBookingFieldAmount} />
            {!isAvailableEditDuration() && <div className="booking-warning">
                <i className="fa fa-exclamation-circle" aria-hidden="true"/>
                <div className="booking-warning-text">
                  <span>{t("booking:warnings:disabled_change_duration")}</span>
                </div>
            </div>}
            <div className="booking-locker-room-weekly-row">
                <Dropdown
                    fluid
                    style={{width: "200px"}}
                    selection
                    clearable
                    placeholder={t('schedule:scheduler:booking:locker_room')||''}
                    onChange={(e: any, {value}: any) => setCurrentLockerRoom(value)}
                    value={currentLockerRoom}
                    options={lockerRoomDropDownRows()}
                    />
                <Checkbox
                    onChange={(e, data) => setIsWeekly(data.checked)}
                    checked={isWeekly}
                    disabled={!isNew} label={t('schedule:scheduler:booking:is_weekly')} />
            </div>
            <BookingInventory
                data={data.checkoutData}
                isEditable={isAvailableEditInventory()}
                selectedDuration={selectedDuration}
                selectedInventories={selectedInventories}
                setSelectedInventories={setSelectedInventories}
                getInventoryAmount={getSchedulerBookingInventoryAmount}
                bookingInventories={props.bookingData.inventories||[]}
                headerText={t("booking:scheduler:inventory_header")}/>
            {!isAvailableEditInventory() && <div className="booking-warning">
                <i className="fa fa-exclamation-circle" aria-hidden="true"/>
                <div className="booking-warning-text">
                    <span>{t("booking:warnings:disabled_change_inventory")}</span>
                </div>
            </div>}
            <BookingTotalAmount getTotalAmountValue={getTotalAmountValue} promo={promo} manualDiscount={parseInt(manualDiscount === '' ? '0' : manualDiscount)}/>
            <div className="booking-manual-discount">
                <span>{t("schedule:scheduler:booking:manual_discount")}:</span>
                <Input value={manualDiscount} onChange={(e, {value}) => {
                    if (value) {
                        setManualDiscount(value)
                    }
                    else {
                        setManualDiscount('');
                    }
                }} style={{ width: '100px'}}/>
            </div>
            <BookingCustomer
                name={name}
                setName={setName}
                phoneNumber={phoneNumber}
                setPhoneNumber={setPhoneNumber}
                headerText={t("booking:scheduler:inputs_header")} />
        </Form>
    </Container>
}