interface DayDateBtnProps {
    selectedDate: Date;
    onChange(value: Date): void;
}
declare const DayDateBtn: ({ selectedDate, onChange }: DayDateBtnProps) => JSX.Element;
export { DayDateBtn };
