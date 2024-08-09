import { Wrapper } from "../../Components/Styles/Styles.style";
import {
  Heading,
  NavigationBox,
  NavigationButton,
} from "./AdminPanelPage.style";
import MealsIcon from "../../assets/icons/food-dinner-icon.svg?react";
import BookIcon from "../../assets/icons/book.svg?react";
import TruckIcon from "../../assets/icons/truck.svg?react";
import { useNavigate } from "react-router-dom";
import LoginForm from "../../Components/LoginForm/LoginForm";
import { useAppSelector } from "../../Redux/configureStore";

export default function AdminPanelPage() {
  const navigate = useNavigate();
  const isAuthenticated = useAppSelector((state) => state.auth.isAuthenticated);

  return (
    <Wrapper>
      <Heading>Admin Panel</Heading>
      {isAuthenticated ? (
        <NavigationBox>
          <NavigationButton
            data-testid={`Menu panel`}
            onClick={() => navigate("/admin/menu")}
          >
            <BookIcon />
            Create Menu
          </NavigationButton>
          <NavigationButton
            data-testid={`Meals panel`}
            onClick={() => navigate("/admin/meals")}
          >
            <MealsIcon />
            Meals
          </NavigationButton>
          <NavigationButton onClick={() => navigate("/admin/shoppingList")}>
            <TruckIcon />
            Shopping List
          </NavigationButton>
        </NavigationBox>
      ) : (
        <LoginForm />
      )}
    </Wrapper>
  );
}
