import * as actions from './actions';
import * as authService from './authService';
import * as sagas from './sagas';
import reducer from './reducer';

const NAME = "auth"

export default { NAME, actions, reducer, authService, sagas };
