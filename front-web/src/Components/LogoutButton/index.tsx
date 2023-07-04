import React from 'react';
import authService  from '../../Services/authService';

const LogoutButton: React.FC = () => {
  const handleLogout = () => {
    authService.logout();
    // Redirecione para a página de login ou faça algo depois de deslogar
  };

  return (
    <button onClick={handleLogout}>Logout</button>
  );
};

export default LogoutButton;
