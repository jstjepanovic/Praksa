import axios from "axios";

const find = (name, min, max) =>{
    return axios.get(`https://localhost:44393/get_all_cocktails/?nameSearch=${name}&priceLower=${min}&priceUpper=${max}`);
};

const get = (cocktailId) =>{
    return axios.get(`https://localhost:44393/get_one_cocktail/${cocktailId}`);
};

const create = (cocktail) =>{
    return axios.post("https://localhost:44393/add_cocktail", cocktail);
};

const update = (cocktailId, cocktail) =>{
    return axios.update(`https://localhost:44393/update_cocktail/${cocktailId}`, cocktail);
};

const remove = (cocktailId) =>{
    return axios.delete(`https://localhost:44393/delete_cocktail/${cocktailId}`);
};

const CocktailService = { find, get, create, update, remove };

export default CocktailService