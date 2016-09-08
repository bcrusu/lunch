import { fork, take, call } from 'redux-saga/effects'
import * as actions from './actions'
import api from '../../api'

function* watchPrepareWelcomePage() {
    while (true) {
        yield take(actions.PREPARE_WELCOME_PAGE)
        yield call(api.sagas.loadUserInfo)
    }
}

export function* watch() {
    yield [
        fork(watchPrepareWelcomePage)
    ]
}
