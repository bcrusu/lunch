import React, { Component, PropTypes } from 'react'
import { connect } from 'react-redux'
import * as actions from '../actions'

class SignIn extends Component {
  constructor(props) {
    super(props)
  }

  renderLinkedinSignin() {
    if (this.props.isAuthenticated)
      return null

    return (
      <button className="btn btn-md btn-block btn-linkedin" onClick={this.props.handleLinkedinSignin}>
        <i className="ion-social-linkedin"></i> Sign in with LinkedIn
      </button>
    )
  }

  renderSignout() {
    if (!this.props.isAuthenticated)
      return null

    return (
      <button className="btn btn-block btn-default" onClick={this.props.handleSignout}>
        Sign out
      </button>
    )
  }

  render() {
    return (
      <div>
        {this.renderLinkedinSignin() }
        {this.renderSignout() }
      </div>
    )
  }
}

const mapStateToProps = state => {
  return {
    isAuthenticated: state.auth.isAuthenticated
  }
}

const mapDispatchToProps = {
  handleLinkedinSignin: actions.beginLinkedinSignin,
  handleSignout: actions.beginSignout
}

export default connect(mapStateToProps, mapDispatchToProps)(SignIn)
