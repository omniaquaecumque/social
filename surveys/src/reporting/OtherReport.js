import React, {Component} from 'react';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import RaisedButton from 'material-ui/RaisedButton';

class OtherReport extends Component{
    back = e => {
        e.preventDefault();
        this.props.Clear();
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
                         onChange={Change('date1')}
                         defaultValue={values.date1}
                         className = "question"
                     />
                     <h3 id = "Qname">2.)* Time of Incident</h3>
                     <input
                         onChange={Change('time1')}
                         defaultValue={values.time1}
                         className = "question"
                     />
                     <h3 id = "Qname">3.)* Brief Description</h3>
                     <textarea
                         onChange={Change('desc1')}
                         defaultValue={values.desc1}
                         className = "question"
                         id = "long"
                     />
                     <h3 id = "Qname">4.) Name of Accused (if known)</h3>
                     <input
                         onChange={Change('accuse1')}
                         defaultValue={values.accuse1}
                         className = "question"
                     />
                     <h3 id = "Qname">5.) Witnesses</h3>
                     <input
                         onChange={Change('witness1')}
                         defaultValue={values.witness1}
                         className = "question"
                     />
                     <h3 id = "Qname">6.)* Name</h3>
                     <input
                         onChange={Change('name1')}
                         defaultValue={values.name1}
                         className = "question"
                     />
                     <h3 id = "Qname">7.)* RIN</h3>
                     <input
                         onChange={Change('rin1')}
                         defaultValue={values.rin1}
                         className = "question"
                     />
                     <h3 id = "Qname">8.)* Contact Info</h3>
                     <input
                         onChange={Change('info1')}
                         defaultValue={values.info1}
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