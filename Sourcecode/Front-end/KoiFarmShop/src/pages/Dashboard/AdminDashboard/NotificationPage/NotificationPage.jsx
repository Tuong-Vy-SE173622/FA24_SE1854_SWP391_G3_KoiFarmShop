// import React, { useEffect, useState } from "react";
// import {
//   approveKoiRequest,
//   getKoiPending,
// } from "../../../../services/KoiService";
// import "./NotificationPage.css";
// import { Button, Card, Modal } from "antd";

// function NotificationPage() {
//   const [activeButton, setActiveButton] = useState(1);
//   const [koiLs, setKoiLs] = useState([]);
//   const [total, setTotal] = useState(0);
//   const pageSize = 5;

//   const handleButtonClick = (buttonId) => {
//     setActiveButton(buttonId);
//   };

//   const fetchKoi = async () => {
//     try {
//       const res = await getKoiPending();
//       setKoiLs(res.data);
//     } catch (err) {
//       console.log("fetch koi to accept fail: ", err);
//     }
//   };

//   const handleApproval = async (koiId, isApproved, closeModal) => {
//     try {
//       await approveKoiRequest(koiId, isApproved);
//       message.success(
//         `Koi ${isApproved ? "được phê duyệt" : "bị từ chối"} thành công!`
//       );
//       if (closeModal) closeModal();
//       // Fetch lại danh sách sau khi phê duyệt
//       fetchKoi();
//     } catch (error) {
//       console.error("Approval failed:", error);
//       message.error("Xử lý thất bại. Vui lòng thử lại.");
//     }
//   };

//   useEffect(() => {
//     fetchKoi();
//   }, []);

//   return (
//     <div className="notification-container">
//       <div className="notification-btn">
//         <span
//           onClick={() => handleButtonClick(1)}
//           className={activeButton === 1 ? "notification-btn-active" : ""}
//         >
//           Kois
//         </span>
//         <span
//           onClick={() => handleButtonClick(2)}
//           className={activeButton === 2 ? "notification-btn-active" : ""}
//         >
//           Consignment Requests
//         </span>
//         <span
//           onClick={() => handleButtonClick(3)}
//           className={activeButton === 3 ? "notification-btn-active" : ""}
//         >
//           Care Requests
//         </span>
//       </div>
//       <div className="notification-wrapper">
//         {activeButton === 1 &&
//           koiLs.map((koi) => (
//             <KoiCheckApprove
//               key={koi.koiId}
//               koi={koi}
//               onApprove={handleApproval}
//             />
//           ))}

//         {activeButton === 2 && <div>Consignment Requests</div>}
//         {activeButton === 3 && <div>Care Requests</div>}
//       </div>
//     </div>
//   );
// }

// const KoiCheckApprove = ({ koi, onApprove }) => {
//   const [isModalVisible, setIsModalVisible] = useState(false);

//   const handleDetailClick = () => {
//     setIsModalVisible(true);
//   };

//   const handleModalClose = () => {
//     setIsModalVisible(false);
//   };

//   const showConfirmModal = (isApproved) => {
//     Modal.confirm({
//       title: `Xác nhận ${isApproved ? "duyệt" : "từ chối"}?`,
//       content: `Bạn có chắc chắn muốn ${isApproved ? "duyệt" : "từ chối"} koi ${
//         koi.koiTypeName
//       }?`,
//       okText: "Xác nhận",
//       cancelText: "Hủy",
//       onOk: () => onApprove(koi.koiId, isApproved, handleModalClose),
//     });
//   };

//   return (
//     <Card
//       className="koi-card"
//       bodyStyle={{ display: "flex", alignItems: "center" }}
//     >
//       <img src={koi.image} alt={koi.koiTypeName} className="koi-card-image" />
//       <div className="koi-card-info">
//         <h3>{koi.koiTypeName}</h3>
//         <p>Origin: {koi.origin}</p>
//       </div>
//       <div className="koi-card-actions">
//         <Button type="primary" onClick={handleDetailClick}>
//           Detail
//         </Button>
//         <Button type="default" onClick={() => showConfirmModal(true)}>
//           Approve
//         </Button>
//         <Button danger onClick={() => showConfirmModal(false)}>
//           Reject
//         </Button>
//       </div>
//       <Modal
//         title={`Details of ${koi.koiTypeName}`}
//         open={isModalVisible}
//         onCancel={handleModalClose}
//         footer={null}
//       >
//         <p>
//           <strong>Origin:</strong> {koi.origin}
//         </p>
//         <p>
//           <strong>Price:</strong> {koi.price} VND
//         </p>
//         <p>
//           <strong>Gender:</strong> {koi.gender === 0 ? "Male" : "Female"}
//         </p>
//         <p>
//           <strong>Age:</strong> {koi.age} years
//         </p>
//         <p>
//           <strong>Size:</strong> {koi.size} cm
//         </p>
//         <p>
//           <strong>Generation:</strong> {koi.generation}
//         </p>
//         <p>
//           <strong>Status:</strong> {koi.status}
//         </p>
//         <img
//           src={koi.certificate}
//           alt="Certificate"
//           style={{ width: "100%", marginTop: "1rem" }}
//         />
//       </Modal>
//     </Card>
//   );
// };

// export default NotificationPage;
import React, { useEffect, useState } from "react";
import {
  approveKoiRequest,
  getKoiPending,
} from "../../../../services/KoiService";
import "./NotificationPage.css";
import { Button, Card, Empty, Modal, message } from "antd";

function NotificationPage() {
  const [activeButton, setActiveButton] = useState(1);
  const [koiLs, setKoiLs] = useState([]);
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

  useEffect(() => {
    fetchKoi();
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

        {activeButton === 2 && <div>Consignment Requests</div>}
        {activeButton === 3 && <div>Care Requests</div>}
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

export default NotificationPage;
