import React from 'react'

export default ({ userName }) => (
  <div className="col-xs-offset-1">
    <h3 className="text-primary">Welcome {userName}!</h3>
    <h4 className="text-primary">Your account has been created.</h4>
  </div>
)
