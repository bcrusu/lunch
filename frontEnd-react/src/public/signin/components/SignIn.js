import React, { Component, PropTypes } from 'react'
import { connect } from 'react-redux'
import * as authService from '../../../auth/authService'

class SignIn extends Component {
  constructor(props) {
    super(props)
  }

  componentWillMount() {

  }

  testSignin() {
    authService.signin(authService.NETWORKS.linkedin)
      .then(token => { console.log('success: ' + token) },
      error => { console.log('error: ' + error) })
  }

  render() {
    return (
      <div>
        <button className="btn btn-md btn-block btn-linkedin" onClick={this.testSignin}>
          <i className="ion-social-linkedin"></i> Sign in with LinkedIn
        </button>
      </div>
    )
  }
}

function mapStateToProps(state) {
}

export default connect()(SignIn)