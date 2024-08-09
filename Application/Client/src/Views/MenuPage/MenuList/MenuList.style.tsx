import styled from "styled-components";

export const OneMenu = styled.div`
  display: flex;
  flex-direction: column;
  border: 2px solid ${(props) => props.theme.colors.brandColorFour};
  background-color: ${(props) => props.theme.colors.brandColorThree};
  padding: 20px;
  border-radius: 5px;
  overflow: hidden;
  max-height: 500px;
  overflow-y: auto;
  width: 100%;
  box-sizing: border-box;

  @media (min-width: 768px) {
    max-height: 650px;
    width: 450px;
  }

  &::-webkit-scrollbar {
    width: 8px;
  }

  &::-webkit-scrollbar-track {
    background-color: ${(props) => props.theme.colors.brandColorThree};
    border-radius: 4px;
  }

  &::-webkit-scrollbar-thumb {
    background-color: ${(props) => props.theme.colors.brandColorFour};
    border-radius: 4px;
  }

  &::-webkit-scrollbar-thumb:hover {
    background-color: ${(props) => props.theme.colors.brandColorFive};
  }

  scrollbar-width: thin;
`;

export const AddMealButton = styled.button`
  background-color: ${(props) => props.theme.colors.brandColorFour};
  color: #fff;
  border: none;
  border-radius: 4px;
  padding: 8px 16px;
  font-size: 14px;
  cursor: pointer;
  transition: background-color 0.3s;
  margin-top: 16px;

  &:hover {
    background-color: ${(props) => props.theme.colors.brandColorFive};
  }
`;

export const CheckboxLabel = styled.label`
  display: flex;
  align-items: center;
  margin-bottom: 16px;
  font-size: 16px;
  color: ${(props) => props.theme.colors.brandColorFive};

  input[type="checkbox"] {
    margin-right: 8px;
    width: 16px;
    height: 16px;
    cursor: pointer;
  }
`;
