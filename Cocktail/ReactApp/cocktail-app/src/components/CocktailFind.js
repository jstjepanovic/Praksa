import React, { useState, useEffect } from 'react';
import CocktailService from '../services/CocktailService';
import SubmitButton from './SubmitButton';

const CocktailFind = () => {
    const [cocktails, setCocktails] = useState([]);
    const [nameSearch, setCurrentNameSearch] = useState("");
    const [minPrice, setMinPrice] = useState();
    const [maxPrice, setMaxPrice] = useState();

    useEffect(() => {
        findCocktails();
      }, []);
  
    const handleSearchChange = (e) =>{
        setCurrentNameSearch(e.target.value);
    }

    const handleMaxChange = (e) =>{
        setMaxPrice(e.target.value);
    }

    const handleMinChange = (e) =>{
        setMinPrice(e.target.value);
    }

    const findCocktails = () =>{
        CocktailService.find(nameSearch, minPrice, maxPrice).then(response => {
            setCocktails(response.data);
        })
    }

    return (
    <div>
        <div className='inputGroup'>
            <input
                type="text"
                className="nameSearch"
                placeholder="Search by name"
                value={nameSearch}
                onChange={handleSearchChange}
            />
            <input
                type="number"
                className="minSearch"
                placeholder="min"
                value={minPrice}
                onChange={handleMinChange}
            />
            <input
                type="number"
                className="maxSearch"
                placeholder="max"
                value={maxPrice}
                onChange={handleMaxChange}
            />
        </div>
        <div> <SubmitButton buttonTitle = "Find" submitFunction = {findCocktails}/> </div>
        <div className='table'>
            <table>
                <tbody>
                    <tr key="cocktailTable">
                        <th>Name</th>
                        <th>Price</th>
                    </tr>
                    {cocktails &&
                        cocktails.map((cocktail) => (
                        <tr key={cocktail.CocktailId}>
                            <td> {cocktail.Name} </td>
                            <td> {cocktail.Price} </td>
                        </tr>
                        ))}
                </tbody>
            </table>
        </div>
    </div>
  )
}

export default CocktailFind