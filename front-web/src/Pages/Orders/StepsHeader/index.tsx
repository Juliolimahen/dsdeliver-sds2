import React from 'react';

import {
    OrderStepsContainer,
    OrderStepsContent,
    StepsTitle,
    StepsItems,
    StepsNumber,
} from '../styles';

const StepHeader: React.FC = () => {
    return (
        <OrderStepsContainer>
            <OrderStepsContent>
                <StepsTitle>
                    SIGA AS
                    <br />
                    ETAPAS
                </StepsTitle>
                <StepsItems>
                    <li>
                        <StepsNumber>1</StepsNumber>
                        Selecione os produtos e localização
                    </li>
                    <li>
                        <StepsNumber>2</StepsNumber>
                        Depois clique em
                        <strong> "ENVIAR PEDIDO"</strong>
                    </li>
                </StepsItems>
            </OrderStepsContent>
        </OrderStepsContainer>
    );
};

export default StepHeader;