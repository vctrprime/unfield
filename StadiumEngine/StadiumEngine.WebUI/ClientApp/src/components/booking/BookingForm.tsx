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
import {Dropdown} from "semantic-ui-react";


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
        bookingFormService.getBookingForm(currentDate, token === undefined ? null : token, cityId)
            .then((result: BookingFormDto) => {
                setData(result);
            })
    }
    

    return <div>{data === null ? <span/> : 
            <Container className="booking-form-container">
                <div className="booking-form-header">
                    <Col sm={6} xs={12} className="booking-form-header-left">
                        {token === undefined && cityId !== null &&
                            <Col style={{ padding: 0}} lg={6} md={12} sm={12} xs={12}>
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
                    </Col>
                    <Col sm={6} xs={12} className="booking-form-header-right">
                        <LanguageSelect withRequest={false} style={{marginRight: '10px'}}/>
                        <SemanticDatepicker firstDayOfWeek={1}
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
                {data.fields.map((f, i) => {
                return <FieldCard 
                    key={i}
                    field={f}/>
            })}
            </Container>
        }
    </div>
}