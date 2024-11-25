// // // CareRequestForm.js
// // import React from "react";
// // import { Form, Input, Button, message } from "antd";
// // import { createCareRequest } from "../../../../services/CareRequestService";
// // import { useNavigate } from "react-router-dom";

// // const CareRequestForm = () => {
// //   const [form] = Form.useForm();
// //   const navigate = useNavigate();

// //   const onFinish = async (values) => {
// //     try {
// //       const response = await createCareRequest({
// //         customerId: values.customerId,
// //         koiId: values.koiId,
// //         status: values.status,
// //         note: values.note,
// //         createdBy: values.createdBy,
// //       });
// //       console.log(response);

// //       alert("Gửi yêu cầu chăm sóc thành công!");
// //       console.log("Navigating to requestId:", response.requestId);
// //       navigate(`/dashboard/care-request-detail/${response.requestId}`);
// //       form.resetFields();
// //     } catch (error) {
// //       message.error("Có lỗi xảy ra, vui lòng thử lại!");
// //       console.error(error);
// //     }
// //   };

// //   return (
// //     <div
// //       style={{
// //         display: "flex",
// //         justifyContent: "center",
// //         // alignItems: "center",
// //         // minHeight: "100vh",
// //         padding: "24px",
// //         backgroundColor: "#f0f2f5",
// //       }}
// //     >
// //       <Form
// //         form={form}
// //         layout="horizontal"
// //         labelCol={{ span: 6 }}
// //         wrapperCol={{ span: 18 }}
// //         onFinish={onFinish}
// //         initialValues={{
// //           customerId: 1,
// //           koiId: 0,
// //           status: "",
// //           note: "",
// //           createdBy: "",
// //         }}
// //         style={{
// //           padding: "24px",
// //           background: "white",
// //           borderRadius: "8px",
// //           boxShadow: "0 4px 12px rgba(0, 0, 0, 0.1)",
// //           maxWidth: "500px",
// //           width: "100%",
// //         }}
// //       >
// //         <h1
// //           style={{
// //             fontSize: 18,
// //             fontWeight: 600,
// //             textAlign: "center",
// //             marginBottom: 20,
// //           }}
// //         >
// //           Form yêu cầu chăm sóc
// //         </h1>
// //         {/* <Form.Item
// //           name="customerId"
// //           label="Customer ID"
// //           rules={[{ required: true, message: "Vui lòng nhập Customer ID!" }]}
// //           style={{ marginBottom: 0 }} // Tắt margin bottom
// //         >
// //           <Input type="number" />
// //         </Form.Item>

// //         <Form.Item
// //           name="koiId"
// //           label="Koi ID"
// //           rules={[{ required: true, message: "Vui lòng nhập Koi ID!" }]}
// //           style={{ marginBottom: 0 }}
// //         >
// //           <Input type="number" />
// //         </Form.Item> */}

// //         <Form.Item
// //           name="customerId"
// //           label="Customer ID"
// //           rules={[
// //             { required: true, message: "Vui lòng nhập Customer ID!" },
// //             {
// //               validator: (_, value) =>
// //                 value > 0
// //                   ? Promise.resolve()
// //                   : Promise.reject("Customer ID phải lớn hơn 0!"),
// //             },
// //           ]}
// //           style={{ marginBottom: 0 }}
// //         >
// //           <Input type="number" />
// //         </Form.Item>

// //         <Form.Item
// //           name="koiId"
// //           label="Koi ID"
// //           rules={[
// //             { required: true, message: "Vui lòng nhập Koi ID!" },
// //             {
// //               validator: (_, value) =>
// //                 value > 0
// //                   ? Promise.resolve()
// //                   : Promise.reject("Koi ID phải lớn hơn 0!"),
// //             },
// //           ]}
// //           style={{ marginBottom: 0 }}
// //         >
// //           <Input type="number" />
// //         </Form.Item>
// //         <Form.Item
// //           name="status"
// //           label="Status"
// //           rules={[{ required: true, message: "Vui lòng nhập Status!" }]}
// //           style={{ marginBottom: 0 }}
// //         >
// //           <Input />
// //         </Form.Item>

