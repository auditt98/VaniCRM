import {buildQueryURI, config} from "@/config/config";
import {requestOptions} from "@/helper/request-options";
import {handleResponse} from "@/helper/handle-response";
import {authenticationService} from "@/service/authentication.service";
import {fetchRetry} from "@/helper/fetchRetry";


export const taskService = {
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
    return fetch(`${config.apiUrl}/tasks?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/tasks?${buildQueryURI(q)}`, requestOptions.get(), 2).then(handleResponse)
            }
        }) ;
}

function create(lead) {
    return fetch(`${config.apiUrl}/tasks`, requestOptions.post(lead))
        .then(handleResponse);
}

function remove(id, type) {
    return fetch(`${config.apiUrl}/${type}/${id}`, requestOptions.delete())
        .then(handleResponse);
}

function getById(id) {
    return fetch(`${config.apiUrl}/tasks/${id}`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/tasks/${id}`, requestOptions.get(), 2).then(handleResponse)
            }
        });
}


function loadAllObject() {
    return fetch(`${config.apiUrl}/tasks/prepare`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/tasks/prepare`, requestOptions.get(), 2).then(handleResponse)
            }
        }) ;
}

function update(lead, id) {
    return fetch(`${config.apiUrl}/tasks/${id}`, requestOptions.post(lead))
        .then(handleResponse);
}

function changeAvatar(formData, id) {
    return fetch(`${config.apiUrl}/tasks/${id}/change_avatar`, {
        method: 'POST',
        ...headers(),
        body: formData
    }).then(handleResponse);
}

function createNote(note, id) {
    return fetch(`${config.apiUrl}/tasks/${id}/notes`, {
        method: 'POST',
        ...headers(),
        body: note
    }).then(handleResponse);
}

function createTag(tag, id) {
    return fetch(`${config.apiUrl}/tasks/${id}/tags`, requestOptions.post(tag))
        .then(handleResponse);
}

function removeNote(id, noteId) {
    return fetch(`${config.apiUrl}/tasks/${id}/notes/${noteId}`, requestOptions.delete())
        .then(handleResponse);
}

function removeTag(id, noteId) {
    return fetch(`${config.apiUrl}/tasks/${id}/tags/${noteId}`, requestOptions.delete())
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