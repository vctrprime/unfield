import { FieldInputProps, InputTypes, ProcessedEvent } from "../types";
import { SelectedRange } from "../store/types";
export type StateItem = {
    value: any;
    validity: boolean;
    type: InputTypes;
    config?: FieldInputProps;
};
export type StateEvent = (ProcessedEvent & SelectedRange) | Record<string, any>;
declare const Editor: () => JSX.Element;
export default Editor;