// //         <Form.Item
// //           name="note"
// //           label="Note"
// //           rules={[{ required: true, message: "Vui lòng nhập Note!" }]}
// //           style={{ marginBottom: 0 }}
// //         >
// //           <Input />
// //         </Form.Item>

// //         <Form.Item
// //           name="createdBy"
// //           label="Created By"
// //           rules={[{ required: true, message: "Vui lòng nhập Created By!" }]}
// //           style={{ marginBottom: 0 }}
// //         >
// //           <Input />
// //         </Form.Item>

// //         <Form.Item
// //           wrapperCol={{ offset: 6, span: 18 }}
// //           style={{ marginBottom: 0 }}
// //         >
// //           <Button type="primary" htmlType="submit">
// //             Gửi Yêu Cầu
// //           </Button>
// //         </Form.Item>
// //       </Form>
// //     </div>
// //   );
// // };

// // export default CareRequestForm;

// import { Form } from "antd";
// import React, { useEffect, useState } from "react";
// import { getKoiByCustomerId } from "../../../../services/KoiService";
// import { getCarePlan } from "../../../../services/CarePlanService";

// function CareRequestForm() {
//   const [form] = Form.useForm();
//   const [koiList, setKoiList] = useState([]);
//   const [loading, setLoading] = useState(false);
//   const [carePlanLs, setCarePlanLs] = useState([]);

//   const fetchKoiList = async () => {
//     console.log("Fetching koi list...");
//     setLoading(true);
//     try {
//       const userId = localStorage.getItem("customerId");
//       console.log("User ID:", userId);

//       const response = await getKoiByCustomerId(userId);
//       console.log("API Response:", response);

//       // Lọc các cá Koi có trạng thái "APPROVED"
//       const approvedKoiList = (response.data || []).filter(
//         (koi) => koi.status === "APPROVED"
//       );
//       console.log("Filtered koi list:", approvedKoiList);

//       setKoiList(approvedKoiList);
//     } catch (error) {
//       console.error("Error fetching koi list:", error);
//       Modal.error({
//         title: "Lỗi tải danh sách cá Koi",
//         content: "Không thể tải danh sách cá Koi của bạn.",
//       });
//       setKoiList([]);
//     } finally {
//       setLoading(false);
//     }
//   };

//   const fetchCarePlan = async () => {
//     try {
//       const res = await getCarePlan();
//       setCarePlanLs(res);
//     } catch (err) {
//       console.error("Error fetching care plan list:", err);
//       Modal.error({
//         title: "Lỗi tải danh sách carePlan",
//         content: "Không thể tải danh sách carePlan cho bạn.",
//       });
//       setKoiList([]);
//     }
//   };

//   useEffect(() => {
//     fetchKoiList();
//     fetchCarePlan();
//   }, []);
//   return <div>CareRequestForm</div>;
// }

// export default CareRequestForm;

import React, { useEffect, useState } from "react";
import { Form, Select, Button, Input, Modal, Spin } from "antd";
import { getKoiByCustomerId } from "../../../../services/KoiService";
import { getCarePlan } from "../../../../services/CarePlanService";
import { createCareRequest } from "../../../../services/CareRequestService";
import moment from "moment";
import CreateKoiForm from "../../../../components/KoiModal/CreateKoiModal";

const { Option } = Select;

