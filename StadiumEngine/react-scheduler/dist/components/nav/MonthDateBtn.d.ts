interface MonthDateBtnProps {
    selectedDate: Date;
    onChange(value: Date): void;
}
declare const MonthDateBtn: ({ selectedDate, onChange }: MonthDateBtnProps) => JSX.Element;
export { MonthDateBtn };
