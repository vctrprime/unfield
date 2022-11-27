import { useRecoilState } from 'recoil';

import { authAtom } from '../state/auth';
import {loadingAtom} from "../state/loading";
//import { useAlertActions } from '';

export { useFetchWrapper };

function useFetchWrapper() {
    const [auth, setAuth] = useRecoilState(authAtom);
    const [loading, setLoading] = useRecoilState(loadingAtom);
    //const alertActions = useAlertActions();

    return {
        get: request('GET'),
        post: request('POST'),
        put: request('PUT'),
        delete: request('DELETE')
    };

    function request(method) {
        return (url, body = null, withSpinner = true) => {
            if (withSpinner) setLoading(true);
            const requestOptions = {
                method,
                headers: {}
            };
            if (body) {
                requestOptions.headers['Content-Type'] = 'application/json';
                requestOptions.body = JSON.stringify(body);
            }
            return fetch(url, requestOptions).then(handleResponse);
        }
    }
    
    function handleResponse(response) {
        setLoading(false);
        return response.text().then(text => {
            const data = text && JSON.parse(text);

            if (!response.ok) {
                if ([401].includes(response.status) && auth?.token) {
                    // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
                    localStorage.removeItem('user');
                    setAuth(null);

                    window.location.pathname = "/lk/sign-in";
                }

                const error = (data && data.message) || response.statusText;
                //alertActions.error(error);
                return Promise.reject(error);
            }
            return data;
        });
    }
}