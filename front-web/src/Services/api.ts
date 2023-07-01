import axios from "axios";
import { OrderPayload, Product } from "../Pages/Orders/types";

const API_URL = process.env.REACT_APP_API_URL ?? 'https://localhost:44369';
const mapboxToken = process.env.REACT_APP_ACCESS_TOKEN_MAP_BOX;


export function fetchProducts() {
    return axios(`${API_URL}/products`)
}

export function fetchLocalMapBox(local: string) {
    return axios(`https://api.mapbox.com/geocoding/v5/mapbox.places/${local}.json?access_token=${mapboxToken}`)
}

export function saveOrder(payload: OrderPayload) {
    return axios.post(`${API_URL}/orders`, JSON.stringify(payload), {
        headers: {
            'Content-Type': 'application/json',
        },
    });
}


export function saveProduct(product: Product) {
    return axios.put(`${API_URL}/products/${product.id}`, JSON.stringify(product), {
        headers: {
            'Content-Type': 'application/json',
        },
    });
}

export function createProduct(product: Product) {
    return axios.post(`${API_URL}/products`, product, {
        headers: {
            'Content-Type': 'application/json',
        },
    });
}

export function deleteProduct(productId: number) {
    return axios.delete(`${API_URL}/products/${productId}`);
}