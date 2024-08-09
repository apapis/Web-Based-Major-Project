// CostManager.style.ts
import styled from "styled-components";
import MealInputForm from "../../FormInput/MealInputForm";

export const Box = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
`;

export const Button = styled.button`
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 50px;
  border-radius: 8px;
  background-color: ${({ theme }) => theme.colors.brandColorFour};
  color: ${({ theme }) => theme.colors.white};
  border: none;
  transition: background-color 0.3s ease;

  &:hover {
    cursor: pointer;
    background-color: ${({ theme }) => theme.colors.brandColorFive};
  }
`;

export const Cost = styled.div`
  display: flex;
  flex-direction: column;
  width: calc(50% - 10px);
  padding: 20px;
  border-radius: 8px;
  background-color: ${({ theme }) => theme.colors.brandColorThree};
  position: relative;

  @media (min-width: 768px) {
    width: calc(33.33% - 14px);
  }

  button {
    position: absolute;
    top: 10px;
    right: 10px;
    display: flex;
    justify-content: center;
    align-items: center;
    width: 30px;
    height: 30px;
    border-radius: 50%;
    background-color: ${({ theme }) => theme.colors.brandColorFour};
    color: ${({ theme }) => theme.colors.white};
    border: none;
    transition: background-color 0.3s ease;

    &:hover {
      cursor: pointer;
      background-color: ${({ theme }) => theme.colors.brandColorFive};
    }
  }
`;

export const InputForm = styled(MealInputForm)`
  display: flex;
  flex-direction: column;
  margin-bottom: 20px;
  width: 100%;

  input {
    border: 1px solid ${({ theme }) => theme.colors.brandColorTwo};
    border-radius: 8px;
    font-size: 16px;
    padding: 10px;
    background-color: ${({ theme }) => theme.colors.white};
  }

  label {
    font-size: 18px;
    margin-bottom: 8px;
    font-weight: 500;
    color: ${({ theme }) => theme.colors.brandColorFive};
  }
`;
