import { NotFoundContainer, NotFoundHeading, NotFoundMessage } from './style'

const PageNotFound = () => {
  return (
    <NotFoundContainer>
      <NotFoundHeading>Página não encontrada</NotFoundHeading>
      <NotFoundMessage>A rota que você digitou não existe.</NotFoundMessage>
    </NotFoundContainer>
  );
};

export default PageNotFound;
