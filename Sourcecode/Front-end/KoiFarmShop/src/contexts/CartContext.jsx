import React, { useState, useEffect, createContext } from "react";
import { useNavigate } from "react-router-dom";
import { Modal } from "antd"; // Import Modal từ Ant Design

export const CartContext = createContext();

export function CartProvider({ children }) {
  const [cart, setCart] = useState([]);
  const navigate = useNavigate();
  const [showWarning, setShowWarning] = useState(false); // State cho popup cảnh báo

  useEffect(() => {
    // Kiểm tra nếu dữ liệu cart đã có trong localStorage
    const savedCart = localStorage.getItem("cart");
    if (savedCart) {
      setCart(JSON.parse(savedCart));
    }
  }, []);

  useEffect(() => {
    // Cập nhật localStorage mỗi khi giỏ hàng thay đổi
    if (cart.length > 0) {
      localStorage.setItem("cart", JSON.stringify(cart));
    }
  }, [cart]);

  // Hàm kiểm tra đăng nhập
  function isLoggedIn() {
    return localStorage.getItem("accessToken") !== null;
  }

  function addToCart(product) {
    if (!isLoggedIn()) {
      alert("Vui lòng đăng nhập để thêm sản phẩm vào giỏ hàng");
      // Chuyển hướng đến trang đăng nhập nếu cần
      navigate("/login"); // Điều chỉnh URL theo trang đăng nhập của bạn
      return;
    }

    // Kiểm tra nếu sản phẩm đã tồn tại trong giỏ hàng
    const isProductInCart = cart.some((item) => item.koiId === product.koiId);

    if (isProductInCart) {
      // Nếu sản phẩm trùng, hiển thị modal cảnh báo
      setShowWarning(true);
    } else {
      // Nếu sản phẩm chưa tồn tại, thêm sản phẩm vào giỏ hàng
      setCart([...cart, { ...product, quantity: 1 }]);
    }
  }

  // Hàm xóa sản phẩm khỏi giỏ hàng
  function removeFromCart(koiId) {
    setCart(cart.filter((item) => item.koiId !== koiId));
  }

  // Hàm đóng modal cảnh báo
  function closeWarning() {
    setShowWarning(false);
  }

  return (
    <CartContext.Provider value={{ cart, addToCart, removeFromCart }}>
      {children}

      {/* Modal cảnh báo khi sản phẩm đã tồn tại trong giỏ hàng */}
      <Modal
        title="Cảnh báo"
        visible={showWarning}
        onCancel={closeWarning}
        onOk={closeWarning}
        cancelButtonProps={{ style: { display: "none" } }} // Ẩn nút hủy
        okText="Đóng"
      >
        <p>Sản phẩm này đã có trong giỏ hàng!</p>
      </Modal>
    </CartContext.Provider>
  );
}
