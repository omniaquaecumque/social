import React, {Component} from 'react';
import Discrimination from './Discrimination';
import OtherReport from './OtherReport';
import ReportDiscrimination from './ReportDiscrimination';
import ReportTitleIX from './ReportTitleIX';
import ReviewReport from './ReviewReport';
import TitleIX from './TitleIX';

export class Report extends Component{
    state = {
        step: 1,
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

    NextStep2 = () => {
        const{ step } = this.state;
        this.setState({
            step: step + 2
        });
    };

    PrevStep2 = () => {
        const{ step } = this.state;
        this.setState({
            step: step - 2
        });
    };
    
    Change = input => e => {
        this.setState({ [input]: e.target.value });
    };

    Clear = () => {
        this.setState({date: '', time: '', desc: '',
                    accuse: '', witness: '', name: '',
                    rin: '', info: '', date1: '', time1: '', desc1: '',
                    accuse1: '', witness1: '', name1: '',
                    rin1: '', info1: ''})
    }

    Submit = e =>{
        if(this.state.date1 === '' || this.state.time1 === '' || this.state.desc1 === ''
        || this.state.name1 === '' || this.state.rin1 === '' || this.state.info1 === ''){
            alert(`Please fill out the required fields`)
        } else {
            this.setState({
                date: this.state.date1,
                time: this.state.time1,
                desc: this.state.desc1,
                name: this.state.name1,
                accuse: this.state.accuse1,
                witness: this.state.witness1,
                rin: this.state.rin1,
                info: this.state.info1,
            })
            this.NextStep();
        }
    }

    render(){
        const {step} = this.state;
        const {date, time, desc, accuse, witness, name, rin, info, date1, time1, desc1, accuse1, witness1, name1, rin1, info1} = this.state;
        const values = {date, time, desc, accuse, witness,
             name, rin, info, date1, time1, desc1, accuse1, witness1, name1, rin1, info1};
        
        switch (step){
            case 0:
                return (
                    <ReportTitleIX
                    NextStep = {this.NextStep}
                    />
                )
            case 1:
                return (
                    <TitleIX
                    PrevStep = {this.PrevStep}
                    NextStep = {this.NextStep}
                    />
                );
            case 2:
                return(
                    <Discrimination
                    PrevStep = {this.PrevStep}
                    NextStep = {this.NextStep}
                    NextStep2 = {this.NextStep2}
                    />
                )
            case 3:
                return(
                    <ReportDiscrimination
                    PrevStep = {this.PrevStep}
                    NextStep = {this.NextStep}
                    />
                )
            case 4:
                return(
                    <OtherReport
                    PrevStep2 = {this.PrevStep2}
                    Change={this.Change}
                    Clear = {this.Clear}
                    NextStep = {this.NextStep}
                    values={values}
                    />
                )
            case 5:
                return(
                    <ReviewReport
                    PrevStep = {this.PrevStep}
                    Change={this.Change}
                    Submit = {this.Submit}
                    values={values}
                    />
                )
            case 6:
                return(
                    <h1>SUCCESS</h1>
                )
            default:
                console.log('problem');
        }
    }
}

export default Report