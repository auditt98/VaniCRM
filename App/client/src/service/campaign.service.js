import {buildQueryURI, config} from "@/config/config";
import {requestOptions} from "@/helper/request-options";
import {handleResponse} from "@/helper/handle-response";
import {authenticationService} from "@/service/authentication.service";
import {fetchRetry} from "@/helper/fetchRetry";


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
    loadContacts,
    loadLeads,
    loadAllObject,
    sendEmail,
    addContact,
    addLead
};

function addLead(id, payload) {
    return fetch(`${config.apiUrl}/campaigns/${id}/leads`, requestOptions.post(payload))
        .then(handleResponse)
}

function addContact(id, payload) {
    return fetch(`${config.apiUrl}/campaigns/${id}/contacts`, requestOptions.post(payload))
        .then(handleResponse)
}

function loadContacts(q, id) {
    return fetch(`${config.apiUrl}/campaigns/${id}/contacts?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/campaigns/${id}/contacts?${buildQueryURI(q)}`, requestOptions.get(), 2).then(handleResponse)
            }
        }) ;
}

function loadLeads(q, id) {
    return fetch(`${config.apiUrl}/campaigns/${id}/leads?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/campaigns/${id}/leads?${buildQueryURI(q)}`, requestOptions.get(), 2).then(handleResponse)
            }
        }) ;
}

function getAll(q) {
    return fetch(`${config.apiUrl}/campaigns?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/campaigns?${buildQueryURI(q)}`, requestOptions.get(), 2).then(handleResponse)
            }
        })
}

function create(campaign) {
    return fetch(`${config.apiUrl}/campaigns`, requestOptions.post(campaign))
        .then(handleResponse);
}

function remove(id) {
    return fetch(`${config.apiUrl}/campaigns/${id}`, requestOptions.delete())
        .then(handleResponse);
}

function sendEmail(id) {
    return fetch(`${config.apiUrl}/campaigns/${id}/send_email`, requestOptions.post())
        .then(handleResponse);
}

function getById(id) {
    return fetch(`${config.apiUrl}/campaigns/${id}`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/campaigns/${id}`, requestOptions.get(), 2).then(handleResponse)
            }
        })
}

function loadAllObject() {
    return fetch(`${config.apiUrl}/campaigns/prepare`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/campaigns/prepare`, requestOptions.get(), 2).then(handleResponse)
            }
        })
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