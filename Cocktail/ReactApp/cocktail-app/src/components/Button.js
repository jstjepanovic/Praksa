import React from 'react';
import '../css/Button.css'

function Button(props) {
  return (
        <button type="button">
          {props.buttonTitle}
        </button>
  )
}

export default Button