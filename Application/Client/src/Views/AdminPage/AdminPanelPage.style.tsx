import styled from "styled-components";

export const Heading = styled.h1`
  text-align: center;
  margin-bottom: 60px;
  font-size: ${(props) => props.theme.fontSize.xxlPlus};
`;

export const NavigationBox = styled.div`
  display: flex;
  gap: 40px;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
`;

export const NavigationButton = styled.a`
  max-width: 350px;
  background-color: ${(props) => props.theme.colors.brandColorThree};
  padding: 40px 60px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  border-radius: 15px;
  text-align: center;
  font-size: ${(props) => props.theme.fontSize.xxl};
  border: 3px solid transparent;

  svg {
    color: ${(props) => props.theme.colors.brandColorFour};
    width: 180px;
    height: 180px;
    stroke-width: 1px;
    margin-bottom: 10px;
  }

  // We need to do it this way beacuse of svg
  &:nth-child(2) {
    fill: ${(props) => props.theme.colors.brandColorFour};
  }

  &:hover {
    cursor: pointer;
    border: 3px solid ${(props) => props.theme.colors.brandColorFour};
  }
`;
