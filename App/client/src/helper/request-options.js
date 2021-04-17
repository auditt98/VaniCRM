import {authenticationService} from "@/service/authentication.service";

export const requestOptions = {
    get() {
        return {
            method: 'GET',
            ...headers()
        };
    },
    post(body) {
        return {
            method: 'POST',
            ...headers(),
            body: JSON.stringify(body),
            // credentials: 'include'
        };
    },
    patch(body) {
        return {
            method: 'PATCH',
            ...headers(),
            body: JSON.stringify(body)
        };
    },
    put(body) {
        return {
            method: 'PUT',
            ...headers(),
            body: JSON.stringify(body)
        };
    },
    delete() {
        return {
            method: 'DELETE',
            ...headers()
        };
    }
}

function headers() {
    const currentUser = authenticationService.currentUserValue || {};
    const authHeader = currentUser.jwt ? { 'Authorization': currentUser.jwt } : {};
    return {
        headers: {
            ...authHeader,
            'Content-Type': 'application/json'
        }
    }
}