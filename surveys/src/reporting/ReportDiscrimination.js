import React, {Component} from 'react';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import RaisedButton from 'material-ui/RaisedButton'

class ReportDiscrimination extends Component{
    render(){
        return(
            <MuiThemeProvider>
                <React.Fragment>
                    <h2>You will be redirected to the RPI Bias Incident Response Team (BART) page.
                    <span>In this page you will be filling in the following questions</span>
                    <span>(* = required question):</span></h2>
                    <li>*Your info (who is impacted, date/time of incident</li>
                    <li>Involved parties</li>
                    <li>*Incident details</li>
                    <li>Supporting documentation</li>
                    <br></br>
                    <RaisedButton 
                        label = "Direct me please"
                        style = {styles.button}
                        onClick={(e) => {
                            e.preventDefault();
                            window.location.href='https://cm.maxient.com/reportingform.php?RensselaerPolyInst&layout_id=11';
                        }}
                    />
                    <a href = "/report/Discrimination">
                    <RaisedButton 
                        label = "Cancel"
                        style = {styles.button}
                        onClick = {this.back}
                    />
                    </a>
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

export default ReportDiscrimination;