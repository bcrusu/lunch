const storage = 'localStorage';
const storageKey = 'lunch_jwt';

export function hasToken() {
    return getToken() ? true : false
}

export function getToken() {
    return getStorage().getItem(storageKey);
}

export function setToken(token) {
    getStorage().setItem(storageKey, token);
}

export function removeToken() {
    getStorage().removeItem(storageKey)
}

function getStorage() {
    return window[storage]
}
