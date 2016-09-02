import { combineReducers } from 'redux'
import { routerReducer} from 'react-router-redux'
import publicReducer from './public/publicReducer'
import profileReducer from './profile/profileReducer'

const rootReducer = combineReducers({
    routing: routerReducer,
    public: publicReducer,
    //profile: profileReducer
})

export default rootReducer
