import styled from "styled-components";
import { Swiper } from "swiper/react";

export const MealContainer = styled.div`
  display: grid;
  grid-template-columns: 1fr;
  margin-bottom: 20px;
  border: 2px solid ${(props) => props.theme.colors.brandColorFour};
  background-color: ${(props) => props.theme.colors.brandColorThree};
  border-radius: 5px;
  overflow: hidden;
  min-height: 280px;
  @media (min-width: 765px) {
    grid-template-columns: 3fr 3fr;
  }

  &.no-image {
    grid-template-columns: 1fr;
  }
`;

export const MealImage = styled.div`
  position: relative;
  padding-top: 56.25%;

  img {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    object-fit: cover;
  }
`;

export const MealDetails = styled.div`
  padding: 20px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
`;

export const MealName = styled.h2`
  margin-top: 0;
  font-size: 24px;
  color: ${(props) => props.theme.colors.brandColorFive};
`;

export const MealDescription = styled.p`
  margin-bottom: 10px;
  font-size: 16px;
  color: ${(props) => props.theme.colors.brandColorFour};
`;

export const MealProducts = styled.p`
  margin-bottom: 10px;
  font-size: 16px;
  color: ${(props) => props.theme.colors.brandColorFour};
`;

export const MealAllergens = styled.p`
  margin-bottom: 10px;
  font-size: 16px;
  color: ${(props) => props.theme.colors.brandColorFour};
`;

export const MealPrice = styled.p`
  margin-top: auto;
  font-size: 18px;
  font-weight: bold;
  color: ${(props) => props.theme.colors.brandColorFive};

  padding-top: 10px;
`;

export const StyledSwiperContainer = styled(Swiper)`
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;

  .swiper-slide {
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .swiper-slide img {
    width: 100%;
    height: 100%;
    object-fit: cover;
  }

  .swiper-button-next,
  .swiper-button-prev {
    color: white;
  }

  .swiper-pagination-bullet-active {
    background-color: white;
  }
`;

export const ClosedMessage = styled.div`
  text-align: center;
  font-size: 24px;
  color: ${(props) => props.theme.colors.brandColorFour};
  background-color: ${(props) => props.theme.colors.brandColorThree};
  padding: 20px;
  border-radius: 5px;
`;

export const SectionHeading = styled.h3`
  margin-bottom: 10px;
  font-size: 20px;
  color: ${(props) => props.theme.colors.brandColorFive};
  border-bottom: 1px solid ${(props) => props.theme.colors.brandColorFour};
  padding-bottom: 5px;
`;
