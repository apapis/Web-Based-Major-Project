import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import HomePage from "../Views/HomePage/HomePage";
import AdminPage from "../Views/AdminPage/AdminPage";
import ProductPage from "../Views/ProductPage/ProductPage";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App/>,
        children: [
            {path: '', element: <HomePage/>},
            {path: 'admin', element: <AdminPage/>},
            {path: 'product', element: <ProductPage/>}
        ]
    }
])