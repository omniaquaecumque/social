import React, {Component} from 'react';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import RaisedButton from 'material-ui/RaisedButton'

class TitleIX extends Component{
    render(){
        return (
            <MuiThemeProvider>
                <React.Fragment>
                    <h3>I may have or I have experienced an incident that involved sexual misconduct.</h3>
                    <p class = "text">"sexual misconduct includes, but is not limited to:
                    <span>sexual harassment, sexual assault, dating and domestic violence,</span>
                    <span>intimate partner violence, and stalking."</span>
                     - (<a href ="https://sexualviolence.rpi.edu">https://sexualviolence.rpi.edu</a>)</p>
                    <br/>
                    <a href = "/report/TitleIX">
                    <RaisedButton 
                        label = "Yes"
                        style = {styles.button}
                    />
                    </a>
                    <a href = "/report/Discrimination">
                    <RaisedButton 
                        label = "No"
                        style = {styles.button}
                    />
                    </a>
                </React.Fragment>
            </MuiThemeProvider>
        );
    }
}

const styles = {
    button: {
        margin: 15
    }
}

export default TitleIX;