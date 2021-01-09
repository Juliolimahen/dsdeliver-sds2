import './styles.css';
import StepsHeader from './StepsHeader';
import ProductsList from './ProductsList';
import { useEffect, useState } from 'react';
import { OrderLocationdata, Product } from './types';
import { fetchProducts } from './api';
import OrderLocation from './OrderLocation';

function Orders (){
    const [products, setProducts] = useState<Product[]>([])
    const [orderLoacation , setOrderLocation] = useState<OrderLocationdata>();

    useEffect(() =>{
        fetchProducts()
            .then(response => setProducts(response.data))
            .catch(error => console.log(error))
    }, []);
    
    return (
        
        <div className="orders-container">
            <StepsHeader/>
            <ProductsList products={products} />
            <OrderLocation onChangeLocation={location => setOrderLocation(location)} />
        </div>
    )
}

export default Orders;