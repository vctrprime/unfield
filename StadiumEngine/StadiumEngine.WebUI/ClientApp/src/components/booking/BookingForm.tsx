import React, {useEffect, useState} from 'react';
import {useParams} from "react-router-dom";
import {useInject} from "inversify-hooks";
import {IBookingFormService} from "../../services/BookingFormService";
import {BookingFormDto} from "../../models/dto/booking/BookingFormDto";
import {FieldCard} from "./FieldCard";
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


export const BookingForm = () => {
    let {token} = useParams();
    const setLoading = useSetRecoilState(loadingAtom);
    
    document.title = getTitle("booking:title");

    const [currentDate, setNewDate] = useState(new Date());
    const onChange = (event: any, data: any) => {
        setNewDate(data.value);
    }
    
    const [data, setData] = useState<BookingFormDto|null>(null);
    
    const [cities, setCities] = useState<CityDto[]>([]);
    const [cityId, setCityId] = useState<number|null>(null);
    const [citySearch, setCitySearch] = useState<string|null>(null);
    const handleSearchChange = (e: any, { searchQuery }: any) => {
        setCitySearch(searchQuery);
    }
    const handleChange = (e: any, { value }: any) => {
        setCities(cities.filter(c => c.id == value));
        setCityId(value);
        setCitySearch(null);
    }

    const [searchString, setSearchString] = useState<string|null>(null);
    
    const [bookingFormService] = useInject<IBookingFormService>('BookingFormService');
    const [geoService] = useInject<IGeoService>('GeoService');
    
    useEffect(() => {
        if (token === undefined) {
            if (cityId === null) {
                setLoading(true);
                geoService.getCities(citySearch).then((result) => {
                    setCities([result[0]]);
                    setCityId(result[0].id);
                })
            }
            else {
                fetchData();
            }
        }
        else {
            fetchData();
        }
    }, [currentDate])

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
    
    const fetchData = () => {
        bookingFormService.getBookingForm(currentDate, token === undefined ? null : token, cityId, searchString)
            .then((result: BookingFormDto) => {
                setData(result);
            })
    }
    

    return <div>{data === null ? <span/> :
            <Container className="booking-form-container">
                <div className="booking-form-header">
                    <Col sm={7} xs={12} className="booking-form-header-left">
                        {token === undefined && cityId !== null &&
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
                        {token === undefined &&
                            <Col className="booking-form-search-input-container" style={{ padding: 0}} lg={7} md={7} sm={7} xs={12}>
                                <Input
                                    className="booking-form-search-input"
                                    style={{ marginLeft: '5px'}}
                                    value={searchString}
                                    placeholder={t('booking:search_placeholder')}
                                    onChange={(e) => setSearchString(e.target.value)}
                                />
                                <Button style={{ marginLeft: '5px', padding: '11px 10px'}} onClick={fetchData}>
                                    <Icon style={{margin: 0}} className='search'/>
                                </Button>
                            </Col>}
                    </Col>
                    <Col sm={5} xs={12} className="booking-form-header-right">
                        <LanguageSelect withRequest={false} style={{marginRight: '10px'}}/>
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
                            pointing="right"
                        />
                        
                    </Col>
                    
                </div>
                <div className="booking-form-cards">
                    {data.fields.length === 0 ? <div className="booking-form-no-fields">{t('booking:no_fields')}</div> : data.fields.map((f, i) => {
                        return <FieldCard
                            key={i}
                            field={f}/>
                    })}
                </div>
                <div className="booking-form-footer">{t('common:footer')}</div>
            </Container>
        }
        
    </div>
}