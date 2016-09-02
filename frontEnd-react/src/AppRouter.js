import React from 'react'
import { Router, Route } from 'react-router'
import { createPublicRoute } from './public/publicRoute'
import { createProfileRoute } from './profile/profileRoute'
import publicRoot from './public/root'

export default ({ history }) => (
  <Router history={history}>
    { createPublicRoute() }
    { createProfileRoute() }
    <Route path="*" component={publicRoot.components.PublicRoot}/>
  </Router>
)
