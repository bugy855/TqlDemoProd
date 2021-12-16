import React from 'react';
import { Switch, Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import Home from './components/Home';
import NotFound from './components/NotFound';
import Verify from './components/Verify';

import './custom.css'

function App() {
  const URI = "https://tqldemomessager.azurewebsites.net/api/Subscriber";

  return (
    <Layout>
      
        <Switch>
          <Route exact path='/' render={props => <Home {...props} uri={URI} />} />
          <Route path="/api/Subscriber/verify-subscriber/:id" render={props => <Verify {...props} uri={URI} />}/>
          <Route path="/" component={NotFound} />
        </Switch>
      
    </Layout>
  );
}

export default App;
