import { formatPrice } from '../helpers';
import {
    OrderSummaryContainer,
    OrderSummaryContent,
    AmountSelectedContainer,
    AmountSelected,
    OrderSummaryTotal,
    OrderSummaryMakeOrder
} from '../styles';

type Props = {
    amount: number;
    totalPrice: number;
    onSubmit: () => void;
}

function OrderSummary({ amount, totalPrice, onSubmit }: Props) {
    return (
        <OrderSummaryContainer>
            <OrderSummaryContent>
                <div>
                    <AmountSelectedContainer>
                        <AmountSelected>{amount}</AmountSelected>
                        PEDIDOS SELECIONADOS
                    </AmountSelectedContainer>
                    <OrderSummaryTotal>
                        <AmountSelected>{formatPrice(totalPrice)}</AmountSelected>
                        VALOR TOTAL
                    </OrderSummaryTotal>
                </div>
                <OrderSummaryMakeOrder onClick={onSubmit}>
                    FAZER PEDIDO
                </OrderSummaryMakeOrder>
            </OrderSummaryContent>
        </OrderSummaryContainer>
    )
}

export default OrderSummary;
