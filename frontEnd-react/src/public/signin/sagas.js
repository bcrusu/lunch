import { put, take, call, fork } from 'redux-saga/effects'
import { push } from 'react-router-redux'
import * as actions from './actions'
import auth from '../../auth'

function* watchSignin() {
    while (true) {
        console.log('waiting for begin signin')
        let action = yield take(actions.BEGIN_SIGNIN)
        yield fork(auth.sagas.signin, action.network)

        action = yield take([auth.actions.AUTH_SIGNIN_SUCCESS, auth.actions.AUTH_SIGNIN_FAILURE])
        console.log('auth result: ' + action)


        if (action.type === auth.actions.AUTH_SIGNIN_SUCCESS) {
            yield put(push('/profile'))
        }
    }
}

function* watchSignout() {
    while (true) {
        yield take(actions.BEGIN_SIGNOUT)
        yield put(auth.actions.authSignout())

        yield put(push('/'))
    }
}

export function* watch() {
    yield [
        fork(watchSignin),
        fork(watchSignout)
    ]
}