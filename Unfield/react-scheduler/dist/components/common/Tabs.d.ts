import { CSSProperties } from "react";
export type ButtonTabProps = {
    id: string | number;
    label: string | JSX.Element;
    component: JSX.Element;
};
interface ButtonTabsProps {
    tabs: ButtonTabProps[];
    tab: string | number;
    setTab(tab: string | number): void;
    variant?: "scrollable" | "standard" | "fullWidth";
    indicator?: "primary" | "secondary" | "info" | "error";
    style?: CSSProperties;
}
declare const ButtonTabs: {
    ({ tabs, variant, tab, setTab, indicator, style }: ButtonTabsProps): JSX.Element;
    defaultProps: {
        variant: string;
        indicator: string;
    };
};
export { ButtonTabs };
