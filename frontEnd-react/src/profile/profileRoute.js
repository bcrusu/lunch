import React from 'react'
import { Router, Route } from 'react-router'
import ProfileRoot from './components/ProfileRoot'

export const createProfileRoute = () => (
    <Route path="/profile" component={ProfileRoot}>
    </Route>
)