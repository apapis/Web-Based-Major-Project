import axios, { AxiosResponse } from "axios";
import { Product } from "../Models/product";
axios.defaults.baseURL = "https://localhost:7271/api/";

const responseBody = (response: AxiosResponse) => response.data;

const requests = {
  get: (url: string) => axios.get(url).then(responseBody),
  post: <T>(url: string, body: T) => axios.post(url, body).then(responseBody),
  put: <T>(url: string, body: T) => axios.put(url, body).then(responseBody),
  delete: (url: string) => axios.delete(url).then(responseBody),
};

const Products = {
  list: () => requests.get("products"),
  details: (id: number) => requests.get(`products/${id}`),
  add: (product: Product) => requests.post<Product>("products", product),
  edit: (id: number, product: Partial<Product>) =>
    requests.put<Partial<Product>>(`products/${id}`, product),
  delete: (id: number) => requests.delete(`products/${id}`),
};

const agent = {
  Products,
};

export default agent;
