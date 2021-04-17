import {buildQueryURI, config} from "@/config/config";
import {requestOptions} from "@/helper/request-options";
import {handleResponse} from "@/helper/handle-response";
import {authenticationService} from "@/service/authentication.service";
import {fetchRetry} from "@/helper/fetchRetry";


export const callService = {
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
    return fetch(`${config.apiUrl}/calls?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/calls?${buildQueryURI(q)}`, requestOptions.get(), 2).then(handleResponse)
            }
        })
}

function create(call) {
    return fetch(`${config.apiUrl}/calls`, requestOptions.post(call))
        .then(handleResponse);
}

function remove(id) {
    return fetch(`${config.apiUrl}/calls/${id}`, requestOptions.delete())
        .then(handleResponse);
}

function getById(id) {
    return fetch(`${config.apiUrl}/calls/${id}`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/calls/${id}`, requestOptions.get(), 2).then(handleResponse)
            }
        })
}

function loadAllObject() {
    return fetch(`${config.apiUrl}/calls/prepare`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/calls/prepare`, requestOptions.get(), 2).then(handleResponse)
            }
        })
}

function update(call, id) {
    return fetch(`${config.apiUrl}/calls/${id}`, requestOptions.post(call))
        .then(handleResponse);
}

function changeAvatar(formData, id) {
    return fetch(`${config.apiUrl}/calls/${id}/change_avatar`, {
        method: 'POST',
        ...headers(),
        body: formData
    }).then(handleResponse);
}

function createNote(note, id) {
    return fetch(`${config.apiUrl}/calls/${id}/notes`, {
        method: 'POST',
        ...headers(),
        body: note
    }).then(handleResponse);
}

function createTag(tag, id) {
    return fetch(`${config.apiUrl}/calls/${id}/tags`, requestOptions.post(tag))
        .then(handleResponse);
}

function removeNote(id, noteId) {
    return fetch(`${config.apiUrl}/calls/${id}/notes/${noteId}`, requestOptions.delete())
        .then(handleResponse);
}

function removeTag(id, noteId) {
    return fetch(`${config.apiUrl}/calls/${id}/tags/${noteId}`, requestOptions.delete())
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