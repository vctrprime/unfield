import { ProcessedEvent } from "../../types";
interface MonthEventProps {
    events: ProcessedEvent[];
    today: Date;
    eachWeekStart: Date[];
    daysList: Date[];
    onViewMore(day: Date): void;
    cellHeight: number;
}
declare const MonthEvents: ({ events, today, eachWeekStart, daysList, onViewMore, cellHeight, }: MonthEventProps) => JSX.Element;
export default MonthEvents;
