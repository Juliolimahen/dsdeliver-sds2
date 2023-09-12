import axios, { AxiosRequestConfig } from "axios";
import authService from './authService';
import { OrderPayload, Product } from "../Pages/Orders/types";

const API_URL = process.env.REACT_APP_API_URL ?? 'https://localhost:44369';
const mapboxToken = process.env.REACT_APP_ACCESS_TOKEN_MAP_BOX ?? 'pk.eyJ1IjoianVsaW8wOTkwOTkiLCJhIjoiY2tqcTZqdTRxMG96bDM0bW5vbTR0YWdzYiJ9.g0UhwSpA0s4KEvqqrh8Kvg';

const api = axios.create({
    baseURL: API_URL,
});

api.interceptors.request.use(
    (config: AxiosRequestConfig) => {
        const token = authService.getToken();

        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }

        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

export function fetchProducts() {
    return api.get<Product[]>('/products');
}

export function fetchLocalMapBox(local: string) {
    return axios.get(`https://api.mapbox.com/geocoding/v5/mapbox.places/${local}.json?access_token=${mapboxToken}`);
}

export function saveOrder(payload: OrderPayload) {
    return api.post('/orders', payload);
}

export function login(login: string, password: string) {
    return axios
        .post<{ token: string }>(`${API_URL}/api/User/Login`, { login, password })
        .then((response) => {
            const token = response.data.token;
            authService.setToken(token);
        });
}

export function saveProduct(product: Product) {
    return api.put<void>(`/products/${product.id}`, product);
}

export function createProduct(product: Product) {
    return api.post<Product>('/products', product);
}

export function deleteProduct(productId: number) {
    return api.delete<void>(`/products/${productId}`);
}
