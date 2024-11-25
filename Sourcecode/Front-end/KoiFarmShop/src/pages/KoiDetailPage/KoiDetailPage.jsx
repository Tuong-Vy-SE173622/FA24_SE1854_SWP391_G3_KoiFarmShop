import React, { useEffect, useState } from "react";
import "./KoiDetailPage.css";
import { useNavigate, useParams } from "react-router-dom";
import { getKoiByID } from "../../services/KoiService";
import { Button, Col, Modal, Row } from "antd";
import { FaRegHeart } from "react-icons/fa";
import { FaCodeCompare } from "react-icons/fa6";
import { createOrder, createOrderItem } from "../../services/OrderService";

function KoiDetailPage() {
  const { KoiID } = useParams();
  const [koi, setKoi] = useState(null); // Đảm bảo koi bắt đầu là null để có thể kiểm tra khi render
  const nav = useNavigate();
  const fetchKoi = async () => {
    try {
      const data = await getKoiByID(KoiID);
      setKoi(data);
      console.log("Koi", data);
    } catch (err) {
      console.log("Không fetch được Koi ID: ", KoiID);
    }
  };

  useEffect(() => {
    fetchKoi();
  }, []);

  const handleAddToCart = async () => {
    try {
      const customerId = localStorage.getItem("customerId");
      if (!customerId) {
        Modal.warning({
          title: "Yêu cầu đăng nhập",
          content: "Vui lòng đăng nhập để thêm sản phẩm vào giỏ hàng.",
          onOk: () => nav("/login"),
        });
        return;
      }

      // Lấy `orderId` từ localStorage (nếu có)
      let currentOrderId = localStorage.getItem("orderId");

      // Nếu chưa có Order hiện tại, tạo Order mới
      if (!currentOrderId) {
        const order = await createOrder(customerId);
        currentOrderId = order.orderId;
        localStorage.setItem("orderId", currentOrderId); // Lưu OrderId mới
        console.log("Order created:", order);
      }

      // Thêm sản phẩm vào OrderItem thuộc Order hiện tại
      const orderItem = await createOrderItem(
        currentOrderId, // Sử dụng OrderId hiện tại
        koi.koiId,
        1, // Số lượng mặc định là 1
        koi.price
      );
      console.log("OrderItem created:", orderItem);

      Modal.success({
        title: "Thêm vào giỏ hàng thành công",
        content: "Sản phẩm đã được thêm vào đơn hàng của bạn.",
      });
    } catch (error) {
      console.error("Error during add to cart:", error);
      Modal.error({
        title: "Lỗi thêm vào giỏ hàng",
        content: error.message || "Không thể thêm sản phẩm vào đơn hàng.",
      });
    }
  };

  // Nếu koi chưa có dữ liệu, hiển thị một thông báo hoặc loading
  if (!koi) {
    return <div>Loading...</div>;
  }

  return (
    <div
      style={{
        marginTop: "100px",
        // display: "flex",
        // justifyContent: "center",
        // height: "max-content",
      }}
    >
      <div className="koi-detai-page-container">
        <div className="koi-detai-page-p1">
          <div className="koi-detai-page-left">
            <img src={koi.image} alt="koi" />
          </div>
          <div
            className="koi-detai-page-right"
            // style={{
            //   width: 750,
            //   // backgroundColor: "lightcoral",
            //   marginTop: 60,
            // }}
          >
            <div className="koi-detai-page-items">
              <h1>{koi.koiTypeName}</h1>
              <Row gutter={16} style={{ marginBottom: 10 }}>
                <Col className="col-koi-detail-page-style">
                  <div className="icon">
                    <FaRegHeart />
                  </div>
                  <p>Danh sách yêu thích</p>
                </Col>
                <Col className="col-koi-detail-page-style">
                  <div className="icon">
                    <FaCodeCompare />
                  </div>
                  <p>So sánh</p>
                </Col>
              </Row>
              <Row gutter={16}>
                <Col span={7}>
                  <p style={{ fontSize: 18.5, fontWeight: 600 }}>Nguồn gôc</p>
                </Col>
                <Col span={16}>
                  <p style={{ fontSize: 18.5 }}>{koi.origin}</p>
                </Col>
              </Row>
              <Row gutter={16}>
                <Col span={7}>
                  <p style={{ fontSize: 18.5, fontWeight: 600 }}>Người bán:</p>
                </Col>
                <Col span={16}>
                  <p style={{ fontSize: 18.5 }}>
                    {koi.isOwnedByFarm ? "Koi Farm shop" : "Trại khác"}
                  </p>
                </Col>
              </Row>
            </div>
            <p className="koi-detai-page-price" style={{ fontSize: 27 }}>
              {new Intl.NumberFormat("vi-VN").format(koi.price)}đ
            </p>
            <Button
              type="primary"
              shape="round"
              size="large"
              onClick={handleAddToCart} // Gọi hàm xử lý thêm vào giỏ hàng
            >
              <p style={{ fontWeight: 600 }}>Bỏ vào giỏ hàng</p>
            </Button>
          </div>
        </div>
        <div className="koi-detai-page-p2">
          <p className="detail-page-info">Mô tả</p>
          <div className="detai-page-info-items">
            {/* <Row gutter={16}>
              <Col span={7}>
                <p style={{ fontSize: 16.5, fontWeight: 600 }}>Type:</p>
              </Col>
              <Col span={16}>
                <p>{koi.koiTypeName}</p>
              </Col>
            </Row> */}
            <Row gutter={16}>
              <Col span={7}>
                <p style={{ fontSize: 16.5, fontWeight: 600 }}>Giới tính:</p>
              </Col>
              <Col span={7}>
                <p style={{ fontSize: 16.5 }}>
                  {koi.gender === 0 ? "Cái" : koi.gender === 1 ? "Đực" : ""}
                </p>
              </Col>
            </Row>
            <Row gutter={16}>
              <Col span={7}>
                <p style={{ fontSize: 16.5, fontWeight: 600 }}>Tuổi:</p>
              </Col>
              <Col span={16}>
                <p style={{ fontSize: 16.5 }}>{koi.age}</p>
              </Col>
            </Row>
            <Row gutter={16}>
              <Col span={7}>
                <p style={{ fontSize: 16.5, fontWeight: 600 }}>Kích thước:</p>
              </Col>
              <Col span={16}>
                <p style={{ fontSize: 16.5 }}>{koi.size} cm</p>
              </Col>
            </Row>
            <Row gutter={16}>
              <Col span={7}>
                <p style={{ fontSize: 16.5, fontWeight: 600 }}>Nguồn gốc:</p>
              </Col>
              <Col span={16}>
                <p style={{ fontSize: 16.5 }}>{koi.origin}</p>
              </Col>
            </Row>
            <Row gutter={16}>
              <Col span={7}>
                <p style={{ fontSize: 16.5, fontWeight: 600 }}>Thế hệ:</p>
              </Col>
              <Col span={16}>
                <p>{koi.generation}</p>
              </Col>
            </Row>
            <Row gutter={16}>
              <Col span={7}>
                <p style={{ fontSize: 16.5, fontWeight: 600 }}>
                  Tỉ lệ sàng lọc:
                </p>
              </Col>
              <Col span={16}>
                <p>{koi.screeningRate}%</p>
              </Col>
            </Row>
            <Row gutter={16}>
              <Col span={7}>
                <p style={{ fontSize: 16.5, fontWeight: 600 }}>Đặc trưng:</p>
              </Col>
              <Col span={16}>
                <p style={{ fontSize: 16.5 }}>{koi.characteristics}</p>
              </Col>
            </Row>
            <Row gutter={16}>
              <Col span={7}>
                <p style={{ fontSize: 16.5, fontWeight: 600 }}>Chú thích:</p>
              </Col>
              <Col span={16}>
                <p style={{ fontSize: 16.5 }}>{koi.note}</p>
              </Col>
            </Row>
            <Row gutter={16}>
              <Col span={7}>
                <p style={{ fontSize: 16.5, fontWeight: 600 }}>
                  Số lần cho trong 1 ngày:
                </p>
              </Col>
              <Col span={16}>
                <p>{koi.feedingAmountPerDay} lần</p>
              </Col>
            </Row>
            <img src={koi.certificate} alt="" />
          </div>
        </div>
      </div>
    </div>
  );
}

export default KoiDetailPage;
