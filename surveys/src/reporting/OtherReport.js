import React, {Component} from 'react';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import RaisedButton from 'material-ui/RaisedButton';
import TextField from 'material-ui/TextField';

class OtherReport extends Component{
    back = e => {
        e.preventDefault();
        this.props.Clear();
        this.props.PrevStep2();
    }

    submit = e =>{
        e.preventDefault();
        this.props.Submit();
    }
    render(){
        const { values, Change } = this.props;
        return (
            <MuiThemeProvider>
                <React.Fragment>
                    <h2>Please fill out the following form to the best of your ability</h2>
                    <form ref = "form" className = "form">
                    <h3>1.)* Date of Incident</h3>
                    <input type = "text" defaultValue = {values.date} className = "question" onChange={Change('date1')}/>
                    <h3>2.)* Time of Incident</h3>
                    <input type = "text" defaultValue = {values.time} className = "question" onChange={Change('time1')}/>
                    <h3>3.)* Brief Description</h3>
                    <textarea className = "question" defaultValue = {values.desc} onChange={Change('desc1')} id = "long" ></textarea>
                    <h3>4.) Name of Accused (if known)</h3>
                    <input type = "text" defaultValue = {values.accuse} className = "question" onChange={Change('accuse1')}/>
                    <h3>5.) Witnesses</h3>
                    <input type = "text" defaultValue = {values.witness} className = "question" onChange={Change('witness1')}/>
                    <h3>6.)* Name</h3>
                    <input type = "text" defaultValue = {values.name} className = "question" onChange={Change('name1')}/>
                    <h3>7.)* RIN</h3>
                    <input type = "text" defaultValue = {values.rin} className = "question" onChange={Change('rin1')}/>
                    <h3>8.)* Contact Info</h3>
                    <input type = "text" defaultValue = {values.info} className = "question" onChange={Change('info1')}/>
                    </form>

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