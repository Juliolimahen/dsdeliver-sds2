import styled, { css } from 'styled-components';

interface OrderCardContainerProps {
  isSelected: boolean;
}

export const OrderContainer = styled.div`
  background-color: var(--light-color);

  h3 {
    margin: 0;
  }
`;

export const OrderStepsContainer = styled.header`
  display: flex;
  justify-content: center;
  background-color: #fff;
  margin-bottom: 30px;
`;

export const OrderStepsContent = styled.div`
  display: flex;
  padding: 25px 0;
  width: 70%;

  @media only screen and (max-width: 768px) {
    display: flex;
    flex-direction: column;
    padding: 0;
    width: 90%;
  }
`;

export const StepsTitle = styled.h1`
  font-weight: bold;
  font-size: 36px;
  line-height: 34px;
  letter-spacing: -0.015em;
  color: var(--primary-color);
  margin-right: 100px;
`;

export const StepsItems = styled.ul`
  list-style-type: none;

  li {
    font-weight: normal;
    font-size: 18px;
    line-height: 25px;
    letter-spacing: -0.015em;
    color: var(--secondary-color);
  }

  @media only screen and (max-width: 768px) {
    padding: 0;
  }
`;

export const StepsNumber = styled.span`
  font-size: 24px;
  font-style: normal;
  font-weight: bold;
  line-height: 33px;
  letter-spacing: -0.015em;
  color: var(--primary-color);
  margin-right: 10px;
`;

export const OrderListContainer = styled.div`
  display: flex;
  justify-content: center;
`;

export const OrderListItems = styled.div`
  width: 70%;
  display: grid;
  grid-template-columns: repeat(auto-fill, 235px);
  grid-gap: 20px 15px;
  justify-content: space-between;
`;

export const OrderCardContainer = styled.div<OrderCardContainerProps>`
  background-color: #fff;
  box-shadow: 0px 4px 20px rgba(0, 0, 0, 0.25);
  border-radius: 10px;
  padding: 15px;
  cursor: pointer;

  ${props =>
    props.isSelected &&
    css`
      border: 4px solid #008e5b;
      box-sizing: border-box;
      box-shadow: 0px 4px 20px rgba(0, 0, 0, 0.25);
      border-radius: 10px;
    `}

  &:hover {
    transform: scale(1.01);
  }
`;

export const OrderCardTitle = styled.h3<OrderCardContainerProps>`
  font-weight: bold;
  font-size: 18px;
  line-height: 25px;
  letter-spacing: -0.015em;
  color: var(--primary-color);
  text-align: center;

  ${props =>
    props.isSelected &&
    css`
      color: #008e5b;
    `}
`;

export const OrderCardImage = styled.img`
  border-radius: 10px;
  margin-top: 15px;
  margin-bottom: 15px;
  width: 100%;
`;

export const OrderCardPrice = styled.h3<OrderCardContainerProps>`
  font-weight: bold;
  font-size: 24px;
  line-height: 33px;
  letter-spacing: -0.015em;
  color: var(--primary-color);
  text-align: left;

  ${props =>
    props.isSelected &&
    css`
      color: #008e5b;
    `}
`;

export const OrderCardDescription = styled.div`
  border-top: 1px solid #e6e6e6;
  margin-top: 15px;
  padding-top: 15px;

  h3 {
    font-size: 16px;
    line-height: 22px;
    letter-spacing: -0.015em;
    color: var(--secondary-color);
  }

  p {
    font-weight: normal;
    font-size: 14px;
    line-height: 19px;
    letter-spacing: -0.015em;

    color: var(--secondary-color);
  }
`;

export const OrderLocationContainer = styled.div`
  display: flex;
  justify-content: center;

  @media only screen and (max-width: 768px) {
    margin-bottom: 20px;
  }
`;

export const OrderLocationContent = styled.div`
  width: 70%;
  margin: 30px 0 120px 0;
  padding: 20px 0;
  height: 400px;
  background: #fff;
  box-shadow: 0px 4px 20px rgba(0, 0, 0, 0.25);
  border-radius: 10px;

  .leaflet-container {
    width: 100%;
    height: 100%;
    border-radius: 10px;
    margin-top: 20px;
    z-index: 0;
  }

  .filter {
    width: 80%;

    > div {
      border-radius: 10px;
      min-height: 60px;
    }
  }

  @media only screen and (max-width: 768px) {
    width: 90%;
  }
`;

export const OrderLocationTitle = styled.h3`
  font-weight: bold;
  font-size: 18px;
  line-height: 25px;
  text-align: center;
  letter-spacing: -0.015em;
  color: var(--secondary-color);
`;

export const FilterContainer = styled.div`
  display: flex;
  justify-content: center;
  margin-top: 20px;
`;

export const OrderSummaryContainer = styled.div`
  display: flex;
  justify-content: center;
  margin-bottom: 30px;
`;

export const OrderSummaryContent = styled.div`
  width: calc(70% - 40px);
  display: flex;
  justify-content: space-between;
  background: #fff;
  box-shadow: 0px 4px 20px rgba(0, 0, 0, 0.25);
  border-radius: 10px;
  background-color: var(--primary-color);
  color: #fff;
  padding: 20px;

  @media only screen and (max-width: 768px) {
    width: 80%;
    display: flex;
    flex-direction: column;
  }
`;

export const AmountSelectedContainer = styled.span`
  font-weight: normal;
  font-size: 11px;
  line-height: 15px;
  letter-spacing: -0.015em;
  display: block;
  margin-bottom: 10px;
`;

export const AmountSelected = styled.span`
  font-weight: bold;
  font-size: 16px;
  line-height: 15px;
  letter-spacing: -0.015em;
  margin-right: 5px;
`;

export const OrderSummaryTotal = styled.span`
  font-weight: normal;
  font-size: 14px;
  line-height: 15px;
  letter-spacing: -0.015em;
  color: #fff;
  display: block;
`;

export const OrderSummaryMakeOrder = styled.button`
  width: 220px;
  height: 45px;
  background: #fff;
  border-radius: 10px;
  font-weight: bold;
  font-size: 18px;
  line-height: 25px;
  text-align: center;
  letter-spacing: -0.015em;
  color: var(--primary-color);
  border: 0;
  cursor: pointer;

  &:focus {
    outline: none;
  }

  &:hover {
    background-color: #fcf2f2;
    transform: scale(1.01);
  }

  @media only screen and (max-width: 768px) {
    margin-top: 15px;
    width: 100%;
  }
`;