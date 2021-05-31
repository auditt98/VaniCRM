import {BehaviorSubject} from "rxjs";
import {config} from "@/config/config";
import {requestOptions} from "@/helper/request-options";
import {handleResponse} from "@/helper/handle-response";
import {getters, mutations} from "@/helper/observable";

import router from "@/router";
// import { resolve } from "core-js/fn/promise";


const currentUserSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('currentUser')));

export const authenticationService = {
    login,
    logout,
    requestResetPass,
    updateNewPass,
    getRefreshToken,
    currentUser: currentUserSubject.asObservable(),
    get currentUserValue () { return currentUserSubject.value }
};

function login(email, password) {
    return fetch(`${config.apiUrl + 'login'}`, 
                //requestOptions.post({ email, password }
                {
                    method: "POST", 
                    credentials: 'include', 
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({email, password}),
                }
        )
        .then(handleResponse)
        .then(res => {
            const user = res.data.user;
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('currentUser', JSON.stringify(user));
            currentUserSubject.next(user);

            return user;
        });
}

function updateNewPass(newPassword, key) {
    return fetch(`${config.apiUrl + 'reset_password'}`, requestOptions.post({ newPassword, key }))
        .then(handleResponse)
        .then(res => {
            return res;
        });
}

function requestResetPass(email) {
    return fetch(`${config.apiUrl + 'forgot_password?email=' + email}`, requestOptions.get())
        .then(handleResponse)
        .then(res => {
            return res;
        });
}

function getRefreshToken() {
    // return new Promise(() => {
        mutations.setIsRefreshing(true);
        console.log("Set is refresh token: ",getters.isRefreshing())
        return fetch(`${config.apiUrl + 'refresh_token'}`, {method: "GET", credentials: 'include', headers: {
            'Content-Type': 'application/json'
        }}).then((res) => {
                return res.text().then(text =>{
                    const data = text && JSON.parse(text);
                    if(data.status == "success"){
                        if(data.data && data.data.user){
                            let user = data.data.user;
                            localStorage.setItem('currentUser', JSON.stringify(user));
                            currentUserSubject.next(user);
                            mutations.setIsRefreshing(false);
                            return data.data.user;
                        }
                    } else{
                        return logout();
                    }
                })
            }).catch((e) => {
                console.log(e)
                return logout();
            });
    // })
    
}

function logout(nextUrl) {
    // remove user from local storage to log user out
    return fetch(`${config.apiUrl + 'logout'}`, {method: "GET", credentials: 'include'}).then(() =>{
        localStorage.removeItem('currentUser');
        currentUserSubject.next(null);
        const url = nextUrl ? '/login?returnUrl=' + nextUrl : '/login';
        router.push(url);
    })
}