import React from 'react'
import { Router, Route } from 'react-router'
import root from './root'
import signin from './signin'

export const createPublicRoute = () => (
    <Route path="/" component={root.components.PublicRoot}>
        <Route path="signin" component={signin.components.SignIn}/>
    </Route>
)
