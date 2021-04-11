import {buildQueryURI, config} from "@/config/config";
import {requestOptions} from "@/helper/request-options";
import {handleResponse} from "@/helper/handle-response";
import {authenticationService} from "@/service/authentication.service";


export const campaignService = {
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
    loadAllObject
};

function getAll(q) {
    return fetch(`${config.apiUrl}/campaigns?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse);
}

function create(lead) {
    return fetch(`${config.apiUrl}/campaigns`, requestOptions.post(lead))
        .then(handleResponse);
}

function remove(id) {
    return fetch(`${config.apiUrl}/campaigns/${id}`, requestOptions.delete())
        .then(handleResponse);
}

function getById(id) {
    return fetch(`${config.apiUrl}/campaigns/${id}`, requestOptions.get())
        .then(handleResponse);
}

function loadAllObject() {
    return fetch(`${config.apiUrl}/campaigns/prepare`, requestOptions.get())
        .then(handleResponse);
}

function update(lead, id) {
    return fetch(`${config.apiUrl}/campaigns/${id}`, requestOptions.post(lead))
        .then(handleResponse);
}

function changeAvatar(formData, id) {
    return fetch(`${config.apiUrl}/campaigns/${id}/change_avatar`, {
        method: 'POST',
        ...headers(),
        body: formData
    }).then(handleResponse);
}

function createNote(note, id) {
    return fetch(`${config.apiUrl}/campaigns/${id}/notes`, {
        method: 'POST',
        ...headers(),
        body: note
    }).then(handleResponse);
}

function createTag(tag, id) {
    return fetch(`${config.apiUrl}/campaigns/${id}/tags`, requestOptions.post(tag))
        .then(handleResponse);
}

function removeNote(id, noteId) {
    return fetch(`${config.apiUrl}/campaigns/${id}/notes/${noteId}`, requestOptions.delete())
        .then(handleResponse);
}

function removeTag(id, noteId) {
    return fetch(`${config.apiUrl}/campaigns/${id}/tags/${noteId}`, requestOptions.delete())
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