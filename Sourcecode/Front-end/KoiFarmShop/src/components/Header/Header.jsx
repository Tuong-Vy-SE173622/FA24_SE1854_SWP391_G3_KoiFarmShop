// import React, { useContext, useEffect, useState } from "react";
// import "./Header.css";
// import { TiShoppingCart } from "react-icons/ti";
// import { IoSearch } from "react-icons/io5";
// import { Badge, Button } from "antd";
// import { useNavigate } from "react-router-dom";
// import { logout } from "../../services/authService";
// import { getAllKoiType, getKoiType } from "../../services/KoiTypeService";
// import { CartContext } from "../../contexts/CartContext";

// function Header() {
//   const [isInputVisible, setIsInputVisible] = useState(false);
//   const [hoveredItem, setHoveredItem] = useState(null);
//   const [isLoggedIn, setIsLoggedIn] = useState(false);
//   const [koiTypeLs, setKoiTypeLs] = useState([]);
//   const [role, setRole] = useState(null);
//   const [searchQuery, setSearchQuery] = useState(""); // State cho tìm kiếm
//   const navigate = useNavigate();
//   const { cart } = useContext(CartContext);

//   useEffect(() => {
//     const accessToken = localStorage.getItem("accessToken");
//     const role = JSON.parse(localStorage.getItem("roles"));
//     if (accessToken) {
//       setIsLoggedIn(true);
//       setRole(role);
//     }
//   }, []);

//   const navigateLink = (link) => navigate(link);
//   const handleMouseEnter = (item) => setHoveredItem(item);
//   const handleMouseLeave = () => setHoveredItem(null);
//   const navigateLogin = () => navigate("/login");
//   const navigateRegister = () => navigate("/register");

//   const IntroductionList = [
//     { key: 1, label: "Giới thiệu", link: "" },
//     { key: 2, label: "Giới thiệu sàn ký gửi", link: "" },
//     { key: 3, label: "Đơn vị bán Koi", link: "" },
//     { key: 4, label: "Nguồn Koi", link: "" },
//   ];

//   const userList = [
//     { key: 1, label: "Profile", link: "/dashboard/profile/stella" },
//     { key: 2, label: "Logout", link: "" },
//   ];

//   const adminMenu = [
//     { key: 1, label: "Dashboard", link: "/admin/dashboard" },
//     { key: 2, label: "Profile", link: "/admin/profile/stella" },
//     { key: 3, label: "Logout", link: "" },
//   ];

//   const handleLogout = async () => {
//     try {
//       await logout();
//       setIsLoggedIn(false);
//       navigate("/login");
//     } catch (err) {
//       console.error("Logout failed: ", err);
//     }
//   };

//   const handleSearch = () => {
//     if (searchQuery.trim()) {
//       navigate(`/search?searchData=${encodeURIComponent(searchQuery)}`);
//     }
//   };

//   const handleCartPage = () => {
//     if (isLoggedIn) {
//       navigate("/cart");
//     } else {
//       alert("Vui lòng đăng nhập để truy cập giỏ hàng");
//       navigate("/login");
//     }
//   };

//   // Hàm xử lý khi nhấn Enter trong ô tìm kiếm
//   const handleKeyDown = (event) => {
//     if (event.key === "Enter") {
//       handleSearch();
//     }
//   };

//   useEffect(() => {
//     const fetchKoiType = async () => {
//       try {
//         const data = await getKoiType();
//         setKoiTypeLs(data);
//       } catch (err) {
//         console.error("Failed to fetch Koi types", err);
//       }
//     };

//     fetchKoiType();
//   }, []);

//   return (
//     <header>
//       <div className="header-container">
//         <div className="header-logo">
//           <img
//             src="/logo-web/logo.png"
//             alt="logo"
//             onClick={() => navigate("/")}
//             style={{ cursor: "pointer" }}
//           />
//         </div>
//         <div className="middle-section">
//           <div className="header-items">
//             <div
//               className="header-item"
//               onMouseEnter={() => handleMouseEnter("introduction")}
//               onMouseLeave={handleMouseLeave}
//             >
//               Giới thiệu
//               {hoveredItem === "introduction" && (
//                 <ul className="dropdown">
//                   {IntroductionList.map((item) => (
//                     <li className="dropdown-item" key={item.key}>
//                       {item.label}
//                     </li>
//                   ))}
//                 </ul>
//               )}
//             </div>
//             <div
//               className="header-item"
//               onMouseEnter={() => handleMouseEnter("koi")}
//               onMouseLeave={handleMouseLeave}
//             >
//               Cá Koi Nhật
//               {hoveredItem === "koi" && (
//                 <ul className="dropdown">
//                   {koiTypeLs.map((item) => (
//                     <li className="dropdown-item" key={item.koiTypeId}>
//                       {item.name}
//                     </li>
//                   ))}
//                 </ul>
//               )}
//             </div>
//           </div>
//           <div className="header-search">
//             <div className={`search ${isInputVisible ? "show-input" : ""}`}>
//               <input
//                 type="text"
//                 placeholder="Tìm kiếm"
//                 value={searchQuery}
//                 onChange={(e) => setSearchQuery(e.target.value)}
//                 onKeyDown={handleKeyDown}
//               />
//               <span className="search-icon">
//                 <IoSearch size={"18px"} onClick={handleSearch} />
//               </span>
//             </div>
//           </div>
//         </div>
//         <div className="header-account">
//           <div className="cart-icon">
//             <Badge count={cart.length}>
//               <TiShoppingCart size={"30px"} onClick={handleCartPage} />
//             </Badge>
//           </div>

