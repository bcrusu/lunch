import React from 'react'

export default () => (
  <nav className="navbar navbar-default navbar-static-top" role="navigation">
    <div className="navbar-header">
      <button type="button" className="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
        <span className="icon-bar"></span>
        <span className="icon-bar"></span>
        <span className="icon-bar"></span>
      </button>
      <a className="navbar-brand" href="#/app">
        <i className="fa fa-home"></i>
        <span>Welcome</span>
      </a>
    </div>

    <div className="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
      <ul className="nav navbar-nav">
        <li>TODO</li>
        <li>TODO</li>
        <li>TODO</li>
      </ul>
    </div>
  </nav>
)
