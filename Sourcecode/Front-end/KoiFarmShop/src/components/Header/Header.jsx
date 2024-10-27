import React, { useEffect, useState } from "react";
import "./Header.css";
import { TiShoppingCart } from "react-icons/ti";
import { IoSearch } from "react-icons/io5";
import { Button } from "antd";
import { useNavigate } from "react-router-dom";
import { logout } from "../../services/authService";
import { getAllKoiType } from "../../services/KoiTypeService";

function Header() {
  const [isInputVisible, setIsInputVisible] = useState(false);
  const [hoveredItem, setHoveredItem] = useState(null);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [koiTypeLs, setKoiTypeLs] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const accessToken = localStorage.getItem("accessToken");
    if (accessToken) {
      setIsLoggedIn(true);
    }
  }, []);

  const navigateLink = (link) => navigate(link);
  const handleMouseEnter = (item) => setHoveredItem(item);
  const handleMouseLeave = () => setHoveredItem(null);
  const navigateLogin = () => navigate("/login");
  const navigateRegister = () => navigate("/register");

  const IntroductionList = [
    { key: 1, label: "Giới thiệu", link: "" },
    { key: 2, label: "Giới thiệu sàn ký gửi", link: "" },
    { key: 3, label: "Đơn vị bán Koi", link: "" },
    { key: 4, label: "Nguồn Koi", link: "" },
  ];

  // const KoiList = [
  //   { key: 1, label: "Koi Kohaku", link: "" },
  //   { key: 2, label: "Koi Ogon", link: "" },
  //   { key: 3, label: "Koi Showa", link: "" },
  //   { key: 4, label: "Koi Tancho", link: "" },
  //   { key: 5, label: "Koi Bekko", link: "" },
  // ];

  const taskList = [
    { key: 1, label: "Profile", link: "/dashboard/profile/stella" },
    { key: 2, label: "Logout", link: "" },
  ];

  const handleLogout = async () => {
    try {
      await logout(); // Gọi hàm logout
      setIsLoggedIn(false); // Cập nhật trạng thái đăng nhập
      navigate("/login"); // Chuyển hướng đến trang login
    } catch (err) {
      console.error("Logout failed: ", err);
    }
  };

  useEffect(() => {
    const fetchKoiType = async () => {
      try {
        const data = await getAllKoiType();
        setKoiTypeLs(data);
        // console.log("KoiType", koiTypeLs);
      } catch (err) {
        console.error("Failed to fetch Koi types", err);
      }
    };

    fetchKoiType();
  }, []);

  return (
    <header>
      <div className="header-container">
        <div className="header-logo">
          <img
            src="/logo-web/logo.png"
            alt="logo"
            onClick={() => navigate("/")}
            style={{ cursor: "pointer" }}
          />
        </div>
        <div className="middle-section">
          <div className="header-items">
            <div
              className="header-item"
              onMouseEnter={() => handleMouseEnter("introduction")}
              onMouseLeave={handleMouseLeave}
            >
              Giới thiệu
              {hoveredItem === "introduction" && (
                <ul className="dropdown">
                  {IntroductionList.map((item) => (
                    <li className="dropdown-item" key={item.key}>
                      {item.label}
                    </li>
                  ))}
                </ul>
              )}
            </div>
            <div
              className="header-item"
              onMouseEnter={() => handleMouseEnter("koi")}
              onMouseLeave={handleMouseLeave}
            >
              Cá Koi Nhật
              {hoveredItem === "koi" && (
                <ul className="dropdown">
                  {koiTypeLs.map((item) => (
                    <li className="dropdown-item" key={item.koiTypeId}>
                      {item.name}
                    </li>
                  ))}
                </ul>
              )}
            </div>
            <div className="header-item">Khuyến mãi</div>
          </div>
          <div className="header-search">
            <div className={`search ${isInputVisible ? "show-input" : ""}`}>
              <input type="text" placeholder="Tìm kiếm" />
              <span className="search-icon">
                <IoSearch size={"18px"} onClick={() => navigate("/search")} />
              </span>
            </div>
          </div>
        </div>
        <div className="header-account">
          <div className="cart-icon">
            <TiShoppingCart size={"30px"} />
          </div>

          {isLoggedIn ? (
            <div
              className="header-item header-avatar"
              onMouseEnter={() => handleMouseEnter("user")}
              onMouseLeave={handleMouseLeave}
            >
              <img
                src="https://i.pinimg.com/736x/d6/46/02/d64602a7b954a8b2f09bac97a7911bf8.jpg"
                alt="avatar"
              />
              {hoveredItem === "user" && (
                <ul className="dropdown">
                  {taskList.map((item) => (
                    <li
                      key={item.key}
                      className="dropdown-item"
                      onClick={
                        item.label === "Logout"
                          ? handleLogout
                          : () => navigateLink(item.link)
                      }
                    >
                      {item.label}
                    </li>
                  ))}
                </ul>
              )}
            </div>
          ) : (
            <div
              className="header-item"
              style={{ display: "flex", alignItems: "center" }}
            >
              <Button
                type="primary"
                onClick={navigateLogin}
                style={{ marginRight: "10px" }}
              >
                Login
              </Button>
              <Button
                type="primary"
                onClick={navigateRegister}
                style={{ marginRight: "10px" }}
              >
                Register
              </Button>
            </div>
          )}
        </div>
      </div>
    </header>
  );
}

export default Header;
