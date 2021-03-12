import React, {Component} from 'react';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import RaisedButton from 'material-ui/RaisedButton'

class TitleIX extends Component{
    continue = e => {
        e.preventDefault();
        this.props.NextStep();
    }
    back = e => {
        e.preventDefault();
        this.props.PrevStep();
    }

    render(){
        return (
            <MuiThemeProvider>
                <React.Fragment>
                    <h2>Is your incident related to any of these:</h2>
                    <li>Sexual Harrassment</li>
                    <li>Gender Discrimination</li>
                    <li>Stalking</li>
                    <p>(For the university's definition of sexual harrassment, visit https://sexualviolence.rpi.edu)</p>
                    <br/>
                    <RaisedButton 
                        label = "Yes"
                        style = {styles.button}
                        onClick = {this.back}
                    />
                    <RaisedButton 
                        label = "No"
                        style = {styles.button}
                        onClick = {this.continue}
                    />
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