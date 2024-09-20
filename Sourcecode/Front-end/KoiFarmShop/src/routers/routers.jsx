import { createBrowserRouter, Outlet } from "react-router-dom";
import LoginPage from "../pages/LoginPage/LoginPage";
import RegisterPage from "../pages/RegisterPage/RegisterPage";
import Header from "../components/Header/Header";
import Footer from "../components/Footer/Footer";
import KoiDetailPage from "../pages/KoiDetailPage/KoiDetailPage";
import HomePage from "../pages/homePage/HomePage";

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
    ],
  },
  {
    path: "/sign-in",
    element: <LoginPage />,
  },
  {
    path: "/sign-up",
    element: <RegisterPage />,
  },
]);
