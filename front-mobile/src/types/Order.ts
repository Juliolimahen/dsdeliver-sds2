import { Product } from "./Product";

export type Order = {

    id: number;
    address: string;
    latitude: number;
    longitude: number;
    moment: string
    status: string;
    total: number;
    products: Product[];
}