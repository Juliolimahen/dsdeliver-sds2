import './styles.css';
import { toast } from 'react-toastify';
import StepsHeader from './StepsHeader';
import ProductsList from './ProductsList';
import { useEffect, useState } from 'react';
import { OrderLocationData, Product } from './types';
import { fetchProducts, saveOrder } from './api';
import OrderLocation from './OrderLocation';
import OrderSummary from './OrderSummary';
import Footer from '../Footer';
import { checkIsSelected } from './helpers';

function Orders() {
  const [products, setProducts] = useState<Product[]>([]);
  const [selectedProducts, setSelectedProducts] = useState<Product[]>([]);
  const [orderLocation, setOrderLocation] = useState<OrderLocationData>();
  const [loading, setLoading] = useState(true); // Added loading state
  const totalPrice = selectedProducts.reduce((sum, item) => {
    return sum + item.price;
  }, 0);

  useEffect(() => {
    fetchProducts()
      .then(response => {
        setProducts(response.data);
        setLoading(false); // Set loading to false when the response is received
      })
      .catch(() => {
        toast.warning('Erro ao listar produtos!');
        setLoading(false); // Set loading to false in case of an error
      });
  }, []);

  const handleSubmit = () => {
    setLoading(true); // Set loading to true when submitting the order

    const productsIds = selectedProducts.map(({ id }) => ({ id }));
    const payload = {
      ...orderLocation!,
      products: productsIds
    };

    saveOrder(payload)
      .then((response) => {
        toast.error(`Pedido enviado com sucesso! Nº ${response.data.id}`);
        setSelectedProducts([]);
        setLoading(false); // Set loading to false after successful submission
      })
      .catch(() => {
        toast.warning('Erro ao enviar pedido');
        setLoading(false); // Set loading to false in case of an error
      });
  };

  const handleSelectProduct = (product: Product) => {
    const isAlreadySelected = checkIsSelected(selectedProducts, product);

    if (isAlreadySelected) {
      const selected = selectedProducts.filter(item => item.id !== product.id);
      setSelectedProducts(selected);
    } else {
      setSelectedProducts(previous => [...previous, product]);
    }
  };

  return (
    <>
      <div className="orders-container">
        <StepsHeader />
        {loading ? (
          <div className="loading">Loading...</div>
        ) : (
          <ProductsList
            products={products}
            onSelectProduct={handleSelectProduct}
            selectedProducts={selectedProducts}
          />
        )}
        <OrderLocation onChangeLocation={location => setOrderLocation(location)} />
        <OrderSummary
          amount={selectedProducts.length}
          totalPrice={totalPrice}
          onSubmit={handleSubmit}
        />
      </div>
      <Footer />
    </>
  );
}

export default Orders;
