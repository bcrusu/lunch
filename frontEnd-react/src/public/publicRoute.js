import React from 'react'
import { Router, Route } from 'react-router'
import root from './root'
import signIn from './signIn'

export const createPublicRoute = () => (
    <Route path="/" component={root.components.PublicRoot}>
        <Route path="signin" component={signIn.components.SignIn}/>
    </Route>
)
