import styled, { createGlobalStyle, css } from "styled-components";

export const ModalWindow = styled.div`
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background-color: ${(props) => props.theme.colors.white};
  padding: 30px;
  z-index: 9999;
  border-radius: 10px;
  max-height: 80vh;
  overflow-y: auto;
  width: 90%;
  max-width: 600px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);

  @media (max-width: ${(props) => props.theme.size.mobileL}) {
    width: 100%;
    height: 100%;
    max-height: none;
    border-radius: 0;
    padding: 10px;
  }
`;

export const Overlay = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 1050;
  background-color: rgba(0, 0, 0, 0.7);
  backdrop-filter: blur(5px);
`;

export const CloseButton = styled.button`
  position: absolute;
  top: 10px;
  right: 10px;
  padding: 5px;
  background-color: transparent;
  border: none;
  font-size: 24px;
  color: ${(props) => props.theme.colors.brandColorFour};
  cursor: pointer;
  transition: color 0.2s;

  &:hover {
    color: ${(props) => props.theme.colors.brandColorOne};
  }
`;

interface GlobalStyleProps {
  hidden: boolean;
}

export const GlobalStyle = createGlobalStyle<GlobalStyleProps>`
  body {
    ${({ hidden }) =>
      hidden &&
      css`
        overflow: hidden;
      `}
  }
`;
