import { apiGet, apiPost } from './apiFetch'

// API Function signature: (request) => promise

export const signInLinkedIn = ({client_id}) => apiPost('account/signInLinkedin', { client_id })
export const signOut = () => apiPost('account/signOutCurrentSession')

export const getUserInfo = () => apiGet('userProfile/userInfo')