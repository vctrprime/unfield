import { useRecoilState } from 'recoil';

import { authAtom } from '../state/auth';
import {loadingAtom} from "../state/loading";
import {useNavigate} from "react-router-dom";
import {NotificationManager} from "react-notifications";
//import { useAlertActions } from '';

export { useFetchWrapper };

function useFetchWrapper() {
    const [auth, setAuth] = useRecoilState(authAtom);
    const [loading, setLoading] = useRecoilState(loadingAtom);
    const navigate = useNavigate();

    return {
        get: request('GET'),
        post: request('POST'),
        put: request('PUT'),
        delete: request('DELETE')
    };

    function request(method) {
        return (url, body = null, successMessage = null, withSpinner = true) => {
            if (withSpinner) setLoading(true);
            const requestOptions = {
                method,
                headers: {}
            };
            if (body) {
                requestOptions.headers['Content-Type'] = 'application/json';
                requestOptions.body = JSON.stringify(body);
            }
            return fetch(url, requestOptions).then((response) => handleResponse(response, successMessage));
        }
    }
    
    function handleResponse(response, successMessage) {
        setLoading(false);
        return response.text().then(text => {
            const data = text && JSON.parse(text);

            if (!response.ok) {
                if ([401].includes(response.status) && auth?.token) {
                    // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
                    localStorage.removeItem('user');
                    setAuth(null);
                    navigate("/lk/sign-in");
                }

                const error = (data && data.message) || response.statusText;
                //alertActions.error(error);
                NotificationManager.error(error, "Ошибка", 2000);
                
                return Promise.reject(error);
            }
            
            if (successMessage) NotificationManager.success(successMessage, 'Успешно!', 2000);
            
            return data;
        });
    }
}