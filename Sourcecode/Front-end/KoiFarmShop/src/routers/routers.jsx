import { createBrowserRouter, Outlet } from "react-router-dom";
import LoginPage from "../pages/LoginPage/LoginPage";
import RegisterPage from "../pages/RegisterPage/RegisterPage";
import Header from "../components/Header/Header";
import Footer from "../components/Footer/Footer";
import KoiDetailPage from "../pages/KoiDetailPage/KoiDetailPage";
import HomePage from "../pages/homePage/HomePage";
import SearchPage from "../pages/SearchPage/SearchPage";
// import ProfilePage from "../pages/Dashboard/CustomerDashboard/ProfilePage/ProfilePage";
import CustomerDashboardLayout from "../pages/DashboardLayout/customer/CustomerDashboardLayout";
import KoiBoughtPage from "../pages/Dashboard/CustomerDashboard/KoiBoughtPage/KoiBoughtPage";
// import DepositedKoiPage from "../pages/Dashboard/CustomerDashboard/DepositedKoiPage/DepositedKoiPage";
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
import CareRequest from "../pages/Dashboard/CustomerDashboard/CareRequestForm/CareRequestForm";
import ConsignmentRequestForm from "../pages/Dashboard/CustomerDashboard/ConsignmentRequestForm/ConsignmentRequestForm";
import ConsignmentDetail from "../pages/Dashboard/CustomerDashboard/ConsignmentDetail/ConsignmentDetail";
import CareRequestForm from "../pages/Dashboard/CustomerDashboard/CareRequestForm/CareRequestForm";
import CareRequestDetailForm from "../pages/Dashboard/CustomerDashboard/CareRequestDetailForm/CareRequestDetailForm";
// import { CartProvider } from "../contexts/CartContext";
import CartPage from "../pages/CartPage/CartPage";
import { CartProvider } from "../contexts/CartContext";

export const router = createBrowserRouter([
  {
    path: "/",
    element: (
      <>
        <CartProvider>
          <ScrollToTop />
          <Header />
          <Outlet />
          <Footer />
        </CartProvider>
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
        path: "/koi-detail/:KoiID",
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
      {
        path: "/compare",
        element: <ComparePage />,
      },
      {
        path: "/cart",
        element: <CartPage />,
      },
    ],
  },
  {
    path: "/dashboard",
    element: (
      <>
        <CartProvider>
          <ScrollToTop />
          <Header />
          <CustomerDashboardLayout />
        </CartProvider>
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
        path: "care-request",
        element: <CareRequestForm />,
      },
      {
        path: "care-request-detail/:careRequestID",
        element: <CareRequestDetailForm />,
      },
      {
        path: "consignment-request",
        element: <ConsignmentRequestForm />,
      },
      {
        path: "consignment-detail/:consignmentId",
        element: <ConsignmentDetail />,
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
