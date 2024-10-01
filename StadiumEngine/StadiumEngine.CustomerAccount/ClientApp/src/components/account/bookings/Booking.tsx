import React, {useEffect, useRef, useState} from "react";
import {useNavigate, useParams} from "react-router-dom";
import {useInject} from "inversify-hooks";
import {IBookingsService} from "../../../services/BookingsService";
import {BookingListItemDto} from "../../../models/dto/bookings/BookingListItemDto";
import {t} from "i18next";
import {Button, Checkbox, Dropdown, Form} from "semantic-ui-react";
import {Container} from "reactstrap";
import {Dialog} from "@mui/material";
import {BookingDuration} from "./common/BookingDuration";
import {BookingCheckoutDto} from "../../../models/dto/bookings/BookingCheckoutDto";
import {getWeeklyClosedText} from "../../../helpers/utils";
import {
    getFieldAmountValueByBooking,
    getInventoryAmountByBooking,
    getPromoDiscount
} from "../../../helpers/booking-utils";
import {BookingFieldAmount} from "./common/BookingFieldAmount";
import {BookingInventory} from "./common/BookingInventory";
import {BookingTotalAmount} from "./common/BookingTotalAmount";
import {BookingHeader} from "./common/BookingHeader";
import {dateFormatterByStringWithoutTime} from "../../../helpers/date-formatter";
import {BookingDto} from "../../../models/dto/bookings/BookingDto";
import {parseNumber} from "../../../helpers/time-point-parser";

