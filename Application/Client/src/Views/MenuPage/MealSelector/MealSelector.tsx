import { Meal } from "../../../Models/meal";
import {
  MealSelectorWrapper,
  MealItem,
  ActionButton,
  Title,
} from "./MealSelector.style";

interface MealSelectorProps {
  meals: Meal[];
  selectedMealIds: number[];
  onMealSelect: (mealId: number) => void;
  onSave: () => void;
  onClose: () => void;
}

const MealSelector: React.FC<MealSelectorProps> = ({
  meals,
  selectedMealIds,
  onMealSelect,
  onSave,
  onClose,
}) => {
  return (
    <MealSelectorWrapper>
      <Title>Select Meals to Add</Title>
      {meals.map((meal) => (
        <MealItem
          data-testid={`Add meal ${meal.name}`}
          key={meal.id}
          selected={selectedMealIds.includes(meal.id)}
          onClick={() => onMealSelect(meal.id)}
        >
          {meal.name}
        </MealItem>
      ))}
      <ActionButton data-testid="Save meal to menu" onClick={onSave}>
        Save
      </ActionButton>
      <ActionButton data-testid="Cancel save meal to menu" onClick={onClose}>
        Cancel
      </ActionButton>
    </MealSelectorWrapper>
  );
};

export default MealSelector;
