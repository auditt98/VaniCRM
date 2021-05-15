import { config} from "@/config/config";
import {requestOptions} from "@/helper/request-options";
import {handleResponse} from "@/helper/handle-response";
import {fetchRetry} from "@/helper/fetchRetry";

export const reportService = {
    getAmountByStageReport,
    getTopSalesReport,
    getTopMarketingsReport,
    getKeyAccountsReport,
    getAccountsByIndustryReport,
    getRevenueComparisonReport,
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

function getTopSalesReport() {
    return fetch(`${config.apiUrl}/reports/top_sales`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/reports/top_sales`, requestOptions.get(), 2).then(handleResponse)
            }
        }) ;
}

function getTopMarketingsReport() {
    return fetch(`${config.apiUrl}/reports/top_marketings`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/reports/top_marketings`, requestOptions.get(), 2).then(handleResponse)
            }
        }) ;
}

function getKeyAccountsReport() {
    return fetch(`${config.apiUrl}/reports/key_accounts`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/reports/key_accounts`, requestOptions.get(), 2).then(handleResponse)
            }
        }) ;
}

function getAccountsByIndustryReport() {
    return fetch(`${config.apiUrl}/reports/accounts_by_industry`, requestOptions.get())
        .then(handleResponse).then(resolve => {
            return resolve
        }, reject =>{
            if(reject == "retry"){
                return fetchRetry(`${config.apiUrl}/reports/accounts_by_industry`, requestOptions.get(), 2).then(handleResponse)
            }
        }) ;
}

function getRevenueComparisonReport(){
    return fetch(`${config.apiUrl}/reports/revenue_comparison`, requestOptions.get())
    .then(handleResponse).then(resolve => {
        return resolve
    }, reject =>{
        if(reject == "retry"){
            return fetchRetry(`${config.apiUrl}/reports/revenue_comparison`, requestOptions.get(), 2).then(handleResponse)
        }
    });
}