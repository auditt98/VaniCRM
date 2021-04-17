export function fetchRetry(url, options, n){
    return fetch(url, options).catch(function(error) {
        if (n === 1) throw error;
        return fetchRetry(url, options, n - 1);
    });
}