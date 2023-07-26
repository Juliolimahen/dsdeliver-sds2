import React, { useState, useEffect } from 'react';
import LoginForm from '../../../Components/LoginForm';
import { useHistory } from 'react-router-dom';
import authService from '../../../Services/authService';
import { Container, ErrorMsg } from './style';

interface LoginProps {
  setIsAuthenticated: React.Dispatch<React.SetStateAction<boolean>>;
}

const Login: React.FC<LoginProps> = ({ setIsAuthenticated }) => {
  const history = useHistory();
  const [error, setError] = useState<string>('');

  useEffect(() => {
    setIsAuthenticated(false);
  }, [setIsAuthenticated]);

  const handleLoginSubmit = async (login: string, password: string) => {
    try {
      const { success } = await authService.login(login, password);

      if (success) {
        history.push('/admin/products');
        setIsAuthenticated(true);
      } else {
        setError('Credenciais inválidas');
      }
    } catch (error) {
      setError('Erro ao fazer a requisição de login');
      console.log(error);
    }
  };

  return (
    <Container>
      <LoginForm onSubmit={handleLoginSubmit} />
      {error && <ErrorMsg>{error}</ErrorMsg>}
    </Container>
  );
};

export default Login;
