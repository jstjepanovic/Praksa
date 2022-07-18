import React from 'react'
import Button from './Button'
import './Navbar.css'
import Textform from './Textform'

function Navbar() {
  return (
    <div className='navbar'>
      <div className="flex-container">
        <div><Button type = "button" buttonTitle = "Home" /></div>
        <div><Button type = "button" buttonTitle = "Cocktails" /></div>
        <div><Button type = "button" buttonTitle = "Ingredients" /></div>
        <div><Button type = "button" buttonTitle = "Shop" /></div>
        <div><Textform type= "text" label = "Username"/></div>
        <div><Textform type= "password" label = "Password"/></div>
        <div><Button type = "submit" buttonTitle = "Login" /></div>
      </div>
    </div>
  )
}

export default Navbar