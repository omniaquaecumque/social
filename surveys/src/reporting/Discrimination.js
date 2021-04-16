import React, {Component} from 'react';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import RaisedButton from 'material-ui/RaisedButton'

class Discrimination extends Component{
    render(){
        return (
            <MuiThemeProvider>
                <React.Fragment>
                    <h2>I may have or I have been subject to discrimination. Discrimination
                        <span>can include, but is not limited to:</span></h2>
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
                    <p>(For the university's definition of discrimination, visit this website: <a href = "https://info.rpi.edu/bart">https://info.rpi.edu/bart</a>)</p>
                    <br/>
                    <a href = "/report">
                        <RaisedButton 
                            label = "Back"
                            style = {styles.button}
                        />
                    </a>
                    <a href = "/report/BART">
                    <RaisedButton 
                        label = "Yes"
                        style = {styles.button}
                    />
                    </a>
                    <a href = "/report/SocialReport">
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

export default Discrimination;