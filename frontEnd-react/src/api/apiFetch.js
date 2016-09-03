import 'isomorphic-fetch'

const API_ROOT = 'http://localhost:7777/api'   //TODO: force https

function getFullApiUrl(endpoint) {
    return (endpoint.indexOf(API_ROOT) === -1) ? API_ROOT + endpoint : endpoint
}

function getFetchInitObject(method, body = undefined) {
    const headers = new Headers()
    headers.append('pragma', 'no-cache');
    headers.append('cache-control', 'no-cache');
    headers.append('Accept', 'application/json');
    headers.append('Content-Type', 'application/json');

    //TODO: add authService
    if (authService.isAuthenticated()) {
        let token = authService.getAccessToken();
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
        init.body = body

    return init;
}

function apiFetch(endpoint, method, body = undefined) {
    const fullUrl = getFullApiUrl(endpoint)
    const init = getFetchInitObject(method, body)

    return fetch(fullUrl, init)
        .then(response => {
            console.log(response)  //TODO: remove

            if (!response.ok) {
                return Promise.reject(response)
            }

            return response.json().then(json => ({ json, response }))
        })
        .then(response => ({ response }), error => ({ error: error.message || 'API call failed.' }))
}

export function apiGet(endpoint) {
    return apiFetch(endpoint, 'GET')
}

export function apiPost(endpoint, body) {
    return apiFetch(endpoint, 'POST', body)
}

export function apiPut(endpoint, body) {
    return apiFetch(endpoint, 'PUT', body)
}

export function apiDelete(endpoint) {
    return apiFetch(endpoint, 'DELETE')
}
