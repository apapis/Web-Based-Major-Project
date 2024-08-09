import styled from "styled-components";

export const MenuMealItemWrapper = styled.div`
  background-color: ${(props) => props.theme.colors.brandColorTwo};
  border-radius: 8px;
  padding: 16px;
  margin-bottom: 16px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  position: relative;
  transition: box-shadow 0.3s;

  &:hover {
    box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
  }
`;

export const MealName = styled.h2`
  font-size: 24px;
  margin-bottom: 12px;
  color: ${(props) => props.theme.colors.brandColorFive};
`;

export const MealInfo = styled.p`
  font-size: 16px;
  margin-bottom: 8px;
  color: ${(props) => props.theme.colors.brandColorSix};
`;

export const MealInfoLabel = styled.span`
  font-weight: bold;
  color: ${(props) => props.theme.colors.brandColorFive};
`;

export const AllergenList = styled.ul`
  list-style-type: none;
  padding: 0;
  margin-bottom: 12px;
  display: flex;
  flex-wrap: wrap;
  gap: 4px;
`;

export const AllergenItem = styled.li`
  font-size: 14px;
  padding: 4px 8px;
  background-color: ${(props) => props.theme.colors.brandColorFour};
  color: ${(props) => props.theme.colors.white};
  border-radius: 4px;
`;

export const ImageList = styled.ul`
  list-style-type: none;
  padding: 0;
  display: flex;
  flex-wrap: wrap;
  gap: 8px;
`;

export const ImageItem = styled.li`
  flex: 0 0 calc(50% - 4px);
  max-width: calc(50% - 4px);

  @media (min-width: 768px) {
    flex: 0 0 calc(33.33% - 5.33px);
    max-width: calc(33.33% - 5.33px);
  }
`;

export const Image = styled.img`
  width: 100%;
  height: auto;
  border-radius: 4px;
  transition: transform 0.3s;

  &:hover {
    transform: scale(1.05);
  }
`;

export const RemoveButton = styled.button`
  position: absolute;
  top: 8px;
  right: 8px;
  background-color: transparent;
  color: ${(props) => props.theme.colors.brandColorFive};
  border: none;
  font-size: 24px;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  width: 36px;
  height: 36px;
  border-radius: 50%;
  transition: background-color 0.3s;

  &:hover {
    background-color: rgba(255, 0, 0, 0.1);
  }
`;
