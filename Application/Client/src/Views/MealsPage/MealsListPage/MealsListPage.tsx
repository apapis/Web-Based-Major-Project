import { useEffect, useState } from "react";
import agent from "../../../api/agent";
import MealsListItem from "../../../Components/MealCard/MealCard";
import {
  AddButton,
  Box,
  ButtonWrapper,
  Header,
  SearchInput,
  Wrapper,
} from "./MealsListPage.style";
import { useNavigate } from "react-router-dom";

export default function MealsListPage() {
  const [meals, setMeals] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    fetchMeals();
  }, []);

  const fetchMeals = async () => {
    try {
      const meals = await agent.Meals.list();
      setMeals(meals);
    } catch (error) {
      console.error("Error fetching meals:", error);
    }
  };

  const handleDeleteMeal = async (mealId: number) => {
    try {
      await agent.Meals.delete(mealId);
      fetchMeals();
    } catch (error) {
      console.error("Error deleting meal:", error);
    }
  };

  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSearchTerm(event.target.value);
  };

  const filteredMeals = meals.filter((meal) =>
    meal.name.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <Wrapper>
      <Header>
        <h1>Meals list page</h1>
        <SearchInput
          type="text"
          placeholder="Search meals by name"
          value={searchTerm}
          onChange={handleSearchChange}
        />
      </Header>
      <Box>
        {filteredMeals.map((meal) => (
          <MealsListItem
            meal={meal}
            key={meal.id}
            onDelete={handleDeleteMeal}
          />
        ))}
        <ButtonWrapper>
          <AddButton
            data-testid="Add new meal"
            onClick={() => navigate("/admin/meals/mealsList/meal")}
          >
            +
          </AddButton>
        </ButtonWrapper>
      </Box>
    </Wrapper>
  );
}
