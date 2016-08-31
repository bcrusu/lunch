import React, { Component, PropTypes } from 'react'
import { connect } from 'react-redux'

//TODO: do not bundle css
import 'bootstrap/dist/css/bootstrap.css'
import '../../styles/styles.css'

class App extends Component {
  constructor(props) {
    super(props)
  }

  render() {
    const { children } = this.props
    return (
      <div className="container">
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
