import { createAction } from '../../utils/actionUtils'

export const PREPARE_WELCOME_PAGE = 'PREPARE_WELCOME_PAGE';

export const prepareWelcomePage = () => createAction(PREPARE_WELCOME_PAGE)
