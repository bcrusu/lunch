import { combineReducers } from 'redux'
import { routerReducer } from 'react-router-redux'
import api from './api'
import auth from './auth'
import public_ from './public'
import profile from './profile'

export default combineReducers({
    routing: routerReducer,
    [api.NAME]: api.reducer,
    [auth.NAME]: auth.reducer,
    [public_.NAME]: public_.reducer,
    [profile.NAME]: profile.reducer
})
