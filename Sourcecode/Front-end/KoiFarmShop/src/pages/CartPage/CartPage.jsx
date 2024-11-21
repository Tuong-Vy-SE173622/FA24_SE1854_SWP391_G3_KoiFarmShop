// import React, { useContext } from "react";
// import { CartContext } from "../../contexts/CartContext";
// import { Button, Row, Col } from "antd";
// import { DeleteOutlined } from "@ant-design/icons";
// import "./CartPage.css"; // Đảm bảo có file CSS để style trang này

// function CartPage() {
//   const { cart, removeFromCart } = useContext(CartContext);

//   const total = cart.reduce((sum, item) => sum + item.price, 0); // Tính tổng giá trị giỏ hàng

//   return (
//     <div
//       className="page-container"
//       style={{ display: "flex", flexDirection: "column", alignItems: "center" }}
//     >
//       <div className="cart-page-container">
//         <h2 className="cart-page-title">Giỏ hàng</h2>

//         {cart.length === 0 ? (
//           <h3 className="cart-page-empty">Giỏ hàng của bạn đang trống!</h3>
//         ) : (
//           <div className="cart-page-items">
//             {cart.map((item) => (
//               <div key={item.koiId} className="cart-page-item">
//                 <Row>
//                   <Col span={5}>
//                     <img
//                       alt={item.koiTypeName}
//                       src={item.image}
//                       className="cart-page-item-image"
//                     />
//                   </Col>
//                   <Col span={12}>
//                     <div className="cart-page-item-details">
//                       {/* Phần trên: KoiTypeName */}
//                       <h3 className="cart-page-item-name">
//                         {item.koiTypeName}
//                       </h3>

//                       {/* Phần dưới chia làm 2 cột */}
//                       <Row gutter={16}>
//                         <Col span={12}>
//                           <div className="cart-page-item-left">
//                             <p>
//                               <strong>Nguồn gốc:</strong> {item.origin}
//                             </p>
//                             <p>
//                               <strong>Tuổi:</strong> {item.age} tuổi
//                             </p>
//                           </div>
//                         </Col>
//                         <Col span={12}>
//                           <div className="cart-page-item-right">
//                             <p>
//                               <strong>Kích cỡ:</strong> {item.size} cm
//                             </p>
//                             <p>
//                               <strong>Giá:</strong>{" "}
//                               {new Intl.NumberFormat("vi-VN").format(
//                                 item.price
//                               )}
//                               đ
//                             </p>
//                           </div>
//                         </Col>
//                       </Row>
//                     </div>
//                   </Col>
//                   <Col span={6} className="cart-page-item-delete">
//                     <Button
//                       type="primary"
//                       danger
//                       icon={<DeleteOutlined />}
//                       onClick={() => removeFromCart(item.koiId)}
//                       className="cart-page-delete-button"
//                     >
//                       Xóa
//                     </Button>
//                   </Col>
//                 </Row>
//                 <hr />
//               </div>
//             ))}
//           </div>
//         )}

//         <div className="cart-page-total">
//           <h3>
//             <span>
//               Tổng cộng: {new Intl.NumberFormat("vi-VN").format(total)}đ
//             </span>
//           </h3>
//           <Button
//             type="primary"
//             //   onClick={() => removeFromCart(item.koiId)}
//             style={{
//               width: 180,
//               fontSize: 16,
//               fontWeight: 600,
//             }}
//           >
//             Thanh toán
//           </Button>
//         </div>
//       </div>
//     </div>
//   );
// }

// export default CartPage;

import React, { useEffect, useState } from "react";
import { Button, Row, Col, Spin, Modal } from "antd";
import { DeleteOutlined, ExclamationCircleOutlined } from "@ant-design/icons";
import { getOrderById, createPayment } from "../../services/orderService"; // Sử dụng API mới
import "./CartPage.css";

