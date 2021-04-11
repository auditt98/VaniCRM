import {buildQueryURI, config} from "@/config/config";
import {requestOptions} from "@/helper/request-options";
import {handleResponse} from "@/helper/handle-response";
import {authenticationService} from "@/service/authentication.service";


export const accountService = {
    getAll,
    create,
    update,
    getById,
    remove,
    changeAvatar,
    createNote,
    createTag,
    removeNote,
    removeTag,
    loadAllObject,
    loadContacts
};

function getAll(q) {
    return fetch(`${config.apiUrl}/accounts?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse);
}

function create(lead) {
    return fetch(`${config.apiUrl}/accounts`, requestOptions.post(lead))
        .then(handleResponse);
}

function remove(id) {
    return fetch(`${config.apiUrl}/accounts/${id}`, requestOptions.delete())
        .then(handleResponse);
}

function getById(id) {
    return fetch(`${config.apiUrl}/accounts/${id}`, requestOptions.get())
        .then(handleResponse);
}

function loadContacts(id) {
    return fetch(`${config.apiUrl}/accounts/${id}/contacts`, requestOptions.get())
        .then(handleResponse);
}

function loadAllObject() {
    return fetch(`${config.apiUrl}/accounts/prepare`, requestOptions.get())
        .then(handleResponse);
}

function update(lead, id) {
    return fetch(`${config.apiUrl}/accounts/${id}`, requestOptions.post(lead))
        .then(handleResponse);
}

function changeAvatar(formData, id) {
    return fetch(`${config.apiUrl}/accounts/${id}/change_avatar`, {
        method: 'POST',
        ...headers(),
        body: formData
    }).then(handleResponse);
}

function createNote(note, id) {
    return fetch(`${config.apiUrl}/accounts/${id}/notes`, {
        method: 'POST',
        ...headers(),
        body: note
    }).then(handleResponse);
}

function createTag(tag, id) {
    return fetch(`${config.apiUrl}/accounts/${id}/tags`, requestOptions.post(tag))
        .then(handleResponse);
}

function removeNote(id, noteId) {
    return fetch(`${config.apiUrl}/accounts/${id}/notes/${noteId}`, requestOptions.delete())
        .then(handleResponse);
}

function removeTag(id, noteId) {
    return fetch(`${config.apiUrl}/accounts/${id}/tags/${noteId}`, requestOptions.delete())
        .then(handleResponse);
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