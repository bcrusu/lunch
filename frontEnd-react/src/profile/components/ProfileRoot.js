import React from 'react'
import NavBar from './NavBar'

export default ({ children }) => (
  <div className="container">
    <NavBar/>
    {children}
  </div>
)
