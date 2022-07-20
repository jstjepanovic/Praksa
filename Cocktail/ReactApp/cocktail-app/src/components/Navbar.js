import React from 'react'
import Button from './Button'
import '../css/Navbar.css'
import Textform from './Textform'

function Navbar() {
  return (
    <div className='navbar'>
      <div className="flex-container">
        <div><Button buttonTitle = "Home" /></div>
        <div><Button buttonTitle = "Cocktails" /></div>
        <div><Button buttonTitle = "Ingredients" /></div>
        <div><Button buttonTitle = "Shop" /></div>
      </div>
      {/* <div className="submit"><Textform type="text" label ="Username"/></div> */}
    </div>
  )
}

export default Navbar