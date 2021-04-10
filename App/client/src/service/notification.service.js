import {buildQueryURI, config} from "@/config/config";
import {handleResponse} from "@/helper/handle-response";
import {authenticationService} from "@/service/authentication.service";


export const notificationService = {
    getAll
};

function getAll(q) {
    return fetch(`${config.apiUrl}/notifications?${buildQueryURI(q)}`)
        .then(handleResponse);
}
