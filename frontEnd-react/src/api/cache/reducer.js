import * as actions from './actions'
import * as apiActions from '../actions'

export default function (state = {}, action) {
    switch (action.type) {
        case actions.API_CLEAR_CACHE:
            return {}
        case apiActions.TYPES.GET_USER_INFO.SUCCESS:
            let userInfo = {
                firstName: action.firstName,
                displayName: action.displayName,
                pictureUrl: action.pictureUrl
            }

            return {
                ...state,
                userInfo
            }
        default:
            return state
    }
}
