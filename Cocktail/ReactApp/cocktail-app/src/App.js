import logo from './logo.svg';
import './App.css';
import Navbar from './Navbar';
import Table from './Table';

function App() {
  return (
    <div className="App">
      <Navbar /> <br />
      <Table columnsNumber={2} columns={["Username", "Date"]} />
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
      </header>
    </div>
  );
}

export default App;
