import './App.css';
import Header from "./Components/Header/Header";
import Auth from "./Components/Auth/Auth";
import {Route, Routes} from "react-router-dom";
import {useState} from "react";
import {Profile} from "./Components/Profile/Profile";

function App() {
    const [user, setUser] = useState({
        id: null,
        login: null,
        name: null,
        email: null,
        address: null
    });




  return (
    <div className="App">


        <Routes>
            <Route path="/" element={<Header isAuth={user?.id !== null}/>} />
            <Route path="/auth" element={<Auth setUser={setUser}/>} />
            <Route path={"/profile"} element={< Profile user={user}/>} />
        </Routes>

    </div>
  );
}

export default App;
