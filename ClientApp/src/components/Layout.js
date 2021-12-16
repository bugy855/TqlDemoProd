import React, { Component } from 'react';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div className='jumbotron v-center'>
        <div className='container-fluid'>
          <div className='row align-items-center justify-content-center'>
            {this.props.children}
          </div>
        </div>
      </div>
    );
  }
}
