import React from 'react';
import {
  OrdersStepsContainer,
  OrdersStepsContent,
  StepsTitle,
  StepsItems,
  StepsItem,
  StepsNumber
} from './StepsHeader.style';

const StepsHeader: React.FC = () => {
  return (
    <OrdersStepsContainer>
      <OrdersStepsContent>
        <StepsTitle>
          SIGA AS <br /> ETAPAS
        </StepsTitle>
        <StepsItems>
          <StepsItem>
            <StepsNumber>1</StepsNumber>
            Selecione <strong>"Editar"</strong> para editar os produtos.
          </StepsItem>
          <StepsItem>
            <StepsNumber>2</StepsNumber>
            Clique em <strong>"Adicionar Produto"</strong> para adicionar um novo.
          </StepsItem>
        </StepsItems>
      </OrdersStepsContent>
    </OrdersStepsContainer>
  );
}

export default StepsHeader;