function CareRequestForm() {
  const [form] = Form.useForm();
  const [koiList, setKoiList] = useState([]); // Danh sách cá Koi
  const [carePlanLs, setCarePlanLs] = useState([]); // Danh sách kế hoạch chăm sóc
  const [loading, setLoading] = useState(false); // Trạng thái tải dữ liệu
  const [isModalVisible, setIsModalVisible] = useState(false);

  const handleOpenModal = () => {
    setIsModalVisible(true);
  };

  const handleCloseModal = () => {
    setIsModalVisible(false);
  };

  const handleSuccessCreateKoi = () => {
    setIsModalVisible(false);
    fetchKoiList(); // Cập nhật danh sách cá Koi sau khi tạo thành công
  };

  // Lấy danh sách cá Koi
  const fetchKoiList = async () => {
    setLoading(true);
    try {
      const userId = localStorage.getItem("userID");
      const response = await getKoiByCustomerId(userId);

      const approvedKoiList = (response.data || []).filter(
        (koi) => koi.status === "APPROVED"
      );

      setKoiList(approvedKoiList);
    } catch (error) {
      Modal.error({
        title: "Lỗi tải danh sách cá Koi",
        content: "Không thể tải danh sách cá Koi của bạn.",
      });
    } finally {
      setLoading(false);
    }
  };

  // Lấy danh sách kế hoạch chăm sóc
  const fetchCarePlan = async () => {
    try {
      const response = await getCarePlan();
      setCarePlanLs(response || []);
    } catch (error) {
      Modal.error({
        title: "Lỗi tải danh sách kế hoạch chăm sóc",
        content: "Không thể tải danh sách kế hoạch chăm sóc.",
      });
    }
  };

  useEffect(() => {
    fetchKoiList();
    fetchCarePlan();
  }, []);

  // Gửi yêu cầu chăm sóc
  const handleSubmit = async (values) => {
    try {
      const payload = {
        customerId: localStorage.getItem("customerId"),
        koiId: values.koiId,
        carePlanId: values.carePlanId,
        note: values.note || "",
      };

      await createCareRequest(payload);

      Modal.success({
        title: "Thành công",
        content: "Yêu cầu chăm sóc đã được gửi thành công.",
      });

      form.resetFields();
    } catch (error) {
      Modal.error({
        title: "Lỗi",
        content: "Không thể gửi yêu cầu chăm sóc. Vui lòng thử lại.",
      });
    }
  };

  return (
    <div>
      <h2 style={{ textAlign: "center", marginBottom: "20px" }}>
        Gửi Yêu Cầu Chăm Sóc
      </h2>

      <div
        className="action-buttons"
        style={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          maxWidth: "600px",
          margin: "0 auto 20px auto", // Khoảng cách dưới giữa nút "Tạo Cá Koi" và form
        }}
      >
        <Button type="primary" onClick={handleOpenModal}>
          Tạo Cá Koi
        </Button>
        <CreateKoiForm
          visible={isModalVisible}
          onCancel={handleCloseModal}
          onSuccess={handleSuccessCreateKoi}
        />
      </div>

      <Form
        form={form}
        layout="vertical"
        onFinish={handleSubmit}
        style={{
          maxWidth: 600,
          margin: "0 auto",
          padding: "20px",
          border: "1px solid #ddd",
          borderRadius: "8px",
          backgroundColor: "#fff",
          boxShadow: "0 4px 10px rgba(0,0,0,0.1)",
        }}
      >
        <Form.Item
          name="koiId"
          label="Chọn Cá Koi"
          rules={[{ required: true, message: "Vui lòng chọn một cá Koi" }]}
        >
          <Select placeholder="Chọn Cá Koi">
            {koiList.map((koi) => (
              <Option key={koi.koiId} value={koi.koiId}>
                {`ID: ${koi.koiId} - ${koi.origin} - ${koi.characteristics}`}
              </Option>
            ))}
          </Select>
        </Form.Item>
        <Form.Item
          name="carePlanId"
          label="Chọn Kế Hoạch Chăm Sóc"
          rules={[
            {
              required: true,
              message: "Vui lòng chọn một kế hoạch chăm sóc",
            },
          ]}
        >
          <Select placeholder="Chọn Kế Hoạch Chăm Sóc">
            {carePlanLs.map((plan) => (
              <Option key={plan.carePlanId} value={plan.carePlanId}>
                {/* {`${plan.name} - ${plan.price}`} */}
                {`${plan.name} - ${new Intl.NumberFormat("vi-VN", {
                  style: "currency",
                  currency: "VND",
                }).format(plan.price)}`}
              </Option>
            ))}
          </Select>
        </Form.Item>
        <Form.Item name="note" label="Ghi Chú">
          <Input.TextArea placeholder="Nhập ghi chú (nếu có)" />
        </Form.Item>
        <Button type="primary" htmlType="submit" style={{ width: "100%" }}>
          Gửi Yêu Cầu
        </Button>
      </Form>
    </div>
  );
}

export default CareRequestForm;
