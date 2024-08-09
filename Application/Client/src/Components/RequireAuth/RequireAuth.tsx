import { Navigate, useLocation } from "react-router-dom";
import { useAppSelector } from "../../Redux/configureStore";

const RequireAuth = ({ children }) => {
  const isAuthenticated = useAppSelector((state) => state.auth.isAuthenticated);
  const location = useLocation();

  if (!isAuthenticated && location.pathname !== "/admin") {
    // Only redirect if we are not already on the login page
    return <Navigate to="/admin" state={{ from: location }} replace />;
  }

  return children;
};

export default RequireAuth;
