import React from 'react';
import '../css/Button.css';

function SubmitButton(props) {
  return (
    <button type="submit" onClick={props.submitFunction}>
          {props.buttonTitle}
    </button>
  )
}

export default SubmitButton