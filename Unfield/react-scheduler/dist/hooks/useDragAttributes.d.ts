import { DragEvent } from "react";
import { ProcessedEvent } from "../types";
declare const useDragAttributes: (event: ProcessedEvent) => {
    draggable: boolean;
    onDragStart: (e: DragEvent<HTMLElement>) => void;
    onDragEnd: (e: DragEvent<HTMLElement>) => void;
    onDragOver: (e: DragEvent<HTMLElement>) => void;
    onDragEnter: (e: DragEvent<HTMLElement>) => void;
};
export default useDragAttributes;