export const Booking = () => {
    let { stadiumToken, number, day } = useParams();

    const [data, setData] = useState<BookingListItemDto|null>(null);
    const [bookingsService] = useInject<IBookingsService>('BookingsService');

    useEffect(() => {
        if ( number) {
            bookingsService.getBooking(number, day).then((result) => {
                setData(result);
            })
        }
    }, []);

    const getBookingFieldAmount = () => {
        return getFieldAmountValueByBooking(data?.originalData ?? {} as BookingDto);
    }

    const getBookingInventoryAmount = () => {
        return getInventoryAmountByBooking(data?.originalData ?? {} as BookingDto);
    }

    const getTotalAmountValue = () => {
        return getBookingFieldAmount() + getBookingInventoryAmount();
    }

    const getEventEachText = () => {
        const date = new Date(data?.originalData?.day ?? '');
        return t(`booking:weekly:each:${date?.getDay()}`);
    }

    const cancelReason = useRef<any>();
    const [oneInRow, setOneInRow] = useState(true);
    const [cancelConfirm, setCancelConfirm] = useState(false);

    const navigate = useNavigate();

    const cancelBooking = () => {
        if (data) {
            bookingsService.cancelBooking({
                bookingNumber: data.number,
                cancelOneInRow: oneInRow,
                reason: cancelReason.current?.value,
                day: data.day
            }).then(() => {
                navigate( `/${stadiumToken}/bookings/future`)
            });
        }
    }

    return data ? <Container className="booking-checkout-container" style={{minHeight: "auto"}}>
            <Form style={{paddingBottom: '15px'}}>
                <BookingHeader data={{
                    bookingNumber: data.originalData.number,
                    day: dateFormatterByStringWithoutTime(data.day?.toString() || '' ),
                    stadiumName: '',
                    field: data.originalData.field
                } as BookingCheckoutDto} dayText={data.originalData.isWeekly ? getEventEachText(): null} withCurrentDate={true} withStadiumName={false}/>
                <div className="booking-locker-room-weekly-row">
                    <div style={{display: 'flex', flexDirection: "column"}}>
                        <Checkbox
                            checked={data.originalData.isWeekly}
                            disabled={true}
                            label={t('booking:is_weekly')}/>
                        {data.originalData.isWeekly &&
                            <span style={{fontSize: 10}}>{getWeeklyClosedText(data.originalData)}</span>}
                    </div>
                    {!data.originalData.isWeekly && data.originalData.lockerRoom &&
                        <div className="booking-locker-room-weekly-row-right">
                            <Dropdown
                                fluid
                                selection
                                disabled={true}
                                style={{width: "200px"}}
                                placeholder={t('booking:locker_room') || ''}
                                value={data.originalData.lockerRoom?.id || ''}
                                options={[{
                                    key: data.originalData.lockerRoom?.id,
                                    value: data.originalData.lockerRoom?.id,
                                    text: data.originalData.lockerRoom?.name
                                }]}
                            />
                        </div>}
                </div>
                <div className="scheduler-booking-tariff-buttons">
                    <span>{t("booking:field_card:tariff")}: <b>{data.originalData.tariff.name}</b></span>
                </div>
                {data.originalData.promo && <div style={{
                    marginTop: '5px',
                    fontSize: '12px',
                    color: '#666'
                }}><i style={{color: '#00d2ff'}} className="fa fa-exclamation-circle"
                      aria-hidden="true"/> {t('booking:promo_applied')}: <b
                    style={{color: 'black'}}>{data.originalData.promo.code} (-{getPromoDiscount(data.originalData.promo, data.originalData.totalAmountBeforeDiscount)})</b>.
                </div>}
                <BookingDuration
                    isEditable={false}
                    data={{
                        pointPrices: [{
                            displayStart: parseNumber(data.originalData.startHour)
                        }]
                    } as BookingCheckoutDto}
                    selectedDuration={data.originalData.hoursCount}
                    setSelectedDuration={() => null}/>
                <BookingFieldAmount getFieldAmount={getBookingFieldAmount}/>
                <BookingInventory
                    data={null}
                    isEditable={false}
                    selectedDuration={0}
                    selectedInventories={[]}
                    setSelectedInventories={() => null}
                    getInventoryAmount={getBookingInventoryAmount}
                    bookingInventories={data.originalData.inventories || []}
                    headerText={t("booking:scheduler:inventory_header")}/>
                <BookingTotalAmount getTotalAmountValue={getTotalAmountValue} promo={data.originalData.promo}
                                    manualDiscount={data.originalData.manualDiscount ?? undefined}/>
                {data.originalData.manualDiscount && data.originalData.manualDiscount > 0 && <div style={{
                    marginTop: '5px',
                    fontSize: '12px',
                    textAlign: 'right',
                    color: '#666'
                }}><i style={{color: '#00d2ff'}} className="fa fa-exclamation-circle"
                      aria-hidden="true"/> {t('booking:manual_discount_applied')} <b
                    style={{color: 'black'}}>(-{data.originalData.manualDiscount})</b></div>}
                <div className="booking-checkout-buttons">
                    {new Date(data.end) > new Date() && <Button style={{backgroundColor: '#CD5C5C', color: 'white'}} onClick={() => {
                        setCancelConfirm(true);
                    }}>{t("booking:checkout:cancel_button")}</Button>}
                </div>
            </Form>

            <Dialog
                onClose={() => setCancelConfirm(false)}
                open={cancelConfirm}
            >
                <div className="booking-cancel-overlay">
                    <textarea rows={3} ref={cancelReason} placeholder={t("booking:cancel:reason") || ''}/>
                    {data.originalData.isWeekly &&
                        <Checkbox label={t("booking:cancel:one_in_row")} checked={oneInRow}
                                  onChange={() => setOneInRow(!oneInRow)}/>
                    }
                    <div className="booking-cancel-overlay-buttons">
                        <Button
                            style={{backgroundColor: '#3CB371', color: 'white'}} onClick={() => {
                            cancelBooking();
                        }}>
                            {t("common:confirm")}
                        </Button>
                        <Button style={{backgroundColor: '#CD5C5C', color: 'white'}} onClick={() => {
                            setCancelConfirm(false);
                        }}>{t("common:back")}</Button>
                    </div>
                </div>

            </Dialog>
        </Container> :
        <span/>;
}