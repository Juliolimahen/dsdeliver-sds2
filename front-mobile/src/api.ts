import axios from "axios";

const API_URL ='https://dev-project-sds2.herokuapp.com'

export function fetchOrders() {
    return axios(`${API_URL}/orders`);
}

export function confirmDelivery(orderId : number){
    return axios.put(`${API_URL}/orders/${orderId}/delivered`)
}