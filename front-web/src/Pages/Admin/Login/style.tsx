import styled from 'styled-components';

export const Container = styled.div`
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

export const ErrorMsg = styled.p`
  color: red;
  margin-top: 10px;
`;