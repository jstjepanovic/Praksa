import './css/App.css';
import Navbar from './components/Navbar';
import CocktailFind from './components/CocktailFind';
import CocktailCreate from './components/CocktailCreate';

function App() {
  return (
    <div className="App">
      <Navbar />
      <CocktailFind />
      <CocktailCreate />
    </div>
  );
}

export default App;
