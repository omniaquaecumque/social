import React, {Component} from 'react';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import RaisedButton from 'material-ui/RaisedButton';

class ReviewReport extends Component{
    back = e => {
        e.preventDefault();
        this.props.PrevStep();
    }

    submit = e =>{
        e.preventDefault();
        this.props.Submit();
    }

    render(){
        const { values } = this.props;
        return (
            <MuiThemeProvider>
                <h1>Confirm Your Information</h1>
                <h2 class = "confirm">Date of Incident</h2>
                <p class = "info">{values.date1}</p>

                <h2 class = "confirm">Time of Incident</h2>
                <p class = "info">{values.time1}</p>

                <h2 class = "confirm">Brief Description</h2>
                <p class = "info">{values.desc1}</p>

                <h2 class = "confirm">Name of Accused</h2>
                <p class = "info">{values.accuse1}</p>

                <h2 class = "confirm">Witnesses</h2>
                <p class = "info">{values.witness1}</p>

                <h2 class = "confirm">Name</h2>
                <p class = "info">{values.name1}</p>

                <h2 class = "confirm">RIN</h2>
                <p class = "info">{values.rin1}</p>

                <h2 class = "confirm">Contact Info</h2>
                <p class = "info">{values.info1}</p>
                <br />
                <RaisedButton 
                    label = "Back"
                    style = {styles.button}
                    onClick = {this.back}
                />
                <RaisedButton 
                    label = "Submit"
                    style = {styles.button}
                    onClick={this.submit}
                />
            </MuiThemeProvider>
        );
    }
}

const styles = {
    button: {
        margin: 15
    }
}

export default ReviewReport