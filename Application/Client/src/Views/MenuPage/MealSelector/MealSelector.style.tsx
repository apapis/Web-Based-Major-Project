import styled from "styled-components";

interface MealItemProps {
  selected: boolean;
}

export const MealSelectorWrapper = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 35px;
`;

export const Title = styled.h2`
  margin-bottom: 20px;
  color: ${(props) => props.theme.colors.brandColorFive};
`;

export const MealItem = styled.div<MealItemProps>`
  background-color: ${(props) =>
    props.selected
      ? props.theme.colors.brandColorFour
      : props.theme.colors.white};
  color: ${(props) =>
    props.selected
      ? props.theme.colors.white
      : props.theme.colors.brandColorSix};
  cursor: pointer;
  padding: 10px 20px;
  margin: 5px 0;
  border-radius: 5px;
  width: 100%;
  text-align: center;
  transition: background-color 0.3s, color 0.3s;

  &:hover {
    background-color: ${(props) =>
      props.selected
        ? props.theme.colors.brandColorFive
        : props.theme.colors.brandColorTwo};
    color: ${(props) => props.theme.colors.white};
  }
`;

export const ActionButton = styled.button`
  background-color: ${(props) => props.theme.colors.brandColorFour};
  color: ${(props) => props.theme.colors.white};
  border: none;
  border-radius: 4px;
  padding: 10px 20px;
  font-size: 16px;
  cursor: pointer;
  transition: background-color 0.3s;
  margin-top: 20px;

  &:hover {
    background-color: ${(props) => props.theme.colors.brandColorFive};
  }
`;
