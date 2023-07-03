import React, { useState, useEffect } from 'react';
import { BrowserRouter, Switch, Route } from 'react-router-dom';
import { ThemeProvider } from '@mui/material';
import Home from './Pages/Home/index';
import Navbar from './Components/Navbar/index';
import Orders from './Pages/Orders/index';
import Login from './Pages/Admin/Login';
import theme from './styles/theme/theme';
import Footer from './Components/Footer';
import NotFound from './Components/NotFound';
import ProtectedRoute from './Components/ProtectedRoute';
import Cadastro from './Pages/Cadastro/index';
import jwt, { JwtPayload } from 'jsonwebtoken';

const Routes: React.FC = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [token, setToken] = useState<string | null>(null);

  const isTokenExpired = (token: string): boolean => {
    try {
      const decodedToken = jwt.decode(token) as JwtPayload;
      if (decodedToken && typeof decodedToken.exp === 'number') {
        const expirationDate = new Date(decodedToken.exp * 1000);
        const currentDate = new Date();
        return expirationDate < currentDate;
      }
      return true;
    } catch (error) {
      return true;
    }
  };

  useEffect(() => {
    // Verifica se o token está presente no localStorage
    const storedToken = localStorage.getItem('token');

    if (storedToken) {
      if (isTokenExpired(storedToken)) {
        localStorage.removeItem('token');
        setIsAuthenticated(false);
        setToken(null);
      } else {
        setIsAuthenticated(true);
        setToken(storedToken);
      }
    } else {
      setIsAuthenticated(false);
      setToken(null);
    }
  }, []);

  return (
    <ThemeProvider theme={theme}>
      <BrowserRouter>
        <Navbar />
        <Switch>
          <Route path="/orders" component={Orders} />
          <ProtectedRoute
            path="/admin/products"
            component={Cadastro}
            isAuthenticated={isAuthenticated}
            redirectPath="/admin"
          />
          <Route exact path="/" component={Home} />
          <Route
            path="/admin"
            render={(props) => (
              <Login {...props} setIsAuthenticated={setIsAuthenticated} setToken={setToken} />
            )}
          />
          <Route path="*" component={NotFound} />
        </Switch>
        <Footer />
      </BrowserRouter>
    </ThemeProvider>
  );
};

export default Routes;
