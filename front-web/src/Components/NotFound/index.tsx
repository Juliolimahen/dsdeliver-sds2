import styled from 'styled-components';

const NotFoundContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100vh;
`;

const NotFoundHeading = styled.h1`
  font-size: 24px;
  margin-bottom: 16px;
`;

const NotFoundMessage = styled.p`
  font-size: 18px;
`;

const NotFound = () => {
  return (
    <NotFoundContainer>
      <NotFoundHeading>Página não encontrada</NotFoundHeading>
      <NotFoundMessage>A rota que você digitou não existe.</NotFoundMessage>
    </NotFoundContainer>
  );
};

export default NotFound;
