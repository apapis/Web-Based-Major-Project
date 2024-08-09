import { useEffect, useState } from "react";
import { MenuDto } from "../../Models/menu";
import Modal from "../../Components/Modal/Modal";
import { Meal } from "../../Models/meal";
import {
  fetchMenus,
  fetchMeals,
  handleAddMealWrapper,
  handleMealSelect,
  handleRemoveMealWrapper,
  handleRestaurantOpenChangeWrapper,
  handleSaveMealsWrapper,
} from "./menuActions";
import MealSelector from "./MealSelector/MealSelector";
import MenuList from "./MenuList/MenuList";
import { Box, Wrapper } from "./MenuPage.style";

export default function MenuPage() {
  const [allMenus, setAllMenus] = useState<MenuDto[]>([]);
  const [allMeals, setAllMeals] = useState<Meal[]>([]);
  const [selectedMealIds, setSelectedMealIds] = useState<number[]>([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedMenuId, setSelectedMenuId] = useState<number | null>(null);

  useEffect(() => {
    fetchMenus(setAllMenus);
  }, []);

  return (
    <Wrapper>
      <h1>Menu Page</h1>
      <Box>
        <MenuList
          menus={allMenus}
          onRestaurantOpenChange={(menuId, isOpen) =>
            handleRestaurantOpenChangeWrapper(menuId, isOpen, setAllMenus)
          }
          onAddMeal={(menuId) =>
            handleAddMealWrapper(
              menuId,
              setSelectedMenuId,
              setSelectedMealIds,
              allMenus,
              () => fetchMeals(setAllMeals),
              setIsModalOpen
            )
          }
          onRemoveMeal={(menuId, mealId) =>
            handleRemoveMealWrapper(menuId, mealId, setAllMenus)
          }
        />
      </Box>
      <Modal open={isModalOpen} onClose={() => setIsModalOpen(false)}>
        <MealSelector
          meals={allMeals}
          selectedMealIds={selectedMealIds}
          onMealSelect={(mealId) =>
            handleMealSelect(mealId, selectedMealIds, setSelectedMealIds)
          }
          onSave={() =>
            handleSaveMealsWrapper(
              selectedMenuId,
              selectedMealIds,
              allMenus,
              setAllMenus,
              setIsModalOpen
            )
          }
          onClose={() => setIsModalOpen(false)}
        />
      </Modal>
    </Wrapper>
  );
}
