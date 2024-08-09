import styled from "styled-components";

export const PageContainer = styled.div`
  padding: 20px;
`;

export const Title = styled.h1`
  color: black;
`;

export const Input = styled.input`
  padding: 10px;
  margin-right: 5px;
  max-width: 250px;
`;

export const Button = styled.button`
  padding: 10px 15px;
  background-color: ${(props) => props.theme.colors.brandColorOne};
  color: white;
  border: none;
  cursor: pointer;

  &:hover {
    background-color: ${(props) => props.theme.colors.brandColorFour};
  }
`;

export const List = styled.ul`
  list-style: none;
  padding: 0;
`;

export const ListItem = styled.li`
  padding: 10px;
  margin-top: 5px;
  background-color: ${(props) => props.theme.colors.brandColorThree};
  display: flex;
  align-items: center;

  & input[type="checkbox"] {
    margin-right: 10px;
  }

  & span {
    flex-grow: 1;
  }
`;
