import React from 'react'
import { Router, Route } from 'react-router'
import root from './root'
import welcome from './welcome'

export const createProfileRoute = () => (
    <Route path="/profile" component={root.components.ProfileRoot}>
        <Route path="welcome" component={welcome.components.Welcome}/>
    </Route>
)