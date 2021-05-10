import {authenticationService} from "@/service/authentication.service";

export function handleResponse(response) {
    return response.text().then(async text => {
        console.log("handle response", " ", response.ok, " ", response.status)
        const data = text && JSON.parse(text);
        if (!response.ok) {
            if ([401, 403].indexOf(response.status) !== -1) {
                let res = await authenticationService.getRefreshToken()
                if(Object.prototype.hasOwnProperty.call(res, 'jwt')){
                    return Promise.reject("retry")
                }
                if(!res || !res.data){
                    authenticationService.logout();
                    authenticationService.logout(window.location.pathname + window.location.search);
                    const error = (data && data.message) || response.statusText;
                    return Promise.reject(error);
                } else{
                    return data
                }
            }
        } else {
            return data;
        }
    });
}