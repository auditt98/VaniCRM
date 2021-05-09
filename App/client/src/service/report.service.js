import { config} from "@/config/config";
import {requestOptions} from "@/helper/request-options";
import {handleResponse} from "@/helper/handle-response";
import {fetchRetry} from "@/helper/fetchRetry";

export const reportService = {
    getAmountByStageReport,
};

function getAmountByStageReport() {
    return fetch(`${config.apiUrl}/reports/amount_by_stage`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/reports/amount_by_stage`, requestOptions.get(), 2).then(handleResponse)
            }
        }) ;
}
