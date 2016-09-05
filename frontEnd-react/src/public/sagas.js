import { fork } from 'redux-saga/effects'
import root from './root'
import signin from './signin'

export function* watch() {
    yield [
        fork(root.sagas.watch),
        fork(signin.sagas.watch)
    ]
}