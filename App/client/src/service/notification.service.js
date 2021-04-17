import {buildQueryURI, config} from "@/config/config";
import {handleResponse} from "@/helper/handle-response";


export const notificationService = {
    getAll
};

function getAll(q) {
    return fetch(`${config.apiUrl}/notifications?${buildQueryURI(q)}`)
        .then(handleResponse);
}
