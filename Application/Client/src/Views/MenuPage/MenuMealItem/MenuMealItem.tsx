import { MenuMealDto } from "../../../Models/menu";
import FaTimes from "../../../assets/icons/close.svg?react";
import {
  MenuMealItemWrapper,
  MealName,
  MealInfo,
  MealInfoLabel,
  AllergenList,
  AllergenItem,
  ImageList,
  ImageItem,
  RemoveButton,
  Image,
} from "./MenuMealItem.style";

interface MenuMealItemProps {
  meal: MenuMealDto;
  onRemove: () => void;
}

const MenuMealItem: React.FC<MenuMealItemProps> = ({ meal, onRemove }) => {
  return (
    <MenuMealItemWrapper>
      <RemoveButton
        onClick={onRemove}
        data-testid={`Remove menu meal ${meal.name}`}
      >
        <FaTimes />
      </RemoveButton>
      <MealName>{meal.name}</MealName>
      <MealInfo>
        <MealInfoLabel>Price: £</MealInfoLabel> ${meal.price.toFixed(2)}
      </MealInfo>
      <MealInfo>
        <MealInfoLabel>Description:</MealInfoLabel> {meal.description}
      </MealInfo>
      <MealInfo>
        <MealInfoLabel>Products:</MealInfoLabel> {meal.products.join(", ")}
      </MealInfo>
      <MealInfo>
        <MealInfoLabel>Allergens:</MealInfoLabel>
      </MealInfo>
      <AllergenList>
        {meal.allergens.map((allergen, index) => (
          <AllergenItem key={index}>{allergen}</AllergenItem>
        ))}
      </AllergenList>
      <MealInfo>
        <MealInfoLabel>Images:</MealInfoLabel>
      </MealInfo>
      <ImageList>
        {meal.imageUrls.map((imageUrl, index) => (
          <ImageItem key={index}>
            <Image
              src={imageUrl}
              alt={`Meal ${meal.name} image ${index + 1}`}
            />
          </ImageItem>
        ))}
      </ImageList>
    </MenuMealItemWrapper>
  );
};

export default MenuMealItem;
