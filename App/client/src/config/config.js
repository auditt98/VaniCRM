import {format, parseISO} from "date-fns";

export const DATE_TIME_FORMAT = 'yyyy-MM-dd HH:mm';
export const DATE_FORMAT = 'dd/MM/yyyy';
export const config = {
    apiUrl: 'https://localhost:44375/'
}

export function buildQueryURI(data) {
    const ret = [];
    for (let d in data) ret.push(encodeURIComponent(d) + '=' + encodeURIComponent(data[d]));
    return ret.join('&');
}

export function formatDate(value, type) {
    if (!value) {
        return '';
    }
    return format(parseISO(value), type ? type : DATE_TIME_FORMAT);
}

export function mapValue(source, des) {
    for(let i=0; i<des.length; i++) {
        source[i].value = des[i];
    }
}

export function getValueInArr(arr, type, key) {
    if (!arr || arr.length === 0) {
        return '';
    }
    const lst = arr.filter(p => {if (p[type]) return p});
    if (!lst || lst.length === 0) {
        return '';
    }
    return lst[0][key];
}

export function hourToSecond(obj) {
    if (!obj['h']) {
        obj['h'] = 0;
    }
    if (!obj['m']) {
        obj['m'] = 0;
    }
    if (!obj['s']) {
        obj['s'] = 0;
    }
    return Number(obj['h']) * 60 * 60 + Number(obj['m']) * 60 + Number(obj['s']);
}
export function parseDate(s) {
    let b = s.split(/\D/);
    return new Date(Date.UTC(b[0], --b[1], b[2]));
}