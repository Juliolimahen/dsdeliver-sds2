import React, { useState, ChangeEvent, FormEvent } from 'react';
import { BrowserRouter, Switch, Route } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material';
import Home from './Pages/Home/index';
import Navbar from './Components/Navbar/index';
import Orders from './Pages/Orders/index';
import ProductList from './Pages/Cadastro/index';
import Login from './Pages/Login';

const theme = createTheme();

const Routes: React.FC = () => {
  return (
    <ThemeProvider theme={theme}>
      <BrowserRouter>
        <Navbar />
        <Switch>
          <Route path="/orders">
            <Orders />
          </Route>
          <Route path="/admin/products">
            <ProductList />
          </Route>
          <Route path="/" exact>
            <Home />
          </Route>
          <Route path="/admin">
            <Login />
          </Route>
        </Switch>
      </BrowserRouter>
    </ThemeProvider>
  );
};

export default Routes;

