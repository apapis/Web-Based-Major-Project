// TopNavigation.style.ts
import styled from "styled-components";
import { Link } from "react-router-dom";

interface NavProps {
  isOpen: boolean;
}

export const Nav = styled.nav<NavProps>`
  background-color: ${(props) => props.theme.colors.brandColorOne};
  padding: 20px;
  position: sticky;
  top: 0;
  z-index: 999;

  @media (max-width: 768px) {
    position: fixed;
    top: 0;
    left: 0;
    height: 100%;
    width: 250px;
    background-color: ${(props) => props.theme.colors.brandColorTwo};
    transform: ${({ isOpen }) =>
      isOpen ? "translateX(0)" : "translateX(-100%)"};
    transition: transform 0.3s ease-in-out;
    z-index: 999;
  }
`;

export const NavList = styled.ul`
  display: flex;
  list-style: none;
  margin: 0;
  padding: 0;

  @media (max-width: 768px) {
    flex-direction: column;
  }
`;

export const NavItem = styled.li`
  margin-right: 20px;

  &:last-child {
    margin-right: 0;
  }

  @media (max-width: 768px) {
    margin-right: 0;
    margin-bottom: 20px;
  }
`;

export const NavLink = styled(Link)`
  color: ${(props) => props.theme.colors.brandColorFour};
  text-decoration: none;
  font-size: 18px;
  font-weight: bold;

  &:hover {
    color: ${(props) => props.theme.colors.brandColorFive};
  }

  &.active {
    color: ${(props) => props.theme.colors.brandColorFive};
  }
`;

export const HamburgerButton = styled.button`
  display: none;
  background: none;
  border: none;
  cursor: pointer;
  padding: 0;

  @media (max-width: 768px) {
    display: block;
    position: fixed;
    top: 20px;
    right: 20px;
    z-index: 1000;
  }
`;

interface HamburgerIconProps {
  isOpen: boolean;
}

export const HamburgerIcon = styled.div<HamburgerIconProps>`
  width: 30px;
  height: 3px;
  background-color: ${(props) => props.theme.colors.brandColorFour};
  margin: 5px 0;
  transition: transform 0.3s ease-in-out;

  &:nth-child(1) {
    transform: ${({ isOpen }) =>
      isOpen ? "rotate(45deg) translate(5px, 6px)" : "none"};
  }

  &:nth-child(2) {
    opacity: ${({ isOpen }) => (isOpen ? "0" : "1")};
  }

  &:nth-child(3) {
    transform: ${({ isOpen }) =>
      isOpen ? "rotate(-45deg) translate(5px, -6px)" : "none"};
  }
`;
