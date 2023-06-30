import styled from 'styled-components';
import {
  Paper,
  Button,
  Theme
} from '@mui/material';

export const Container = styled.div<{ hasProducts: boolean }>`
  margin: 0 auto;
  padding: 20px;
  display: flex;
  flex-direction: column;
  align-items: center;
  background-color: var(--light-color);

  @media only screen and (max-width: 768px) {
    max-width: 100%;
    margin-bottom: 50px;
  }

  @media (max-height: 700px) {
    margin-bottom: 30px;
  }

  ${({ hasProducts }) => !hasProducts && `
    height: calc(100vh - 254px);
    justify-content: center;
  `}
`;


export const ProductImage = styled.img`
  width: 100%;
  height: auto;
  margin-bottom: 5px;
  border-radius: 50%;
`;

export const AddProductButtonWrapper = styled.div`
  display: flex;
  justify-content: center;
  margin-top: 20px;
`;

export const StyledButton = styled(Button)`
  && {
    margin-bottom: 20px;
    background-color: var(--primary-color);
    color: white;
    &:hover {
      background-color: var(--primary-hover-color);
    }
  }
`;

export const StyledModalPaper = styled(Paper) <{ theme: Theme }>`
  && {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: 400px;
    padding: 24px;

    ${({ theme }) => theme.breakpoints.down('sm')} {
      width: 90%;
    }
  }
`;

export const ButtonAlignmentStyle = styled.div`
    display: flex; 
    gap: 8px;
`