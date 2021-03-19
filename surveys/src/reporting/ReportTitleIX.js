import React, {Component} from 'react';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import RaisedButton from 'material-ui/RaisedButton'

class ReportTitleIX extends Component{
    continue = e => {
        e.preventDefault();
        this.props.NextStep();
    }
    render(){
        return(
            <MuiThemeProvider>
                <React.Fragment>
                    <h2>You will be redirected to the RPI Title IX page
                    <span>In this page you will be filling in the following questions</span>
                    <span>(* = required question):</span></h2>
                    <li>*Date of Incident</li>
                    <li>*Time of Incident</li>
                    <li>*Location of Incident</li>
                    <li>*Brief Description</li>
                    <li>*Incident in building or street (2 options)</li>
                    <li>*Did incident occur at University sponsored event (2 options)</li>
                    <li>Name(s) of accused (optional)</li>
                    <li>Witnesses (optional)</li>
                    <li>Contact info (optional)</li>
                    <br></br>
                    <RaisedButton 
                        label = "Direct me please"
                        style = {styles.button}
                        onClick={(e) => {
                            e.preventDefault();
                            window.location.href='https://sexualviolence.rpi.edu/reporting';
                        }}
                    />
                    <RaisedButton 
                        label = "back"
                        style = {styles.button}
                        onClick = {this.continue}
                    />
                </React.Fragment>
            </MuiThemeProvider>
        )
    }
}

const styles = {
    button: {
        margin: 15
    }
}

export default ReportTitleIX;