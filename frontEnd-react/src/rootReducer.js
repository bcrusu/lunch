import { combineReducers } from 'redux'
import { routerReducer} from 'react-router-redux'
import public_ from './public'
import profile from './profile'
import auth from './auth'

const rootReducer = combineReducers({
    routing: routerReducer,
    [auth.NAME]: auth.reducer,
    [public_.NAME]: public_.reducer,
    //profile: profileReducer
})

export default rootReducer
