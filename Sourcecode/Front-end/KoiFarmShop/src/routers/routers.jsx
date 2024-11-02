import { createBrowserRouter, Outlet } from "react-router-dom";
import LoginPage from "../pages/LoginPage/LoginPage";
import RegisterPage from "../pages/RegisterPage/RegisterPage";
import Header from "../components/Header/Header";
import Footer from "../components/Footer/Footer";
import HomePage from "../pages/homePage/HomePage";
import SearchPage from "../pages/SearchPage/SearchPage";
// import ProfilePage from "../pages/Dashboard/CustomerDashboard/ProfilePage/ProfilePage";
import CustomerDashboardLayout from "../pages/DashboardLayout/customer/CustomerDashboardLayout";
import KoiBoughtPage from "../pages/Dashboard/CustomerDashboard/KoiBoughtPage/KoiBoughtPage";
import DepositedKoiPage from "../pages/Dashboard/CustomerDashboard/DepositedKoiPage/DepositedKoiPage";
import ScrollToTop from "../components/ScrollTop/ScrollToTop";
import CompareBar from "../components/CompareBar/CompareBar";
import ComparePage from "../pages/ComparePage/ComparePage";
import AdminDashboardLayout from "../pages/DashboardLayout/admin/AdminDashboardLayout";
import AdminDashboardPage from "../pages/Dashboard/AdminDashboard/AdminDashboardPage/AdminDashboardPage";
import KoiPage from "../pages/Dashboard/AdminDashboard/KoiPage.jsx/KoiPage";
import KoiTypePage from "../pages/Dashboard/AdminDashboard/KoiTypePage/KoiTypePage";
import PromotionPage from "../pages/Dashboard/AdminDashboard/PromotionPage/PromotionPage";
import AccountPage from "../pages/Dashboard/AdminDashboard/AccountPage/AccountPage";
import ProfilePage from "../pages/Dashboard/ProfilePage/ProfilePage";
import CartPage from "../pages/cartPage/CartPage";
import KoiDetail from "../pages/KoiDetailPage/KoiDetail";
import ConsignmentRequestForm from "../components/Consignment/ConsignmentRequestForm";
import ConsignmentDetail from "../pages/ConsignmentDetailPage/ConsignmentDetailPage";

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
        path: "/koi/:koiId",
        element: <KoiDetail />,
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
      {
        path: "/compare",
        element: <ComparePage />,
      },
      {
        path: "/cart-Page",
        element: <CartPage />,
      },
      {
        path: "/consignment-request",
        element: <ConsignmentRequestForm />, // Trang gửi yêu cầu consignments
      },
      {
        path: "/consignment-detail/:consignmentId",
        element: <ConsignmentDetail />, // Trang chi tiết consignments
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
    path: "/admin",
    element: <AdminDashboardLayout />,
    children: [
      {
        path: "dashboard",
        element: <AdminDashboardPage />,
      },
      {
        path: "profile/:username",
        element: <ProfilePage />,
      },
      {
        path: "koi-types",
        element: <KoiTypePage />,
      },
      {
        path: "kois",
        element: <KoiPage />,
      },
      {
        path: "promotions",
        element: <PromotionPage />,
      },
      {
        path: "accounts",
        element: <AccountPage />,
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
