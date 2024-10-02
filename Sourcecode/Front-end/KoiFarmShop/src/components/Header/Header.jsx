import React, { useEffect, useState } from "react";
import "./Header.css";
import { FiSearch } from "react-icons/fi";
import { TiShoppingCart } from "react-icons/ti";
import { IoSearch } from "react-icons/io5";
import { GiHamburgerMenu } from "react-icons/gi";
// import { Menu } from "antd";
import { useNavigate } from "react-router-dom";

function Header() {
  const [isInputVisible, setIsInputVisible] = useState(false);
  const [hoveredItem, setHoveredItem] = useState(null);
  const [isSideBarOpen, setIsSideBarOpen] = useState(false);
  const [isSticky, setIsSticky] = useState(false);
  const [openKeys, setOpenKeys] = useState(["introduction", "koi", "food"]);
  const navigate = useNavigate();
  const createMenuItems = (label, key, children = []) => ({
    key,
    label,
    children,
  });

  const navigateResearchPage = () => navigate("/search");
  const navigateHome = () => navigate("/");
  const navigateLink = (link) => navigate(link);

  const handleMouseEnter = (item) => {
    setHoveredItem(item);
  };

  const handleMouseLeave = () => {
    setHoveredItem(null);
  };

  const toggleInputVisibility = () => {
    setIsInputVisible(!isInputVisible);
  };

  const handleSideBar = () => {
    setIsSideBarOpen(!isSideBarOpen);
  };
  const onOpenChange = (keys) => {
    setOpenKeys(keys);
  };

  // y

  const IntroductionList = [
    {
      lable: "Giới thiệu",
      link: "",
    },
    {
      lable: "Giới thiệu sàn ký gửi",
      link: "",
    },
    {
      lable: "Đơn vị bán Koi",
      link: "",
    },
    {
      lable: "Nguồn Koi",
      link: "",
    },
  ];

  const KoiList = [
    {
      lable: "Koi Kohaku",
      link: "",
    },
    {
      lable: "Koi Ogon",
      link: "",
    },
    {
      lable: "Koi Showa",
      link: "",
    },
    {
      lable: "Koi Tancho",
      link: "",
    },
    {
      lable: "Koi Bekko",
      link: "",
    },
  ];

  const FoodKoiList = [
    {
      lable: "Cám thương hiệu JDP",
      link: "",
    },
    {
      lable: "Cám thương hiệu Sakura",
      link: "",
    },
    {
      lable: "Cám thương hiệu Hikari",
      link: "",
    },
    {
      lable: "Cám thương hiệu Aqua Master",
      link: "",
    },
  ];

  const menuItems = [
    createMenuItems(
      "Giới thiệu",
      "introduction",
      IntroductionList.map((item) => ({ label: item.lable, key: item.key }))
    ),
    createMenuItems(
      "Cá Koi Nhật",
      "koi",
      KoiList.map((item) => ({ label: item.lable, key: item.key }))
    ),
    createMenuItems(
      "Thức ăn cá Koi",
      "food",
      FoodKoiList.map((item) => ({ label: item.lable, key: item.key }))
    ),
  ];

  const taskList = [
    {
      key: 1,
      lable: "Login",
      link: "/login",
    },
    {
      key: 2,
      lable: "Register",
      link: "/register",
    },
  ];

  return (
    <>
      <header>
        <div className="header-container">
          <div className="left-section">
            <GiHamburgerMenu size={"30px"} onClick={handleSideBar} />
          </div>
          <div className="header-logo">
            <img src="/logo-web/logo.png" alt="logo" onClick={navigateHome} />
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
                    {IntroductionList.map((item) => {
                      return <li className="dropdown-item">{item.lable}</li>;
                    })}
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
                    {KoiList.map((item) => {
                      return <li className="dropdown-item">{item.lable}</li>;
                    })}
                  </ul>
                )}
              </div>
              <div
                className="header-item"
                onMouseEnter={() => handleMouseEnter("food")}
                onMouseLeave={handleMouseLeave}
              >
                Thức ăn cá Koi
                {hoveredItem === "food" && (
                  <ul className="dropdown">
                    {FoodKoiList.map((item) => {
                      return <li className="dropdown-item">{item.lable}</li>;
                    })}
                  </ul>
                )}
              </div>
              <div className="header-item">Khuyến mãi</div>
            </div>
            <div className="header-search">
              <div className={`search ${isInputVisible ? "show-input" : ""}`}>
                <input
                  type="text"
                  name=""
                  id=""
                  placeholder="Tìm kiếm"
                  // className={`${isInputVisible ? "visible" : "hidden"}`}
                />
                <span className="search-icon">
                  <IoSearch size={"18px"} onClick={navigateResearchPage} />
                </span>
              </div>
            </div>
          </div>
          <div className="header-account">
            <div className="cart-icon">
              <TiShoppingCart size={"30px"} />
            </div>
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
                  {taskList.map((item) => {
                    return (
                      <li
                        key={item.key}
                        className="dropdown-item"
                        onClick={() => navigateLink(item.link)}
                      >
                        {item.lable}
                      </li>
                    );
                  })}
                </ul>
              )}
            </div>
          </div>
        </div>
      </header>
    </>
  );
}

export default Header;
