import { Link } from 'react-router-dom';
import styled from 'styled-components';


export const HomeContainer = styled.div`
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

export const HomeContent = styled.div`
  display: flex;
  justify-content: center;

  @media only screen and (max-width: 768px) {
    flex-direction: column-reverse;
  }
`;

export const HomeActions = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
`;

export const HomeTitle = styled.h1`
  font-weight: bold;
  font-size: 55px;
  line-height: 58px;
  letter-spacing: -0.015em;
  color: var(--dark-color);
  margin-bottom: 40px !important;

  @media only screen and (max-width: 768px) {
    font-size: 40px;
  }
`;

export const HomeSubtitle = styled.h3`
  padding-left:2%;  
  font-weight: normal;
  font-size: 24px;
  line-height: 33px;
  letter-spacing: -0.015em;
  color: var(--secondary-color);
  margin-bottom: 30px;
`;

export const HomeButton = styled(Link)`
  background-color: var(--primary-color);
  border-radius: 10px;
  height: 70px;
  width: 235px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: bold;
  font-size: 24px;
  letter-spacing: -0.015em;
  color: #FFF;
  cursor: pointer;
  text-decoration: none;

  &:hover {
    background-color: var(--primary-hover-color);
    transform: scale(1.01);
  }
`;

export const HomeImage = styled.div`
  svg {
    width: 100% !important;
  }

  @media only screen and (max-width: 768px) {
    text-align: center;

    svg {
      width: 80%;
    }
  }
`;