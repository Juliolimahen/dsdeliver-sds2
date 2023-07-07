import React, { useState, useEffect} from 'react';
import { useHistory } from 'react-router-dom';
import { ReactComponent as Logo } from '../../assets/navbar/logo.svg';
import { MainNavbar, LogoText, LogoutButton } from './style';
import authService from '../../Services/authService';

const Navbar: React.FC = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const history = useHistory();

  useEffect(() => {
    const checkAuthStatus = () => {
      const isAuthenticated = authService.isAuthenticated();
      setIsAuthenticated(isAuthenticated);
    };

    checkAuthStatus();

    // Chamamos a função checkAuthStatus sempre que a history.push é chamada
    history.listen((_location) => {
      checkAuthStatus();
    });
  }, [history]);

  const handleLogout = () => {
    authService.logout();
    setIsAuthenticated(false);
    history.push('/admin');
  };

  return (
    <MainNavbar>
      <Logo />
      <LogoText to="/">DS Delivery</LogoText>
      {isAuthenticated && <LogoutButton onClick={handleLogout}>Logout</LogoutButton>}
    </MainNavbar>
  );
};

export default Navbar;
