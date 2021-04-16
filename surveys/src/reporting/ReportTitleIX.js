import React, {Component} from 'react';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import RaisedButton from 'material-ui/RaisedButton'
import flyer from '../reporting/flyer.pdf';

class ReportTitleIX extends Component{
    render(){
        return(
            <MuiThemeProvider>
                <React.Fragment>
                    <h3>For more information, please visit <a href = "sexualviolence.rpi.edu">sexualviolence.rpi.edu</a>. To directly contact
                    <span>a member of RPI's Title IX department, please refer to the flyer below:</span></h3>
                    <embed src = {flyer} type = "application/pdf" class = "flyer"/>
                    <br></br>
                    <a href = "/report">
                    <RaisedButton 
                        label = "back"
                        style = {styles.button}
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

export default ReportTitleIX;