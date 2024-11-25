import React, { useEffect, useState } from "react";
import {
  approveKoiRequest,
  getKoiPending,
} from "../../../../services/KoiService";
import "./NotificationPage.css";
import { Button, Card, Empty, Modal, message } from "antd";
import {
  approveCareRequest,
  getCareRequestPending,
  rejectCareRequest,
} from "../../../../services/CareRequestService";
import {
  approveConsignmentRequest,
  getConsignmentRequestPending,
} from "../../../../services/consignmentRequest";

function NotificationPage() {
  const [activeButton, setActiveButton] = useState(1);
  const [koiLs, setKoiLs] = useState([]);
  const [careRequest, setCareRequest] = useState([]);
  const [consignmentLs, setConsignment] = useState([]);
  const pageSize = 5;

  const handleButtonClick = (buttonId) => {
    setActiveButton(buttonId);
  };

  const fetchKoi = async () => {
    try {
      const res = await getKoiPending();
      setKoiLs(res.data);
    } catch (err) {
      console.error("Failed to fetch koi:", err);
    }
  };

  const fetchCareRequest = async () => {
    try {
      const res = await getCareRequestPending();
      setCareRequest(res.data);
    } catch (err) {
      console.error("Failed to fetch Care Request:", err);
    }
  };

  const fetchConsignmentRequest = async () => {
    try {
      const res = await getConsignmentRequestPending();
      console.log(res.data);

      setConsignment(res.data);
    } catch (err) {
      console.error("Failed to fetch Consignment Request:", err);
    }
  };

  const handleApproval = async (koiId, isApproved, closeModal) => {
    try {
      await approveKoiRequest(koiId, isApproved);
      message.success(
        `Koi ${isApproved ? "được phê duyệt" : "bị từ chối"} thành công!`
      );
      if (closeModal) closeModal(); // Đóng modal chi tiết
      fetchKoi(); // Fetch lại danh sách sau khi xử lý
    } catch (error) {
      console.error("Approval failed:", error);
      message.error("Xử lý thất bại. Vui lòng thử lại.");
    }
  };

  const handleCareRequestApproval = async (
    careRequestId,
    isApproved,
    closeModal
  ) => {
    try {
      if (isApproved) {
        await approveCareRequest(careRequestId);
        message.success("Yêu cầu chăm sóc đã được phê duyệt!");
      } else {
        await rejectCareRequest(careRequestId);
        message.success("Yêu cầu chăm sóc đã bị từ chối!");
      }
      if (closeModal) closeModal(); // Đóng modal nếu cần
      fetchCareRequest(); // Fetch lại danh sách yêu cầu sau khi xử lý
    } catch (error) {
      console.error("Approval failed:", error);
      message.error("Xử lý thất bại. Vui lòng thử lại.");
    }
  };

  // const handleConsignmentRequestApproval = async (consignmentId, isApproved, closeModal) => {
  //   try {
  //     await approveConsignmentRequest(consignmentId, isApproved);
  //     message.success(
  //       `Koi ${isApproved ? "được phê duyệt" : "bị từ chối"} thành công!`
  //     );
  //     if (closeModal) closeModal(); // Đóng modal chi tiết
  //     fetchKoi(); // Fetch lại danh sách sau khi xử lý
  //   } catch (error) {
  //     console.error("Approval failed:", error);
  //     message.error("Xử lý thất bại. Vui lòng thử lại.");
  //   }
  // };

  const handleConsignmentRequestApproval = async (
    consignmentId,
    isApproved,
    closeModal
  ) => {
    try {
      await approveConsignmentRequest(consignmentId, isApproved);
      message.success(
        `Consignment ${
          isApproved ? "được phê duyệt" : "bị từ chối"
        } thành công!`
      );
      if (closeModal) closeModal(); // Đóng modal chi tiết
      fetchConsignmentRequest(); // Fetch lại danh sách sau khi xử lý
    } catch (error) {
      console.error("Approval failed:", error);
      message.error("Xử lý thất bại. Vui lòng thử lại.");
    }
  };

  useEffect(() => {
    fetchKoi();
    fetchCareRequest();
    fetchConsignmentRequest();
  }, []);

  return (
    <div className="notification-container">
      <div className="notification-btn">
        <span
          onClick={() => handleButtonClick(1)}
          className={activeButton === 1 ? "notification-btn-active" : ""}
        >
          Kois
        </span>
        <span
          onClick={() => handleButtonClick(2)}
          className={activeButton === 2 ? "notification-btn-active" : ""}
        >
          Consignment Requests
        </span>
        <span
          onClick={() => handleButtonClick(3)}
          className={activeButton === 3 ? "notification-btn-active" : ""}
        >
          Care Requests
        </span>
      </div>
      <div className="notification-wrapper">
        {/* {activeButton === 1 &&
          koiLs.map((koi) => (
            <KoiCheckApprove
              key={koi.koiId}
              koi={koi}
              onApprove={handleApproval}
            />
          ))} */}

        {activeButton === 1 &&
          (koiLs.length > 0 ? (
            koiLs.map((koi) => (
              <KoiCheckApprove
                key={koi.koiId}
                koi={koi}
                onApprove={handleApproval}
              />
            ))
          ) : (
            <Empty description="Không có koi nào đang chờ duyệt" />
          ))}

        {activeButton === 2 &&
          (consignmentLs.length > 0 ? (
            consignmentLs.map((consignment) => (
              <ConsignmentRequestApprove
                key={consignment.consignmentId}
                consignmentRequest={consignment}
                onApprove={handleConsignmentRequestApproval}
              />
            ))
          ) : (
            <Empty description="Không có koi nào đang chờ duyệt" />
          ))}

        {activeButton === 3 &&
          (careRequest.length > 0 ? (
            careRequest.map((care) => (
              <CareRequestApprove
                key={care.careRequestId}
                careRequest={care}
                onApprove={handleCareRequestApproval}
              />
            ))
          ) : (
            <Empty description="Không có koi nào đang chờ duyệt" />
          ))}
      </div>
    </div>
  );
}

