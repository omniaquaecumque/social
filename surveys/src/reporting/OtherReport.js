import React, {Component} from 'react';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import RaisedButton from 'material-ui/RaisedButton';
import ReviewReport from './ReviewReport';

class OtherReport extends Component{
    /* 
    How this works is that there are a bunch of temporary variables
    date1 to info1 that are updated automatically on this form.
    When we hit submit, it moves the temp variables into the actual variables
    */
    state = {
        step: 1, //indexes the page number essentially
        date: '',
        time: '',
        desc: '',
        accuse: '',
        witness: '',
        name: '',
        rin: '',
        info: '',
        date1: '',
        time1: '',
        desc1: '',
        accuse1: '',
        witness1: '',
        name1: '',
        rin1: '',
        info1: '',
    }

    NextStep = () => {
        const{ step } = this.state;
        this.setState({
            step: step + 1
        });
    };

    PrevStep = () => {
        const{ step } = this.state;
        this.setState({
            step: step - 1
        });
    };

    //so that the temp vars automatically update
    Change = input => e => {
        e.preventDefault();
        this.setState({ [input]: e.target.value });
    };

    //submits the temp variables into the actual variables
    Submit = e =>{
        this.setState({
            date: this.state.date1,
            time: this.state.time1,
            desc: this.state.desc1,
            name: this.state.name1,
            accuse: this.state.accuse1,
            witness: this.state.witness1,
            rin: this.state.rin1,
            info: this.state.info1,
            //clears the temp variables too
            date1: '',
            time1: '',
            desc1: '',
            accuse1: '',
            witness1: '',
            name1: '',
            rin1: '',
            info1: '',
        })
        this.NextStep();
    }

    //prevents us from moving forward w/o filling in required fields
    Next = e =>{
        e.preventDefault();
        if(this.state.date1 === '' || this.state.time1 === '' || this.state.desc1 === ''
        || this.state.name1 === '' || this.state.rin1 === '' || this.state.info1 === ''){
            alert(`Please fill out the required fields`)
        } else {
            this.NextStep();
        }
    }

    //asks if we want to move back
    navigateToPage = () => {
        if(this.state.date1 !== '' || this.state.time1 !== '' || this.state.desc1 !== ''
        || this.state.name1 !== '' || this.state.rin1 !== '' || this.state.info1 !== ''
        || this.state.accuse1 !== ''|| this.state.witness1 !== ''){
            var answer = window.confirm("Are you sure you want to go back? You will lose all your progress if you do.");
            if (answer) {
                this.props.history.push('/report/Discrimination');
            }
        } else {
            this.props.history.push('/report/Discrimination');
        }
        
      };

    render(){
        const {step} = this.state;
        const {date, time, desc, accuse, witness, name, rin, info, date1, time1, desc1, accuse1, witness1, name1, rin1, info1} = this.state;
        const values = {date, time, desc, accuse, witness,
             name, rin, info, date1, time1, desc1, accuse1, witness1, name1, rin1, info1};
        switch (step){
            case 1:
                return (
                    <MuiThemeProvider>
                        <React.Fragment>
                            <h2>Please fill out the following form to the best of your ability</h2>
                            <form ref = "form" className = "form">
                            <h3 id = "Qname">1.)* Date of Incident</h3>
                             <input
                                 onChange={this.Change('date1')}
                                 defaultValue={values.date1}
                                 className = "question"
                             />
                             <h3 id = "Qname">2.)* Time of Incident</h3>
                             <input
                                 onChange={this.Change('time1')}
                                 defaultValue={values.time1}
                                 className = "question"
                             />
                             <h3 id = "Qname">3.)* Brief Description</h3>
                             <textarea
                                 onChange={this.Change('desc1')}
                                 defaultValue={values.desc1}
                                 className = "question"
                                 id = "long"
                             />
                             <h3 id = "Qname">4.) Name of Accused (if known)</h3>
                             <input
                                 onChange={this.Change('accuse1')}
                                 defaultValue={values.accuse1}
                                 className = "question"
                             />
                             <h3 id = "Qname">5.) Witnesses</h3>
                             <input
                                 onChange={this.Change('witness1')}
                                 defaultValue={values.witness1}
                                 className = "question"
                             />
                             <h3 id = "Qname">6.)* Name</h3>
                             <input
                                 onChange={this.Change('name1')}
                                 defaultValue={values.name1}
                                 className = "question"
                             />
                             <h3 id = "Qname">7.)* RIN</h3>
                             <input
                                 onChange={this.Change('rin1')}
                                 defaultValue={values.rin1}
                                 className = "question"
                             />
                             <h3 id = "Qname">8.)* Contact Info</h3>
                             <input
                                 onChange={this.Change('info1')}
                                 defaultValue={values.info1}
                                 className = "question"
                             />
                             </form>

                            <RaisedButton 
                                label = "Back"
                                style = {styles.button}
                                onClick = {this.navigateToPage}
                            />
                            <RaisedButton 
                                label = "Submit"
                                style = {styles.button}
                                onClick = {this.Next}
                            />
                        </React.Fragment>
                    </MuiThemeProvider>
                );
            case 2:
                return(
                    <ReviewReport
                    PrevStep = {this.PrevStep}
                    Submit = {this.Submit}
                    values={values}
                    />
                );
            case 3:
                return(
                    <h1> SUCCESS </h1>
                );
            default:
                console.log('problem');
        }
    }
}

const styles = {
    button: {
        margin: 15
    }
}

export default OtherReport