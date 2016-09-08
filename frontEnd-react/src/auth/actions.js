import { createAction } from '../utils/actionUtils'

export const AUTH_INIT = 'AUTH_INIT';
export const AUTH_SIGNIN_SUCCESS = 'AUTH_SIGNIN_SUCCESS';
export const AUTH_SIGNIN_FAILURE = 'AUTH_SIGNIN_FAILURE';
export const AUTH_SIGNOUT_SUCCESS = 'AUTH_SIGNOUT_SUCCESS';
export const AUTH_SIGNOUT_FAILURE = 'AUTH_SIGNOUT_FAILURE';

export const init = isAuthenticated => {
    return createAction(AUTH_INIT, { isAuthenticated })
}

export const signinSuccess = () => {
    return createAction(AUTH_SIGNIN_SUCCESS)
}

export const signinFailure = error => {
    return createAction(AUTH_SIGNIN_FAILURE, { error })
}

export const signoutSuccess = () => {
    return createAction(AUTH_SIGNOUT_SUCCESS)
}

export const signoutFailure = error => {
    return createAction(AUTH_SIGNOUT_FAILURE, { error })
}
