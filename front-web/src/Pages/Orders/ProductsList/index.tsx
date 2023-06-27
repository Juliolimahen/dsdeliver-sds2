import { checkIsSelected } from "../helpers";
import ProductCard from "../ProductCard";
import { Product } from "../types";

type Props = {
    products: Product[];
    selectedProducts: Product[];
    onSelectProduct: (product: Product) => void;
}

function ProductsList({ products, selectedProducts, onSelectProduct }: Props) {
    return (
        <div
            className="orders-list-container">
            <div className="orders-list-items">
                {products.map(product => (
                    <ProductCard
                        key={product.id}
                        product={product}
                        onSelectProduct={onSelectProduct}
                        isSelected={checkIsSelected(selectedProducts, product)}
                    />
                ))}
            </div>
        </div>
    )
}

export default ProductsList;
