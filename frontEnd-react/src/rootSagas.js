import { fork, call, put } from 'redux-saga/effects'
import public_ from './public'
import profile from './profile'
import auth from './auth'

function* runInit() {
  yield put(auth.actions.authInit())
}

export function* watch() {
  yield call(runInit)

  yield [
    fork(public_.sagas.watch),
    fork(profile.sagas.watch)
  ]
}
