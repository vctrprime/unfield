import React, {useEffect} from 'react';
import { Fragment, useRef, useState } from "react";
import {Icon} from "semantic-ui-react";
import { Scheduler } from "react-scheduler";
import {DayHours, ProcessedEvent, SchedulerRef, ViewEvent} from "react-scheduler/types";
import {getDateFnsLocale} from "../../../i18n/i18n";
import {useRecoilValue} from "recoil";
import {languageAtom} from "../../../state/language";
import {t} from "i18next";
import {useInject} from "inversify-hooks";
import {IScheduleService} from "../../../services/ScheduleService";
import {SportKind} from "../../../models/dto/offers/enums/SportKind";
import {stadiumAtom} from "../../../state/stadium";
import {SchedulerFieldDto, SchedulerFieldsDto} from "../../../models/dto/schedule/SchedulerFieldsDto";
import {SchedulerEventDto} from "../../../models/dto/schedule/SchedulerEventDto";
import {SchedulerBookingEditor} from "./SchedulerBookingEditor";

export interface FieldsSchedulerProps {
    mode: string,
    setView: any
}

export const FieldsScheduler = (props: FieldsSchedulerProps) => {
    const language = useRecoilValue(languageAtom);
    const stadium = useRecoilValue(stadiumAtom);
    
    const isInitialMount = useRef(true);
    
    const [scheduleService] = useInject<IScheduleService>('ScheduleService');
    
    const [fields, setFields] = useState<SchedulerFieldsDto|null>(null);
    const [eventsData, setEventsData] = useState<SchedulerEventDto[]>([]);
    
    const calendarRef = useRef<SchedulerRef>(null);
    
    useEffect(() => {
        scheduleService.getFields().then((response) => {
            setFields(response);
        })
    }, [])
    
    useEffect(() => {
        calendarRef.current?.scheduler?.handleState(
            props.mode,
            "resourceViewMode"
        );
    }, [props.mode, fields])
    
    const deleteEvent = (deletedId: string|number) => {
        const deletedEvent = eventsData.find( e => e.event_id === deletedId);
        const updatedEvents = eventsData.filter((e) => e.sourceBooking !== deletedEvent?.data?.id);
        setEventsData(updatedEvents);
        calendarRef.current?.scheduler?.handleState(updatedEvents, "events");
    }

    const updateEvent = (deletedId: string|number, event: SchedulerEventDto) => {
        const deletedEvent = eventsData.find( e => e.event_id === deletedId);
        
        const updatedEvents = eventsData.filter((e) => e.sourceBooking !== deletedEvent?.sourceBooking);
        event.event_id = deletedId;
        event.start = new Date(event.start);
        event.end = new Date(event.end);

        const disabledEvents = eventsData.filter( e => e.data === null && e.sourceBooking === deletedEvent?.sourceBooking);
        disabledEvents.forEach((e) => {
            e.start = event.start;
            e.end = event.end;
            e.title = event.title;
            e.sourceBooking = event.sourceBooking;
            updatedEvents.push(e);
        })
        updatedEvents.push(event);
        setEventsData(updatedEvents);
        calendarRef.current?.scheduler?.handleState(updatedEvents, "events");
    }

    useEffect(() => {
        if (isInitialMount.current) {
            isInitialMount.current = false;
        } else {
            window.location.reload();
        }
    }, [language, stadium])
    
    

    const fetchEvents = async (query: ViewEvent): Promise<ProcessedEvent[]> => {
        const events = await scheduleService.getEvents(query.start, query.end);
        events.map((e) => {
            e.start = new Date(e.start);
            e.end = new Date(e.end);
        });

        setEventsData(events);
        return new Promise((res) => {
            res(events);
        });
    };
    
    const cellHeight = () => {
        if (fields) {
            const calcHeight = (window.innerHeight - 300) / ((fields.endHour - fields.startHour) * 2);
            return calcHeight > 20 ? calcHeight : 20;
        }
    }
    
    return <Fragment>
        {fields && <Scheduler
            ref={calendarRef}
            day={{
                startHour: fields.startHour as DayHours,
                endHour: fields.endHour as DayHours,
                step: 30,
            }}
            week={{
                startHour: fields.startHour as DayHours,
                endHour: fields.endHour as DayHours,
                step: 30,
                weekDays: [0, 1, 2, 3, 4, 5, 6],
                weekStartOn: 1,
            }}
            cellHeight={cellHeight()}
            customEditor={(scheduler) => <SchedulerBookingEditor 
                deleteEvent={deleteEvent} 
                updateEvent={updateEvent}
                scheduler={scheduler} 
                events={eventsData} />}
            locale={getDateFnsLocale()}
            onViewChange={(v) => props.setView(v)}
            month={null}
            draggable={false}
            translations={{
                navigation: {
                    month: t('schedule:scheduler:month'),
                    week: t('schedule:scheduler:week'),
                    day: t('schedule:scheduler:day'),
                    today: t('schedule:scheduler:today')
                },
                form: {
                    addTitle: "Add Event",
                    editTitle: "Edit Event",
                    confirm: "Confirm",
                    delete: t('schedule:scheduler:delete'),
                    cancel: t('schedule:scheduler:cancel')
                },
                event: {
                    title: "Title",
                    start: "Start",
                    end: "End",
                    allDay: "All Day"
                },
                moreEvents: "More...",
                loading: "Loading..."
            }}
            view={"day"}
            hourFormat={"24"}
            loading={false}
            getRemoteEvents={fetchEvents}
            resources={fields.data}
            resourceFields={{
                idField: "field_id",
                textField: "name",
                subTextField: "",
                avatarField: "",
                colorField: "",
            }}
            viewerExtraComponent={(fields, event) => {
                return <div style={{marginTop: '10px'}}>
                    <div className="event-extra-row">
                        <Icon name="user"/><span>{event?.data?.customer.name}</span>
                    </div>
                    <div className="event-extra-row">
                        <Icon name="phone"/><span>+{event?.data?.customer.phoneNumber}</span>
                    </div>
                    <div className="event-extra-row">
                        <Icon name="dollar sign"/><span>{event?.data?.totalAmountAfterDiscount}</span>
                    </div>
                    {event?.data?.promoCode && 
                        <div style={{ marginTop: '5px'}} className="event-extra-row">
                            <span>{t('schedule:scheduler:promo')}: {event?.data?.promoCode} ({t('schedule:scheduler:discount')} - {event?.data?.discount})</span>
                        </div>}
                    {event?.data?.note && <div style={{ marginTop: '10px'}} className="event-extra-row">{event?.data?.note}</div>}
                </div>
            }}
            recourseHeaderComponent={(field : SchedulerFieldDto) => {
                return <div className="scheduler-field">
                    {field.data.images.length ?
                        <img src={"/legal-images/" + field.data.images[0]}/>  : <span/>
                    }
                    <div className="scheduler-field-text">
                        <div className="scheduler-field-name">{field.data.name}</div>
                        <div className="scheduler-field-sports">
                            {field.data.sportKinds.length === 0 ?
                                <span style={{paddingLeft: '10px', fontSize: '12px', fontWeight: "bold"}}>{t("booking:field_card:no_sports")}</span> :
                                field.data.sportKinds.map((s, i) => {
                                    const value = SportKind[s];
                                    const text = t("offers:sports:" + value.toLowerCase());
    
                                    return <div style={ i === 0 ? { marginLeft: 0} : {}} key={`${field.data.id}-${i}`} className="field-sport">{text}</div>;
                                })}
                        </div>
                    </div>
                </div>
            }}
        />}
    </Fragment>
}

