import { take, put, call, fork, select } from 'redux-saga/effects'
import * as actions from './actions'
import * as apiFunctions from './apiFunctions'

function* executeApiFunction(actionCreators, apiFunction, request) {
  yield put(actionCreators.request(request))

  try {
    const response = yield call(apiFunction, request)
    yield put(actionCreators.success(response))
  }
  catch (error) {
    yield put(actionCreators.failure(error))
  }
}

export const signInLinkedIn = executeApiFunction.bind(null, actions.SIGN_IN_LINKEDIN, apiFunctions.signInLinkedIn)
export const signOut = executeApiFunction.bind(null, actions.SIGN_OUT, apiFunctions.signOut)
export const getUserInfo = executeApiFunction.bind(null, actions.GET_USER_INFO, apiFunctions.getUserInfo)
