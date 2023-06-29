import axios from "axios";
import { OrderPayload, Product } from "../Pages/Orders/types";

const API_URL = process.env.REACT_APP_API_URL ?? 'https://localhost:44369';
const mapboxToken = process.env.REACT_APP_ACCESS_TOKEN_MAP_BOX ?? 'pk.eyJ1IjoianVsaW8wOTkwOTkiLCJhIjoiY2tqcTZqdTRxMG96bDM0bW5vbTR0YWdzYiJ9.g0UhwSpA0s4KEvqqrh8Kvg';


export function fetchProducts() {
    return axios(`${API_URL}/products`)
}

export function fetchLocalMapBox(local: string) {
    return axios(`https://api.mapbox.com/geocoding/v5/mapbox.places/${local}.json?access_token=${mapboxToken}`)
}

export function saveOrder(payload: OrderPayload) {
    debugger
    return axios.post(`${API_URL}/orders`, payload, {
       headers: {
            'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
        }
    });
  }
  

export function saveProduct(product: Product) {
    return axios.put(`${API_URL}/products/${product.id}`, product);
}
