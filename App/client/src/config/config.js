export const config = {
    apiUrl: 'https://localhost:44375/'
}

export function buildQueryURI(data) {
    const ret = [];
    for (let d in data) ret.push(encodeURIComponent(d) + '=' + encodeURIComponent(data[d]));
    return ret.join('&');
}