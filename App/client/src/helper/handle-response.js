import {authenticationService} from "@/service/authentication.service";
import {getters, } from "@/helper/observable";
export function handleResponse(response) {
    return response.text().then(async text => {
        const data = text && JSON.parse(text);
        //if there is an error 
        if (!response.ok) {
            //if the error is related to authentication
            console.log([403].indexOf(response.status) !== -1)
            if ([401].indexOf(response.status) !== -1) {
                if(getters.isRefreshing()){
                    var result = await new Promise((resolve) => {
                        const intervalId = setInterval(() => {
                            // if the state indicates that there is no refresh token request anymore, it clears the time interval and retries the failed API call with updated token data
                            if (!getters.isRefreshing()) {
                                clearInterval(intervalId);
                                return resolve("retry");
                            }
                        }, 100);
                    })
                    if(result == "retry"){
                        
                        return Promise.reject("retry")
                    }
                    // var x = 1
                } else{
                    let res = await authenticationService.getRefreshToken()
                    
                    if(Object.prototype.hasOwnProperty.call(res, 'jwt')){
                        //reject the promise and ask for retry
                        return Promise.reject("retry")
                    }
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
            }
            if([403].indexOf(response.status) !== -1){
                alert(data.message)
            }
            
        } else {
            //if there is no error then just return the data
            return data;
        }
    });
}