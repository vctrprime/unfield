export {useFetchWrapper};

function useFetchWrapper() {
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
                    contentType = 'application/json'
                } = {}) => {
            
            const date = new Date();
            const dateHeader = new Date(date.getTime() - (date.getTimezoneOffset() * 60000)).toJSON();
            
            const requestOptions: RequestInit = {
                method,
                headers: contentType === null ? {
                    'Client-Date': dateHeader
                } : {
                    'Content-Type': contentType,
                    'Client-Date': dateHeader
                }
            };
            if (body) {
                if (contentType === null) {
                    requestOptions.body = body;
                } else {
                    requestOptions.body = JSON.stringify(body);
                }

            }
            return fetch(url, requestOptions).then((response) => handleResponse(url, response));
        }
    }

    function handleResponse(
                            url: string,
                            response: Response ) {

        return response.text().then(text => {
            const data = text && JSON.parse(text);

            if (!response.ok) {
                const error = (data && data.message) || response.statusText;
                console.log(error);
                

                return Promise.reject(error);
            }
            return data;
        });
    }
}