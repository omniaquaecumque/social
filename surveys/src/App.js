import React, {Component} from 'react';
import './App.css';
import Report from './reporting/Reports';
import {BrowserRouter, Route, Switch} from 'react-router-dom';
import ReportTitleIX from './reporting/ReportTitleIX';
import Discrimination from './reporting/Discrimination';
import ReportDiscrimination from './reporting/ReportDiscrimination';
import OtherReport from './reporting/OtherReport';

class App extends Component{
  /*
  sets the links for routing to each page
  */
  render(){
    return (
      <BrowserRouter basename={process.env.PUBLIC_URL}>
        <div className="App">
          <Switch>
            <Route path = "/" exact component = {Home}/>
            <Route path = "/report" exact component = {Report}/>
            <Route path = "/report/TitleIX" exact component = {ReportTitleIX}/>
            <Route path = "/report/Discrimination" exact component = {Discrimination}/>
            <Route path = "/report/BART" exact component = {ReportDiscrimination}/>
            <Route path = "/report/SocialReport" exact component = {OtherReport}/>
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