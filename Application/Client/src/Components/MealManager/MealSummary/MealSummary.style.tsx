import styled from "styled-components";

export const MealSummaryContainer = styled.div`
  width: 50%;
  margin: auto;
  position: sticky;
  bottom: 20px;
`;

export const Wrapper = styled.div`
  width: 100%;
  display: flex;
  flex-wrap: wrap;
  border-radius: 20px;
  justify-content: space-around;
  padding: 10px;
  color: white;
  background-color: ${(props) => props.theme.colors.brandColorFour};

  @media (min-width: 768px) {
    justify-content: center;
    gap: 20px;
  }
`;

export const SummaryItem = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 10px;

  span:first-child {
    font-weight: bold;
    margin-bottom: 5px;
  }

  @media (min-width: 768px) {
    flex-direction: row;
    gap: 10px;
    margin-bottom: 0;
  }
`;
