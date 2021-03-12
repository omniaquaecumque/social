import React, {Component} from 'react';
import Discrimination from './Discrimination';
import OtherReport from './OtherReport';
import ReportDiscrimination from './ReportDiscrimination';
import ReportTitleIX from './ReportTitleIX';
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
        this.setState({ [input]: e.target.value})
    };

    render(){
        const {step} = this.state;
        const {date, time, desc, accuse, witness, name, rin, info} = this.state;
        const values = {date, time, desc, accuse, witness, name, rin, info};
        
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
                    NextStep = {this.NextStep}
                    values={values}
                    />
                )
            case 5:
                return(
                    <h1> SUCCESS! </h1>
                )
            default:
                console.log('this is a certified hood classic');
        }
    }
}

export default Report