import { put, take, call, fork } from 'redux-saga/effects'
import { push } from 'react-router-redux'
import * as actions from './actions'
import auth from '../../auth'

function* watchLinkedinSignin() {
    while (true) {
        yield take(actions.BEGIN_LINKEDIN_SIGNIN)
        yield fork(auth.sagas.signinLinkedin)

        //TODO: redirect to profile page
    }
}   

function* watchSignout() {
    while (true) {
        yield take(actions.BEGIN_SIGNOUT)
        yield put(auth.actions.authSignout())
    }
}

export function* watch() {
    yield [
        fork(watchLinkedinSignin),
        fork(watchSignout)
    ]
}