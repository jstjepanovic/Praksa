import React from 'react'
import './Table.css'

function Table(props) {
    const columnsNumber = props.columnsNumber;
    let text = '<table border="1"><tr>';
    for (let i = 0; i < columnsNumber; i++) {
        text += '<th>' + props.columns[i] + '</th>';
      }
    text += '</table></tr>';
  return (
    <div dangerouslySetInnerHTML={{ __html: text }}>
    </div>
  )
}

export default Table