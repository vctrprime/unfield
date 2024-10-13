import { SchedulerProps } from "../types";
export declare const defaultProps: (props: Partial<SchedulerProps>) => {
    height: number;
    navigation: boolean;
    selectedDate: Date;
    disableViewNavigator: boolean;
    events: never[] & import("../types").ProcessedEvent[];
    fields: never[] & import("../types").FieldProps[];
    loading: undefined;
    customEditor: undefined;
    onConfirm: undefined;
    onDelete: undefined;
    viewerExtraComponent: undefined;
    resources: never[] & import("../types").DefaultRecourse[];
    recourseHeaderComponent: undefined;
    resourceViewMode: "default" | "tabs";
    direction: "ltr" | "rtl";
    dialogMaxWidth: "md" | "xs" | "sm" | "lg" | "xl";
    locale: Locale;
    deletable: boolean;
    editable: boolean;
    hourFormat: "12" | "24";
    draggable: boolean;
    navigationPickerProps?: Partial<Omit<import("@mui/x-date-pickers").DateCalendarProps<Date>, "onClose" | "open" | "onChange" | "openTo" | "views" | "value" | "readOnly">> | undefined;
    eventRenderer?: ((props: import("../types").EventRendererProps) => JSX.Element | null) | undefined;
    getRemoteEvents?: ((params: import("../types").ViewEvent) => Promise<void | import("../types").ProcessedEvent[]>) | undefined;
    customViewer?: ((event: import("../types").ProcessedEvent, close: () => void) => JSX.Element) | undefined;
    viewerTitleComponent?: ((event: import("../types").ProcessedEvent) => JSX.Element) | undefined;
    timeZone?: string | undefined;
    onEventDrop?: ((droppedOn: Date, updatedEvent: import("../types").ProcessedEvent, originalEvent: import("../types").ProcessedEvent) => Promise<void | import("../types").ProcessedEvent>) | undefined;
    onEventClick?: ((event: import("../types").ProcessedEvent) => void) | undefined;
    onSelectedDateChange?: ((date: Date) => void) | undefined;
    onViewChange?: ((view: import("../components/nav/Navigation").View) => void) | undefined;
    cellHeight?: number | undefined;
    translations: {
        moreEvents: string;
        loading: string;
        navigation: {
            month: string;
            week: string;
            day: string;
            today: string;
        } & Record<import("../components/nav/Navigation").View, string> & {
            today: string;
        };
        form: {
            addTitle: string;
            editTitle: string;
            confirm: string;
            delete: string;
            cancel: string;
        } & {
            addTitle: string;
            editTitle: string;
            confirm: string;
            delete: string;
            cancel: string;
        };
        event: {
            title: string;
            start: string;
            end: string;
            allDay: string;
        } & Record<string, string> & {
            title: string;
            start: string;
            end: string;
            allDay: string;
        };
    };
    resourceFields: {
        idField: string;
        textField: string;
        subTextField: string;
        avatarField: string;
        colorField: string;
    } & {
        idField: string;
        textField: string;
        subTextField?: string | undefined;
        avatarField?: string | undefined;
        colorField?: string | undefined;
    } & Record<string, string>;
    view: import("../components/nav/Navigation").View;
    month: ({
        weekDays: number[];
        weekStartOn: number;
        startHour: number;
        endHour: number;
        navigation: boolean;
        disableGoToDay: boolean;
    } & import("../views/Month").MonthProps) | null;
    week: ({
        weekDays: number[];
        weekStartOn: number;
        startHour: number;
        endHour: number;
        step: number;
        navigation: boolean;
        disableGoToDay: boolean;
    } & import("../views/Week").WeekProps) | null;
    day: ({
        startHour: number;
        endHour: number;
        step: number;
        navigation: boolean;
    } & import("../views/Day").DayProps) | null;
};
export declare const initialStore: {
    setProps: () => void;
    dialog: boolean;
    selectedRange: undefined;
    selectedEvent: undefined;
    selectedResource: undefined;
    handleState: () => void;
    getViews: () => never[];
    triggerDialog: () => void;
    triggerLoading: () => void;
    handleGotoDay: () => void;
    confirmEvent: () => void;
    onDrop: () => void;
    height: number;
    navigation: boolean;
    selectedDate: Date;
    disableViewNavigator: boolean;
    events: never[] & import("../types").ProcessedEvent[];
    fields: never[] & import("../types").FieldProps[];
    loading: undefined;
    customEditor: undefined;
    onConfirm: undefined;
    onDelete: undefined;
    viewerExtraComponent: undefined;
    resources: never[] & import("../types").DefaultRecourse[];
    recourseHeaderComponent: undefined;
    resourceViewMode: "default" | "tabs";
    direction: "ltr" | "rtl";
    dialogMaxWidth: "md" | "xs" | "sm" | "lg" | "xl";
    locale: Locale;
    deletable: boolean;
    editable: boolean;
    hourFormat: "12" | "24";
    draggable: boolean;
    navigationPickerProps?: Partial<Omit<import("@mui/x-date-pickers").DateCalendarProps<Date>, "onClose" | "open" | "onChange" | "openTo" | "views" | "value" | "readOnly">> | undefined;
    eventRenderer?: ((props: import("../types").EventRendererProps) => JSX.Element | null) | undefined;
    getRemoteEvents?: ((params: import("../types").ViewEvent) => Promise<void | import("../types").ProcessedEvent[]>) | undefined;
    customViewer?: ((event: import("../types").ProcessedEvent, close: () => void) => JSX.Element) | undefined;
    viewerTitleComponent?: ((event: import("../types").ProcessedEvent) => JSX.Element) | undefined;
    timeZone?: string | undefined;
    onEventDrop?: ((droppedOn: Date, updatedEvent: import("../types").ProcessedEvent, originalEvent: import("../types").ProcessedEvent) => Promise<void | import("../types").ProcessedEvent>) | undefined;
    onEventClick?: ((event: import("../types").ProcessedEvent) => void) | undefined;
    onSelectedDateChange?: ((date: Date) => void) | undefined;
    onViewChange?: ((view: import("../components/nav/Navigation").View) => void) | undefined;
    cellHeight?: number | undefined;
    translations: {
        moreEvents: string;
        loading: string;
        navigation: {
            month: string;
            week: string;
            day: string;
            today: string;
        } & Record<import("../components/nav/Navigation").View, string> & {
            today: string;
        };
        form: {
            addTitle: string;
            editTitle: string;
            confirm: string;
            delete: string;
            cancel: string;
        } & {
            addTitle: string;
            editTitle: string;
            confirm: string;
            delete: string;
            cancel: string;
        };
        event: {
            title: string;
            start: string;
            end: string;
            allDay: string;
        } & Record<string, string> & {
            title: string;
            start: string;
            end: string;
            allDay: string;
        };
    };
    resourceFields: {
        idField: string;
        textField: string;
        subTextField: string;
        avatarField: string;
        colorField: string;
    } & {
        idField: string;
        textField: string;
        subTextField?: string | undefined;
        avatarField?: string | undefined;
        colorField?: string | undefined;
    } & Record<string, string>;
    view: import("../components/nav/Navigation").View;
    month: ({
        weekDays: number[];
        weekStartOn: number;
        startHour: number;
        endHour: number;
        navigation: boolean;
        disableGoToDay: boolean;
    } & import("../views/Month").MonthProps) | null;
    week: ({
        weekDays: number[];
        weekStartOn: number;
        startHour: number;
        endHour: number;
        step: number;
        navigation: boolean;
        disableGoToDay: boolean;
    } & import("../views/Week").WeekProps) | null;
    day: ({
        startHour: number;
        endHour: number;
        step: number;
        navigation: boolean;
    } & import("../views/Day").DayProps) | null;
};
