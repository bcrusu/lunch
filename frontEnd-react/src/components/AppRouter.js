import React from 'react'
import { Router, Route } from 'react-router'
import App from '../containers/App'
import SignIn from '../containers/SignIn'

const AppRouter = ({ history }) => (
  <Router history={history}>
    <Route path="/" component={App}>
      <Route path="/signin" component={SignIn}/>
    </Route>
  </Router>
)

export default AppRouter