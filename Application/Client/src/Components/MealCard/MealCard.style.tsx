// MealCard.style.ts
import styled from "styled-components";

export const Card = styled.div`
  max-width: 400px;
  height: auto;
  padding: 20px;
  background-color: ${({ theme }) => theme.colors.brandColorThree};
  border-radius: 15px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s ease;

  &:hover {
    transform: translateY(-5px);
  }
`;

export const Heading = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 20px;

  h3 {
    margin: 0;
    font-size: 24px;
    font-weight: bold;
    color: ${({ theme }) => theme.colors.brandColorFive};
  }
`;

export const Box = styled.div`
  display: flex;
  gap: 20px;
`;

export const Img = styled.img`
  width: 150px;
  height: 150px;
  object-fit: cover;
  border-radius: 15px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
`;

export const CostBox = styled.div`
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
`;

export const CostItem = styled.div`
  padding: 10px;
  background-color: ${({ theme }) => theme.colors.brandColorOne};
  border-radius: 5px;
  margin-bottom: 10px;
  color: ${({ theme }) => theme.colors.brandColorFive};
`;

export const Allergies = styled.div`
  margin-top: 20px;
  font-style: italic;
  color: ${({ theme }) => theme.colors.brandColorFour};
`;
