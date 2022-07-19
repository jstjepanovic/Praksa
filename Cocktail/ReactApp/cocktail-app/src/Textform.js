import React, {useState} from 'react'
import './Textform.css'
import Table from './Table';
import SubmitButton from './SubmitButton';

function Textform(props) {
  const [login, setLogin] = useState({});
  const [list, setList] = useState([])
  
  function handleChange(e){
    setLogin({...login, [e.target.name] : e.target.value, date : Date().toLocaleString()})
  }

  function handleSubmit(){
    setList([...list, login])
    setLogin({ username : "", date : ""})
  }

  return (
    <div>
      <SubmitButton buttonTitle = "Login" submitFunction = {handleSubmit}/>
      <form>
        <label>
          <input type="text" placeholder="Username" name="username" onChange={handleChange} required/>
        </label>
        <label>
          <input type="password" placeholder="Password" required/>
        </label>
      </form>
      <br />
      <Table list={list}/>
    </div>
  )
}

export default Textform