import React, { Component, PropTypes } from 'react'
import { connect } from 'react-redux'
import * as actions from '../actions'

class PublicRoot extends Component {
  constructor(props) {
    super(props)
  }

  componentWillMount() {
    
  }

  render() {
    return (
      <div className="container">
        {this.props.children}
      </div>
    )
  }
}

const mapStateToProps = state => ({})

const mapDispatchToProps = dispatch => ({
  
})

export default connect(mapStateToProps, mapDispatchToProps)(PublicRoot)