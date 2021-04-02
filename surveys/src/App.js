import React, {Component} from 'react';
import './App.css';
import Report from './reporting/Reports';
import {BrowserRouter, Route, Switch} from 'react-router-dom';

class App extends Component{
  render(){
    return (
      <BrowserRouter>
        <div className="App">
          <Switch>
            <Route path = "/" exact component = {Home}/>
            <Route path = "/report" exact component = {Report}/>
          </Switch>
        </div>
      </BrowserRouter>
    );
  }
}

const Home = () => (
  <div>
    <h1> Home Page </h1>
    <h3><a href = "/report">report</a></h3>
  </div>
);

export default App;