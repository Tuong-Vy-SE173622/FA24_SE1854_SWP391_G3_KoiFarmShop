import React, { useEffect, useState } from "react";
import { Table } from "antd";
import "./ComparePage.css";
import { getKoiByID } from "../../services/KoiService";

function ComparePage() {
  const [compareData, setCompareData] = useState([]);

  useEffect(() => {
    const storedCompareItems =
      JSON.parse(localStorage.getItem("Compare")) || [];

    const storedCompareIds = storedCompareItems.map((item) => item.koiId);

    // Nếu có ít nhất 2 ID, tiến hành gọi API
    if (storedCompareIds.length >= 2) {
      const [koiId1, koiId2] = storedCompareIds;

      // Gọi API lấy dữ liệu 2 cá Koi
      Promise.all([getKoiByID(koiId1), getKoiByID(koiId2)])
        .then((data) => {
          setCompareData(data); // Cập nhật compareData với dữ liệu đã lấy
        })
        .catch((error) => {
          console.error("Error fetching Koi data for comparison:", error);
        });
    }
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
      attribute: "Xuất xứ",
      koi1: compareData[0]?.origin || "N/A",
      koi2: compareData[1]?.origin || "N/A",
    },
    {
      key: "3",
      attribute: "Giới tính",
      koi1: compareData[0]?.gender === 0 ? "Cái" : "Đực",
      koi2: compareData[1]?.gender === 0 ? "Cái" : "Đực",
    },
    {
      key: "4",
      attribute: "Tuổi",
      koi1: compareData[0]?.age || "N/A",
      koi2: compareData[1]?.age || "N/A",
    },
    {
      key: "5",
      attribute: "Kích thước (cm)",
      koi1: compareData[0]?.size || "N/A",
      koi2: compareData[1]?.size || "N/A",
    },
    {
      key: "6",
      attribute: "Giá",
      koi1: `${new Intl.NumberFormat("vi-VN").format(
        compareData[0]?.price || 0
      )}đ`,
      koi2: `${new Intl.NumberFormat("vi-VN").format(
        compareData[1]?.price || 0
      )}đ`,
    },
    {
      key: "7",
      attribute: "Đặc trưng",
      koi1: compareData[0]?.characteristics || "N/A",
      koi2: compareData[1]?.characteristics || "N/A",
    },
    {
      key: "8",
      attribute: "Số lần cho ăn mỗi ngày",
      koi1: compareData[0]?.feedingAmountPerDay || "N/A",
      koi2: compareData[1]?.feedingAmountPerDay || "N/A",
    },
    {
      key: "9",
      attribute: "Tỷ lệ sàng lọc",
      koi1: `${compareData[0]?.screeningRate || "N/A"}%`,
      koi2: `${compareData[1]?.screeningRate || "N/A"}%`,
    },
    {
      key: "10",
      attribute: "Sở hữu bởi trang trại",
      koi1: compareData[0]?.isOwnedByFarm ? "Có" : "Không",
      koi2: compareData[1]?.isOwnedByFarm ? "Có" : "Không",
    },
    {
      key: "11",
      attribute: "Nhập khẩu",
      koi1: compareData[0]?.isImported ? "Có" : "Không",
      koi2: compareData[1]?.isImported ? "Có" : "Không",
    },
    {
      key: "12",
      attribute: "Thế hệ",
      koi1: compareData[0]?.generation || "N/A",
      koi2: compareData[1]?.generation || "N/A",
    },
    {
      key: "13",
      attribute: "Ghi chú",
      koi1: compareData[0]?.note || "N/A",
      koi2: compareData[1]?.note || "N/A",
    },
  ];

  console.log("koi 1", compareData[1]);
  console.log("koi 2", compareData[2]);

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
