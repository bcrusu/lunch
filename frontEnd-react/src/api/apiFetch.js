import 'isomorphic-fetch'
import auth from '../auth'
import { API_ROOT } from '../config'

function getFullApiUrl(endpoint) {
    return (endpoint.indexOf(API_ROOT) === -1) ? API_ROOT + endpoint : endpoint
}

function getFetchInitObject(method, body = undefined) {
    const headers = new Headers()
    headers.append('pragma', 'no-cache');
    headers.append('cache-control', 'no-cache');
    headers.append('Accept', 'application/json');
    headers.append('Content-Type', 'application/json');

    if (auth.authService.isAuthenticated()) {
        let token = auth.authService.getAuthToken();
        headers.append("Authorization", `Bearer ${token}`);
    }

    const init = {
        method: method,
        headers: headers,
        mode: 'cors',
        referrer: 'client',
        redirect: 'error',
        cache: 'no-store',
        credentials: 'omit'
    }

    if (body)
        init.body = JSON.stringify(body)

    return init;
}

export function apiFetch(endpoint, method, body = undefined) {
    const fullUrl = getFullApiUrl(endpoint)
    const init = getFetchInitObject(method, body)

    return fetch(fullUrl, init)
        .then(response => {
            if (!response.ok) {
                return Promise.reject(response)
            }

            return Promise.resolve(response)
        })
        .then(response => response, error => ({ error: error.message || 'API call failed.' }))
}

function apiFetchJson(endpoint, method, body = undefined) {
    return apiFetch(endpoint, method, body)
        .then(response => {
            return response.json()
        })
}

export function apiGet(endpoint) {
    return apiFetchJson(endpoint, 'GET')
}

export function apiPost(endpoint, body) {
    return apiFetchJson(endpoint, 'POST', body)
}

export function apiPut(endpoint, body) {
    return apiFetchJson(endpoint, 'PUT', body)
}

export function apiDelete(endpoint) {
    return apiFetchJson(endpoint, 'DELETE')
}

