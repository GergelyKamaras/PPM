import './App.css'
import React from 'react'
import { BrowserRouter, Routes, Route } from 'react-router-dom';

function App() {

  return (
    <BrowserRouter>
        <Routes>
            <Route path="" element={<h1>PPM - Property Portfolio Manager</h1>}></Route>
            <Route path="/registration" element={<h1>Registartion</h1>}></Route>
            <Route path="/login" element={<h1>Login</h1>}></Route>
        </Routes>
    </BrowserRouter>
  )
}
export default App
