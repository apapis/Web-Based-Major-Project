import styled from "styled-components";

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

export const SuccessMessage = styled.div`
  color: green;
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
