import { createAction } from '../utils/actionUtils'
import * as authService from './authService'

export const AUTH_INIT = 'AUTH_INIT';
export const AUTH_SIGNIN_SUCCESS = 'AUTH_SIGNIN_SUCCESS';
export const AUTH_SIGNIN_FAILURE = 'AUTH_SIGNIN_FAILURE';
export const AUTH_SIGNOUT = 'AUTH_SIGNOUT';

export const authInit = () => {
    const isAuthenticated = authService.isAuthenticated()
    return createAction(AUTH_INIT, { isAuthenticated })
}

export const authSigninSuccess = () => {
    return createAction(AUTH_SIGNIN_SUCCESS)
}

export const authSigninFailure = error => {
    return createAction(AUTH_SIGNIN_FAILURE, error)
}

export const authSignout = () => {
    authService.signout()
    return createAction(AUTH_SIGNOUT)
}
