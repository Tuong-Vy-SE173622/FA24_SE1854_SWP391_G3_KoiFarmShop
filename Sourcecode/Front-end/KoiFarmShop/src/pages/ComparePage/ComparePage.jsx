import React, { useEffect, useState } from "react";
import { Table } from "antd";
import "./ComparePage.css";

function ComparePage() {
  const [compareData, setCompareData] = useState([]);

  useEffect(() => {
    const storedCompareData = JSON.parse(localStorage.getItem("Compare")) || [];
    setCompareData(storedCompareData);
  }, []);

  // Cấu hình các cột cho bảng so sánh
  const columns = [
    {
      title: "Thông tin",
      dataIndex: "attribute",
      key: "attribute",
      render: (text) => <strong>{text}</strong>,
    },
    {
      title: "Cá Koi 1",
      dataIndex: "koi1",
      key: "koi1",
    },
    {
      title: "Cá Koi 2",
      dataIndex: "koi2",
      key: "koi2",
    },
  ];

  // Chuẩn bị dữ liệu cho bảng
  const dataSource = [
    {
      key: "1",
      attribute: "Loại",
      koi1: compareData[0]?.koiTypeName || "N/A",
      koi2: compareData[1]?.koiTypeName || "N/A",
    },
    {
      key: "2",
      attribute: "Ảnh",
      koi1: (
        <img
          src="https://5.imimg.com/data5/QH/WR/MY-47947495/koi-carp-aquarium-fish.jpg"
          alt="koi"
          style={{ width: "80px", height: "auto" }}
        />
      ),
      koi2: (
        <img
          src="https://5.imimg.com/data5/QH/WR/MY-47947495/koi-carp-aquarium-fish.jpg"
          alt="koi"
          style={{ width: "80px", height: "auto" }}
        />
      ),
    },
    {
      key: "3",
      attribute: "Xuất xứ",
      koi1: compareData[0]?.origin || "N/A",
      koi2: compareData[1]?.origin || "N/A",
    },
    {
      key: "4",
      attribute: "Giới tính",
      koi1: compareData[0]?.gender === 0 ? "Cái" : "Đực",
      koi2: compareData[1]?.gender === 0 ? "Cái" : "Đực",
    },
    {
      key: "5",
      attribute: "Tuổi",
      koi1: compareData[0]?.age || "N/A",
      koi2: compareData[1]?.age || "N/A",
    },
    {
      key: "6",
      attribute: "Kích thước (cm)",
      koi1: compareData[0]?.size || "N/A",
      koi2: compareData[1]?.size || "N/A",
    },
    {
      key: "7",
      attribute: "Giá",
      koi1: `${new Intl.NumberFormat("vi-VN").format(
        compareData[0]?.price || 0
      )}đ`,
      koi2: `${new Intl.NumberFormat("vi-VN").format(
        compareData[1]?.price || 0
      )}đ`,
    },
    {
      key: "8",
      attribute: "Đặc trưng",
      koi1: compareData[0]?.characteristics || "N/A",
      koi2: compareData[1]?.characteristics || "N/A",
    },
    {
      key: "9",
      attribute: "Ghi chú",
      koi1: compareData[0]?.note || "N/A",
      koi2: compareData[1]?.note || "N/A",
    },
  ];

  return (
    <div className="page-container compare-page-container">
      <div className="compare-page-wrapper">
        <Table
          dataSource={dataSource}
          columns={columns}
          pagination={false}
          bordered
          className="compare-table"
        />
      </div>
    </div>
  );
}

export default ComparePage;
