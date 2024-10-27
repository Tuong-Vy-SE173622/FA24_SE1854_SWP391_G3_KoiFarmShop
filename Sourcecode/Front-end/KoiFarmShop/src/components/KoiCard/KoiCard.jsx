import React, { useState } from "react";
import { Col, Modal, Row } from "antd";
import { IoIosSearch } from "react-icons/io";
import { TiShoppingCart } from "react-icons/ti";
import { FaRegHeart } from "react-icons/fa";
import { FaCodeCompare } from "react-icons/fa6";
import { useNavigate } from "react-router-dom";
import "./KoiCard.css";

function KoiCard({ koi }) {
  const nav = useNavigate();
  const [isModalOpen, setIsModalOpen] = useState(false);

  const koiSelectItems = [
    {
      key: 1,
      icon: <IoIosSearch />,
      label: "Xem nhanh",
      onClick: () => setIsModalOpen(true), // Open modal on click
    },
    {
      key: 2,
      icon: <TiShoppingCart />,
      label: "Thêm vào giỏ hàng",
    },
    {
      key: 3,
      icon: <FaRegHeart />,
      label: "Yêu thích",
    },
    {
      key: 4,
      icon: <FaCodeCompare />,
      label: "So sánh",
    },
  ];

  const handleDetailPage = () => nav(`/koi-detail/${koi.koiId}`);
  const handleModalCancel = () => setIsModalOpen(false);

  if (!koi) return <div>Loading...</div>;

  return (
    <div className="koi-card-container">
      <img
        src="https://5.imimg.com/data5/QH/WR/MY-47947495/koi-carp-aquarium-fish.jpg"
        alt="koi"
        onClick={handleDetailPage}
      />
      <div className="koi-title">{koi.koiTypeName}</div>
      <div className="koi-card-select">
        {koiSelectItems.map((item) => (
          <div
            className="koi-card-select-item"
            key={item.key}
            onClick={item.onClick}
          >
            <span className="select-item-icon">{item.icon}</span>
          </div>
        ))}
      </div>

      {/* Modal Component */}
      {/* <Modal
        title="Quick View"
        open={isModalOpen}
        onCancel={handleModalCancel}
        footer={null}
        centered // Ensures the modal is vertically centered by default
        bodyStyle={{
          height: "400px",
          overflowY: "auto",
        }}
        width={600}
        style={{
          // Adjusts position to be exactly center
        }}
      > */}
      <Modal
        open={isModalOpen}
        onCancel={handleModalCancel}
        centered
        footer={null}
        className="custom-modal"
        width={700}
        style={{
          position: "fixed",
          top: "12%",
          left: "24%",
          zIndex: 50,
        }}
      >
        <div className="popup-koi-detail-container">
          <div className="popup-koi-detail-p1">
            <div className="popup-koi-detail-left">
              <img
                src="https://5.imimg.com/data5/QH/WR/MY-47947495/koi-carp-aquarium-fish.jpg"
                alt="koi"
              />
            </div>
            <div className="popup-koi-detail-right">
              <div className="popup-koi-detail-items">
                <Row gutter={16}>
                  <Col span={7}>
                    <p style={{ fontSize: 14.5, fontWeight: 600 }}>Type:</p>
                  </Col>
                  <Col span={16}>
                    <p>{koi.koiTypeName}</p>
                  </Col>
                </Row>
                <Row gutter={16}>
                  <Col span={7}>
                    <p style={{ fontSize: 14.5, fontWeight: 600 }}>
                      Giới tính:
                    </p>
                  </Col>
                  <Col span={7}>
                    <p style={{ fontSize: 14.5 }}>
                      {koi.gender === 0 ? "Cái" : koi.gender === 1 ? "Đực" : ""}
                    </p>
                  </Col>
                </Row>
                <Row gutter={16}>
                  <Col span={7}>
                    <p style={{ fontSize: 14.5, fontWeight: 600 }}>Tuổi:</p>
                  </Col>
                  <Col span={16}>
                    <p style={{ fontSize: 14.5 }}>{koi.age}</p>
                  </Col>
                </Row>
                <Row gutter={16}>
                  <Col span={7}>
                    <p style={{ fontSize: 14.5, fontWeight: 600 }}>
                      Kích thước:
                    </p>
                  </Col>
                  <Col span={16}>
                    <p style={{ fontSize: 14.5 }}>{koi.size} cm</p>
                  </Col>
                </Row>
                <Row gutter={16}>
                  <Col span={7}>
                    <p style={{ fontSize: 14.5, fontWeight: 600 }}>
                      Nguồn gốc:
                    </p>
                  </Col>
                  <Col span={16}>
                    <p style={{ fontSize: 14.5 }}>{koi.origin}</p>
                  </Col>
                </Row>
                <Row gutter={16}>
                  <Col span={7}>
                    <p style={{ fontSize: 14.5, fontWeight: 600 }}>
                      Đặc trưng:
                    </p>
                  </Col>
                  <Col span={16}>
                    <p style={{ fontSize: 14.5 }}>{koi.characteristics}</p>
                  </Col>
                </Row>
                <Row gutter={16}>
                  <Col span={7}>
                    <p style={{ fontSize: 14.5, fontWeight: 600 }}>
                      Chú thích:
                    </p>
                  </Col>
                  <Col span={16}>
                    <p style={{ fontSize: 14.5 }}>{koi.note}</p>
                  </Col>
                </Row>
              </div>
              <p className="popup-koi-detail-price">
                {new Intl.NumberFormat("vi-VN").format(koi.price)}đ
              </p>
            </div>
          </div>
          <div className="popup-koi-detail-p2"></div>
        </div>
      </Modal>
    </div>
  );
}

export default KoiCard;
