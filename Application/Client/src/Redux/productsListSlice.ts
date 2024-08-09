import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { Product } from "../Models/product";

interface ProductsListState {
  products: Product[];
}

const initialState: ProductsListState = {
  products: [],
};

export const productsListSlice = createSlice({
  name: "productsList",
  initialState,
  reducers: {
    setProductsList: (state, action: PayloadAction<Product[]>) => {
      state.products = action.payload;
    },
    addProduct: (state, action: PayloadAction<Product>) => {
      state.products.push(action.payload);
    },
    editProduct: (state, action: PayloadAction<Product>) => {
      const index = state.products.findIndex(
        (product) => product.id === action.payload.id
      );
      if (index !== -1) {
        state.products[index] = action.payload;
      }
    },
    deleteProduct: (state, action: PayloadAction<number>) => {
      state.products = state.products.filter(
        (product) => product.id !== action.payload
      );
    },
  },
});

export const { setProductsList, addProduct, editProduct, deleteProduct } =
  productsListSlice.actions;
