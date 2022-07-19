import React from 'react'
import './Table.css'

function Table(props) {  
  return (
    <div>
      <table border="2" id="table1Id">
        <tr>
          <th>Username</th>
          <th>Date</th>
        </tr>
        {props.list.map((login)=>(
        login.username !== ""
        ?
        <tr>
          <td>{login.username}</td>
          <td>{login.date}</td>
        </tr>
        : null
        ))}
      </table>
    </div>
  )
}

export default Table