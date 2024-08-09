import { Allergen } from "./allergen";

export interface Product {
  id: number;
  name: string;
  store: string;
  quantity: number;
  price: number;
  pricePerUnit: number;
  unit: string;
  allergens: Allergen[];
}
export interface ProductData extends FormValues {
  pricePerUnit: number;
  allergenIds: number[];
}
export interface ProductValues {
  name: string;
  store: string;
  weight: number;
  price: number;
  pricePerGram?: number;
}
export interface ProductTableProps {
  productsList: Product[];
}

export interface ProductManagerProps {
  product?: Product | null;
}

export interface FormValues {
  id?: number;
  name: string;
  store: string;
  quantity: number;
  price: number;
  unit: string;
}
