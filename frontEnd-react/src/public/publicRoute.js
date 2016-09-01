import React from 'react'
import { Router, Route } from 'react-router'
import PublicRoot from './components/PublicRoot'
import SignIn from './containers/SignIn'

export const createPublicRoute = () => (
    <Route path="/" component={PublicRoot}>
        <Route path="/signin" component={SignIn}/>
    </Route>
)
