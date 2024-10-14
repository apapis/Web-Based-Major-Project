import "./App.css";
import { CssVarsProvider } from "@mui/joy/styles";
import CssBaseline from "@mui/joy/CssBaseline";
import { Outlet } from "react-router-dom";
import { Wrapper } from "./App.style";
import Cookies from "js-cookie";
import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { setCredentials, logout } from "./Redux/AuthenticationSlice";
import TopNavigation from "./Components/TopNavigation/TopNavigation";
import { useAppSelector } from "./Redux/configureStore";
import Sidebar from "./Components/Sidebar";
import Box from "@mui/joy/Box";
import Header from "./Components/Header";

function App() {
  const dispatch = useDispatch();
  const isAuthenticated = useAppSelector((state) => state.auth.isAuthenticated);

  useEffect(() => {
    const token = Cookies.get("jwt");
    if (token) {
      dispatch(setCredentials({ token }));
    } else {
      dispatch(logout());
    }
  }, [dispatch]);
  return (
    <>
      <CssVarsProvider disableTransitionOnChange>
        <Box sx={{ display: "flex", minHeight: "100dvh" }}>
          <CssBaseline />
          <Header />
          <Sidebar />
          <Box
            component="main"
            className="MainContent"
            sx={{
              px: { xs: 2, md: 6 },
              pt: {
                xs: "calc(12px + var(--Header-height))",
                sm: "calc(12px + var(--Header-height))",
                md: 3,
              },
              pb: { xs: 2, sm: 2, md: 3 },
              flex: 1,
              display: "flex",
              flexDirection: "column",
              minWidth: 0,
              height: "100dvh",
              gap: 1,
            }}
          >
            <Outlet />
          </Box>
        </Box>
      </CssVarsProvider>
    </>
  );
}

export default App;
