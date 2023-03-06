import './App.css'
import React from 'react'
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Login from './Components/login.jsx';
import Registration from './Components/registration.jsx';

function App() {

  return (
    <BrowserRouter>
        <Routes>
            <Route path="" element={<h1>PPM - Property Portfolio Manager</h1>}></Route>
            <Route path="/registration" element={<Registration />}></Route>
            <Route path="/login" element={<Login />}></Route>
        </Routes>
    </BrowserRouter>
  )
}
export default App
