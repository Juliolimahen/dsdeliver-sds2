import axios from "axios";
import { OrderPayload } from "./types";

const API_URL = process.env.REACT_APP_API_URL;

//https://ds-deliver-juliolimahen.onrender.com

//const mapboxToken = process.env.REACT_APP_ACCESS_TOKEN_MAP_BOX;

const mapboxToken = 'pk.eyJ1IjoianVsaW8wOTkwOTkiLCJhIjoiY2tqcTZqdTRxMG96bDM0bW5vbTR0YWdzYiJ9.g0UhwSpA0s4KEvqqrh8Kvg';


export function fetchProducts() {
    return axios(`${API_URL}/products`)
}

export function fetchLocalMapBox(local: string) {
    return axios(`https://api.mapbox.com/geocoding/v5/mapbox.places/${local}.json?access_token=${mapboxToken}`)
}

export function saveOrder(payload: OrderPayload) {
    return axios.post(`${API_URL}/orders`, payload);
}