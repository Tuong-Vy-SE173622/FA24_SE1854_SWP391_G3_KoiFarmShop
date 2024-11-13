import React, { useContext, useEffect, useState } from "react";
import { Col, Modal, Row } from "antd";
import { IoIosSearch } from "react-icons/io";
import { TiShoppingCart } from "react-icons/ti";
import { FaRegHeart } from "react-icons/fa";
import { FaCodeCompare } from "react-icons/fa6";
import { useNavigate } from "react-router-dom";
import "./KoiCard.css";
import { CartContext } from "../../contexts/CartContext";

function KoiCard({ koi }) {
  const nav = useNavigate();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [compareData, setCompareData] = useState([]);

  const { addToCart } = useContext(CartContext);

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
      onClick: () => addToCart(koi),
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
      onClick: () => addtoCompare(koi),
    },
  ];

  const showWarning = () => {
    Modal.warning({
      title: "Danh sách so sánh đã đầy",
      content: "Vui lòng xóa bớt phần tử trước khi thêm mới.",
      okButtonProps: {
        style: { backgroundColor: "red", borderColor: "red", color: "white" },
      },
      okText: "Đóng",
    });
  };

  const handleDetailPage = () => nav(`/koi-detail/${koi.koiId}`);
  const handleModalCancel = () => setIsModalOpen(false);

  if (!koi) return <div>Loading...</div>;

  const addtoCompare = (koi) => {
    let compareList = JSON.parse(localStorage.getItem("Compare")) || [];

    // Kiểm tra nếu phần tử đã có trong Compare
    if (compareList.some((item) => item.koiId === koi.koiId)) {
      Modal.warning({
        title: "Phần tử đã tồn tại",
        content: "Phần tử này đã có trong danh sách so sánh.",
        okButtonProps: {
          style: { backgroundColor: "red", borderColor: "red", color: "white" },
        },
        okText: "Đóng",
      });
      return;
    }

    // Kiểm tra nếu Compare đã đạt giới hạn 2 phần tử
    if (compareList.length >= 2) {
      showWarning();
      return;
    }

    // Thêm phần tử mới vào Compare
    compareList.push(koi);
    localStorage.setItem("Compare", JSON.stringify(compareList));
    setCompareData(compareList);
  };

  useEffect(() => {
    const handleStorageChange = () => {
      const storedCompareData =
        JSON.parse(localStorage.getItem("Compare")) || [];
      setCompareData(storedCompareData);
    };

    // Lắng nghe sự kiện `storage`
    window.addEventListener("storage", handleStorageChange);

    // Xóa lắng nghe khi component bị unmount
    return () => {
      window.removeEventListener("storage", handleStorageChange);
    };
  }, [compareData]);

  return (
    <div className="koi-card-container">
      <img src={koi.image} alt="koi" onClick={handleDetailPage} />
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
              <img src={koi.image} alt="koi" />
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
