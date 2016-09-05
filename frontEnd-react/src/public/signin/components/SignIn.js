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
        <h3 className="text-center message">Meet someone new every day!</h3>

        <button className="btn btn-md btn-block btn-linkedin">
          <i className="ion-social-linkedin"></i> Sign in with LinkedIn
        </button>

        <a className="btn btn-block btn-default" href="#/app">
          <i className="ion-social-linkedin"></i> Open App
        </a>
      </div>
    )
  }
}

function mapStateToProps(state) {
}

export default connect()(SignIn)