import { createBrowserRouter, Outlet } from "react-router-dom";
import LoginPage from "../pages/LoginPage/LoginPage";
import RegisterPage from "../pages/RegisterPage/RegisterPage";
import Header from "../components/Header/Header";
import Footer from "../components/Footer/Footer";
import KoiDetailPage from "../pages/KoiDetailPage/KoiDetailPage";
import HomePage from "../pages/homePage/HomePage";
import SearchPage from "../pages/SearchPage/SearchPage";

export const router = createBrowserRouter([
  {
    path: "/",
    element: (
      <>
        <Header />
        <Outlet />
        <Footer />
      </>
    ),
    children: [
      {
        path: "/",
        element: <HomePage />,
      },
      {
        path: "/koi-detail/:id",
        element: <KoiDetailPage />,
      },
      {
        path: "/search",
        element: <SearchPage />,
      },
    ],
  },
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "/register",
    element: <RegisterPage />,
  },
]);
