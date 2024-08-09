import {
  Box,
  Card,
  CostBox,
  Img,
  CostItem,
  Allergies,
  Heading,
} from "./MealCard.style";
import DeletICon from "../../assets/icons/close.svg?react";
import EditIcon from "../../assets/icons/edit.svg?react";
import { ButtonsWithSvg } from "../../assets/styles/Button.style";
import { useNavigate } from "react-router-dom";

export default function MealsListItem({ meal, onDelete }) {
  const navigate = useNavigate();

  const handleDelete = () => {
    onDelete(meal.id);
  };

  const handleEdit = () => {
    navigate(`/admin/meals/mealsList/meal/edit?id_meal=${meal.id}`);
  };

  return (
    <Card>
      <Heading>
        <h3>{meal.name}</h3>
        <div>
          <ButtonsWithSvg
            onClick={handleEdit}
            data-testid={`Edit meal ${meal.name}`}
          >
            <EditIcon />
          </ButtonsWithSvg>
          <ButtonsWithSvg onClick={handleDelete}>
            <DeletICon />
          </ButtonsWithSvg>
        </div>
      </Heading>
      <Box>
        {meal.imageUrls && meal.imageUrls.length > 0 && (
          <Img src={meal.imageUrls[0]} alt={meal.name} />
        )}
        <CostBox>
          <CostItem>
            Cost of all ingredients £{meal.pricing.costOfAllIngredients}
          </CostItem>
          <CostItem>Cost of make it £{meal.pricing.costOfMakeIt}</CostItem>
          <CostItem>
            Price: £{meal.pricing.price} (Proposed price:{" £"}
            {meal.pricing.proposedPrice})
          </CostItem>
        </CostBox>
      </Box>
      <Allergies data-testid={`Allergies ${meal.name}`}>
        Allergies:{" "}
        {meal.allergens && meal.allergens.length > 0
          ? meal.allergens.join(", ")
          : "none"}
      </Allergies>
    </Card>
  );
}
