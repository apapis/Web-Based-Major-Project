import { Meal } from "./meal";

export interface Menu {
  id?: number;
  date: string;
  isRestauranOpen: boolean;
  meals: Meal[];
}

export interface MenuDto {
  id?: number;
  dayOfWeek: string;
  isRestaurantOpen: boolean;
  meals: MenuMealDto[];
}

export interface MenuMealDto {
  id: number;
  name: string;
  price: number;
  products: string[];
  allergens: string[];
  description: string;
  imageUrls: string[];
}

export interface UpdateRestaurantOpenDto {
  isOpen: boolean;
}

export interface AddMealToMenuDto {
  mealId: number;
}

export interface UpdateMenuMealsDto {
  mealIdsToAdd: number[];
  mealIdsToRemove: number[];
}
