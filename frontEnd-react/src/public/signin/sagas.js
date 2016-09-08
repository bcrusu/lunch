import { put, take, call, fork } from 'redux-saga/effects'
import { push } from 'react-router-redux'
import * as actions from './actions'
import auth from '../../auth'

function* watchSignin() {
    while (true) {
        let action = yield take(actions.BEGIN_SIGNIN)

        yield fork(auth.sagas.signin, action.network)
        action = yield take([auth.actions.AUTH_SIGNIN_SUCCESS, auth.actions.AUTH_SIGNIN_FAILURE])

        if (action.type === auth.actions.AUTH_SIGNIN_SUCCESS) {
            yield put(push('/profile'))
        }
        else {
            //TODO: notify user 
        }
    }
}

function* watchSignout() {
    while (true) {
        let action = yield take(actions.BEGIN_SIGNOUT)

        yield fork(auth.sagas.signout)
        action = yield take([auth.actions.AUTH_SIGNOUT_SUCCESS, auth.actions.AUTH_SIGNOUT_FAILURE])

        if (action.type === auth.actions.AUTH_SIGNOUT_SUCCESS) {
            yield put(push('/'))
        }
        else {
            //TODO: notify user 
        }
    }
}

export function* watch() {
    yield [
        fork(watchSignin),
        fork(watchSignout)
    ]
}