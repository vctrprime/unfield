import {container} from "inversify-hooks";
import {CommonService, ICommonService} from "../services/CommonService";

export function registerServices() {
    container.addRequest<ICommonService>(CommonService, 'CommonService');
}