import { put, call } from 'redux-saga/effects'
import * as actions from './actions'
import * as authService from './authService'

export function* signin(network) {
    try {
        yield call(authService.signin, network)
        yield put(actions.authSigninSuccess())
    }
    catch (error) {
        yield put(actions.authSigninFailure(error))
    }
}