function CartPage() {
  const [cart, setCart] = useState([]);
  const [loading, setLoading] = useState(true);
  const [total, setTotal] = useState(0);
  const [orderId, setOrderId] = useState(
    localStorage.getItem("orderId") || null
  ); // Lấy `orderId` từ localStorage

  const fetchOrderData = async () => {
    try {
      if (!orderId) {
        Modal.warning({
          title: "Không tìm thấy giỏ hàng",
          content: "Vui lòng thêm sản phẩm vào giỏ hàng trước.",
        });
        return;
      }

      setLoading(true);

      // Gọi API `getOrderById` để lấy chi tiết giỏ hàng
      const order = await getOrderById(orderId);

      // Lọc chỉ hiển thị các mục có `paymentStatus: "Pending"`
      if (order.paymentStatus !== "Pending") {
        setCart([]); // Nếu không có mục nào Pending, hiển thị giỏ hàng trống
      } else {
        const cartData = order.orderItems.map((item) => ({
          ...item,
          koiTypeName: "Không xác định", // Placeholder nếu không có dữ liệu koi
          image: "https://via.placeholder.com/150", // Placeholder nếu không có ảnh
        }));
        setCart(cartData);
        calculateTotal(cartData);
      }
    } catch (error) {
      console.error("Error fetching order data:", error);
      Modal.error({
        title: "Lỗi tải dữ liệu",
        content: "Không thể tải dữ liệu giỏ hàng. Vui lòng thử lại sau.",
      });
    } finally {
      setLoading(false);
    }
  };

  // Tính tổng giá trị giỏ hàng
  const calculateTotal = (cartData) => {
    const sum = cartData.reduce(
      (acc, item) => acc + item.price * item.amount,
      0
    );
    setTotal(sum);
  };

  // Xóa một sản phẩm khỏi giỏ hàng
  const removeFromCart = (orderItemId) => {
    Modal.confirm({
      title: "Xác nhận xóa",
      icon: <ExclamationCircleOutlined />,
      content: "Bạn có chắc muốn xóa sản phẩm này khỏi giỏ hàng?",
      okText: "Xóa",
      cancelText: "Hủy",
      onOk: () => {
        const updatedCart = cart.filter(
          (item) => item.orderItemId !== orderItemId
        );
        setCart(updatedCart);
        calculateTotal(updatedCart);
      },
    });
  };

  // Xử lý thanh toán
  const handlePayment = async () => {
    try {
      if (!orderId) {
        Modal.warning({
          title: "Không tìm thấy giỏ hàng",
          content: "Vui lòng thêm sản phẩm vào giỏ hàng trước khi thanh toán.",
        });
        return;
      }

      // Gọi API thanh toán
      const paymentResponse = await createPayment(
        total,
        "Thanh toán đơn hàng",
        orderId
      );

      if (paymentResponse.paymentUrl) {
        // Redirect đến URL thanh toán
        window.location.href = paymentResponse.paymentUrl;
      } else {
        Modal.error({
          title: "Lỗi thanh toán",
          content: "Không thể tạo yêu cầu thanh toán. Vui lòng thử lại.",
        });
      }
    } catch (error) {
      console.error("Error during payment:", error);
      Modal.error({
        title: "Lỗi thanh toán",
        content: "Có lỗi xảy ra khi tạo yêu cầu thanh toán.",
      });
    }
  };

  useEffect(() => {
    fetchOrderData();
  }, [orderId]); // Chỉ chạy khi `orderId` thay đổi

  if (loading) {
    return (
      <div className="loading-container">
        <Spin tip="Đang tải giỏ hàng..." size="large" />
      </div>
    );
  }

  return (
    <div
      className="page-container"
      style={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
      }}
    >
      <div className="cart-page-container">
        <h2 className="cart-page-title">Giỏ hàng</h2>

        {cart.length === 0 ? (
          <h3 className="cart-page-empty">Giỏ hàng của bạn đang trống!</h3>
        ) : (
          <div className="cart-page-items">
            {cart.map((item) => (
              <div key={item.orderItemId} className="cart-page-item">
                <Row>
                  <Col span={5}>
                    <img
                      alt={item.koiTypeName}
                      src={item.image}
                      className="cart-page-item-image"
                    />
                  </Col>
                  <Col span={12}>
                    <div className="cart-page-item-details">
                      <h3 className="cart-page-item-name">
                        {item.koiTypeName}
                      </h3>
                      <Row gutter={16}>
                        <Col span={12}>
                          <p>
                            <strong>Giá:</strong>{" "}
                            {new Intl.NumberFormat("vi-VN").format(item.price)}{" "}
                            đ
                          </p>
                        </Col>
                        <Col span={12}>
                          <p>
                            <strong>Số lượng:</strong> {item.amount}
                          </p>
                        </Col>
                      </Row>
                    </div>
                  </Col>
                  <Col span={6} className="cart-page-item-delete">
                    <Button
                      type="primary"
                      danger
                      icon={<DeleteOutlined />}
                      onClick={() => removeFromCart(item.orderItemId)}
                      className="cart-page-delete-button"
                    >
                      Xóa
                    </Button>
                  </Col>
                </Row>
                <hr />
              </div>
            ))}
          </div>
        )}

        <div className="cart-page-total">
          <h3>Tổng cộng: {new Intl.NumberFormat("vi-VN").format(total)} đ</h3>
          <Button
            type="primary"
            style={{
              width: 180,
              fontSize: 16,
              fontWeight: 600,
            }}
            onClick={handlePayment}
          >
            Thanh toán
          </Button>
        </div>
      </div>
    </div>
  );
}

export default CartPage;
