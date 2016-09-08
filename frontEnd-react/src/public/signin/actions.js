import { createAction } from '../../utils/actionUtils'
import auth from '../../auth'

export const BEGIN_SIGNIN = 'BEGIN_SIGNIN';
export const BEGIN_SIGNOUT = 'BEGIN_SIGNOUT';

export const beginSignin = network => createAction(BEGIN_SIGNIN, { network })
export const beginLinkedinSignin = () => beginSignin(auth.sagas.NETWORKS.linkedin)

export const beginSignout = () => createAction(BEGIN_SIGNOUT)
