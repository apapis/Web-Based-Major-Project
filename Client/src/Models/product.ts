export interface Product {
  id: number;
  name: string;
  store: string;
  weight: number;
  price: number;
  pricePerGram?: number;
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

export type FormValues = {
  id?: number; // Make id optional
  name: string;
  store: string;
  weight: number;
  price: number;
};
