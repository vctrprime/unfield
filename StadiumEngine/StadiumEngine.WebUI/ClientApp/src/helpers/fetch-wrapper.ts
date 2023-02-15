import { useSetRecoilState } from 'recoil';
import { authAtom } from '../state/auth';
import {loadingAtom} from "../state/loading";
import {useNavigate} from "react-router-dom";
import {t} from "i18next";

const ReactNotifications = require('react-notifications');
const { NotificationManager } = ReactNotifications;

export { useFetchWrapper };

function useFetchWrapper() {
    const setAuth = useSetRecoilState(authAtom);
    const setLoading = useSetRecoilState(loadingAtom);
    const navigate = useNavigate();

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
            const requestOptions: RequestInit = {
                method,
                headers: {
                    'Content-Type': contentType
                }
            };
            if (body) {
                requestOptions.body = JSON.stringify(body);
            }
            return fetch(url, requestOptions).then((response) => handleResponse(response, successMessage, hideSpinner, withSpinner, showErrorAlert));
        }
    }
    
    function handleResponse(response : Response, 
                                  successMessage?: string, 
                                  hideSpinner?: boolean, 
                                  withSpinner?: boolean,
                                  showErrorAlert? :boolean) {
        if (withSpinner && hideSpinner) setLoading(false);
        
        return response.text().then(text => {
            const data = text && JSON.parse(text);
            
            if (!response.ok) {
                const errorKey = (data && data.message) || response.statusText;
                const error = t(`errors:${errorKey}`);
                
                setLoading(false);
                if ([401].includes(response.status)) {
                    localStorage.removeItem('user');
                    setAuth(null);
                    navigate("/lk/sign-in");
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