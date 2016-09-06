import { createAction } from '../../utils/actionUtils'

export const BEGIN_LINKEDIN_SIGNIN = 'BEGIN_LINKEDIN_SIGNIN';
export const BEGIN_SIGNOUT = 'BEGIN_SIGNOUT';

export const beginLinkedinSignin = () => createAction(BEGIN_LINKEDIN_SIGNIN)
export const beginSignout = () => createAction(BEGIN_SIGNOUT)
