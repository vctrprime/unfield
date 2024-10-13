import noImage from "../../../img/no-image.png";
import {t} from "i18next";
import {Icon} from "semantic-ui-react";
import React, {useEffect} from "react";
import {BookingCheckoutDto, BookingCheckoutInventoryDto} from "../../../models/dto/booking/BookingCheckoutDto";
import {BookingInventoryDto} from "../../../models/dto/booking/BookingDto";

export interface SelectedInventory {
    id: number;
    quantity: number;
    price: number;
}

interface InventoryRowProps {
    inventory: BookingCheckoutInventoryDto;
    selectedInventories: SelectedInventory[];
    setSelectedInventories: any;
    isEditable: boolean;
    isReadonly: boolean;
    bookingInventory?: BookingInventoryDto;
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
    
    const getPriceValue = () => {
        if (props.bookingInventory) {
            return props.bookingInventory.price;
        }
        
        return props.inventory.price;
    }

    return <div
        style={selectedInventory || props.isReadonly ? {backgroundColor: 'rgba(245, 245, 245, 1)'} : {} }
        className="booking-checkout-inventory-row">
        <div className="booking-checkout-inventory-row-block" style={{justifyContent: 'flex-start'}}>
            {props.inventory.image ?
                <img src={"/stadium-group-images/" + props.inventory.image}/> :
                <img src={noImage}/>
            }
            <div className="booking-checkout-inventory-row-name" title={props.isReadonly ? t("common:reserved_qty")||'' : t("common:available_qty")||''}>{props.inventory.name} ({props.inventory.quantity})</div>
        </div>
        <div className="booking-checkout-inventory-row-block" style={{justifyContent: 'flex-end'}}>
            <div className="booking-checkout-inventory-row-price">{getPriceValue()} {t("booking:checkout:rub")}/{t("common:hour_long")}</div>

            <div className="booking-checkout-inventory-row-button">
                {props.isEditable && <Icon onClick={() => removeInventory()} name='minus circle' style={{ color: '#CD5C5C', opacity: selectedInventory ? 1 : 0.5}} />}
                {!props.isReadonly && <span style={props.isEditable ? {} : { marginRight: '10px'}}>{selectedInventory?.quantity || 0}</span>}
                {props.isEditable && <Icon onClick={() => addInventory()} name='add circle' style={{ color: '#3CB371', marginLeft: '5px', opacity: (selectedInventory?.quantity||0) < props.inventory.quantity ? 1 : 0.5}} />}
            </div>
        </div>
    </div>
}


export interface BookingInventoryProps {
    data: BookingCheckoutDto|null;
    selectedDuration: number;
    selectedInventories: SelectedInventory[];
    setSelectedInventories: Function;
    getInventoryAmount: Function;
    headerText: string;
    isEditable: boolean;
    bookingInventories: BookingInventoryDto[];
}
export const BookingInventory = (props: BookingInventoryProps) => {
    
    const inventories = props.data ? (props.data.durationInventories.find( x => x.duration === props.selectedDuration)?.inventories || []) : 
    props.bookingInventories.map((i) => {
        return {
            id: i.id,
            name: i.inventory.name,
            price: i.price,
            quantity: i.quantity,
            image: i.inventory.images[0]
        } as BookingCheckoutInventoryDto
    });
    
    useEffect(() => {
        const copySelectedInventories: SelectedInventory[] = [];
        props.selectedInventories.forEach((i) => {
            const inventory = inventories.find( x => x.id === i.id);
            if (inventory) {
                if (i.quantity > inventory.quantity) {
                    i.quantity = inventory.quantity;
                }
                copySelectedInventories.push(i);
            }
        })
        props.setSelectedInventories(copySelectedInventories);
    }, [props.selectedDuration])
    
    return <>
        {inventories.length > 0 ?
            <div className="booking-checkout-inventories">
                <div className="booking-checkout-inventories-title">{props.headerText}</div>
                {inventories.map((inv, i) => {
                    return <InventoryRow 
                        isEditable={props.isEditable} 
                        isReadonly={props.data == null}
                        selectedInventories={props.selectedInventories} 
                        key={i}  
                        inventory={inv} 
                        setSelectedInventories={props.setSelectedInventories}
                        bookingInventory={props.bookingInventories.find(b => b.inventory.id === inv.id)}
                    />
                })}
                <div className="booking-checkout-amount">{t("booking:checkout:amount_inventory")} &nbsp;<span style={{fontWeight: 'bold'}}>{props.getInventoryAmount()} {t("booking:checkout:rub")}</span></div>
            </div> : <span/>}
    </>
}