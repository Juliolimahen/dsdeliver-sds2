import React from 'react';
import LoginForm from '../../Components/LoginForm';
import Footer from '../../Components/Footer';
import { Link } from 'react-router-dom';
import styled from 'styled-components';


const Container = styled.div`
  padding-top: 100px;
  height: calc(100vh - 254px);

  h1 {
    margin: 0;
  }

  @media only screen and (max-width: 768px) {
    height: auto;
    margin-bottom: 50px;
  }

  @media (max-height: 700px) {
    height: auto;
    margin-bottom: 30px;
  }
`;

const Login: React.FC = () => {
    const handleLoginSubmit = (username: string, password: string) => {
    };

    return (
        <div>
            <Container>
                <LoginForm onSubmit={handleLoginSubmit} />
            </Container>
            <Footer />
        </div>
    );
};

export default Login;
