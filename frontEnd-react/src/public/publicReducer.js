import { combineReducers } from 'redux'
import signIn from './signIn'

const reducer = combineReducers({
    [signIn.NAME]: signIn.reducer
})

export default reducer
