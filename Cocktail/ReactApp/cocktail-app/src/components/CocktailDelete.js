import React, { useState, useEffect } from 'react'
import CocktailService from '../services/CocktailService'
import SubmitButton from './SubmitButton';

function CocktailDelete(props) {
    const [toDelete, setToDelete] = useState(false);



    const deleteCocktail = () =>{
        CocktailService.remove(props.cocktailId)
        setToDelete(false)
    }

    const del = () =>{
        setToDelete(true)
    }

    return (
        <td>
        {toDelete ? (
            <SubmitButton buttonTitle = "Confirm" submitFunction = {deleteCocktail}/>
        ) : (
            <SubmitButton buttonTitle = "Delete" submitFunction={del} /> )
        }
        </td> 
    )
}

export default CocktailDelete