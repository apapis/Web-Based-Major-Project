import axios, { AxiosResponse } from "axios";
import { Product, ProductData } from "../Models/product";

import {
  AddMealToMenuDto,
  MenuDto,
  UpdateMenuMealsDto,
  UpdateRestaurantOpenDto,
} from "../Models/menu";
import { NoteDto, CreateNoteDto, UpdateNoteDto } from "../Models/notes";
import Cookies from "js-cookie";
import { Allergen } from "../Models/allergen";

axios.defaults.baseURL = import.meta.env.VITE_API_URL;

const responseBody = (response: AxiosResponse) => response.data;

const setJwt = (jwt: string) => {
  Cookies.set("jwt", jwt, { expires: 7 });
};

const getJwt = () => {
  return Cookies.get("jwt");
};

axios.interceptors.request.use((config) => {
  const token = getJwt();
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

axios.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response && error.response.status === 401) {
      Cookies.remove("jwt");
      window.location.href = "/admin";
    }
    return Promise.reject(error);
  }
);

const requests = {
  get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  post: <T>(url: string, body: T) => axios.post(url, body).then(responseBody),
  put: <T>(url: string, body: T) => axios.put(url, body).then(responseBody),
  delete: (url: string) => axios.delete(url).then(responseBody),
};

const Products = {
  list: () => requests.get("products"),
  details: (id: number) => requests.get(`products/${id}`),
  add: (product: ProductData) =>
    requests.post<ProductData>("products", product),
  edit: (id: number, product: Partial<Product>) =>
    requests.put<Partial<Product>>(`products/${id}`, product),
  delete: (id: number) => requests.delete(`products/${id}`),
};

const Allergens = {
  list: () => requests.get<Allergen[]>("allergens"),
  details: (id: number) => requests.get<Allergen>(`allergens/${id}`),
  add: (allergen: Allergen) => requests.post<Allergen>("allergens", allergen),
  edit: (id: number, allergen: Partial<Allergen>) =>
    requests.put<Partial<Allergen>>(`allergens/${id}`, allergen),
  delete: (id: number) => requests.delete(`allergens/${id}`),
};

const Meals = {
  list: () => requests.get("meals"),
  details: (id: number) => requests.get(`meals/${id}`),
  add: (meal: FormData) => requests.post<FormData>("meals", meal),
  edit: (id: number, meal: FormData) =>
    requests.put<FormData>(`meals/${id}`, meal),
  delete: (id: number) => requests.delete(`meals/${id}`),
};

const Menus = {
  getMenuForWeek: () => requests.get<MenuDto[]>("menu/week"),
  getMenuForToday: () => requests.get<MenuDto>("menu/today"),
  getMenuForDayOfWeek: (dayOfWeek: string) =>
    requests.get<MenuDto>(`menu/${dayOfWeek}`),
  createMenu: (menu: MenuDto) => requests.post<MenuDto>("menu", menu),
  updateMenu: (id: number, menu: MenuDto) =>
    requests.put<MenuDto>(`menu/${id}`, menu),
  deleteMenu: (id: number) => requests.delete(`menu/${id}`),
  updateRestaurantOpen: (menuId: number, isOpen: boolean) =>
    requests.put<UpdateRestaurantOpenDto>(`menu/${menuId}/restaurant-open`, {
      isOpen,
    }),
  addMealToMenu: (menuId: number, mealId: number) =>
    requests.post<AddMealToMenuDto>(`menu/${menuId}/meals`, { mealId }),
  updateMenuMeals: (
    menuId: number,
    mealIdsToAdd: number[],
    mealIdsToRemove: number[]
  ) =>
    requests.put<UpdateMenuMealsDto>(`menu/${menuId}/meals`, {
      mealIdsToAdd,
      mealIdsToRemove,
    }),
  removeMealFromMenu: (menuId: number, mealId: number) =>
    requests.delete(`menu/${menuId}/meals/${mealId}`),
};

const Notes = {
  list: () => requests.get<NoteDto[]>("note"),
  details: (id: number) => requests.get<NoteDto>(`note/${id}`),
  add: (note: CreateNoteDto) => requests.post<CreateNoteDto>("note", note),
  edit: (id: number, note: UpdateNoteDto) =>
    requests.put<UpdateNoteDto>(`note/${id}`, note),
  delete: (id: number) => requests.delete(`note/${id}`),
};

const auth = {
  login: (email: string, password: string) =>
    axios.post("/auth/login", { email, password }).then(responseBody),
  register: (email: string, password: string, confirmPassword: string) =>
    axios
      .post("/auth/register", { email, password, confirmPassword })
      .then(responseBody),
};

const agent = {
  Products,
  Meals,
  Menus,
  Notes,
  Allergens,
  auth,
  setJwt,
  getJwt,
};

export default agent;
