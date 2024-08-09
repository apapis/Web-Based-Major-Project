import { Wrapper } from "../../Components/Styles/Styles.style";
import {
  NavigationBox,
  NavigationButton,
} from "../AdminPage/AdminPanelPage.style";
import BookIcon from "../../assets/icons/book.svg?react";
import TruckIcon from "../../assets/icons/truck.svg?react";
import { useNavigate } from "react-router-dom";
export default function MealsPage() {
  const navigate = useNavigate();
  return (
    <Wrapper>
      <NavigationBox>
        <NavigationButton onClick={() => navigate("/admin/meals/mealsList")}>
          <BookIcon />
          Meals List
        </NavigationButton>
        <NavigationButton onClick={() => navigate("/admin/meals/productsList")}>
          <TruckIcon />
          Product List
        </NavigationButton>
      </NavigationBox>
    </Wrapper>
  );
}
