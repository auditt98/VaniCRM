import {config} from "@/config/config";
import {requestOptions} from "@/helper/request-options";
import {handleResponse} from "@/helper/handle-response";

export const dashboardService = {
    getAllForSale
}
function getAllForSale() {
    return fetch(`${config.apiUrl}sale_dashboard`, requestOptions.get())
        .then(handleResponse)
        .then(res => {
            return res;
        });
}