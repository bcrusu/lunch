import { put, take, call} from 'redux-saga/effects'
import * as actions from './actions'

export function* watch() {
    while (true) {
        const action = yield take(actions.BEGIN_SIGNIN)

        //TODO: implement signin screen workflow
    }
}