import './styles.css';
import { toast } from 'react-toastify';
import { useEffect, useState } from 'react';
import { OrderLocationData, Product } from './types';
import { fetchProducts, saveOrder } from './api';
import OrderLocation from './OrderLocation';
import OrderSummary from './OrderSummary';
import Footer from '../Footer';
import { checkIsSelected } from './helpers';
import { FadeLoader } from 'react-spinners';

import StepsHeader from './StepsHeader';
import ProductsList from './ProductsList';

function Orders() {
  const [products, setProducts] = useState<Product[]>([]);
  const [selectedProducts, setSelectedProducts] = useState<Product[]>([]);
  const [orderLocation, setOrderLocation] = useState<OrderLocationData | undefined>();
  const [loading, setLoading] = useState(true);
  const totalPrice = selectedProducts.reduce((sum, item) => sum + item.price, 0);

  useEffect(() => {
    fetchProducts()
      .then(response => {
        setProducts(response.data);
        setLoading(false);
      })
      .catch(() => {
        toast.warning('Erro ao listar produtos!');
        setLoading(false);
      });
  }, []);

  const handleSubmit = () => {
    setLoading(true);

    const productsIds = selectedProducts.map(({ id }) => ({ id }));
    const payload = {
      ...(orderLocation as OrderLocationData),
      products: productsIds
    };

    saveOrder(payload)
      .then((response) => {
        toast.error(`Pedido enviado com sucesso! Nº ${response.data.id}`);
        setSelectedProducts([]);
        setLoading(false);
      })
      .catch(() => {
        toast.warning('Erro ao enviar pedido');
        setLoading(false);
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
          <div className="loading-container">
            <FadeLoader color="#DA5C5C" loading={loading} />
          </div>
        ) : (
          <ProductsList
            products={products}
            onSelectProduct={handleSelectProduct}
            selectedProducts={selectedProducts}
          />
        )}
        <OrderLocation onChangeLocation={setOrderLocation} />
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