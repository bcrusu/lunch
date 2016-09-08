import { combineReducers } from 'redux'
import * as actions from './actions'
import cache from './cache'

export default combineReducers({
    api: apiReducer,
    [cache.NAME]: cache.reducer
})

function apiReducer(state = {}, action) {
    return state
}
