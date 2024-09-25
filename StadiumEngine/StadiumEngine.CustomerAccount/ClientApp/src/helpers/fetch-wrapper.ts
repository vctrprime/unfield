import {useSetRecoilState} from 'recoil';
import {loadingAtom} from "../state/loading";
import {t} from "i18next";
import {useNavigate, useParams} from "react-router-dom";

const ReactNotifications = require('react-notifications');
const {NotificationManager} = ReactNotifications;

export {useFetchWrapper};

function useFetchWrapper() {
    const setLoading = useSetRecoilState(loadingAtom);
    const navigate = useNavigate();
    const { stadiumToken } = useParams();

    return {
        get: request('GET'),
        post: request('POST'),
        put: request('PUT'),
        delete: request('DELETE')
    };

    function request(method: string) {
        return ({
                    url = "",
                    body = null as any,
                    successMessage = null as unknown as string,
                    withSpinner = true,
                    hideSpinner = true,
                    showErrorAlert = true,
                    contentType = 'application/json'
                } = {}) => {
            if (withSpinner) setLoading(true);

            const date = new Date();
            const dateHeader = new Date(date.getTime() - (date.getTimezoneOffset() * 60000)).toJSON();
            
            const requestOptions: RequestInit = {
                method,
                headers: contentType === null ? {
                    'Client-Date': dateHeader,
                    'SE-Stadium-Token': stadiumToken || '0'
                } : {
                    'Content-Type': contentType,
                    'Client-Date': dateHeader,
                    'SE-Stadium-Token': stadiumToken || '0'
                }
            };
            if (body) {
                if (contentType === null) {
                    requestOptions.body = body;
                } else {
                    requestOptions.body = JSON.stringify(body);
                }

            }
            return fetch(url, requestOptions).then((response) => handleResponse(url, response, successMessage, hideSpinner, withSpinner, showErrorAlert));
        }
    }

    function handleResponse(
                            url: string,
                            response: Response,
                            successMessage?: string,
                            hideSpinner?: boolean,
                            withSpinner?: boolean,
                            showErrorAlert?: boolean) {
        if (withSpinner && hideSpinner) setLoading(false);

        return response.text().then(text => {
            const data = text && JSON.parse(text);

            if (!response.ok) {
                const errorKey = (data && data.message) || response.statusText;
                const error = t(`errors:${errorKey}`);

                setLoading(false);
                if ([401].includes(response.status)) {
                    localStorage.removeItem('customer');
                    navigate( stadiumToken ? "/" + stadiumToken + "/sign-in" : "/");
                    return Promise.reject(error);
                }
                
                if (showErrorAlert) {
                    NotificationManager.error(error, t('common:error_request_title'), 5000);
                }


                return Promise.reject(error);
            }

            if (successMessage) NotificationManager.success(successMessage, t('common:success_request_title'), 2000);

            return data;
        });
    }
}