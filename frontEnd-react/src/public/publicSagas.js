import { fork } from 'redux-saga/effects'
import { watchSignin } from './signin/sagas'

export function* watchPublicRoot() {
    yield [
        fork(watchSignin)
    ]
}