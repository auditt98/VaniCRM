import {authenticationService} from "@/service/authentication.service";

export function handleResponse(response) {
    return response.text().then(text => {
        const data = text && JSON.parse(text);
        if (!response.ok) {
            if ([401, 403].indexOf(response.status) !== -1) {
                /*authenticationService.getRefreshToken()
                    .then(res => {
                        if (res === null) {
                            authenticationService.logout();
                            const error = (data && data.message) || response.statusText;
                            return Promise.reject(error);
                        }
                    }).catch(() => {
                    authenticationService.logout();
                    const error = (data && data.message) || response.statusText;
                    return Promise.reject(error);
                })*/
                authenticationService.logout(window.location.pathname + window.location.search);
                const error = (data && data.message) || response.statusText;
                return Promise.reject(error);
            }
        }

        return data;
    });
}