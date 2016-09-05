import React, { Component, PropTypes } from 'react'
import { connect } from 'react-redux'

class SignIn extends Component {
  constructor(props) {
    super(props)
  }

  componentWillMount () {

  }

  render() {
    return (
      <div>
        <button className="btn btn-md btn-block btn-linkedin">
          <i className="ion-social-linkedin"></i> Sign in with LinkedIn
        </button>
      </div>
    )
  }
}

function mapStateToProps(state) {
}

export default connect()(SignIn)