//           {isLoggedIn ? (
//             <div
//               className="header-item header-avatar"
//               onMouseEnter={() => handleMouseEnter("user")}
//               onMouseLeave={handleMouseLeave}
//             >
//               <img
//                 src="https://i.pinimg.com/736x/d6/46/02/d64602a7b954a8b2f09bac97a7911bf8.jpg"
//                 alt="avatar"
//               />
//               {hoveredItem === "user" && (
//                 <ul className="dropdown">
//                   {(role === "Admin" ? adminMenu : userList).map((item) => (
//                     <li
//                       key={item.key}
//                       className="dropdown-item"
//                       onClick={
//                         item.label === "Logout"
//                           ? handleLogout
//                           : () => navigateLink(item.link)
//                       }
//                     >
//                       {item.label}
//                     </li>
//                   ))}
//                 </ul>
//               )}
//             </div>
//           ) : (
//             <div
//               className="header-item"
//               style={{ display: "flex", alignItems: "center" }}
//             >
//               <Button
//                 type="primary"
//                 onClick={navigateLogin}
//                 style={{ marginRight: "10px" }}
//               >
//                 Login
//               </Button>
//               <Button
//                 type="primary"
//                 onClick={navigateRegister}
//                 style={{ marginRight: "10px" }}
//               >
//                 Register
//               </Button>
//             </div>
//           )}
//         </div>
//       </div>
//     </header>
//   );
// }

// export default Header;
import React, { useEffect, useState } from "react";
import "./Header.css";
import { TiShoppingCart } from "react-icons/ti";
import { IoSearch } from "react-icons/io5";
import { Badge, Button, Modal } from "antd";
import { useNavigate } from "react-router-dom";
import { logout } from "../../services/authService";
import { getAllKoiType } from "../../services/KoiTypeService";
import { getOrderById } from "../../services/OrderService"; // Import API getOrderById

function Header() {
  const [isInputVisible, setIsInputVisible] = useState(false);
  const [hoveredItem, setHoveredItem] = useState(null);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [koiTypeLs, setKoiTypeLs] = useState([]);
  const [role, setRole] = useState(null);
  const [searchQuery, setSearchQuery] = useState("");
  const [cartItemCount, setCartItemCount] = useState(0); // Số lượng item trong giỏ hàng
  const navigate = useNavigate();

  useEffect(() => {
    const accessToken = localStorage.getItem("accessToken");
    const role = JSON.parse(localStorage.getItem("roles"));
    if (accessToken) {
      setIsLoggedIn(true);
      setRole(role);
    }
  }, []);

  useEffect(() => {
    const fetchCartItemCount = async () => {
      const orderId = localStorage.getItem("orderId");
      if (orderId) {
        try {
          const order = await getOrderById(orderId); // Gọi API để lấy thông tin order
          setCartItemCount(order?.orderItems?.length || 0); // Lấy số lượng `orderItems`
        } catch (error) {
          console.error("Error fetching cart item count:", error);
          Modal.error({
            title: "Lỗi tải giỏ hàng",
            content: "Không thể tải số lượng sản phẩm trong giỏ hàng.",
          });
        }
      }
    };

    if (isLoggedIn) {
      fetchCartItemCount(); // Chỉ gọi API nếu đã đăng nhập
    }
  }, [isLoggedIn]); // Gọi lại nếu trạng thái đăng nhập thay đổi

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

  const userList = [
    { key: 1, label: "Profile", link: "/dashboard/profile/stella" },
    { key: 2, label: "Logout", link: "" },
  ];

  const adminMenu = [
    { key: 1, label: "Dashboard", link: "/admin/dashboard" },
    { key: 2, label: "Profile", link: "/admin/profile/stella" },
    { key: 3, label: "Logout", link: "" },
  ];

  const handleLogout = async () => {
    try {
      await logout();
      setIsLoggedIn(false);
      navigate("/login");
    } catch (err) {
      console.error("Logout failed: ", err);
    }
  };
  const handleSearch = () => {
    if (searchQuery.trim()) {
      navigate(`/search?searchData=${encodeURIComponent(searchQuery)}`);
    }
  };

  const handleCartPage = () => {
    if (isLoggedIn) {
      navigate("/cart");
    } else {
      alert("Vui lòng đăng nhập để truy cập giỏ hàng");
      navigate("/login");
    }
  };

  const handleKeyDown = (event) => {
    if (event.key === "Enter") {
      handleSearch();
    }
  };

  useEffect(() => {
    const fetchKoiType = async () => {
      try {
        const data = await getAllKoiType();
        setKoiTypeLs(data);
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
          </div>
          <div className="header-search">
            <div className={`search ${isInputVisible ? "show-input" : ""}`}>
              <input
                type="text"
                placeholder="Tìm kiếm"
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
                onKeyDown={handleKeyDown}
              />
              <span className="search-icon">
                <IoSearch size={"18px"} onClick={handleSearch} />
              </span>
            </div>
          </div>
        </div>
        <div className="header-account">
          <div className="cart-icon">
            <Badge count={cartItemCount}>
              <TiShoppingCart size={"30px"} onClick={handleCartPage} />
            </Badge>
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
                  {(role === "Admin" ? adminMenu : userList).map((item) => (
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
