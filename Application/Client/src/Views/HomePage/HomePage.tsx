import React, { useEffect, useState } from "react";
import { MenuDto } from "../../Models/menu";
import agent from "../../api/agent";
import {
  MealContainer,
  MealImage,
  MealDetails,
  MealName,
  MealDescription,
  MealProducts,
  MealAllergens,
  MealPrice,
  StyledSwiperContainer,
  ClosedMessage,
  SectionHeading,
} from "./HomePage.style";
import { SwiperSlide } from "swiper/react";
import "swiper/swiper-bundle.css";

const HomePage: React.FC = () => {
  const [menu, setMenu] = useState<MenuDto | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    agent.Menus.getMenuForToday()
      .then((response) => {
        setMenu(response);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching menu:", error);
        setLoading(false);
      });
  }, []);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (!menu) {
    return <div>No menu available for today.</div>;
  }

  if (!menu.isRestaurantOpen) {
    return (
      <ClosedMessage>
        Sorry, we are closed today. Please come back another day.
      </ClosedMessage>
    );
  }

  return (
    <div>
      <h1>Today's Menu</h1>
      {menu.meals.map((meal) => (
        <MealContainer key={meal.id}>
          {meal.imageUrls.length > 0 ? (
            <MealImage>
              <StyledSwiperContainer
                spaceBetween={0}
                slidesPerView={1}
                navigation
                pagination={{ clickable: true }}
                autoplay={{ delay: 3000 }}
                loop={meal.imageUrls.length > 1}
              >
                {meal.imageUrls.map((imageUrl, index) => (
                  <SwiperSlide key={index}>
                    <img src={imageUrl} alt={`${meal.name} ${index + 1}`} />
                  </SwiperSlide>
                ))}
              </StyledSwiperContainer>
            </MealImage>
          ) : (
            <MealDetails
              className="no-image"
              data-testid={`Menu item ${meal.name}`}
            >
              <MealName>{meal.name}</MealName>
              <SectionHeading>Description</SectionHeading>
              <MealDescription>{meal.description}</MealDescription>
              <SectionHeading>Products</SectionHeading>
              <MealProducts>{meal.products.join(", ")}</MealProducts>
              <SectionHeading>Allergens</SectionHeading>
              <MealAllergens>
                {meal.allergens.length > 0 ? meal.allergens.join(", ") : "none"}
              </MealAllergens>
              <SectionHeading>Price</SectionHeading>
              <MealPrice>£{meal.price}</MealPrice>
            </MealDetails>
          )}
          {meal.imageUrls.length > 0 && (
            <MealDetails data-testid={`Menu item ${meal.name}`}>
              <MealName>{meal.name}</MealName>
              <SectionHeading>Description</SectionHeading>
              <MealDescription>{meal.description}</MealDescription>
              <SectionHeading>Products</SectionHeading>
              <MealProducts>{meal.products.join(", ")}</MealProducts>
              <SectionHeading>Allergens</SectionHeading>
              <MealAllergens>
                {meal.allergens.length > 0 ? meal.allergens.join(", ") : "none"}
              </MealAllergens>
              <SectionHeading>Price</SectionHeading>
              <MealPrice>£{meal.price}</MealPrice>
            </MealDetails>
          )}
        </MealContainer>
      ))}
    </div>
  );
};

export default HomePage;
