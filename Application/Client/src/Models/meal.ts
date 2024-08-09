export interface Meal {
  id: number;
  name: string;
  price: number;
  mealProducts: MealProduct[];
}

export interface MealProduct {
  productId: number;
  quantity: number;
}