const KoiCheckApprove = ({ koi, onApprove }) => {
  const [isModalVisible, setIsModalVisible] = useState(false);

  const handleDetailClick = () => {
    setIsModalVisible(true);
  };

  const handleModalClose = () => {
    setIsModalVisible(false);
  };

  const showConfirmModal = (isApproved) => {
    Modal.confirm({
      title: `Xác nhận ${isApproved ? "duyệt" : "từ chối"}?`,
      content: `Bạn có chắc chắn muốn ${isApproved ? "duyệt" : "từ chối"} koi ${
        koi.koiTypeName
      }?`,
      okText: "Xác nhận",
      cancelText: "Hủy",
      onOk: () => onApprove(koi.koiId, isApproved, handleModalClose),
    });
  };

  return (
    <Card
      className="koi-card"
      bodyStyle={{ display: "flex", alignItems: "center" }}
    >
      <img src={koi.image} alt={koi.koiTypeName} className="koi-card-image" />
      <div className="koi-card-info">
        <h3>{koi.koiTypeName}</h3>
        <p>Origin: {koi.origin}</p>
      </div>
      <div className="koi-card-actions">
        <Button type="primary" onClick={handleDetailClick}>
          Detail
        </Button>
        <Button type="default" onClick={() => showConfirmModal(true)}>
          Approve
        </Button>
        <Button danger onClick={() => showConfirmModal(false)}>
          Reject
        </Button>
      </div>
      <Modal
        title={`Details of ${koi.koiTypeName}`}
        open={isModalVisible}
        onCancel={handleModalClose}
        footer={[
          <Button key="close" onClick={handleModalClose}>
            Close
          </Button>,
        ]}
      >
        <p>
          <strong>Origin:</strong> {koi.origin}
        </p>
        <p>
          <strong>Price:</strong> {koi.price} VND
        </p>
        <p>
          <strong>Gender:</strong> {koi.gender === 0 ? "Male" : "Female"}
        </p>
        <p>
          <strong>Age:</strong> {koi.age} years
        </p>
        <p>
          <strong>Size:</strong> {koi.size} cm
        </p>
        <p>
          <strong>Generation:</strong> {koi.generation}
        </p>
        <p>
          <strong>Status:</strong> {koi.status}
        </p>
        <img
          src={koi.certificate}
          alt="Certificate"
          style={{ width: "100%", marginTop: "1rem" }}
        />
      </Modal>
    </Card>
  );
};

