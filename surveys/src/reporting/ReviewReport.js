import React, {Component} from 'react';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import RaisedButton from 'material-ui/RaisedButton';

class ReviewReport extends Component{
    back = e => {
        e.preventDefault();
        this.props.PrevStep();
    }

    continue = e => {
        e.preventDefault();
        this.props.NextStep();
    }

    render(){
        const { values, Change } = this.props;
        return (
            <MuiThemeProvider>
                <RaisedButton 
                    label = "Back"
                    style = {styles.button}
                    onClick = {this.back}
                />
                <RaisedButton 
                    label = "Submit"
                    style = {styles.button}
                    onClick={this.continue}
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