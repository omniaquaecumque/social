import React, {Component} from 'react';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import RaisedButton from 'material-ui/RaisedButton'

class Discrimination extends Component{
    continue1 = e => {
        e.preventDefault();
        this.props.NextStep();
    }
    continue2 = e => {
        e.preventDefault();
        this.props.NextStep2();
    }
    back = e => {
        e.preventDefault();
        this.props.PrevStep();
    }

    render(){
        return (
            <MuiThemeProvider>
                <React.Fragment>
                    <h2>Is your incident related to any kind of discrimination 
                        <span>which can include but is not limited to:</span></h2>
                    <div className = "block">
                    <div className = "block1">
                    <li>Accommodation request </li>
                    <li>Age</li>
                    <li>Childcare/eldercare</li>
                    <li>Disability</li>
                    <li>Employee Relations</li>
                    <li>Ethnicity</li>
                    <li>Gender</li>
                    <li>Gender identity or expression</li>
                    <li>General climate</li>
                    </div>
                    <div className = "block2">
                    <li>Marital/family status</li>
                    <li>National origin</li>
                    <li>Pregnancy/family responsibilities</li>
                    <li>Race/color</li>
                    <li>Religion/creed</li>
                    <li>Retaliation</li>
                    <li>Sexual orientation</li>
                    <li>Veteran status</li>
                    <li>Other or not applicable</li>
                    </div>
                    </div>
                    <p>(For the university's definition of discrimination, visit https://info.rpi.edu/bart)</p>
                    <br/>
                    <RaisedButton 
                        label = "Back"
                        style = {styles.button}
                        onClick = {this.back}
                    />
                    <RaisedButton 
                        label = "Yes"
                        style = {styles.button}
                        onClick = {this.continue1}
                    />
                    <RaisedButton 
                        label = "No"
                        style = {styles.button}
                        onClick = {this.continue2}
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

export default Discrimination;