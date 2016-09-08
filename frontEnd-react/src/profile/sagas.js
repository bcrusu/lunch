import { fork } from 'redux-saga/effects'
import welcome from './welcome' 

export function* watch() {
    yield [
        fork(welcome.sagas.watch)
    ]
}