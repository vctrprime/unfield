import {CustomerBookingListItemDto} from "../../../models/dto/bookings/CustomerBookingListItemDto";
import React from "react";
import {Col} from "reactstrap";
import {t} from "i18next";
import {dateFormatterByDate} from "../../../helpers/date-formatter";
import {SportKind} from "../../../models/dto/offers/enums/SportKind";
import {useNavigate, useParams} from "react-router-dom";

export interface BookingListCardProps {
    data: CustomerBookingListItemDto;
}

export const BookingListCard = ({ data }: BookingListCardProps) => {
    const { stadiumToken } = useParams();
    
    const navigate = useNavigate();
    
    const url = `/${stadiumToken}/bookings/${data.number}` +
        (data.isWeekly ? `/${data.day?.toString().split('T')[0]}` : '');
    
    const goToBooking = () => {
        navigate(url);
    }
    
    return <Col xs={12} sm={12} md={6} lg={3} style={{float: 'left', paddingRight: '15px', paddingLeft: '15px'}}>
        <div className="booking-list-card">
            <div className="start-date">{dateFormatterByDate(data.start)}</div>

            <div className="top-data">
                {data.field.images?.length ?
                    <img src={"/stadium-group-images/" + data.field.images[0]}/> : <span/>
                }
                <div className="top-data-text">
                    <div className="booking-number" onClick={goToBooking}>{data.number}</div>
                    <div className="field-name">{data.field.name}</div>
                    <div className="field-sports">
                        {data.field.sportKinds.length === 0 ?
                            <span style={{
                                paddingLeft: '10px',
                                fontSize: '12px',
                                fontWeight: "bold"
                            }}>{t("booking:field_card:no_sports")}</span> :
                            data.field.sportKinds.map((s, i) => {
                                const value = SportKind[s];
                                const text = t("common:sports:" + value.toLowerCase());

                                return <div style={i === 0 ? {marginLeft: 0} : {}} key={i}
                                            className="field-sport">{text}</div>;
                            })}
                    </div>
                </div>
            </div>

            <div className="bottom-data">
                <div className="bottom-data-row">
                    <div className="item-text">{t('booking:list:card:duration')}</div>
                    <div className="item-value">{data.duration}</div>
                </div>
                <div className="bottom-data-row">
                    <div className="item-text">{t('booking:list:card:total')}</div>
                    <div className="item-value">{data.totalAmountAfterDiscount}</div>
                </div>
            </div>
            {data.isWeekly &&
                <div className="is-weekly">{t('booking:list:card:is_weekly')}</div>}
        </div>
    </Col>
}