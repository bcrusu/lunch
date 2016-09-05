export function isAuthenticated() {
    return getAuthToken() ? true : false
}

export function getAuthToken() {
    return null;
}

export function setAuthToken(token) {
    //TODO
}