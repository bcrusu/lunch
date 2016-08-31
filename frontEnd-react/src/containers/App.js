import React, { Component, PropTypes } from 'react'
import { connect } from 'react-redux'
import { browserHistory } from 'react-router'

class App extends Component {
  constructor(props) {
    super(props)
  }

  render() {
    const { children } = this.props
    return (
      <div>
        <h1>Lunch</h1>
        <hr />
        {children}
      </div>
    )
  }
}

App.propTypes = {
  inputValue: PropTypes.string.isRequired,
  children: PropTypes.node
}

function mapStateToProps(state, ownProps) {
  return {
    //TODO: remove:
    inputValue: ownProps.location.pathname.substring(1)
  }
}

export default connect(mapStateToProps)(App)
