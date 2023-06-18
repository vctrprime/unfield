/// <reference types="react" />
import { CellRenderedProps, DayHours } from "../types";
export interface DayProps {
    startHour: DayHours;
    endHour: DayHours;
    step: number;
    cellRenderer?(props: CellRenderedProps): JSX.Element;
    headRenderer?(day: Date): JSX.Element;
    navigation?: boolean;
}
declare const Day: () => JSX.Element;
export { Day };
