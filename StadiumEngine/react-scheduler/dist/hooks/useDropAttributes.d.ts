import { DragEvent } from "react";
interface Props {
    start: Date;
    end: Date;
    resourceKey: string;
    resourceVal: string | number;
}
export declare const useDropAttributes: ({ start, end, resourceKey, resourceVal }: Props) => {
    [x: string]: string | number | ((e: DragEvent<HTMLButtonElement>) => void);
    onClick: () => void;
    onDragOver: (e: DragEvent<HTMLButtonElement>) => void;
    onDragEnter: (e: DragEvent<HTMLButtonElement>) => void;
    onDragLeave: (e: DragEvent<HTMLButtonElement>) => void;
    onDrop: (e: DragEvent<HTMLButtonElement>) => void;
};
export {};
