import React from 'react'
import './Textform.css'

function Textform(props) {
  return (
    <form>
      <label>
        <input type={props.type} placeholder={props.label} />
      </label>
    </form>
  )
}

export default Textform