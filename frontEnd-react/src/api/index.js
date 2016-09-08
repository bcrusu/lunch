import * as actions from './actions';
import * as sagas from './sagas';
import * as apiFunctions from './apiFunctions'
import * as selectors from './selectors'
import reducer from './reducer';

const NAME = "api"

export default { NAME, actions, sagas, reducer, apiFunctions, selectors };
