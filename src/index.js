import React, { Component, Fragment } from "react";
import ReactDOM from "react-dom";
import Unity, { UnityContext } from "react-unity-webgl";
import logo from "./logo.png";
import "./index.css";

class App extends Component {
  constructor() {
    super();
    this.state = {
      showUnity: true,
    };
    this.unityContext = new UnityContext({
      codeUrl: "/build/test.wasm",
      frameworkUrl: "/build/test.framework.js",
      dataUrl: "/build/test.data",
      loaderUrl: "/build/test.loader.js",
    });
  }
  render() {
    return (
      <Fragment>
          <img src={logo} className="App-logo" alt={"logo"} />
          <br></br>
          <button children={"(Not) Show Unity"} onClick={() => this.setState({ showUnity: !this.state.showUnity })}/>
          <div className="Unity">
              {this.state.showUnity === true ? (
                <Unity height={"50%"} unityContext={this.unityContext} devicePixelRatio={2}/>
              ) : (
                <div />
              )}
          </div>
      </Fragment>
    );
  }
}

ReactDOM.render(<App />, document.getElementById("root"));
