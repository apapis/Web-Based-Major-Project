import "./App.css";
import { Outlet } from "react-router-dom";
import { Wrapper } from "./App.style";
import Cookies from "js-cookie";
import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { setCredentials, logout } from "./Redux/AuthenticationSlice";
import TopNavigation from "./Components/TopNavigation/TopNavigation";
import { useAppSelector } from "./Redux/configureStore";

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
      {isAuthenticated ? <TopNavigation /> : null}

      <Wrapper>
        <Outlet />
      </Wrapper>
    </>
  );
}

export default App;
