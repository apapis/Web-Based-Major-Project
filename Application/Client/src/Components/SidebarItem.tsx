import {
  ListItem,
  ListItemButton,
  ListItemContent,
  Typography,
} from "@mui/joy";
import { Link, useLocation } from "react-router-dom";

export default function SidebarItem({ to, icon, text }) {
  const location = useLocation();

  return (
    <ListItem component={Link} to={to} sx={{ textDecoration: "none" }}>
      <ListItemButton selected={location.pathname === to}>
        {icon}
        <ListItemContent>
          <Typography level="title-sm">{text}</Typography>
        </ListItemContent>
      </ListItemButton>
    </ListItem>
  );
}
