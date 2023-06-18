/// <reference types="react" />
import { CellRenderedProps, DayHours } from "../types";
export type WeekDays = 0 | 1 | 2 | 3 | 4 | 5 | 6;
export interface MonthProps {
    weekDays: WeekDays[];
    weekStartOn: WeekDays;
    startHour: DayHours;
    endHour: DayHours;
    cellRenderer?(props: CellRenderedProps): JSX.Element;
    headRenderer?(day: Date): JSX.Element;
    navigation?: boolean;
    disableGoToDay?: boolean;
}
declare const Month: () => JSX.Element;
export { Month };
