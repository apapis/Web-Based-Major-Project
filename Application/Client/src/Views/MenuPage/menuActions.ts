import { Dispatch, SetStateAction } from "react";
import { Meal } from "../../Models/meal";
import { MenuDto } from "../../Models/menu";
import agent from "../../api/agent";

export const fetchMenus = async (
  setAllMenus: Dispatch<SetStateAction<MenuDto[]>>
) => {
  const menus = await agent.Menus.getMenuForWeek();
  setAllMenus(menus);
};

export const fetchMeals = async (
  setAllMeals: Dispatch<SetStateAction<Meal[]>>
) => {
  const meals = await agent.Meals.list();
  setAllMeals(meals);
};

export const handleRestaurantOpenChange = async (
  menuId: number,
  isOpen: boolean,
  setAllMenus: Dispatch<SetStateAction<MenuDto[]>>
) => {
  await agent.Menus.updateRestaurantOpen(menuId, isOpen);
  fetchMenus(setAllMenus);
};

export const handleAddMeal = async (
  menuId: number,
  setSelectedMenuId: Dispatch<SetStateAction<number | null>>,
  setSelectedMealIds: Dispatch<SetStateAction<number[]>>,
  allMenus: MenuDto[],
  fetchMeals: () => Promise<void>,
  setIsModalOpen: Dispatch<SetStateAction<boolean>>
) => {
  setSelectedMenuId(menuId);
  setSelectedMealIds(
    allMenus.find((menu) => menu.id === menuId)?.meals.map((meal) => meal.id) ||
      []
  );
  await fetchMeals();
  setIsModalOpen(true);
};

export const handleRemoveMeal = async (
  menuId: number,
  mealId: number,
  setAllMenus: Dispatch<SetStateAction<MenuDto[]>>
) => {
  await agent.Menus.removeMealFromMenu(menuId, mealId);
  fetchMenus(setAllMenus);
};

export const handleSaveMeals = async (
  selectedMenuId: number | null,
  selectedMealIds: number[],
  allMenus: MenuDto[],
  setAllMenus: Dispatch<SetStateAction<MenuDto[]>>,
  setIsModalOpen: Dispatch<SetStateAction<boolean>>
) => {
  if (selectedMenuId) {
    const currentMealIds =
      allMenus
        .find((menu) => menu.id === selectedMenuId)
        ?.meals.map((meal) => meal.id) || [];
    const mealIdsToAdd = selectedMealIds.filter(
      (id) => !currentMealIds.includes(id)
    );
    const mealIdsToRemove = currentMealIds.filter(
      (id) => !selectedMealIds.includes(id)
    );

    if (mealIdsToAdd.length > 0 || mealIdsToRemove.length > 0) {
      await agent.Menus.updateMenuMeals(
        selectedMenuId,
        mealIdsToAdd,
        mealIdsToRemove
      );
      await fetchMenus(setAllMenus);
    }
    setIsModalOpen(false);
  }
};

export const handleMealSelect = (
  mealId: number,
  selectedMealIds: number[],
  setSelectedMealIds: Dispatch<SetStateAction<number[]>>
) => {
  if (selectedMealIds.includes(mealId)) {
    setSelectedMealIds(selectedMealIds.filter((id) => id !== mealId));
  } else {
    setSelectedMealIds([...selectedMealIds, mealId]);
  }
};

export const handleRestaurantOpenChangeWrapper = (
  menuId: number,
  isOpen: boolean,
  setAllMenus: Dispatch<SetStateAction<MenuDto[]>>
) => {
  handleRestaurantOpenChange(menuId, isOpen, setAllMenus);
};

export const handleAddMealWrapper = (
  menuId: number,
  setSelectedMenuId: Dispatch<SetStateAction<number | null>>,
  setSelectedMealIds: Dispatch<SetStateAction<number[]>>,
  allMenus: MenuDto[],
  fetchMeals: () => Promise<void>,
  setIsModalOpen: Dispatch<SetStateAction<boolean>>
) => {
  handleAddMeal(
    menuId,
    setSelectedMenuId,
    setSelectedMealIds,
    allMenus,
    fetchMeals,
    setIsModalOpen
  );
};

export const handleRemoveMealWrapper = (
  menuId: number,
  mealId: number,
  setAllMenus: Dispatch<SetStateAction<MenuDto[]>>
) => {
  handleRemoveMeal(menuId, mealId, setAllMenus);
};

export const handleSaveMealsWrapper = (
  selectedMenuId: number | null,
  selectedMealIds: number[],
  allMenus: MenuDto[],
  setAllMenus: Dispatch<SetStateAction<MenuDto[]>>,
  setIsModalOpen: Dispatch<SetStateAction<boolean>>
) => {
  handleSaveMeals(
    selectedMenuId,
    selectedMealIds,
    allMenus,
    setAllMenus,
    setIsModalOpen
  );
};
