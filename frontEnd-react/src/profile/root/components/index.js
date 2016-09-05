import ProfileRoot_ from './ProfileRoot'
import { requireAuthentication } from './AuthenticatedComponent'

export { default as NavBar } from './NavBar'
export const ProfileRoot = requireAuthentication(ProfileRoot_)