const CareRequestApprove = ({ careRequest, onApprove }) => {
  const showConfirmModal = (isApproved) => {
    Modal.confirm({
      title: `Xác nhận ${isApproved ? "duyệt" : "từ chối"}?`,
      content: `Bạn có chắc chắn muốn ${
        isApproved ? "duyệt" : "từ chối"
      } yêu cầu chăm sóc từ khách hàng ${careRequest.createdBy} cho koi có ID ${
        careRequest.koiId
      }?`,
      okText: "Xác nhận",
      cancelText: "Hủy",
      onOk: () => onApprove(careRequest.careRequestId, isApproved),
    });
  };

  return (
    <Card
      className="care-request-card"
      bodyStyle={{
        display: "flex",
        alignItems: "center",
        // justifyContent: "space-between",
      }}
    >
      <div className="care-request-info">
        <span>
          <h3>{`Koi Type Name: ${careRequest.koiTypeName}`}</h3>
          <p>
            <strong>Khách hàng:</strong> {careRequest.createdBy}
          </p>
        </span>
        <span>
          <p>
            <strong>Gói chăm sóc:</strong> {careRequest.carePlan.name}
          </p>
          <p>
            <strong>Giá:</strong>{" "}
            {new Intl.NumberFormat("vi-VN", {
              style: "currency",
              currency: "VND",
            }).format(careRequest.carePlan.price)}
          </p>
        </span>
      </div>
      <div className="care-request-actions">
        <Button type="default" onClick={() => showConfirmModal(true)}>
          Approve
        </Button>
        <Button danger onClick={() => showConfirmModal(false)}>
          Reject
        </Button>
      </div>
    </Card>
  );
};

const ConsignmentRequestApprove = ({ consignmentRequest, onApprove }) => {
  // Hàm hiển thị modal xác nhận phê duyệt hoặc từ chối
  const showConfirmModal = (isApproved) => {
    Modal.confirm({
      title: `Xác nhận ${isApproved ? "duyệt" : "từ chối"} yêu cầu gửi hàng?`,
      content: `Bạn có chắc chắn muốn ${
        isApproved ? "duyệt" : "từ chối"
      } yêu cầu gửi hàng có ID ${consignmentRequest.consignmentId}?`,
      okText: "Xác nhận",
      cancelText: "Hủy",
      onOk: () => onApprove(consignmentRequest.consignmentId, isApproved),
    });
  };

  return (
    <Card
      className="consignment-request-card"
      bodyStyle={{
        display: "flex",
        alignItems: "center",
      }}
    >
      <div className="consignment-request-info">
        <h3>{`Consignment Request ID: ${consignmentRequest.consignmentId}`}</h3>
        {/* <p>
          <strong>Khách hàng:</strong> {consignmentRequest.customerName}
        </p> */}
        <p>
          <strong>Koi ID:</strong> {consignmentRequest.koiId}
        </p>
        <p>
          <strong>Giá Koi:</strong>{" "}
          {new Intl.NumberFormat("vi-VN", {
            style: "currency",
            currency: "VND",
          }).format(consignmentRequest.argredSalePrice)}
        </p>
        <p>
          <strong>Start Date:</strong> {consignmentRequest.startDate}
        </p>
        <p>
          <strong>End Date:</strong> {consignmentRequest.endDate}
        </p>
      </div>
      <div className="consignment-request-actions">
        <Button type="default" onClick={() => showConfirmModal(true)}>
          Approve
        </Button>
        <Button danger onClick={() => showConfirmModal(false)}>
          Reject
        </Button>
      </div>
    </Card>
  );
};

export default NotificationPage;
