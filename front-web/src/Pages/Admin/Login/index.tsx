import React from 'react';
import LoginForm from '../../../Components/LoginForm';
import styled from 'styled-components';

const Container = styled.div`
  display: flex;
  flex-direction: column;
  min-height: 90vh;
  align-items: center; /* Alinha os itens verticalmente ao centro */
  justify-content: center; /* Alinha os itens horizontalmente ao centro */
  
  h1 {
    margin: 0;
  }

  @media only screen and (max-width: 768px), (max-height: 700px) {
    height: auto;
    margin-bottom: 10px;
  }
`;

const Login: React.FC = () => {
  const handleLoginSubmit = (username: string, password: string) => {
    // Lógica de envio do formulário de login
  };

  return (
    <Container>
      <LoginForm onSubmit={handleLoginSubmit} />
    </Container>
  );
};

export default Login;
