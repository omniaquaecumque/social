import React, {Component} from 'react';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import RaisedButton from 'material-ui/RaisedButton';
import TextField from 'material-ui/TextField';

class OtherReport extends Component{
    back = e => {
        e.preventDefault();
        this.props.PrevStep2();
    }

    continue = e => {
        e.preventDefault();
        this.props.NextStep();
    }

    render(){
        const { values, Change } = this.props;
        return (
            <MuiThemeProvider>
                <React.Fragment>
                    <h2>Please fill out the following form to the best of your ability</h2>
                    <form ref = "form" className = "form">
                    <h3 id = "Qname">1.)* Date of Incident</h3>
                    <input
                        label="date"
                        onChange={Change('date')}
                        defaultValue={values.date}
                        className = "question"
                    />
                    <h3 id = "Qname">2.)* Time of Incident</h3>
                    <input
                        label="time"
                        onChange={Change('time')}
                        defaultValue={values.time}
                        className = "question"
                    />
                    <h3 id = "Qname">3.)* Brief Description</h3>
                    <textarea
                        label="desc"
                        onChange={Change('desc')}
                        defaultValue={values.desc}
                        className = "question"
                        id = "long"
                    />
                    <h3 id = "Qname">4.) Name of Accused (if known)</h3>
                    <input
                        label="accuse"
                        onChange={Change('accuse')}
                        defaultValue={values.accuse}
                        className = "question"
                    />
                    <h3 id = "Qname">5.) Witnesses</h3>
                    <input
                        label="witness"
                        onChange={Change('witness')}
                        defaultValue={values.witness}
                        className = "question"
                    />
                    <h3 id = "Qname">6.)* Name</h3>
                    <input
                        label="name"
                        onChange={Change('name')}
                        defaultValue={values.name}
                        className = "question"
                    />
                    <h3 id = "Qname">7.)* RIN</h3>
                    <input
                        label="rin"
                        onChange={Change('rin')}
                        defaultValue={values.info}
                        className = "question"
                    />
                    <h3 id = "Qname">8.)* Contact Info</h3>
                    <input
                        label="info"
                        onChange={Change('info')}
                        defaultValue={values.info}
                        className = "question"
                    />
                    </form>

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

export default OtherReport