import { Swiper, SwiperSlide } from "../../../Components/Swiper/Swiper";
import { MenuDto } from "../../../Models/menu";
import MenuMealItem from "../MenuMealItem/MenuMealItem";
import { AddMealButton, OneMenu, CheckboxLabel } from "./MenuList.style";

interface MenuListProps {
  menus: MenuDto[];
  onRestaurantOpenChange: (menuId: number, isOpen: boolean) => void;
  onAddMeal: (menuId: number) => void;
  onRemoveMeal: (menuId: number, mealId: number) => void;
}

const MenuList: React.FC<MenuListProps> = ({
  menus,
  onRestaurantOpenChange,
  onAddMeal,
  onRemoveMeal,
}) => {
  return (
    <Swiper
      breakpoints={{
        0: { slidesPerView: 1 },
        560: { slidesPerView: 1 },
        768: { slidesPerView: 2 },
        975: { slidesPerView: 3 },
      }}
    >
      {menus.map((menu) => (
        <SwiperSlide key={menu.id}>
          <OneMenu data-testid={`Menu for ${menu.dayOfWeek}`}>
            <h3>{menu.dayOfWeek}</h3>
            <CheckboxLabel>
              <input
                type="checkbox"
                checked={menu.isRestaurantOpen}
                onChange={(e) =>
                  onRestaurantOpenChange(menu.id, e.target.checked)
                }
              />
              Is Restaurant Open
            </CheckboxLabel>
            {menu.meals.map((meal) => (
              <MenuMealItem
                key={meal.id}
                meal={meal}
                onRemove={() => onRemoveMeal(menu.id, meal.id)}
              />
            ))}
            <AddMealButton
              data-testid={`Add meal for ${menu.dayOfWeek}`}
              onClick={() => onAddMeal(menu.id)}
            >
              Add Meal
            </AddMealButton>
          </OneMenu>
        </SwiperSlide>
      ))}
    </Swiper>
  );
};

export default MenuList;
