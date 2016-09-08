import * as actions from './actions'

const initialState = {
    isAuthenticated: false
};

export default function (state = initialState, action) {
    let isAuthenticated = false

    switch (action.type) {
        case actions.AUTH_SIGNIN_SUCCESS:
            isAuthenticated = true
            break
        case actions.AUTH_SIGNOUT_SUCCESS:
            isAuthenticated = false
            break
        case actions.AUTH_INIT:
            isAuthenticated = action.isAuthenticated
            break
        case actions.AUTH_SIGNIN_FAILURE:
        case actions.AUTH_SIGNOUT_FAILURE:
        default:
            return state
    }

    return {
        ...state,
        isAuthenticated
    }
}
