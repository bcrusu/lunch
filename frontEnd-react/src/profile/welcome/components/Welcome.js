import React, { Component } from 'react'
import { connect } from 'react-redux'
import * as actions from '../actions'
import api from '../../../api'

class Welcome extends Component {
  constructor(props) {
    super(props)
  }

  componentWillMount() {
    this.props.prepareWelcomePage()
  }

  render() {
    if (!this.props.userInfo)
      return null;

    let displayName = this.props.userInfo.displayName

    return (
      <div className="col-xs-offset-1">
        <h3 className="text-primary">Welcome {displayName}!</h3>
      </div>
    )
  }
}

const mapStateToProps = state => {
  return {
    userInfo: api.selectors.getUserInfo(state)
  }
}

const mapDispatchToProps = {
  prepareWelcomePage: actions.prepareWelcomePage
}

export default connect(mapStateToProps, mapDispatchToProps)(Welcome)
