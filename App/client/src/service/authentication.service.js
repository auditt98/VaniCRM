import {BehaviorSubject} from "rxjs";
import {config} from "@/config/config";
import {requestOptions} from "@/helper/request-options";
import {handleResponse} from "@/helper/handle-response";
import router from "@/router";


const currentUserSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('currentUser')));

export const authenticationService = {
    login,
    logout,
    requestResetPass,
    updateNewPass,
    currentUser: currentUserSubject.asObservable(),
    get currentUserValue () { return currentUserSubject.value }
};

function login(email, password) {
    return fetch(`${config.apiUrl + 'login'}`, requestOptions.post({ email, password }))
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

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    currentUserSubject.next(null);
    router.push('/login');
}