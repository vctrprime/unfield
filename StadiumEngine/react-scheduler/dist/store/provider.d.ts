/// <reference types="react" />
import { SchedulerProps } from "../types";
type Props = {
    children: React.ReactNode;
    initial: Partial<SchedulerProps>;
};
export declare const StoreProvider: ({ children, initial }: Props) => JSX.Element;
export {};
