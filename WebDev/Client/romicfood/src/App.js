import './App.css';
import Header from "./Components/Header/Header";
import Auth from "./Components/Auth/Auth";
import {Route, Routes} from "react-router-dom";
import {useState} from "react";
import {Profile} from "./Components/Profile/Profile";

function App() {
    const [user, setUser] = useState(true);


  return (
    <div className="App">
        <Header isAuth={user}/>


        <Routes>

            <Route path="/auth" element={<Auth />} />
            <Route path={"/profile"} element={< Profile />} />
        </Routes>

    </div>
  );
}

export default App;
