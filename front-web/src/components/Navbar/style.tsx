import styled from 'styled-components';
import { Link } from 'react-router-dom';

export const MainNavbar = styled.nav`
  height: 70px;
  background-color: #DA5C5C;
  padding-left: 45px;
  display: flex;
  align-items: center;
`;

export const LogoText = styled(Link)`
  font-weight: bold;
  font-size: 24px;
  line-height: 33px;
  letter-spacing: -0.015em;
  color: #FFFFFF;
  margin-left: 15px;
`;
