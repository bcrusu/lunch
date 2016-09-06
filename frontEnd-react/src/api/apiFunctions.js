import { apiGet, apiPost } from './apiFetch'

// API Function signature: (request) => promise

export const signInLinkedIn = ({code, redirectUri}) => apiPost('/account/signInLinkedin', { code, redirectUri })
export const signOut = () => apiPost('/account/signOutCurrentSession')

export const getUserInfo = () => apiGet('/userProfile/userInfo')
