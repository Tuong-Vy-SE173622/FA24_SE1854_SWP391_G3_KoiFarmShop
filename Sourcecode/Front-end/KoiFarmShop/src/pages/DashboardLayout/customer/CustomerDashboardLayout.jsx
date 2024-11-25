import React from "react";
// import CustomerSidebar from "../../../components/Sidebar/CuntomerSidebar/CuntomerSidebar.jsx";
import { Outlet, useLocation, useNavigate } from "react-router-dom";
import "./CustomerDashboardLayout.css";
import Footer from "../../../components/Footer/Footer";
import { CgProfile } from "react-icons/cg";
import { PiHandDepositFill } from "react-icons/pi";
import { BiPurchaseTagAlt } from "react-icons/bi";
import { FaHeartCirclePlus } from "react-icons/fa6";

function CustomerDashboardLayout() {
  const navigate = useNavigate();
  const { pathname } = useLocation();

  const menuItems = [
    {
      path: "/dashboard/profile/stella",
      label: "Account",
      icon: <CgProfile />,
    },
    {
      path: "/dashboard/purchase",
      label: "Koi đã mua",
      icon: <BiPurchaseTagAlt />,
    },
    {
      path: "/dashboard/care-request",
      label: "Yêu cầu chăm sóc",
      icon: <FaHeartCirclePlus />,
    },
    {
      path: "/dashboard/consignment-request",
      label: "Yêu cầu ký gửi",
      icon: <FaHeartCirclePlus />,
    },
    // {
    //   path: "/dashboard/deposite",
    //   label: "Chi tiết ký gửi",
    //   icon: <PiHandDepositFill />,
    // },
  ];

  // const currentMenuItem = menuItems.find((item) =>
  //   pathname === "/dashboard/profile/stella" ? item.path === "/dashboard" : item.path === pathname
  // );
  return (
    <div className="page-container  dashboard-layout-container">
      <div className="customer-sidebar-container menu">
        {menuItems.map(({ path, label, icon }) => (
          <div
            key={path}
            className={`menu-item ${pathname === path ? "active" : ""}`}
            onClick={() => navigate(path)}
          >
            <span className="icon">{icon}</span>
            <span className="label">{label}</span>
          </div>
        ))}
      </div>
      <div className="dashboard-layout-main">
        <Outlet />
        <Footer />
      </div>
    </div>
  );
}

export default CustomerDashboardLayout;

// import { Layout } from "antd";
// import { Content } from "antd/es/layout/layout";
// import Sider from "antd/es/layout/Sider";
// import React from "react";
// import { Outlet } from "react-router-dom";

// function CustomerDashboardLayout() {
//   const siderStyle = {
//     overflow: "auto",
//     height: "100vh",
//     position: "fixed",
//     insetInlineStart: 0,
//     top: 100,
//     bottom: 0,
//     scrollbarWidth: "thin",
//     scrollbarColor: "unset",
//     color
//   };
//   return (
//     <Layout hasSider>
//       <Sider style={siderStyle}>customer-sidebar-container</Sider>
//       <Layout
//         style={{
//           marginInlineStart: 200,
//         }}
//       >
//         <Content
//           style={{
//             margin: "24px 16px 0",
//             overflow: "initial",
//           }}
//         >
//           <Outlet />
//         </Content>
//       </Layout>
//     </Layout>
//   );
// }

// export default CustomerDashboardLayout;
