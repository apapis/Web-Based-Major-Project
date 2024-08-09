// ProductSelector.style.ts
import styled from "styled-components";

export const SearchBar = styled.input`
  width: 80%;
  padding: 10px;
  border: 1px solid ${({ theme }) => theme.colors.brandColorTwo};
  border-radius: 8px;
  font-size: 16px;
  margin-bottom: 20px;
`;

export const Box = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 20px;
`;

interface ProductProps {
  isSelected: boolean;
}

export const Product = styled.div<ProductProps>`
  cursor: pointer;
  background-color: ${({ isSelected, theme }) =>
    isSelected ? theme.colors.brandColorOne : theme.colors.white};
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-between;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  transition: background-color 0.3s ease;

  &:hover {
    background-color: ${({ theme }) => theme.colors.brandColorTwo};
  }

  div {
    display: flex;
    flex-direction: column;
    align-items: center;
    text-align: center;
    margin-bottom: 10px;

    span {
      margin-bottom: 5px;
      font-size: 16px;
      color: ${({ theme }) => theme.colors.brandColorFive};

      &:first-child {
        font-size: 18px;
        font-weight: bold;
      }

      &:nth-child(2) {
        font-size: 14px;
        color: ${({ theme }) => theme.colors.brandColorFour};
      }

      &:last-child {
        font-size: 14px;
        color: ${({ theme }) => theme.colors.brandColorFour};
        margin-top: 5px;
      }
    }
  }

  input {
    width: 80px;
    padding: 5px;
    border: 1px solid ${({ theme }) => theme.colors.brandColorTwo};
    border-radius: 4px;
    font-size: 16px;
    text-align: center;
  }
`;
