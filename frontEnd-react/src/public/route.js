import React from 'react'
import { Router, Route, IndexRoute } from 'react-router'
import root from './root'
import signin from './signin'

export const createPublicRoute = () => (
    <Route path="/" component={root.components.PublicRoot}>
        <IndexRoute component={root.components.Home} />
        <Route path="signin" component={signin.components.SignIn}/>
    </Route>
)
