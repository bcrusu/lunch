import * as oauth2 from './oauth2'

export const NETWORKS = {
    linkedin: 'linkedin'
}

export function isAuthenticated() {
    return getAuthToken() ? true : false
}

export function getAuthToken() {
    return token_store;
}

export function signin(network) {
    let tokenPromise = null

    switch (network) {
        case NETWORKS.linkedin:
            tokenPromise = oauth2.linkedinSignin()
            break
        default:
            throw new Error('Invalid network provided.')
    }

    return tokenPromise.then(token => {
        storeAuthToken(token)
        return token
    })
}

export function signout() {
    storeAuthToken(null)
}

let token_store = null;  //TODO: use local storage/cookies
function storeAuthToken(token) {
    token_store = token
}
