import React, {useEffect, useState} from 'react';
import {useNavigate, useParams} from "react-router-dom";
import {useInject} from "inversify-hooks";
import {IBookingService} from "../../services/BookingService";
import {BookingFormDto} from "../../models/dto/booking/BookingFormDto";
import {BookingFormFieldCard} from "./BookingFormFieldCard";
import {Col, Container} from "reactstrap";
import '../../css/booking/BookingForm.scss';
import {LanguageSelect} from "../common/LanguageSelect";
import SemanticDatepicker from "react-semantic-ui-datepickers";
import {getLocale} from "../../i18n/i18n";
import {getTitle} from "../../helpers/utils";
import {CityDto} from "../../models/dto/geo/CityDto";
import {IGeoService} from "../../services/GeoService";
import {useSetRecoilState} from "recoil";
import {loadingAtom} from "../../state/loading";
import {Button, Dropdown, Icon, Input} from "semantic-ui-react";
import {t} from "i18next";
import {AddBookingDraftCommand} from "../../models/command/booking/AddBookingDraftCommand";
import {BookingSource} from "../../models/dto/booking/enums/BookingSource";
import {CustomerAccountButton} from "./CustomerAccountButton";

export const BookingForm = () => {
    let {token} = useParams();
    const setLoading = useSetRecoilState(loadingAtom);
    
    document.title = getTitle("booking:title");
    
    const navigate = useNavigate();

    const storedDate = localStorage.getItem('booking-form-date');
    
    const [currentDate, setNewDate] = useState(storedDate && new Date(storedDate) >= new Date() ? new Date(storedDate) : new Date());
    
    const onChange = (event: any, data: any) => {
        if (currentDate.toDateString() !== data.value.toDateString()) {
            localStorage.setItem('booking-form-date', data.value.toDateString());
            setNewDate(data.value);
            fetchData(data.value);
        }
    }
    
    const [data, setData] = useState<BookingFormDto|null>(null);
    
    const storedCity = localStorage.getItem('booking-form-city');
    
    const [cities, setCities] = useState<CityDto[]>(storedCity && token === undefined ? [JSON.parse(storedCity)] : []);
    const [cityId, setCityId] = useState<number|null>(storedCity && token === undefined ? JSON.parse(storedCity).id : null);
    const [citySearch, setCitySearch] = useState<string|null>(null);
    const handleSearchChange = (e: any, { searchQuery }: any) => {
        setCitySearch(searchQuery);
    }
    const handleChange = (e: any, { value }: any) => {
        const chosenCity = cities.find(c => c.id == value);
        if (chosenCity) {
            setCities([chosenCity]);
            localStorage.setItem('booking-form-city', JSON.stringify(chosenCity))
        }
        setCityId(value);
        setCitySearch(null);
    }

    const [searchString, setSearchString] = useState<string|null>(null);
    
    const [bookingService] = useInject<IBookingService>('BookingService');
    const [geoService] = useInject<IGeoService>('GeoService');
    
    const goToCheckout = (fieldId: number, tariffId: number, hour: number) => {
        bookingService.addBookingDraft({
            fieldId: fieldId,
            tariffId: tariffId,
            hour: hour,
            source: BookingSource.Form,
            day: currentDate?.toDateString()
        } as AddBookingDraftCommand).then((response) => {
            navigate("/checkout", {
                state: {
                    backPath: window.location.pathname,
                    bookingNumber: response.bookingNumber
                }
            });
        })
    }
    
    useEffect(() => {
        if (token === undefined) {
            if (cityId === null) {
                setLoading(true);
                geoService.getCities(citySearch).then((result) => {
                    setCities([result[0]]);
                    setCityId(result[0].id);
                })
            }
        }
        else {
            fetchData();
        }
    }, [])
    
    
    useEffect(() => {
        if (cityId !== null) {
            fetchData();
        }
    }, [cityId])
    
    useEffect(() => {
        if ((citySearch?.length || 0) > 2) {
            geoService.getCities(citySearch).then((result) => {
                setCities(result);
            })
        }
    }, [citySearch])

    const [changeCustomer, setChangeCustomer] = useState(false);
    useEffect(() => {
        if (changeCustomer) {
            fetchData();
            setChangeCustomer(false);
        }
    }, [changeCustomer])
    
    const fetchData = (date: Date|null = null) => {
        bookingService.getBookingForm(date === null ? currentDate : date, token === undefined ? null : token, cityId, searchString)
            .then((result: BookingFormDto) => {
                setData(result);
            })
    }
    
    const getPointing = () => {
        return token || window.innerWidth < 575 ? "left" : "right";
    }
    

    return <div>{data === null ? <span/> :
            <Container className="booking-form-container">
                <div className="booking-form-header">
                    {token === undefined && <Col sm={7} xs={12} className="booking-form-header-left">
                        {cityId !== null &&
                            <Col style={{ padding: 0}} lg={5} md={5} sm={5} xs={12}>
                                <Dropdown
                                    fluid
                                    options={cities.map((c) => {
                                        return {
                                            key: c.id,
                                            text: c.displayName,
                                            value: c.id,
                                        }
                                    })}
                                    search
                                    selection
                                    searchQuery={citySearch||''}
                                    onSearchChange={handleSearchChange}
                                    onChange={handleChange}
                                    value={cityId}
                                />
                            </Col>
                        }
                        <Col className="booking-form-search-input-container" style={{ padding: 0}} lg={7} md={7} sm={7} xs={12}>
                                <Input
                                    className="booking-form-search-input"
                                    style={{ marginLeft: '5px'}}
                                    value={searchString}
                                    placeholder={t('booking:search_placeholder')}
                                    onChange={(e) => setSearchString(e.target.value)}
                                />
                                <Button style={{ marginLeft: '5px', padding: '11px 10px'}} onClick={() => fetchData()}>
                                    <Icon style={{margin: 0}} className='search'/>
                                </Button>
                            </Col>
                    </Col>}
                    <Col sm={token ? 12 : 5} xs={12}
                         className={"booking-form-header-right " + (token ? "space-between-imp" : "")}>
                        <SemanticDatepicker
                            className="booking-form-date-picker"
                            firstDayOfWeek={1}
                            datePickerOnly={true}
                            locale={getLocale()}
                            value={currentDate}
                            format={'DD.MM.YYYY'}
                            minDate={new Date()}
                            onChange={onChange}
                            clearable={false}
                            pointing={getPointing()}
                        />
                        <div className="lang-customer-container">
                            <CustomerAccountButton stadiumToken={token} customer={data.customer} setChangeCustomer={setChangeCustomer} />
                            <LanguageSelect withRequest={false} style={{marginLeft: '10px'}}/>
                        </div>
                    </Col>

                </div>
                
                <div className="booking-form-min-time">{t('booking:min_time')}</div>
                <div className="booking-form-cards">
                    {data.fields.length === 0 ? <div className="booking-form-no-fields">{t('booking:no_fields')}</div> : data.fields.map((f, i) => {
                        return <BookingFormFieldCard
                            key={i}
                            addBookingDraft={goToCheckout}
                            field={f}/>
                    })}
                </div>
                <div className="booking-form-footer">{t('common:footer')}</div>
            </Container>
        }
        
    </div>
}