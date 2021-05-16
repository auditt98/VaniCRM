import {buildQueryURI, config} from "@/config/config";
import {requestOptions} from "@/helper/request-options";
import {handleResponse} from "@/helper/handle-response";
import {authenticationService} from "@/service/authentication.service";
import {fetchRetry} from "@/helper/fetchRetry";


export const dealService = {
    getAll,
    getAllReasons,
    create,
    update,
    getById,
    remove,
    changeAvatar,
    createNote,
    createTag,
    changeStage,
    removeNote,
    removeTag,
    loadTasks,
    loadHistories,
    loadCompetitors,
    createCompetitor,
    loadAllObject
};

function loadTasks(q, id) {
    return fetch(`${config.apiUrl}/deals/${id}/tasks?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/deals/${id}/tasks?${buildQueryURI(q)}`, requestOptions.get(), 2).then(handleResponse)
            }
        }) ;
}

function loadHistories(q, id) {
    return fetch(`${config.apiUrl}/deals/${id}/histories?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/deals/${id}/histories?${buildQueryURI(q)}`, requestOptions.get(), 2).then(handleResponse)
            }
        }) ;
}

function loadCompetitors(q, id) {
    return fetch(`${config.apiUrl}/deals/${id}/competitors?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/deals/${id}/competitors?${buildQueryURI(q)}`, requestOptions.get(), 2).then(handleResponse)
            }
        }) ;
}

function createCompetitor(data, id) {
    return fetch(`${config.apiUrl}/deals/${id}/competitors`, requestOptions.post(data))
        .then(handleResponse);
}

function getAll(q) {
    return fetch(`${config.apiUrl}/deals?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/deals?${buildQueryURI(q)}`, requestOptions.get(), 2).then(handleResponse)
            }
        });
}

function getAllReasons(q) {
    return fetch(`${config.apiUrl}/deals/lost_reasons?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/deals/lost_reasons?${buildQueryURI(q)}`, requestOptions.get(), 2).then(handleResponse)
            }
        });
}

function create(lead) {
    return fetch(`${config.apiUrl}/deals`, requestOptions.post(lead))
        .then(handleResponse);
}

function remove(id) {
    return fetch(`${config.apiUrl}/deals/${id}`, requestOptions.delete())
        .then(handleResponse);
}

function getById(id) {
    return fetch(`${config.apiUrl}/deals/${id}`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/deals/${id}`, requestOptions.get(), 2).then(handleResponse)
            }
        })
}

function changeStage(dealId, stageId) {
    return fetch(`${config.apiUrl}/deals/${dealId}/change_stage/${stageId}`, requestOptions.get())
        .then(handleResponse);
}

function loadAllObject() {
    return fetch(`${config.apiUrl}/deals/prepare`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/deals/prepare`, requestOptions.get(), 2).then(handleResponse)
            }
        });
}

function update(lead, id) {
    return fetch(`${config.apiUrl}/deals/${id}`, requestOptions.post(lead))
        .then(handleResponse);
}

function changeAvatar(formData, id) {
    return fetch(`${config.apiUrl}/deals/${id}/change_avatar`, {
        method: 'POST',
        ...headers(),
        body: formData
    }).then(handleResponse);
}

function createNote(note, id) {
    return fetch(`${config.apiUrl}/deals/${id}/notes`, {
        method: 'POST',
        ...headers(),
        body: note
    }).then(handleResponse);
}

function createTag(tag, id) {
    return fetch(`${config.apiUrl}/deals/${id}/tags`, requestOptions.post(tag))
        .then(handleResponse);
}

function removeNote(id, noteId) {
    return fetch(`${config.apiUrl}/deals/${id}/notes/${noteId}`, requestOptions.delete())
        .then(handleResponse);
}

function removeTag(id, noteId) {
    return fetch(`${config.apiUrl}/deals/${id}/tags/${noteId}`, requestOptions.delete())
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