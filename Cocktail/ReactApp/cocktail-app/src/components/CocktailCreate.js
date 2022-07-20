import React, { useState } from 'react';
import CocktailService from '../services/CocktailService';
import SubmitButton from './SubmitButton';

const CocktailCreate = () =>{
    const [cocktail, setCocktail] = useState({});
    const [isSubmitted, setIsSubmitted] = useState(false);

    const handleInputChange = (e) => {
        setCocktail({ ...cocktail, [e.target.name]: e.target.value });
        console.log(cocktail);
      };

    const createCocktail = () =>{
        CocktailService.create(cocktail).then(response => {
            setCocktail({ CocktailId : response.data.CocktailId, Price : response.data.Price, Name : response.data.Name });
            setIsSubmitted(true);
        })
    };

    const resetSubmit = () =>{
        setCocktail({});
        setIsSubmitted(false);
    }

    return(
        <div>
            {isSubmitted ? (
                <div>
                    <h4>You submitted successfully!</h4>
                    <SubmitButton buttonTitle = "Add" submitFunction = {resetSubmit}/>
                </div>
            ) : (
                <div>
                    <div>
                        <input
                            type="text"
                            className="nameInput"
                            placeholder="Name"
                            name="Name"
                            required
                            value={cocktail.Name}
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
                            value={cocktail.Price}
                            onChange={handleInputChange}
                        />
                    </div>
                    <SubmitButton buttonTitle = "Submit" submitFunction = {createCocktail}/>
                </div>
            )}
        </div>
    )
}

export default CocktailCreate