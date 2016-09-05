import { createAction } from '../../utils/actionUtils'

export const BEGIN_SIGNIN = 'BEGIN_SIGNIN';

export const beginSignin = () => createAction(BEGIN_SIGNIN)