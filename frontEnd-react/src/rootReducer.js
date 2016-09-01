import { combineReducers } from 'redux'
import { routerReducer} from 'react-router-redux'
import publicReducer from './public/reducers'
import profileReducer from './profile/reducers'

const rootReducer = combineReducers({
    routing: routerReducer,
    //public: publicReducer,
    //profile: profileReducer
})

export default rootReducer
