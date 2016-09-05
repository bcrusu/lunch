import React from 'react'
import { Router, Route } from 'react-router'
import public_ from './public'
import profile from './profile'
import publicRoot from './public/root'

export default ({ history }) => (
  <Router history={history}>
    { public_.route.createPublicRoute() }
    { profile.route.createProfileRoute() }
    <Route path="*" component={publicRoot.components.PublicRoot}/>
  </Router>
)
