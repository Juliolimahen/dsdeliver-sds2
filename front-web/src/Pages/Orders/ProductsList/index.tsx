import { checkIsSelected } from "../helpers";
import ProductCard from "../ProductCard";
import { Product } from "../types";
import { OrderListContainer, OrderListItems } from "../styles";

type Props = {
    products: Product[];
    selectedProducts: Product[];
    onSelectProduct: (product: Product) => void;
}

const ProductsList: React.FC<Props> = ({
    products,
    selectedProducts,
    onSelectProduct,
}: Props) => {
    return (
        <OrderListContainer>
            <OrderListItems>
                {products.map(product => (
                    <ProductCard
                        key={product.id}
                        product={product}
                        onSelectProduct={onSelectProduct}
                        isSelected={checkIsSelected(selectedProducts, product)}
                    />
                ))}
            </OrderListItems>
        </OrderListContainer>
    );
};

export default ProductsList;
