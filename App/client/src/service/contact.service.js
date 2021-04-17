import {buildQueryURI, config} from "@/config/config";
import {requestOptions} from "@/helper/request-options";
import {handleResponse} from "@/helper/handle-response";
import {authenticationService} from "@/service/authentication.service";
import {fetchRetry} from "@/helper/fetchRetry";


export const contactService = {
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
    loadTasks,
    loadAllObject
};

function loadTasks(q, id) {
    return fetch(`${config.apiUrl}/contacts/${id}/tasks?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/contacts/${id}/tasks?${buildQueryURI(q)}`, requestOptions.get(), 2).then(handleResponse)
            }
        }) ;
}
function getAll(q) {
    return fetch(`${config.apiUrl}/contacts?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/contacts?${buildQueryURI(q)}`, requestOptions.get(), 2).then(handleResponse)
            }
        }) ;
}

function create(lead) {
    return fetch(`${config.apiUrl}/contacts`, requestOptions.post(lead))
        .then(handleResponse);
}

function remove(id) {
    return fetch(`${config.apiUrl}/contacts/${id}`, requestOptions.delete())
        .then(handleResponse);
}

function getById(id) {
    return fetch(`${config.apiUrl}/contacts/${id}`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/contacts/${id}`, requestOptions.get(), 2).then(handleResponse)
            }
        });
}

function loadAllObject() {
    return fetch(`${config.apiUrl}/contacts/prepare`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/contacts/prepare`, requestOptions.get(), 2).then(handleResponse)
            }
        });
}

function update(lead, id) {
    return fetch(`${config.apiUrl}/contacts/${id}`, requestOptions.post(lead))
        .then(handleResponse);
}

function changeAvatar(formData, id) {
    return fetch(`${config.apiUrl}/contacts/${id}/change_avatar`, {
        method: 'POST',
        ...headers(),
        body: formData
    }).then(handleResponse);
}

function createNote(note, id) {
    return fetch(`${config.apiUrl}/contacts/${id}/notes`, {
        method: 'POST',
        ...headers(),
        body: note
    }).then(handleResponse);
}

function createTag(tag, id) {
    return fetch(`${config.apiUrl}/contacts/${id}/tags`, requestOptions.post(tag))
        .then(handleResponse);
}

function removeNote(id, noteId) {
    return fetch(`${config.apiUrl}/contacts/${id}/notes/${noteId}`, requestOptions.delete())
        .then(handleResponse);
}

function removeTag(id, noteId) {
    return fetch(`${config.apiUrl}/contacts/${id}/tags/${noteId}`, requestOptions.delete())
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