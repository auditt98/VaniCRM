import {config} from "@/config/config";
import {requestOptions} from "@/helper/request-options";
import {handleResponse} from "@/helper/handle-response";
import {fetchRetry} from "@/helper/fetchRetry";

export const dashboardService = {
    getAllForSale
}
function getAllForSale() {
/*    return fetch(`${config.apiUrl}sale_dashboard`, requestOptions.get())
        .then(handleResponse)
        .then(res => {
            return res;
        });*/
    return fetch(`${config.apiUrl}/sale_dashboard`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/sale_dashboard`, requestOptions.get(), 2).then(handleResponse)
            }
        })
}