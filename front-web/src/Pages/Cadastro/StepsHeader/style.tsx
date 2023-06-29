import styled from 'styled-components';

export const OrdersStepsContainer = styled.div`
  display: flex;
  justify-content: center;
  background-color: #FFF;
  margin-bottom: 30px;
`;

export const OrdersStepsContent = styled.div`
  display: flex;
  padding: 10px 0;
  width: 40%;

  @media only screen and (max-width: 768px) {
    display: flex;
    flex-direction: column;
    padding: 0;
    width: 90%;
  }
`;

export const StepsTitle = styled.h2`
  font-weight: bold;
  font-size: 36px;
  line-height: 34px;
  letter-spacing: -0.015em;
  color: var(--primary-color);
  margin-right: 100px;
`;

export const StepsItems = styled.ul`
  list-style-type: none;
  padding: 0;
`;

export const StepsItem = styled.li`
  font-weight: normal;
  font-size: 18px;
  line-height: 25px;
  letter-spacing: -0.015em;
  color: var(--secondary-color);
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
