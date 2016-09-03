import { combineReducers } from 'redux'
import signin from './signin'

const reducer = combineReducers({
    [signin.NAME]: signin.reducer
})

export default reducer
