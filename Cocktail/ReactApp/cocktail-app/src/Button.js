import React from 'react';
import './Button.css';

function Button(props) {
  return (
    <div>
        <button type={props.type}>
          {props.buttonTitle}
        </button>
    </div>
  )
}

export default Button