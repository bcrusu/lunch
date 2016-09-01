import React from 'react'
import { Router, Route } from 'react-router'
import { createPublicRoute } from './public/publicRoute'
import PublicRoot from './public/components/PublicRoot'
import { createProfileRoute } from './profile/profileRoute'

export default ({ history }) => (
  <Router history={history}>
    {createPublicRoute()}
    {createProfileRoute()}
    <Route path="*" component={PublicRoot}/>
  </Router>
)
