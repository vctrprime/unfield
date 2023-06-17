import React, {useEffect} from 'react';
import { Fragment, useRef, useState } from "react";
import {Button} from "semantic-ui-react";
import { Scheduler } from "react-scheduler";
import {ProcessedEvent, SchedulerHelpers, SchedulerRef, ViewEvent} from "react-scheduler/types";
import {getDateFnsLocale} from "../../../i18n/i18n";
import {useRecoilValue} from "recoil";
import {languageAtom} from "../../../state/language";
import {t} from "i18next";
import {useInject} from "inversify-hooks";
import {IScheduleService} from "../../../services/ScheduleService";
import {ScheduleFieldDto} from "../../../models/dto/schedule/ScheduleFieldDto";
import {SportKind} from "../../../models/dto/offers/enums/SportKind";

export interface FieldsSchedulerProps {
    mode: string,
    setView: any
}

interface CustomEditorProps {
    scheduler: SchedulerHelpers;
}
export const CustomEditor = ({ scheduler }: CustomEditorProps) => {
    const event = scheduler.edited;

    // Make your own form/state
    const [state, setState] = useState({
        event_id: event?.event_id || 0,
        title: event?.title || "",
        description: event?.description || ""
    });

    return (
        <div>
            <div style={{ padding: "1rem" }}>
                <p>Load your custom form/fields</p>
                {state.event_id}
            </div>
            <Button onClick={scheduler.close}>Cancel</Button>
        </div>
    );
}

export const FieldsScheduler = (props: FieldsSchedulerProps) => {
    const language = useRecoilValue<string>(languageAtom);
    const isInitialMount = useRef(true);
    
    const [scheduleService] = useInject<IScheduleService>('ScheduleService');
    
    const [fields, setFields] = useState<ScheduleFieldDto[]>([]);
    
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

    useEffect(() => {
        if (isInitialMount.current) {
            isInitialMount.current = false;
        } else {
            window.location.reload();
        }
    }, [language])
    
    

    const fetchEvents = async (query: ViewEvent): Promise<ProcessedEvent[]> => {
        console.log({ query });
        /**Simulate fetchin remote data */
        return new Promise((res) => {
            setTimeout(() => {
                res(EVENTS);
            }, 3000);
        });
    };
    
    return <Fragment>
        {fields.length > 0 && <Scheduler
            ref={calendarRef}
            day={{
                startHour: 8,
                endHour: 23,
                step: 30,
            }}
            week={{
                startHour: 8,
                endHour: 23,
                step: 30,
                weekDays: [0, 1, 2, 3, 4, 5, 6],
                weekStartOn: 1,
            }}
            customEditor={(scheduler) => <CustomEditor scheduler={scheduler} />}
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
                    delete: "Delete",
                    cancel: "Cancel"
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
            //events={EVENTS}
            getRemoteEvents={fetchEvents}
            resources={fields}
            resourceFields={{
                idField: "field_id",
                textField: "name",
                subTextField: "",
                avatarField: "",
                colorField: "",
            }}
            viewerExtraComponent={(fields, event) => {
                return <div style={{marginTop: '20px'}}>{event?.data?.number}</div>
            }}
            recourseHeaderComponent={(field : ScheduleFieldDto) => {
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
    
                                    return <div style={ i === 0 ? { marginLeft: 0} : {}} key={i} className="field-sport">{text}</div>;
                                })}
                        </div>
                    </div>
                </div>
            }}
        />}
    </Fragment>
}

export const EVENTS = [
    {
        event_id: 1,
        title: "20230607-1100-1-5 | Ковальчук",
        start: new Date(new Date(new Date().setHours(9)).setMinutes(30)),
        end: new Date(new Date(new Date().setHours(10)).setMinutes(30)),
        field_id: 1,
        data: {
            number: '123456'
        }
    },
    {
        event_id: 2,
        title: "Event 2",
        start: new Date(new Date(new Date().setHours(10)).setMinutes(0)),
        end: new Date(new Date(new Date().setHours(11)).setMinutes(0)),
        field_id: 2
    },
    {
        event_id: 4,
        title: "Event 4",
        start: new Date(
            new Date(new Date(new Date().setHours(9)).setMinutes(0)).setDate(
                new Date().getDate() - 2
            )
        ),
        end: new Date(
            new Date(new Date(new Date().setHours(10)).setMinutes(0)).setDate(
                new Date().getDate() - 2
            )
        ),
        field_id: 2
    },
    {
        event_id: 4,
        title: "Event 6",
        disabled: true,
        start: new Date(new Date(new Date().setHours(11)).setMinutes(0)),
        end: new Date(new Date(new Date().setHours(12)).setMinutes(0)),
        field_id: 2
    },
    {
        event_id: 7,
        title: "Event 7",
        start: new Date(
            new Date(new Date(new Date().setHours(11)).setMinutes(0)).setDate(
                new Date().getDate() - 1
            )
        ),
        end: new Date(
            new Date(new Date(new Date().setHours(12)).setMinutes(0)).setDate(
                new Date().getDate() - 1
            )
        ),
        field_id: 3
    },
    {
        event_id: 8,
        title: "Event 8",
        start: new Date(
            new Date(new Date(new Date().setHours(13)).setMinutes(0)).setDate(
                new Date().getDate() - 1
            )
        ),
        end: new Date(
            new Date(new Date(new Date().setHours(14)).setMinutes(0)).setDate(
                new Date().getDate() - 1
            )
        ),
        field_id: 4
    },
    {
        event_id: 9,
        title: "Event 11",
        start: new Date(
            new Date(new Date(new Date().setHours(13)).setMinutes(0)).setDate(
                new Date().getDate() + 1
            )
        ),
        end: new Date(
            new Date(new Date(new Date().setHours(15)).setMinutes(30)).setDate(
                new Date().getDate() + 1
            )
        ),
        field_id: 1
    },
    {
        event_id: 10,
        title: "Event 9",
        start: new Date(
            new Date(new Date(new Date().setHours(15)).setMinutes(0)).setDate(
                new Date().getDate() + 1
            )
        ),
        end: new Date(
            new Date(new Date(new Date().setHours(16)).setMinutes(30)).setDate(
                new Date().getDate() + 1
            )
        ),
        field_id: 2
    },
    {
        event_id: 11,
        title: "Event 10",
        start: new Date(
            new Date(new Date(new Date().setHours(11)).setMinutes(0)).setDate(
                new Date().getDate() - 1
            )
        ),
        end: new Date(
            new Date(new Date(new Date().setHours(15)).setMinutes(0)).setDate(
                new Date().getDate() - 1
            )
        ),
        field_id: 1
    }
];

