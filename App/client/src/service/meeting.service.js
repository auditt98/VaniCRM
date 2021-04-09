import {buildQueryURI, config} from "@/config/config";
import {requestOptions} from "@/helper/request-options";
import {handleResponse} from "@/helper/handle-response";
import {authenticationService} from "@/service/authentication.service";


export const meetingService = {
    getAll,
    create,
    addParticipant,
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
    return fetch(`${config.apiUrl}/meetings?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse);
}

function create(meeting) {
    return fetch(`${config.apiUrl}/meetings`, requestOptions.post(meeting))
        .then(handleResponse);
}

function addParticipant(body) {
    return fetch(`${config.apiUrl}/meetings/${body.id}/participants`, requestOptions.post(body))
        .then(handleResponse);
}

function remove(id) {
    return fetch(`${config.apiUrl}/meetings/${id}`, requestOptions.delete())
        .then(handleResponse);
}

function getById(id) {
    return fetch(`${config.apiUrl}/meetings/${id}`, requestOptions.get())
        .then(handleResponse);
}

function loadAllObject() {
    return fetch(`${config.apiUrl}/meetings/prepare`, requestOptions.get())
        .then(handleResponse);
}

function update(meeting, id) {
    return fetch(`${config.apiUrl}/meetings/${id}`, requestOptions.post(meeting))
        .then(handleResponse);
}

function changeAvatar(formData, id) {
    return fetch(`${config.apiUrl}/meetings/${id}/change_avatar`, {
        method: 'POST',
        ...headers(),
        body: formData
    }).then(handleResponse);
}

function createNote(note, id) {
    return fetch(`${config.apiUrl}/meetings/${id}/notes`, {
        method: 'POST',
        ...headers(),
        body: note
    }).then(handleResponse);
}

function createTag(tag, id) {
    return fetch(`${config.apiUrl}/meetings/${id}/tags`, requestOptions.post(tag))
        .then(handleResponse);
}

function removeNote(id, noteId) {
    return fetch(`${config.apiUrl}/meetings/${id}/notes/${noteId}`, requestOptions.delete())
        .then(handleResponse);
}

function removeTag(id, noteId) {
    return fetch(`${config.apiUrl}/meetings/${id}/tags/${noteId}`, requestOptions.delete())
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