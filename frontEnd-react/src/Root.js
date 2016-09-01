import React, { Component, PropTypes } from 'react'
import { Provider } from 'react-redux'
import AppRouter from './AppRouter'

//TODO: do not bundle css
import 'bootstrap/dist/css/bootstrap.css'
import '../styles/styles.css'

export default class Root extends Component {
  render() {
    const { store, history } = this.props
    return (
      <Provider store={store}>
        <AppRouter history={history} />
      </Provider>
    )
  }
}

Root.propTypes = {
  store: PropTypes.object.isRequired,
  history: PropTypes.object.isRequired
}
