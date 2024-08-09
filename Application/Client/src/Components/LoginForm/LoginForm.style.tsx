import styled from "styled-components";
import FormInput from "../FormInput/FormInput";

export const StyledForm = styled.form`
  display: flex;
  flex-direction: column;
  margin: 20px;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
`;

export const ErrorMessage = styled.div`
  color: red;
  font-size: 14px;
  margin-bottom: 20px;
`;

export const SubmitButton = styled.button`
  padding: 10px 20px;
  color: #fff;
  background-color: ${(props) => props.theme.colors.brandColorOne};
  border: none;
  border-radius: 5px;
  cursor: pointer;
  &:hover {
    background-color: ${(props) => props.theme.colors.brandColorFour};
  }
`;

export const InputForm = styled(FormInput)`
  width: 100%;
  border-radius: 15px;
  margin-bottom: 15px;

  input {
    border: none;
    border-radius: 15px;
    font-size: 16px;
  }

  label {
    font-size: 16px;
    margin-bottom: 5px;
    font-weight: 500;
  }

  @media (min-width: 768px) {
    max-width: 50%;
  }

  @media (min-width: 1024px) {
    max-width: 20%;
  }
`;
