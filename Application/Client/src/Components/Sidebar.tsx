import {
  Box,
  Sheet,
  GlobalStyles,
  Typography,
  listItemButtonClasses,
  List,
  Divider,
} from "@mui/joy";
import ColorSchemeToggle from "./ColorSchemeToggle";
import { closeSidebar } from "./utils";
import HomeRoundedIcon from "@mui/icons-material/HomeRounded";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import LocalDiningIcon from "@mui/icons-material/LocalDining";
import StoreIcon from "@mui/icons-material/Store";
import MenuBookIcon from "@mui/icons-material/MenuBook";
import SidebarItem from "./SidebarItem";

export default function Sidebar() {
  return (
    <Sheet
      className="Sidebar"
      sx={{
        position: { xs: "fixed", md: "sticky" },
        transform: {
          xs: "translateX(calc(100% * (var(--SideNavigation-slideIn, 0) - 1)))",
          md: "none",
        },
        transition: "transform 0.4s, width 0.4s",
        zIndex: 10000,
        height: "100dvh",
        width: "var(--Sidebar-width)",
        top: 0,
        p: 2,
        flexShrink: 0,
        display: "flex",
        flexDirection: "column",
        gap: 2,
        borderRight: "1px solid",
        borderColor: "divider",
      }}
    >
      <GlobalStyles
        styles={(theme) => ({
          ":root": {
            "--Sidebar-width": "220px",
            [theme.breakpoints.up("lg")]: {
              "--Sidebar-width": "240px",
            },
          },
        })}
      />
      <Box
        className="Sidebar-overlay"
        sx={{
          position: "fixed",
          zIndex: 9998,
          top: 0,
          left: 0,
          width: "100vw",
          height: "100vh",
          opacity: "var(--SideNavigation-slideIn)",
          backgroundColor: "var(--joy-palette-background-backdrop)",
          transition: "opacity 0.4s",
          transform: {
            xs: "translateX(calc(100% * (var(--SideNavigation-slideIn, 0) - 1) + var(--SideNavigation-slideIn, 0) * var(--Sidebar-width, 0px)))",
            lg: "translateX(-100%)",
          },
        }}
        onClick={() => closeSidebar()}
      />
      <Box sx={{ display: "flex", gap: 1, alignItems: "center" }}>
        <Typography level="title-lg">DragonFly Bistro</Typography>
        <ColorSchemeToggle sx={{ ml: "auto" }} />
      </Box>

      <Box
        sx={{
          minHeight: 0,
          overflow: "hidden auto",
          flexGrow: 1,
          display: "flex",
          flexDirection: "column",
          [`& .${listItemButtonClasses.root}`]: {
            gap: 1.5,
          },
        }}
      >
        <List
          size="sm"
          sx={{
            gap: 1,
            "--List-nestedInsetStart": "30px",
            "--ListItem-radius": (theme) => theme.vars.radius.sm,
          }}
        >
          <SidebarItem to="/admin" icon={<HomeRoundedIcon />} text="Home" />
          <SidebarItem
            to="/admin/meals/productsList"
            icon={<StoreIcon />}
            text="Products"
          />
          <SidebarItem
            to="/admin/meals/mealsList"
            icon={<LocalDiningIcon />}
            text="Meals"
          />
          <SidebarItem to="/admin/menu" icon={<MenuBookIcon />} text="Menu" />
          <SidebarItem
            to="/admin/shoppingList"
            icon={<ShoppingCartIcon />}
            text="Shopping List"
          />
          <SidebarItem
            to="/admin/new-user"
            icon={<HomeRoundedIcon />}
            text="Add new user"
          />
        </List>
      </Box>
      <Divider />
    </Sheet>
  );
}
