// TopNavigation.tsx
import React, { useState } from "react";
import { useLocation } from "react-router-dom";
import {
  Nav,
  NavList,
  NavItem,
  NavLink,
  HamburgerButton,
  HamburgerIcon,
} from "./TopNavigation.style";

const TopNavigation: React.FC = () => {
  const [isOpen, setIsOpen] = useState(false);
  const location = useLocation();

  const toggleMenu = () => {
    setIsOpen(!isOpen);
  };

  return (
    <>
      <HamburgerButton onClick={toggleMenu}>
        <HamburgerIcon isOpen={isOpen} />
        <HamburgerIcon isOpen={isOpen} />
        <HamburgerIcon isOpen={isOpen} />
      </HamburgerButton>
      <Nav isOpen={isOpen}>
        <NavList>
          <NavItem>
            <NavLink
              to="/admin"
              className={location.pathname === "/admin" ? "active" : ""}
              onClick={toggleMenu}
            >
              Admin Panel
            </NavLink>
          </NavItem>
          <NavItem>
            <NavLink
              to="/admin/meals/mealsList"
              className={
                location.pathname === "/admin/meals/mealsList" ? "active" : ""
              }
              onClick={toggleMenu}
            >
              Meals List
            </NavLink>
          </NavItem>
          <NavItem>
            <NavLink
              to="/admin/meals/productsList"
              className={
                location.pathname === "/admin/meals/productsList"
                  ? "active"
                  : ""
              }
              onClick={toggleMenu}
            >
              Product List
            </NavLink>
          </NavItem>
          <NavItem>
            <NavLink
              to="/admin/shoppingList"
              className={
                location.pathname === "/admin/shoppingList" ? "active" : ""
              }
              onClick={toggleMenu}
            >
              Shopping List
            </NavLink>
          </NavItem>
          <NavItem>
            <NavLink
              to="/admin/menu"
              className={location.pathname === "/admin/menu" ? "active" : ""}
              onClick={toggleMenu}
            >
              Menu
            </NavLink>
          </NavItem>
          <NavItem>
            <NavLink
              to="/admin/new-user"
              className={
                location.pathname === "/admin/new-user" ? "active" : ""
              }
              onClick={toggleMenu}
            >
              Create new account
            </NavLink>
          </NavItem>
        </NavList>
      </Nav>
    </>
  );
};

export default TopNavigation;
