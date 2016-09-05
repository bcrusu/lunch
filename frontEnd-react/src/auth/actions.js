import { createAction } from '../utils/actionUtils'
import * as authService from './authService'

export const LOAD_AUTH_FROM_STORE = 'LOAD_AUTH_FROM_STORE';

export const loadAuthFromStore = () => {
    const isAuthenticated = authService.isAuthenticated()
    return createAction(LOAD_AUTH_FROM_STORE, { isAuthenticated })
}
