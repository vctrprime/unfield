interface EditorDatePickerProps {
    type: "date" | "datetime";
    label?: string;
    variant?: "standard" | "filled" | "outlined";
    value: Date | string;
    name: string;
    onChange(name: string, date: Date): void;
    error?: boolean;
    errMsg?: string;
}
declare const EditorDatePicker: {
    ({ type, value, label, name, onChange, variant, error, errMsg, }: EditorDatePickerProps): JSX.Element;
    defaultProps: {
        type: string;
        variant: string;
    };
};
export { EditorDatePicker };
