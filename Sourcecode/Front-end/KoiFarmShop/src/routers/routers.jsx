import { createBrowserRouter, Outlet } from "react-router-dom";
import LoginPage from "../pages/LoginPage/LoginPage";
import RegisterPage from "../pages/RegisterPage/RegisterPage";
import Header from "../components/Header/Header";
import Footer from "../components/Footer/Footer";
import KoiDetailPage from "../pages/KoiDetailPage/KoiDetailPage";
import HomePage from "../pages/homePage/HomePage";
import SearchPage from "../pages/SearchPage/SearchPage";
import ProfilePage from "../pages/Dashboard/CustomerDashboard/ProfilePage/ProfilePage";
import CustomerDashboardLayout from "../pages/DashboardLayout/customer/CustomerDashboardLayout";
import KoiBoughtPage from "../pages/Dashboard/CustomerDashboard/KoiBoughtPage/KoiBoughtPage";
import DepositedKoiPage from "../pages/Dashboard/CustomerDashboard/DepositedKoiPage/DepositedKoiPage";
import ScrollToTop from "../components/ScrollTop/ScrollToTop";
import CompareBar from "../components/CompareBar/CompareBar";

export const router = createBrowserRouter([
  {
    path: "/",
    element: (
      <>
        <ScrollToTop />
        <Header />
        <Outlet />
        <Footer />
      </>
    ),
    children: [
      {
        path: "/",
        element: (
          <>
            <HomePage />
            <CompareBar />
          </>
        ),
      },
      {
        path: "/koi-detail/:id",
        element: <KoiDetailPage />,
      },
      {
        path: "/search",
        element: (
          <>
            <SearchPage />
            <CompareBar />
          </>
        ),
      },
    ],
  },
  {
    path: "/dashboard",
    element: (
      <>
        <ScrollToTop />
        <Header />
        <CustomerDashboardLayout />
      </>
    ),
    children: [
      {
        path: "profile/:username",
        element: <ProfilePage />,
      },
      {
        path: "purchase",
        element: <KoiBoughtPage />,
      },
      {
        path: "deposite",
        element: <DepositedKoiPage />,
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
