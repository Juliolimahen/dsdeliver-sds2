import React, { useState } from 'react';
import LoginForm from '../../../Components/LoginForm';
import styled from 'styled-components';
import { useHistory } from 'react-router-dom';
import { login } from '../../../Services/api';
import { isTokenExpired } from '../../../utils/auth';

interface LoginProps {
  setIsAuthenticated: React.Dispatch<React.SetStateAction<boolean>>;
  setToken: React.Dispatch<React.SetStateAction<string | null>>;
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

const Login: React.FC<LoginProps> = ({ setIsAuthenticated, setToken }) => {
  const history = useHistory();
  const [error, setError] = useState<string>('');

  const handleLoginSubmit = async (username: string, password: string) => {
    try {
      const response = await login(username, password);

      if (response.status === 200) {
        const data = response.data;
        const { token } = data; // Desestruturação para obter o token

        if (isTokenExpired(token)) {

          localStorage.removeItem('token');
          setError('Sua sessão expirou. Por favor, faça login novamente.');
        } else {

          localStorage.setItem('token', token);

          setToken(token);

          setIsAuthenticated(true);

          history.push('/admin/products');
        }
      } else {
        setError('Erro na requisição de login');
      }
    } catch (error) {
      if (error.response && error.response.status === 401) {
        setError('Credenciais inválidas');
      } else {
        setError('Erro ao fazer a requisição de login');
      }
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
