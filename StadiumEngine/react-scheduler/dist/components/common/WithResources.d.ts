import { ReactChild } from "react";
import { DefaultRecourse } from "../../types";
interface WithResourcesProps {
    renderChildren(resource: DefaultRecourse): ReactChild;
}
declare const WithResources: {
    ({ renderChildren }: WithResourcesProps): JSX.Element;
    defaultProps: {
        span: number;
    };
};
export { WithResources };
