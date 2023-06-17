import React, {useEffect} from 'react';
import { Fragment, useRef, useState } from "react";
import {Button} from "semantic-ui-react";
import { Scheduler } from "react-scheduler";
import {SchedulerHelpers, SchedulerRef} from "react-scheduler/types";
import {getDateFnsLocale, getLocale} from "../../../i18n/i18n";
import {useRecoilState, useRecoilValue} from "recoil";
import {rolesAtom} from "../../../state/roles";
import {PermissionsRoleDropDownData} from "../accounts/Permissions";
import {languageAtom} from "../../../state/language";
import {t} from "i18next";

export interface FieldsSchedulerProps {
    mode: string
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
    const [locale, setLocale] = useState<Locale>(getDateFnsLocale());
    
    const calendarRef = useRef<SchedulerRef>(null);
    
    useEffect(() => {
        calendarRef.current?.scheduler?.handleState(
            props.mode,
            "resourceViewMode"
        );
    }, [props.mode])

    useEffect(() => {
        if (isInitialMount.current) {
            isInitialMount.current = false;
        } else {
            window.location.reload();
        }
    }, [language])
    
    return <Fragment>
        <Scheduler
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
            locale={locale}
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
            events={EVENTS}
            resources={RESOURCES}
            resourceFields={{
                idField: "admin_id",
                textField: "title",
                subTextField: "mobile",
                avatarField: "title",
                colorField: "color"
            }}
            fields={[
                {
                    name: "admin_id",
                    type: "select",
                    default: RESOURCES[0].admin_id,
                    options: RESOURCES.map((res) => {
                        return {
                            id: res.admin_id,
                            text: `${res.title} (${res.mobile})`,
                            value: res.admin_id //Should match "name" property
                        };
                    }),
                    config: { label: "Assignee", required: true }
                }
            ]}
            viewerExtraComponent={(fields, event) => {
                return (
                    <div>
                        {fields.map((field, i) => {
                            if (field.name === "admin_id") {
                                const admin = field.options?.find(
                                    (fe) => fe.id === event.admin_id
                                );
                                return (
                                    <span>{admin?.text}</span>
                                );
                            } else {
                                return "";
                            }
                        })}
                    </div>
                );
            }}
        />
    </Fragment>
}

export const EVENTS = [
    {
        event_id: 1,
        title: "Event 1",
        start: new Date(new Date(new Date().setHours(9)).setMinutes(30)),
        end: new Date(new Date(new Date().setHours(10)).setMinutes(30)),
        admin_id: 1
    },
    {
        event_id: 2,
        title: "Event 2",
        start: new Date(new Date(new Date().setHours(10)).setMinutes(0)),
        end: new Date(new Date(new Date().setHours(11)).setMinutes(0)),
        admin_id: 2
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
        admin_id: 2
    },
    {
        event_id: 6,
        title: "Event 6",
        start: new Date(new Date(new Date().setHours(11)).setMinutes(0)),
        end: new Date(new Date(new Date().setHours(12)).setMinutes(0)),
        admin_id: 2
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
        admin_id: 3
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
        admin_id: 4
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
        admin_id: 1
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
        admin_id: 2
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
        admin_id: 1
    }
];

export const RESOURCES = [
    {
        admin_id: 1,
        title: "John",
        mobile: "555666777",
        avatar: "https://picsum.photos/200/300",
        color: "#ab2d2d"
    },
    {
        admin_id: 2,
        title: "Sarah",
        mobile: "545678354",
        avatar: "https://picsum.photos/200/300",
        color: "#58ab2d"
    },
    {
        admin_id: 3,
        title: "Joseph",
        mobile: "543678433",
        avatar: "https://picsum.photos/200/300",
        color: "#a001a2"
    },
    {
        admin_id: 4,
        title: "Mera",
        mobile: "507487620",
        avatar: "https://picsum.photos/200/300",
        color: "#08c5bd"
    }
];

