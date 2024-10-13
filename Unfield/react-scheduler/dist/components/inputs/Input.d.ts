interface EditorInputProps {
    variant?: "standard" | "filled" | "outlined";
    label?: string;
    placeholder?: string;
    required?: boolean;
    min?: number;
    max?: number;
    email?: boolean;
    decimal?: boolean;
    disabled?: boolean;
    multiline?: boolean;
    rows?: number;
    value: string;
    name: string;
    onChange(name: string, value: string, isValid: boolean): void;
    touched?: boolean;
}
declare const EditorInput: {
    ({ variant, label, placeholder, value, name, required, min, max, email, decimal, onChange, disabled, multiline, rows, touched, }: EditorInputProps): JSX.Element;
    defaultProps: {
        variant: string;
    };
};
export { EditorInput };
