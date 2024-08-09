// AllergenSection.style.ts
import styled from "styled-components";

export const Container = styled.div`
  margin-bottom: 20px;
`;

export const Title = styled.h2`
  font-size: 1.2rem;
  margin-bottom: 10px;
  color: ${({ theme }) => theme.colors.brandColorFive};
`;

export const AllergenList = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
`;

export const AllergenItem = styled.div`
  display: flex;
  align-items: center;
  gap: 5px;
  background-color: ${({ theme }) => theme.colors.brandColorThree};
  padding: 5px 10px;
  border-radius: 5px;
  color: ${({ theme }) => theme.colors.brandColorFive};
`;

export const AllergenInput = styled.input`
  margin-right: 5px;
`;

export const AddAllergenContainer = styled.div`
  display: flex;
  gap: 10px;
  margin-top: 10px;
`;

export const AddAllergenInput = styled.input`
  padding: 5px;
  border: 1px solid ${({ theme }) => theme.colors.brandColorTwo};
  border-radius: 5px;
  color: ${({ theme }) => theme.colors.brandColorFive};
`;

export const AddAllergenButton = styled.button`
  padding: 5px 10px;
  background-color: ${({ theme }) => theme.colors.brandColorFour};
  border: none;
  margin-top: 10px;
  border-radius: 5px;
  color: ${({ theme }) => theme.colors.white};
  cursor: pointer;
  transition: background-color 0.2s;

  &:hover {
    background-color: ${({ theme }) => theme.colors.brandColorFive};
  }
`;
