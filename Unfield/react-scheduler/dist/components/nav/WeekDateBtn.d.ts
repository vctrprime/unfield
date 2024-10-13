import { WeekProps } from "../../views/Week";
interface WeekDateBtnProps {
    selectedDate: Date;
    onChange(value: Date): void;
    weekProps: WeekProps;
}
declare const WeekDateBtn: ({ selectedDate, onChange, weekProps }: WeekDateBtnProps) => JSX.Element;
export { WeekDateBtn };
