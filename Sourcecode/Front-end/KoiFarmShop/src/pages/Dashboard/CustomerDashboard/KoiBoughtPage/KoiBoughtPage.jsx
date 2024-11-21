// import React from "react";
// import KoiCard from "../../../../components/KoiCard/KoiCard";

// function KoiBoughtPage() {
//   return (
//     <div className="dashboard-main-container">
//       <div
//         className="test"
//         style={{
//           // marginLeft: 280,
//           display: "flex",
//           flexWrap: "wrap",
//           justifyContent: "space-evenly",
//           rowGap: 20,
//         }}
//       >
//         {Array.from({ length: 12 }).map((_, index) => (
//           <KoiCard key={index} />
//         ))}
//       </div>
//     </div>
//   );
// }

// export default KoiBoughtPage;

import React, { useEffect, useState } from "react";
import { Table, Button, Tag, Modal, Spin } from "antd";
import { ExclamationCircleOutlined } from "@ant-design/icons";
import { useSearchParams } from "react-router-dom";
// import { getOrders } from "../../../../services/OrderService";
import {
  getOrders,
  createPayment,
  updateOrderStatus,
} from "../../../../services/OrderService";

function KoiBoughtPage() {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchParams] = useSearchParams();

  // Xóa orderId khỏi localStorage khi vào trang
  useEffect(() => {
    const orderId = localStorage.getItem("orderId");
    if (orderId) {
      localStorage.removeItem("orderId");
      console.log("Đã xóa orderId khỏi localStorage.");
    }
  }, []); // Chỉ thực hiện một lần khi vào trang

  // Fetch orders data
  const fetchOrders = async () => {
    try {
      setLoading(true);
      const response = await getOrders();
      setOrders(response || []);
    } catch (error) {
      console.error("Error fetching orders:", error);
      Modal.error({
        title: "Lỗi tải đơn hàng",
        content: "Không thể tải dữ liệu đơn hàng. Vui lòng thử lại sau.",
      });
    } finally {
      setLoading(false);
    }
  };

  // Xử lý trạng thái thanh toán từ URL trả về
  const handlePaymentStatus = async () => {
    const responseCode = searchParams.get("vnp_ResponseCode"); // Mã phản hồi từ VNPay
    const orderId = searchParams.get("orderId"); // Lấy orderId từ URL

    if (!orderId || !responseCode) {
      console.warn("Thiếu thông tin orderId hoặc responseCode trong URL.");
      return;
    }

    const paymentStatus = responseCode === "00" ? "Paid" : "Fail"; // Kiểm tra trạng thái
    console.log(`Updating status for orderId ${orderId} to ${paymentStatus}`);

    try {
      await updateOrderStatus(orderId, paymentStatus); // Gọi API cập nhật trạng thái
      Modal.success({
        title: "Kết quả thanh toán",
        content:
          paymentStatus === "Paid"
            ? "Thanh toán thành công!"
            : "Thanh toán thất bại.",
      });
    } catch (error) {
      console.error("Error updating payment status:", error);
      Modal.error({
        title: "Lỗi cập nhật trạng thái",
        content: "Không thể cập nhật trạng thái thanh toán. Vui lòng thử lại.",
      });
    } finally {
      fetchOrders(); // Reload danh sách đơn hàng
    }
  };

  useEffect(() => {
    fetchOrders();
    handlePaymentStatus(); // Kiểm tra trạng thái thanh toán khi vào trang
  }, []);

  // Retry payment if the status is "Fail"
  const handleRetryPayment = async (record) => {
    Modal.confirm({
      title: "Xác nhận thanh toán lại",
      icon: <ExclamationCircleOutlined />,
      content: `Bạn có chắc muốn thanh toán lại cho đơn hàng ID ${record.orderId} không?`,
      okText: "Thanh toán lại",
      cancelText: "Hủy",
      onOk: async () => {
        try {
          const paymentResponse = await createPayment(
            record.subAmount,
            `Thanh toán lại cho đơn hàng ${record.orderId}`,
            record.orderId
          );

          if (paymentResponse?.paymentUrl) {
            window.location.href = paymentResponse.paymentUrl; // Chuyển hướng đến VNPay
          } else {
            throw new Error("Không nhận được URL thanh toán");
          }
        } catch (error) {
          console.error("Error during retry payment:", error);
          Modal.error({
            title: "Lỗi thanh toán lại",
            content: "Không thể tạo yêu cầu thanh toán. Vui lòng thử lại sau.",
          });
        }
      },
    });
  };

  const columns = [
    {
      title: "Order ID",
      dataIndex: "orderId",
      key: "orderId",
    },
    {
      title: "Order Date",
      dataIndex: "orderDate",
      key: "orderDate",
      render: (text) => new Date(text).toLocaleString("vi-VN"),
    },
    {
      title: "Sub Amount (₫)",
      dataIndex: "subAmount",
      key: "subAmount",
      render: (text) =>
        new Intl.NumberFormat("vi-VN", {
          style: "currency",
          currency: "VND",
        }).format(text),
    },
    {
      title: "Payment Status",
      dataIndex: "paymentStatus",
      key: "paymentStatus",
      render: (status) => {
        let color;
        switch (status) {
          case "Pending":
            color = "orange";
            break;
          case "Fail":
            color = "red";
            break;
          case "Paid":
            color = "green";
            break;
          default:
            color = "default";
        }
        return <Tag color={color}>{status}</Tag>;
      },
    },
    {
      title: "Actions",
      key: "actions",
      render: (_, record) => {
        if (record.paymentStatus === "Fail") {
          return (
            <Button type="primary" onClick={() => handleRetryPayment(record)}>
              Thanh toán lại
            </Button>
          );
        }
        return null;
      },
    },
  ];

  return (
    <div className="dashboard-main-container">
      <h2 style={{ marginBottom: 20 }}>Quản lý Thanh Toán</h2>
      {loading ? (
        <Spin tip="Đang tải dữ liệu..." size="large" />
      ) : (
        <Table
          dataSource={orders}
          columns={columns}
          rowKey="orderId"
          pagination={{
            pageSize: 5,
            showSizeChanger: true,
          }}
        />
      )}
    </div>
  );
}

export default KoiBoughtPage;
