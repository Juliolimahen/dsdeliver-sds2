import React, { useState } from 'react';
import LoginForm from '../../../Components/LoginForm';
import styled from 'styled-components';
import { useHistory } from 'react-router-dom';
import { login } from '../../../Services/api';

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

const Login: React.FC = () => {
  const history = useHistory();
  const [error, setError] = useState<string>('');

  const handleLoginSubmit = async (username: string, password: string) => {
    try {
      const response = await login(username, password);

      if (response.ok) {
        const data = await response.json();
        const { token } = data; // Desestruturação para obter o token

        // Armazenar o token JWT no Local Storage
        localStorage.setItem('token', token);

        // Redirecionar para a página de sucesso de login
        history.push('/admin/products');
      } else if (response.status === 401) {
        setError('Credenciais inválidas');
      } else {
        setError('Erro na requisição de login');
      }
    } catch (error) {
      setError('Erro ao fazer a requisição de login');
    }
  };


  return (
    <Container>
      <LoginForm onSubmit={handleLoginSubmit} />
    </Container>
  );
};

export default Login;
