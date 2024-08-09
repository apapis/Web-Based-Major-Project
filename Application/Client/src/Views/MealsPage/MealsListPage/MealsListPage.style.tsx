// MealsListPage.style.ts
import styled from "styled-components";

export const Wrapper = styled.div`
  max-width: 1400px;
  margin: auto;
  display: flex;
  flex-direction: column;
  padding: 50px 20px;
`;

export const Header = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 30px;

  @media (min-width: 768px) {
    flex-direction: row;
    justify-content: space-between;
  }

  h1 {
    font-size: 32px;
    color: ${({ theme }) => theme.colors.brandColorFive};
    margin: 0;
    margin-bottom: 20px;

    @media (min-width: 768px) {
      margin-bottom: 0;
    }
  }
`;

export const SearchInput = styled.input`
  padding: 10px;
  font-size: 16px;
  border: 1px solid ${({ theme }) => theme.colors.brandColorTwo};
  border-radius: 5px;
  width: 100%;
  max-width: 300px;

  @media (min-width: 768px) {
    margin-top: 0;
  }
`;

export const Box = styled.div`
  display: grid;
  grid-template-columns: 1fr;
  gap: 20px;

  @media (min-width: 768px) {
    grid-template-columns: repeat(2, 1fr);
  }

  @media (min-width: 1024px) {
    grid-template-columns: repeat(3, 1fr);
  }
`;

export const ButtonWrapper = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: 30px;
`;

export const AddButton = styled.button`
  width: 60px;
  height: 60px;
  font-size: 24px;
  font-weight: bold;
  color: ${({ theme }) => theme.colors.white};
  background-color: ${({ theme }) => theme.colors.brandColorFour};
  border: none;
  border-radius: 50%;
  cursor: pointer;
  transition: background-color 0.3s;

  &:hover {
    background-color: ${({ theme }) => theme.colors.brandColorFive};
  }

  &:focus {
    outline: none;
    box-shadow: 0 0 0 3px ${({ theme }) => theme.colors.brandColorTwo};
  }
`;
