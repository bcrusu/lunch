import { createAction } from '../utils/actionUtils'

const REQUEST = 'REQUESTING'
const SUCCESS = 'SUCCESS'
const FAILURE = 'FAILURE'

function createApiRequestTypes(base) {
    const res = {};
    [REQUEST, SUCCESS, FAILURE].forEach(type => res[type] = `${base}_${type}`)
    return res;
}

export const TYPES = {
    SIGN_IN_LINKEDIN: createApiRequestTypes('SIGN_IN_LINKEDIN'),
    SIGN_OUT: createApiRequestTypes('SIGN_OUT'),
    GET_USER_INFO: createApiRequestTypes('GET_USER_INFO')
}

export const SIGN_IN_LINKEDIN = {
    request: (request) => createAction(TYPES.SIGN_IN_LINKEDIN.REQUEST, request),
    success: (response) => createAction(TYPES.SIGN_IN_LINKEDIN.SUCCESS, response),
    failure: (error) => createAction(TYPES.SIGN_IN_LINKEDIN.FAILURE, { error })
}

export const SIGN_OUT = {
    request: () => createAction(TYPES.SIGN_OUT.REQUEST),
    success: () => createAction(TYPES.SIGN_OUT.SUCCESS),
    failure: (error) => createAction(TYPES.SIGN_OUT.FAILURE, { error })
}

export const GET_USER_INFO = {
    request: () => createAction(TYPES.GET_USER_INFO.REQUEST),
    success: (response) => createAction(TYPES.GET_USER_INFO.SUCCESS, response),
    failure: (error) => createAction(TYPES.GET_USER_INFO.FAILURE, { error })
}
