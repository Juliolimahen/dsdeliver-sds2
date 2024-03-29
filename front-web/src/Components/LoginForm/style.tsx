import styled from 'styled-components';

export const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  cursor: pointer;
  width: 100%;

  h1 {
    margin: 0;
  }

  @media only screen and (max-width: 768px), (max-height: 700px) {
    height: auto;
    margin-bottom: 20px;
  }
`;

export const Titulo = styled.h2`
  margin-top: 0;
  font-weight: bold;
  font-size: 21.5px;
  line-height: 33px;
  letter-spacing: -0.015em;
  color: var(--secondary-color);
  margin-bottom: 20px !important;

  @media only screen and (max-width: 768px) {
    font-size: 20px;
  }
`;

export const FormContainer = styled.form`
  width: 100%;
  max-width: 400px;
  background-color: #fff;
  box-shadow: 0px 4px 20px rgba(0, 0, 0, 0.25);
  border-radius: 10px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  text-align: center;
  padding: 20px;
  margin-bottom: 10px !important;

  @media only screen and (max-width: 768px), (max-height: 700px) {
    margin-bottom: 20px !important;
    margin-top: 10px;
  }
`;

export const InputContainer = styled.div`
  margin-bottom: 30px;
  width: 100%;
`;

export const Input = styled.input`
  width: 100%;
  max-width: 300px;
  height: 40px;
  padding: 5px;
  border: 1px solid #ccc;
  border-radius: 4px;
`;

export const Button = styled.button`
  background-color: var(--primary-color);
  border-radius: 10px;
  border: none;
  height: 40px;
  width: 100%;
  max-width: 200px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: bold;
  font-size: 20px;
  letter-spacing: -0.015em;
  color: #fff;
  cursor: pointer;
  margin-bottom: 30px !important;
  text-decoration: none;
  padding: 0 16px; /* Adiciona espaçamento interno (esquerda e direita) */

  &:hover {
    background-color: var(--primary-hover-color);
    transform: scale(1.01);
  }

  @media only screen and (max-width: 768px) {
    width: 50%; /* Para preencher a largura total em telas menores */
    max-width: none; /* Remove a restrição de largura máxima */
  }
}`;
