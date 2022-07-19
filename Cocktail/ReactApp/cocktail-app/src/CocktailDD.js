import React, { Component } from 'react'
import Cocktail from './cocktails.json'
import './CocktailDD.css'

export class CocktailDD extends Component {
  constructor(props) {
    super(props);
    this.state = {};
 
    this.handleChange = this.handleChange.bind(this);
  }


  handleChange(e){
    console.log("Selected!!");
    this.setState({name : e.target.value});
  }

  render() {
    return (
      <div>
        <select value={this.state.name} onChange={this.handleChange}>
          <option selected disabled="true">-- Select cocktail --</option>
          {
            Cocktail.CocktailName.map((result) => (<option title={result.id}>{result.name}</option>))    
          }
        </select>
        <p className='selection'>Current selection : {this.state.name}</p>
      </div>
    )
  }
}

export default CocktailDD