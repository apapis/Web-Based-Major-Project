import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import HomePage from "../Views/HomePage/HomePage";
import AdminPanelPage from "../Views/AdminPage/AdminPanelPage";
import ProductPage from "../Views/MealsPage/ProductPage/ProductPage";
import MealsPage from "../Views/MealsPage/MealsPage";
import MealsListPage from "../Views/MealsPage/MealsListPage/MealsListPage";
import MealManager from "../Components/MealManager/MealManager";
import MenuPage from "../Views/MenuPage/MenuPage";
import ShoppingListPage from "../Views/ShoppingListPage/ShoppingListPage";
import RequireAuth from "../Components/RequireAuth/RequireAuth";
import AdminPanel from "../Views/AdminPage/AdminPanel";
import RegisterForm from "../Components/RegisterForm/RegisterForm";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      {
        path: "admin",
        element: (
          <RequireAuth>
            <AdminPanel />
          </RequireAuth>
        ),
        children: [
          { index: true, element: <AdminPanelPage /> },
          { path: "meals", element: <MealsPage /> },
          { path: "meals/productsList", element: <ProductPage /> },
          { path: "meals/mealsList", element: <MealsListPage /> },
          { path: "meals/mealsList/meal", element: <MealManager /> },
          { path: "meals/mealsList/meal/edit", element: <MealManager edit /> },
          { path: "menu", element: <MenuPage /> },
          { path: "shoppingList", element: <ShoppingListPage /> },
          { path: "new-user", element: <RegisterForm /> },
        ],
      },
      { path: "", element: <HomePage /> },
    ],
  },
]);
