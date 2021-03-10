import React, {Component} from 'react'
import './App.css';

class App extends Component{
  
  constructor(props){ //prop is const
    super(props) //so constructor can initialize this.props
    this.state={
      title: "Sample Survey",
      change: 0,
      index: '',
      datas: []
    }
  }

  componentDidMount(){
    this.refs.date.focus();
  }

  fSubmit = e =>{
    e.preventDefault()

    let datas = this.state.datas; //let means variable can be reassigned
    let date = this.refs.date.value;
    let time = this.refs.time.value;
    let desc = this.refs.desc.value;
    let accuse = this.refs.accuse.value;
    let witness = this.refs.witness.value;
    let name = this.refs.name.value;
    let rin = this.refs.rin.value;
    let info = this.refs.info.value;

    if(date != "" && time != "" && desc != "" && name != "" && rin != "" && info != ""){
      if(this.state.change === 0){ // no update
        let data = {
          date, time, desc, accuse, witness, name, rin, info
        }
        datas.push(data);
      } else{                  //update
        let index = this.state.index;
        datas[index].date = date;
        datas[index].time = time;
        datas[index].desc = desc;
        datas[index].accuse = accuse;
        datas[index].witness = witness;
        datas[index].name = name;
        datas[index].rin = rin;
        datas[index].info = info;
      }

      this.setState({
        datas:datas,
        change: 0 //reset back to zero
      });

      this.refs.form.reset();
      this.refs.date.focus();
    }
  }
  
  fDelete = i => {
    let datas = this.state.datas;
    datas.splice(i,1); //remove elemnt
    this.setState({
      datas:datas //correct the list
    })

    this.refs.form.reset();
    this.refs.date.focus();
  }

  fUpdate = i => {
    let data = this.state.datas[i];
    this.refs.date.value = data.date;
    this.refs.time.value = data.time;
    this.refs.desc.value = data.desc;
    this.refs.accuse.value = data.accuse;
    this.refs.witness.value = data.witness;
    this.refs.name.value = data.name;
    this.refs.rin.value = data.rin;
    this.refs.info.value = data.info;

    this.setState({ //lets us know which element to update
      change: 1,
      index: i
    })

    this.refs.date.focus();
  }

  render(){
    let datas = this.state.datas;
    return (
      <div className="App">
        <h1>{this.state.title}</h1>
        <h2>Please fill out the following form to the best of your ability</h2>
        <form ref = "form" className = "form">
          <h3>1.)* Date of Incident</h3>
          <input type = "text" ref = "date" className = "question"/>
          <h3>2.)* Time of Incident</h3>
          <input type = "text" ref = "time" className = "question"/>
          <h3>3.)* Brief Description</h3>
          <textarea ref = "desc" className = "question" id = "long"></textarea>
          <h3>4.) Name of Accused (if known)</h3>
          <input type = "text" ref = "accuse" className = "question"/>
          <h3>5.) Witnesses</h3>
          <input type = "text" ref = "witness" className = "question"/>
          <h3>6.)* Name</h3>
          <input type = "text" ref = "name" className = "question"/>
          <h3>7.)* RIN</h3>
          <input type = "text" ref = "rin" className = "question"/>
          <h3>8.)* Contact Info</h3>
          <input type = "text" ref = "info" className = "question"/>
        </form>

        <br></br>
          <button onClick={(e)=>this.fSubmit(e)} className = "subbutton"> Submit Report </button>
        <pre>
          {datas.map((data,i) =>
            <h5 key = {i}>
              Date of Incident: {data.date}<br></br>
              Time of Incident: {data.time}<br></br>
              Brief Description: {data.desc}<br></br>
              Name of Accused (if known): {data.accuse}<br></br>
              Witnesses: {data.witness}<br></br>
              Name: {data.name}<br></br>
              RIN: {data.rin}<br></br>
              Contact Info: {data.info}<br></br>
              <button onClick={()=>this.fDelete(i)} className = "button"> Delete </button>
              <button onClick={()=>this.fUpdate(i)} className = "button"> Update </button>
            </h5>
          )}
        </pre>
      </div>
    );
  }
}

export default App;