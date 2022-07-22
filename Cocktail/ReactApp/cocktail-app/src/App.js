import './css/App.css';
import Navbar from './components/Navbar';
import CocktailFind from './components/CocktailFind';
import CocktailCreate from './components/CocktailCreate';
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

function App() {
  return (
    <Router>
      <div className="App">
        <Navbar />
        <Routes>
          <Route path='/' element={<Home/>} />
          <Route path='/cocktails' element={<CocktailFind/>} />
          <Route path='/create' element={<CocktailCreate/>} />
        </Routes>
      </div>
    </Router>

  );
}

const Home = () =>(
  <div>
    <h1>Home</h1>
  </div>
)

export default App;
