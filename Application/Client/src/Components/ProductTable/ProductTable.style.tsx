import styled from "styled-components";
import Modal from "../Modal/Modal";

export const Container = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
`;

export const TableCell = styled.td`
  padding: 10px;
`;

export const Table = styled.table`
  width: 100%;
  border-collapse: collapse;
  border-radius: 10px;
  overflow: hidden;
  background-color: ${(props) => props.theme.colors.white};
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);

  @media (max-width: ${(props) => props.theme.size.tablet}) {
    font-size: ${(props) => props.theme.fontSize.m};
  }

  @media (max-width: ${(props) => props.theme.size.mobileL}) {
    font-size: ${(props) => props.theme.fontSize.s};
  }

  th,
  td {
    padding: 10px;
    text-align: left;
  }

  th {
    background-color: ${(props) => props.theme.colors.brandColorFour};
    color: ${(props) => props.theme.colors.white};
  }

  tbody {
    tr {
      transition: all 0.2s;

      &:nth-child(odd) {
        background-color: ${(props) => props.theme.colors.brandColorOne};
      }
      &:nth-child(even) {
        background-color: ${(props) => props.theme.colors.brandColorThree};
      }

      &:hover {
        transform: scale(1.01);
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
      }
    }
  }
`;

export const Button = styled.button`
  padding: 10px 15px;
  background-color: ${(props) => props.theme.colors.brandColorOne};
  color: white;
  border: none;
  cursor: pointer;
  margin-top: 20px;
  border-radius: 15px;
  transition: all 0.2s;

  &:hover {
    background-color: ${(props) => props.theme.colors.brandColorFour};
    transform: translateY(-2px);
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  }

  &:active {
    transform: translateY(0);
    box-shadow: none;
  }
`;

export const ModalWindow = styled(Modal)``;
