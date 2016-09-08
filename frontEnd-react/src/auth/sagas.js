import { put, call, fork, take } from 'redux-saga/effects'
import * as actions from './actions'
import * as tokenStore from './tokenStore'
import * as oauth2 from './oauth2'
import api from '../api'

export const NETWORKS = {
    linkedin: 'linkedin'
}

export function* init() {
    const hasToken = yield call(tokenStore.hasToken)
    yield put(actions.init(hasToken))
}

export function* signin(network) {
    const signinFunction = getSigninFunction(network)

    try {
        const token = yield call(signinFunction)
        yield call(tokenStore.setToken, token)

        yield put(actions.signinSuccess())
    }
    catch (error) {
        yield put(actions.signinFailure(error))
    }
}

export function* signout() {
    yield fork(api.sagas.signOut)
    let action = yield take([api.actions.TYPES.SIGN_OUT.SUCCESS, api.actions.TYPES.SIGN_OUT.FAILURE])

    if (action.type === api.actions.TYPES.SIGN_OUT.SUCCESS) {
        yield call(tokenStore.removeToken)
        yield put(actions.signoutSuccess())
    }
    else {
        yield put(actions.signoutFailure())
    }
}

function getSigninFunction(network) {
    switch (network) {
        case NETWORKS.linkedin:
            return oauth2.linkedinSignin
        default:
            throw new Error('Invalid network provided.')
    }
}
