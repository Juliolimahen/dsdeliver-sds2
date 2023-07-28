import React, { useState } from 'react';
import { BrowserRouter, Switch, Route } from 'react-router-dom';
import { ThemeProvider } from '@mui/material';
import Home from './Pages/Home/index';
import Navbar from './Components/Navbar/index';
import Orders from './Pages/Orders/index';
import Login from './Pages/Admin/Login';
import theme from './styles/theme/theme';
import Footer from './Components/Footer';
import NotFound from './Components/PageNotFound';
import ProtectedRoute from './Components/ProtectedRoute';
import Cadastro from './Pages/Cadastro/index';

const Routes: React.FC = () => {
  const [, setIsAuthenticated] = useState(false);
  return (
    <ThemeProvider theme={theme}>
      <BrowserRouter>
        <Navbar />
        <Switch>
          <Route path="/orders" component={Orders} />
          <ProtectedRoute exact path="/admin/products" component={Cadastro} />
          <Route path="/admin">
            <Login setIsAuthenticated={setIsAuthenticated} />
          </Route>
          <Route exact path="/" component={Home} />
          <Route path="*" component={NotFound} />
        </Switch>
        <Footer />
      </BrowserRouter>
    </ThemeProvider>
  );
};

export default Routes;
