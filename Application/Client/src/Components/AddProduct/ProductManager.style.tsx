// ProductManager.style.ts
import styled from "styled-components";
import FormInput from "../FormInput/FormInput";
import { FormValues } from "../../Models/product";

export const Container = styled.div`
  background-color: ${({ theme }) => theme.colors.white};
  padding: 10px;
  width: 90%;
  display: flex;
  flex-direction: column;
  gap: 20px;
`;

export const Form = styled.form`
  width: 100%;
  display: flex;
  flex-direction: column;
  gap: 20px;
`;

export const FormInputProduct = styled(FormInput<FormValues>)`
  display: flex;
  flex-direction: column;
  width: 100%;

  label {
    font-weight: bold;
    margin-bottom: 5px;
    color: ${({ theme }) => theme.colors.brandColorFive};
  }

  input,
  textarea {
    padding: 10px;
    border: 2px solid ${({ theme }) => theme.colors.brandColorTwo};
    border-radius: 5px;
    font-size: 16px;
    color: ${({ theme }) => theme.colors.brandColorFive};
  }

  input[type="number"] {
    width: 100px;
  }

  p {
    color: ${({ theme }) => theme.colors.error};
    font-size: 14px;
    margin-top: 5px;
  }
`;

export const SubmitButton = styled.button`
  padding: 10px 20px;
  background-color: ${({ theme }) => theme.colors.brandColorFour};
  color: ${({ theme }) => theme.colors.white};
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
  font-size: 16px;
  align-self: flex-end;

  &:hover {
    background-color: ${({ theme }) => theme.colors.brandColorFive};
  }

  &:focus {
    outline: none;
    box-shadow: 0 0 0 3px ${({ theme }) => theme.colors.brandColorTwo};
  }
`;
