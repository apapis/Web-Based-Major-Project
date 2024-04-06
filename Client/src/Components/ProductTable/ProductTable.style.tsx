import styled from 'styled-components';

export const Container = styled.div`
  background-color: ${props => props.theme.colors.white};
  border-radius: 15px;
  padding: 10px 5px;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  border: 1px solid black;
`;

export const TableCell = styled.td`
  padding: 5px 10px;
`

export const Table = styled.table`
  text-align: center;
  th {
    text-align: center;
    padding: 5px 10px;
  }

  tbody {
    tr {
    &:nth-child(odd){
      background-color: ${props => props.theme.colors.brandColorOne};
    }

    &:nth-child(even){
      background-color: ${props => props.theme.colors.brandColorThree};
    }
  }
  }
`
