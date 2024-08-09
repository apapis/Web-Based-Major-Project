// MealManager.style.ts
import styled from "styled-components";
import MealInputForm from "../FormInput/MealInputForm";
import MealImageInput from "../FormInput/MealImageInput";

export const Wrapper = styled.div`
  max-width: 1200px;
  background-color: ${({ theme }) => theme.colors.brandColorThree};
  padding: 20px;
  border-radius: 15px;
  margin: 50px auto;

  @media (min-width: 768px) {
    padding: 30px;
  }

  @media (min-width: 1024px) {
    padding: 40px;
  }
`;

export const Form = styled.form`
  display: flex;
  flex-direction: column;
`;

export const Line = styled.hr`
  width: 100%;
  border: none;
  border-top: 1px solid ${({ theme }) => theme.colors.brandColorFive};
  margin: 20px 0;
`;

export const Heading = styled.h1`
  text-align: center;
  font-size: 28px;
  margin-bottom: 30px;
  color: ${({ theme }) => theme.colors.brandColorFive};

  @media (min-width: 768px) {
    font-size: 32px;
  }
`;

export const SectionHeading = styled.h2`
  font-size: 24px;
  margin-bottom: 10px;
  color: ${({ theme }) => theme.colors.brandColorFour};

  @media (min-width: 768px) {
    font-size: 28px;
  }
`;

export const InputForm = styled(MealInputForm)`
  display: flex;
  flex-direction: column;
  width: 100%;
  margin-bottom: 20px;

  input,
  textarea {
    border: 1px solid ${({ theme }) => theme.colors.brandColorTwo};
    border-radius: 8px;
    font-size: 16px;
    padding: 10px;
    background-color: ${({ theme }) => theme.colors.white};
  }

  textarea {
    resize: vertical;
    min-height: 100px;
  }

  label {
    font-size: 18px;
    margin-bottom: 8px;
    font-weight: 500;
    color: ${({ theme }) => theme.colors.brandColorFive};
  }

  @media (min-width: 768px) {
    max-width: 50%;
  }

  @media (min-width: 1024px) {
    max-width: 30%;
  }
`;

export const ImageInput = styled(MealImageInput)`
  input[type="file"] {
    display: none;
  }

  label {
    display: none;
    font-size: 18px;
    margin-bottom: 8px;
    font-weight: 500;
    color: ${({ theme }) => theme.colors.brandColorFive};
    cursor: pointer;
    display: inline-block;
    padding: 12px 24px;
    background-color: ${({ theme }) => theme.colors.brandColorFour};
    color: ${({ theme }) => theme.colors.white};
    border-radius: 8px;
    transition: background-color 0.3s ease;

    &:hover {
      background-color: ${({ theme }) => theme.colors.brandColorFive};
    }
  }

  p {
    color: ${({ theme }) => theme.colors.brandColorFive};
    font-size: 16px;
    margin-top: 8px;
  }

  @media (min-width: 768px) {
    max-width: 50%;
  }

  @media (min-width: 1024px) {
    max-width: 30%;
  }
`;

export const SaveBtn = styled.button`
  background-color: ${({ theme }) => theme.colors.brandColorFour};
  color: ${({ theme }) => theme.colors.white};
  border: none;
  border-radius: 8px;
  padding: 12px 24px;
  font-size: 18px;
  cursor: pointer;
  align-self: flex-end;
  transition: background-color 0.3s ease;

  &:hover {
    background-color: ${({ theme }) => theme.colors.brandColorFive};
  }

  @media (min-width: 768px) {
    font-size: 20px;
    padding: 14px 28px;
  }
`;
