import { put, call } from 'redux-saga/effects'
import * as actions from './actions'
import * as authService from './authService'

export function* signinLinkedin() {
    try {
        yield call(authService.signin, authService.NETWORKS.linkedin)
        yield put(actions.authSigninSuccess())
    }
    catch (error) {
        console.log(error)
        yield put(actions.authSigninFailure(error))
    }
}
