/**
 * The solution to make headers sticky with overflow
 */
declare const useSyncScroll: () => {
    headersRef: import("react").RefObject<HTMLDivElement>;
    bodyRef: import("react").RefObject<HTMLDivElement>;
};
export default useSyncScroll;
