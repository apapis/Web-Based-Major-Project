// MenuPage.style.ts
import styled from "styled-components";

export const Box = styled.div`
  display: flex;
  flex-direction: column;
  gap: 20px;

  @media (min-width: 768px) {
    flex-direction: row;
    gap: 40px;
  }
`;

export const Wrapper = styled.div`
  max-width: 1400px;
  margin: auto;
  display: flex;
  flex-direction: column;
`;
