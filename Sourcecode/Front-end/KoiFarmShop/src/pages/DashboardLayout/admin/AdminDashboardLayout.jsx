import React, { useEffect } from "react";
import "./AdminDashboardLayout.css";
import { Outlet, useLocation, useNavigate } from "react-router-dom";
import {
  RiCalendarEventFill,
  RiDashboardFill,
  RiDiscountPercentLine,
} from "react-icons/ri";
import { CgMenuGridO, CgProfile } from "react-icons/cg";
import { LuFish, LuUsers2 } from "react-icons/lu";
import { GiCirclingFish } from "react-icons/gi";

function AdminDashboardLayout() {
  const navigate = useNavigate();
  const { pathname } = useLocation();
  const account = JSON.parse(localStorage.getItem("account"));

  const menuItems = [
    { path: "/admin/dashboard", label: "Dashboard", icon: <RiDashboardFill /> },
    {
      path: `/admin/profile/${account.username}`,
      label: "Profile",
      icon: <CgProfile />,
    },
    { path: "/admin/koi-types", label: "Koi Type", icon: <GiCirclingFish /> },
    { path: "/admin/kois", label: "Koi", icon: <LuFish /> },
    {
      path: "/admin/promotions",
      label: "Promotion",
      icon: <RiDiscountPercentLine />,
    },
    { path: "/admin/users", label: "Account", icon: <LuUsers2 /> },
  ];

  const currentMenuItem = menuItems.find((item) => item.path === pathname);
  const handleHome = () => navigate("/");

  useEffect(() => {
    const isAuthenticated = localStorage.getItem("accessToken");
    if (!isAuthenticated) {
      navigate("/login");
    }
  }, [navigate]);

  return (
    <div className="adl-layout-container-l1">
      <div className="adl-sidebar-wrapper">
        <img
          src="/logo-web/logo3.png"
          alt="logo"
          style={{ cursor: "pointer" }}
          onClick={handleHome}
        />
        <div className="adl-menu">
          {menuItems.map(({ path, label, icon }) => (
            <div
              key={path}
              className={`adl-menu-item ${
                pathname === path || (path === "/dashboard" && pathname === "/")
                  ? "adl-active"
                  : ""
              }`}
              onClick={() => navigate(path)}
            >
              <span className="adl-icon">{icon}</span>
              <span className="adl-label">{label}</span>
            </div>
          ))}
        </div>
      </div>
      <div className="adl-main-wrapper">
        <div className="adl-header-container">
          <div className="adl-header-title">
            {currentMenuItem?.label || "Page Not Found"}
          </div>
          <div className="adl-header-account">
            <div className="adl-username-account">
              {account.firstName} {account.lastName}
            </div>
          </div>
        </div>
        <div className="adl-main-container">
          <Outlet />
        </div>
      </div>
    </div>
  );
}

export default AdminDashboardLayout;
