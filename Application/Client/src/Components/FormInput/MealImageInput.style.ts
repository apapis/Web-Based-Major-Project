// MealImageInput.style.ts
import styled from "styled-components";

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  margin-bottom: 20px;
`;

export const HiddenInput = styled.input`
  display: none;
`;

export const Label = styled.label`
  display: inline-block;
  font-size: 18px;
  margin-bottom: 8px;
  font-weight: 500;
  color: ${({ theme }) => theme.colors.white};
  cursor: pointer;
  padding: 12px 24px;
  background-color: ${({ theme }) => theme.colors.brandColorFour};
  border-radius: 8px;
  transition: background-color 0.3s ease;

  &:hover {
    background-color: ${({ theme }) => theme.colors.brandColorFive};
  }
`;

export const ErrorMessage = styled.p`
  color: ${({ theme }) => theme.colors.brandColorFive};
  font-size: 16px;
  margin-top: 8px;
`;

export const ImagePreview = styled.img`
  max-width: 100px;
  max-height: 100px;
  margin-top: 10px;
  border-radius: 8px;
  object-fit: cover;
`;
