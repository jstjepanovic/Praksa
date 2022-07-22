import React, { useState } from 'react'
import CocktailService from '../services/CocktailService'
import SubmitButton from './SubmitButton';

function CocktailUpdate(props) {
    const [currentCocktail, setCurrentCocktail] = useState({});
    const [toEdit, setToEdit] = useState(false);

    const handleInputChange = (e) => {
        setCurrentCocktail({ ...currentCocktail, [e.target.name]: e.target.value });
        console.log(currentCocktail);
      };
    
    const updateCocktail = () => {
        CocktailService.update(props.cocktailId, currentCocktail)
        setToEdit(false)
    };

    const edit = () =>{
        setToEdit(true)
    }

    return (
        <td>
        {toEdit ? (
                <div>
                    <div>
                        <input
                            type="text"
                            className="nameInput"
                            placeholder="Name"
                            name="Name"
                            required
                            value={currentCocktail.Name}
                            onChange={handleInputChange}
                        />
                    </div>
                    <div>
                        <input
                            type="number"
                            className="priceInput"
                            placeholder="Price"
                            name="Price"
                            required
                            value={currentCocktail.Price}
                            onChange={handleInputChange}
                        />
                    </div>
                    <SubmitButton buttonTitle = "Confirm" submitFunction = {updateCocktail}/>
                </div>
        ) : (
             <SubmitButton buttonTitle = "Edit" submitFunction={edit} /> )
        }
        </td> 
  )
}

export default CocktailUpdate