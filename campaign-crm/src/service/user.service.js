import {buildQueryURI, config} from "@/config/config";
import {requestOptions} from "@/helper/request-options";
import {handleResponse} from "@/helper/handle-response";
import {authenticationService} from "@/service/authentication.service";


export const userService = {
    getAll,
    create,
    update,
    getById,
    changePassword,
    remove,
    getAllTasks,
    getAllLeads,
    getAllAccounts,
    getAllContacts,
    changeAvatar
};

function create(user) {
    return fetch(`${config.apiUrl}/users`, requestOptions.post(user))
        .then(handleResponse);
}

function update(user, id) {
    return fetch(`${config.apiUrl}/users/${id}`, requestOptions.post(user))
        .then(handleResponse);
}

function getAll(q) {
    return fetch(`${config.apiUrl}/users?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse);
}

function getById(id) {
    return fetch(`${config.apiUrl}/users/${id}`, requestOptions.get())
        .then(handleResponse);
}

function changePassword(id, oldPassword, newPassword) {
    return fetch(`${config.apiUrl}/users/${id}/change_password`, requestOptions.post({oldPassword, newPassword}))
        .then(handleResponse);
}


function remove(id) {
    return fetch(`${config.apiUrl}/users/${id}`, requestOptions.delete())
        .then(handleResponse);
}


function getAllTasks(id, q) {
    return fetch(`${config.apiUrl}/users/${id}/tasks?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse);
}

function getAllLeads(id, q) {
    return fetch(`${config.apiUrl}/users/${id}/leads?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse);
}

function getAllAccounts(id, q) {
    return fetch(`${config.apiUrl}/users/${id}/accounts?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse);
}

function getAllContacts(id, q) {
    return fetch(`${config.apiUrl}/users/${id}/contacts?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse);
}

function changeAvatar(formData, id) {
    return fetch(`${config.apiUrl}/users/${id}/change_avatar`, {
        method: 'POST',
        ...headers(),
        body: formData
    }).then(handleResponse);
}


function headers() {
    const currentUser = authenticationService.currentUserValue || {};
    const authHeader = currentUser.jwt ? { 'Authorization': currentUser.jwt } : {};
    return {
        headers: {
            ...authHeader
        }
    }
}
