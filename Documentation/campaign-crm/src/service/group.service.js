import {buildQueryURI, config} from "@/config/config";
import {requestOptions} from "@/helper/request-options";
import {handleResponse} from "@/helper/handle-response";


export const groupService = {
    getAll,
    create,
    update,
    getById,
    getAllPermission,
    remove
};

function create(group) {
    return fetch(`${config.apiUrl}/groups`, requestOptions.post(group))
        .then(handleResponse);
}

function update(group, id) {
    return fetch(`${config.apiUrl}/groups/${id}`, requestOptions.post(group))
        .then(handleResponse);
}

function getAll(q) {
    return fetch(`${config.apiUrl}/groups?${buildQueryURI(q)}`, requestOptions.get())
        .then(handleResponse);
}

function getById(id) {
    return fetch(`${config.apiUrl}/groups/${id}`, requestOptions.get())
        .then(handleResponse);
}

function remove(id) {
    return fetch(`${config.apiUrl}/groups/${id}`, requestOptions.delete())
        .then(handleResponse);
}

function getAllPermission() {
    return fetch(`${config.apiUrl}/groups/all_perms`, requestOptions.get())
        .then(handleResponse);
}