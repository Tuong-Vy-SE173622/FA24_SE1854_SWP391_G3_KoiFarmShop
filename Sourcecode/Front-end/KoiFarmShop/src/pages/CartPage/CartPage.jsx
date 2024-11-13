import React, { useContext } from "react";
import { CartContext } from "../../contexts/CartContext";
import { Button, Row, Col } from "antd";
import { DeleteOutlined } from "@ant-design/icons";
import "./CartPage.css"; // Đảm bảo có file CSS để style trang này

function CartPage() {
  const { cart, removeFromCart } = useContext(CartContext);

  const total = cart.reduce((sum, item) => sum + item.price, 0); // Tính tổng giá trị giỏ hàng

  return (
    <div
      className="page-container"
      style={{ display: "flex", flexDirection: "column", alignItems: "center" }}
    >
      <div className="cart-page-container">
        <h2 className="cart-page-title">Giỏ hàng</h2>

        {cart.length === 0 ? (
          <h3 className="cart-page-empty">Giỏ hàng của bạn đang trống!</h3>
        ) : (
          <div className="cart-page-items">
            {cart.map((item) => (
              <div key={item.koiId} className="cart-page-item">
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
                      {/* Phần trên: KoiTypeName */}
                      <h3 className="cart-page-item-name">
                        {item.koiTypeName}
                      </h3>

                      {/* Phần dưới chia làm 2 cột */}
                      <Row gutter={16}>
                        <Col span={12}>
                          <div className="cart-page-item-left">
                            <p>
                              <strong>Nguồn gốc:</strong> {item.origin}
                            </p>
                            <p>
                              <strong>Tuổi:</strong> {item.age} tuổi
                            </p>
                          </div>
                        </Col>
                        <Col span={12}>
                          <div className="cart-page-item-right">
                            <p>
                              <strong>Kích cỡ:</strong> {item.size} cm
                            </p>
                            <p>
                              <strong>Giá:</strong>{" "}
                              {new Intl.NumberFormat("vi-VN").format(
                                item.price
                              )}
                              đ
                            </p>
                          </div>
                        </Col>
                      </Row>
                    </div>
                  </Col>
                  <Col span={6} className="cart-page-item-delete">
                    <Button
                      type="primary"
                      danger
                      icon={<DeleteOutlined />}
                      onClick={() => removeFromCart(item.koiId)}
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
          <h3>
            <span>
              Tổng cộng: {new Intl.NumberFormat("vi-VN").format(total)}đ
            </span>
          </h3>
          <Button
            type="primary"
            //   onClick={() => removeFromCart(item.koiId)}
            style={{
              width: 180,
              fontSize: 16,
              fontWeight: 600,
            }}
          >
            Thanh toán
          </Button>
        </div>
      </div>
    </div>
  );
}

export default CartPage;
