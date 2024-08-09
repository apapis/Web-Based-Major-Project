import styled from "styled-components";

export const SwiperWrapper = styled.div`
  position: relative;
  width: 100%;
`;

export const StyledSwiperContainer = styled("swiper-container")`
  width: 100%;
`;

export const NavigationButton = styled.button`
  position: absolute;
  top: 50%;
  z-index: 10;
  width: 60px;
  height: 60px;
  border: none;
  border-radius: 50%;
  background-color: ${(props) => props.theme.colors.brandColorFour};
  color: ${(props) => props.theme.colors.white};
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
  transform: translateY(-50%);
  display: none;

  &.swiper-button-prev {
    left: -100px;
  }

  &.swiper-button-next {
    right: -100px;
  }

  &:hover {
    background-color: ${(props) => props.theme.colors.brandColorFive};
  }

  @media (min-width: 768px) {
    display: flex;
  }
`;
