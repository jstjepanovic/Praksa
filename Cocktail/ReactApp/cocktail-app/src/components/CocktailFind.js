import React, { useState, useEffect } from 'react';
import CocktailService from '../services/CocktailService';
import SubmitButton from './SubmitButton';
import CocktailUpdate from './CocktailUpdate';
import CocktailDelete from './CocktailDelete';

const CocktailFind = () => {
    const [cocktails, setCocktails] = useState([]);
    const [nameSearch, setCurrentNameSearch] = useState("");
    const [minPrice, setMinPrice] = useState(0);
    const [maxPrice, setMaxPrice] = useState();
    const [rpp, setRpp] = useState(10);

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

    const handleRppChange = (e) =>{
        setRpp(e.target.value)
    }

    const findCocktails = () =>{
        CocktailService.find(nameSearch, minPrice, maxPrice, rpp).then(response => {
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
                                <tr key={cocktail.CocktailID}>
                                    <td> {cocktail.Name} </td>
                                    <td> {cocktail.Price} </td>
                                    <CocktailUpdate cocktailId={cocktail.CocktailID}/>
                                    <CocktailDelete cocktailId={cocktail.CocktailID}/>
                                </tr>
                        ))}                                
                </tbody>
            </table>
        </div>
        <div>
            <select value={rpp} onChange={handleRppChange}>
                <option selected disabled="true">-- Results per page --</option>
                <option title="5"> 5 </option>
                <option title="10"> 10 </option>
            </select>
        </div>
    </div>
  )
}

export default CocktailFind