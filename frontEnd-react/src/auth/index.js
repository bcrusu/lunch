import * as actions from './actions';
import * as sagas from './sagas';
import * as tokenStore from './tokenStore'
import reducer from './reducer';

const NAME = "auth"

export default { NAME, actions, reducer, sagas, tokenStore };
