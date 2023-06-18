export type SelectOption = {
    id: string | number;
    text: string;
    value: any;
};
interface EditorSelectProps {
    options: Array<SelectOption>;
    value: string;
    name: string;
    onChange(name: string, value: string, isValid: boolean): void;
    variant?: "standard" | "filled" | "outlined";
    label?: string;
    placeholder?: string;
    required?: boolean;
    disabled?: boolean;
    touched?: boolean;
    loading?: boolean;
    multiple?: "default" | "chips";
    errMsg?: string;
}
declare const EditorSelect: {
    ({ options, value, name, required, onChange, label, disabled, touched, variant, loading, multiple, placeholder, errMsg, }: EditorSelectProps): JSX.Element;
    defaultProps: {
        variant: string;
    };
};
export { EditorSelect };
