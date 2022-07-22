import React from 'react'
import Button from './Button'
import '../css/Navbar.css'
import { Link } from "react-router-dom";

function Navbar() {
  return (
    <div className='navbar'>
      <div className="flex-container">
        <Link to="/">
        <div><Button buttonTitle = "Home" /></div>
        </Link>
        <Link to="/cocktails">
          <div><Button buttonTitle = "Cocktails" /></div>
        </Link>
        <Link to="/create">
          <div><Button buttonTitle = "New cocktail" /></div>
        </Link>
        <div><Button buttonTitle = "Shop" /></div>
      </div>
    </div>
  )
}

export default Navbar