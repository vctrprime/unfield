import { ProcessedEvent } from "../../types";
interface Props {
    event: ProcessedEvent;
    onDelete(): void;
    onEdit(): void;
    direction: "rtl" | "ltr";
    editable?: boolean;
    deletable?: boolean;
}
declare const EventActions: ({ event, onDelete, onEdit, direction, editable, deletable }: Props) => JSX.Element;
export default EventActions;
