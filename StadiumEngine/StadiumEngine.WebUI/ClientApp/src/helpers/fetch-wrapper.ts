import { useSetRecoilState } from 'recoil';

import { authAtom } from '../state/auth';
import {loadingAtom} from "../state/loading";
import {useNavigate} from "react-router-dom";
import { Store } from 'react-notifications-component';
//import { useAlertActions } from '';

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
                    hideSpinner = true
                } = {}) => {
            if (withSpinner) setLoading(true);
            const requestOptions: RequestInit = {
                method,
                headers: {
                    'Content-Type': 'application/json'
                }
            };
            if (body) {
                requestOptions.body = JSON.stringify(body);
            }
            return fetch(url, requestOptions).then((response) => handleResponse(response, successMessage, hideSpinner, withSpinner));
        }
    }
    
    function handleResponse(response : Response, 
                                  successMessage?: string, 
                                  hideSpinner?: boolean, 
                                  withSpinner?: boolean) {
        if (withSpinner && hideSpinner) setLoading(false);
        
        return response.text().then(text => {
            const data = text && JSON.parse(text);
            
            if (!response.ok) {
                const error = (data && data.message) || response.statusText;
                
                setLoading(false);
                if ([401].includes(response.status)) {
                    localStorage.removeItem('user');
                    setAuth(null);
                    navigate("/lk/sign-in");
                    return Promise.reject(error);
                }
                Store.addNotification({
                    title:  "Ошибка",
                    message: error,
                    type: "danger",
                    insert: "top",
                    container: "top-right",
                    animationIn: ["animate__animated", "animate__fadeIn"],
                    animationOut: ["animate__animated", "animate__fadeOut"],
                    dismiss: {
                        duration: 1500,
                        onScreen: true
                    }
                })
                
                return Promise.reject(error);
            }
            
            if (successMessage) {
                Store.addNotification({
                    title: "Успешно!",
                    message: successMessage,
                    type: "success",
                    insert: "bottom",
                    container: "top-right",
                    animationIn: ["animate__animated", "animate__fadeIn"],
                    animationOut: ["animate__animated", "animate__fadeOut"],
                    dismiss: {
                        duration: 1000,
                        onScreen: true
                    }
                })
            }
            
            return data;
        });
    }
}