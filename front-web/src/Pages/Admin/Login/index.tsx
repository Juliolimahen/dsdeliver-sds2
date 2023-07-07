import React, { useState, useEffect } from 'react';
import LoginForm from '../../../Components/LoginForm';
import styled from 'styled-components';
import { useHistory } from 'react-router-dom';
import authService from '../../../Services/authService';

interface LoginProps {
  setIsAuthenticated: React.Dispatch<React.SetStateAction<boolean>>;
}

const Container = styled.div`
  display: flex;
  flex-direction: column;
  min-height: 90vh;
  align-items: center;
  justify-content: center;

  h1 {
    margin: 0;
  }

  @media only screen and (max-width: 768px), (max-height: 700px) {
    height: auto;
    margin-bottom: 10px;
  }
`;

const ErrorMsg = styled.p`
  color: red;
  margin-top: 10px;
`;

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