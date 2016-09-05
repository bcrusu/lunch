import * as actions from './actions'

const initialState = {
    isAuthenticated: false
};

export default function (state = initialState, action) { 
    switch (action.type) {
        case actions.LOAD_AUTH_FROM_STORE:
            const isAuthenticated = action.isAuthenticated
            return {
                ...state,
                isAuthenticated
            }
        default:
            return state
    }
}