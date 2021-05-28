import {authenticationService} from "@/service/authentication.service";

export function handleResponse(response) {
    return response.text().then(async text => {
        console.log("handle response", " ", response.ok, " ", response.status)
        const data = text && JSON.parse(text);
        //if there is an error 
        if (!response.ok) {
            //if the error is related to authentication
            if ([401, 403].indexOf(response.status) !== -1) {
                //try to get new refresh token
                let res = await authenticationService.getRefreshToken()
                //if response has a field called jwt
                console.log(res);
                if(Object.prototype.hasOwnProperty.call(res, 'jwt')){
                    //reject the promise and ask for retry
                    return Promise.reject("retry")
                }
                //if response is empty
                if(!res || !res.data){
                    //logout and reject the promise
                    authenticationService.logout();
                    authenticationService.logout(window.location.pathname + window.location.search);
                    const error = (data && data.message) || response.statusText;
                    return Promise.reject(error);
                } else{
                    return data
                }
            }
        } else {
            //if there is no error then just return the data
            return data;
        }
    });
}