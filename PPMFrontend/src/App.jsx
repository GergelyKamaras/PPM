import './App.css';
import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Login from './Components/Login.jsx';
import Registration from './Components/Registration.jsx';
import NavBar from './Components/NavBar';
import Properties from './Components/Properties';
import { AuthProvider } from './Contexts/AuthContext';

function App() {

    return (
        <>
            <AuthProvider>
                <NavBar />
                <BrowserRouter>
                    <Routes>
                        <Route path="" element={<h1>PPM - Property Portfolio Manager</h1>}></Route>
                        <Route path="/registration" element={<Registration />}></Route>
                        <Route path="/login" element={<Login />}></Route>
                        <Route path="/properties" element={<Properties />}></Route>
                    </Routes>
                </BrowserRouter>
            </AuthProvider>
        </>
    )
}
export default App
