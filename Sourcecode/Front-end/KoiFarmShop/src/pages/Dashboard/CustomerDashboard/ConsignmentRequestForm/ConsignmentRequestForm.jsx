// import React, { useState } from "react";
// import { useNavigate } from "react-router-dom";
// import "./ConsignmentRequestForm.css";
// import { createConsignmentRequestForm } from "../../../../services/consignmentRequest";

// function ConsignmentRequestForm() {
//   const [request, setRequest] = useState({
//     customerId: "",
//     paymentMethod: "",
//     note: "",
//     status: "",
//     isOnline: false,
//   });

//   const navigate = useNavigate();

//   const handleChange = (e) => {
//     const { name, value, type, checked } = e.target;
//     setRequest((prevRequest) => ({
//       ...prevRequest,
//       [name]: type === "checkbox" ? checked : value,
//     }));
//   };

//   const handleSubmit = async (e) => {
//     e.preventDefault();
//     try {
//       const data = await createConsignmentRequestForm(request);
//       console.log("API response for consignment request:", data);
//       navigate(`/dashboard/consignment-detail/${data.consignmentId}`);
//     } catch (error) {
//       console.error("Lỗi khi gửi yêu cầu ký gửi:", error);

//       alert("Đã xảy ra lỗi trong quá trình gửi yêu cầu ký gửi");
//     }
//   };

//   return (
//     <div>
//       <form className="crf-form" onSubmit={handleSubmit}>
//         <input
//           type="number"
//           name="customerId"
//           placeholder="Mã Khách Hàng"
//           value={request.customerId}
//           onChange={handleChange}
//         />
//         <input
//           type="text"
//           name="paymentMethod"
//           placeholder="Phương Thức Thanh Toán"
//           value={request.paymentMethod}
//           onChange={handleChange}
//         />
//         <input
//           type="text"
//           name="note"
//           placeholder="Ghi Chú"
//           value={request.note}
//           onChange={handleChange}
//         />
//         <input
//           type="text"
//           name="status"
//           placeholder="Trạng Thái"
//           value={request.status}
//           onChange={handleChange}
//         />
//         <label>
//           Yêu Cầu Trực Tuyến
//           <input
//             type="checkbox"
//             name="isOnline"
//             checked={request.isOnline}
//             onChange={handleChange}
//           />
//         </label>
//         <button type="submit">Gửi Yêu Cầu Ký Gửi</button>
//       </form>
//     </div>
//   );
// }

// export default ConsignmentRequestForm;

import React, { useState, useEffect } from "react";
import {
  Modal,
  Select,
  Button,
  Input,
  Checkbox,
  DatePicker,
  InputNumber,
  Form,
} from "antd";
import { createConsignmentRequest } from "../../../../services/consignmentRequest";
// import { getKoiByCustomerId } from "../../../../services/koiService";
import CreateKoiForm from "../../../../components/KoiModal/CreateKoiModal";
import moment from "moment";
import { getKoiByCustomerId } from "../../../../services/KoiService";

const { Option } = Select;

function ConsignmentRequestForm() {
  const [form] = Form.useForm();
  const [koiList, setKoiList] = useState([]); // Danh sách cá Koi
  const [loading, setLoading] = useState(false); // Trạng thái tải
  const [isModalVisible, setIsModalVisible] = useState(false); // Trạng thái hiển thị modal

  // Lấy danh sách cá Koi của user
  const fetchKoiList = async () => {
    setLoading(true);
    try {
      const userId = localStorage.getItem("customerId");
      const response = await getKoiByCustomerId(userId);
      const approvedKoiList = (response.data || []).filter(
        (koi) => koi.status === "APPROVED"
      );
      setKoiList(approvedKoiList); // Lưu lại danh sách đã lọc
    } catch (error) {
      console.error("Error fetching koi list:", error);
      Modal.error({
        title: "Lỗi tải danh sách cá Koi",
        content: "Không thể tải danh sách cá Koi của bạn.",
      });
      setKoiList([]); // Trong trường hợp lỗi, gán koiList là mảng rỗng
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchKoiList(); // Tải danh sách cá Koi khi component được mount
  }, []);

  // Xử lý hiển thị modal tạo cá Koi
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

  // Gửi yêu cầu ký gửi
  const handleSubmit = async (values) => {
    try {
      const payload = {
        customerId: localStorage.getItem("customerId"),
        koiId: values.koiId,
        argredSalePrice: values.argredSalePrice,
        startDate: values.startDate.format("YYYY-MM-DD"),
        endDate: values.endDate.format("YYYY-MM-DD"),
        note: values.note || "",
      };

      await createConsignmentRequest(payload);

      Modal.success({
        title: "Thành công",
        content: "Yêu cầu ký gửi đã được gửi thành công.",
      });

      form.resetFields();
    } catch (error) {
      console.error("Lỗi khi gửi yêu cầu ký gửi:", error);
      Modal.error({
        title: "Lỗi",
        content: "Không thể gửi yêu cầu ký gửi. Vui lòng thử lại.",
      });
    }
  };

  return (
    <div>
      <h2>Gửi Yêu Cầu Ký Gửi</h2>

      <div className="action-buttons">
        <Button
          type="primary"
          onClick={handleOpenModal}
          style={{ marginBottom: 16 }}
        >
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
        style={{ maxWidth: 600, margin: "0 auto" }}
      >
        <Form.Item
          name="koiId"
          label="Chọn Cá Koi"
          rules={[{ required: true, message: "Vui lòng chọn một cá Koi" }]}
        >
          <Select placeholder="Chọn Cá Koi" loading={loading}>
            {koiList.map((koi) => (
              <Option key={koi.koiId} value={koi.koiId}>
                {`ID: ${koi.koiId} - ${koi.origin} - ${koi.characteristics}`}
              </Option>
            ))}
          </Select>
        </Form.Item>
        <Form.Item
          name="argredSalePrice"
          label="Giá Bán Thỏa Thuận"
          rules={[
            { required: true, message: "Vui lòng nhập giá bán thỏa thuận" },
          ]}
        >
          <InputNumber
            placeholder="Nhập giá bán thỏa thuận"
            style={{ width: "100%" }}
          />
        </Form.Item>
        <Form.Item
          name="startDate"
          label="Ngày Bắt Đầu"
          rules={[{ required: true, message: "Vui lòng chọn ngày bắt đầu" }]}
        >
          <DatePicker
            placeholder="Chọn ngày bắt đầu"
            style={{ width: "100%" }}
            disabledDate={(current) =>
              current && current < moment().startOf("day")
            }
          />
        </Form.Item>
        <Form.Item
          name="endDate"
          label="Ngày Kết Thúc"
          rules={[{ required: true, message: "Vui lòng chọn ngày kết thúc" }]}
        >
          <DatePicker
            placeholder="Chọn ngày kết thúc"
            style={{ width: "100%" }}
            disabledDate={(current) =>
              current && current < moment().startOf("day")
            }
          />
        </Form.Item>
        <Form.Item name="note" label="Ghi Chú">
          <Input.TextArea placeholder="Nhập ghi chú (nếu có)" />
        </Form.Item>
        <Button type="primary" htmlType="submit" style={{ marginTop: 16 }}>
          Gửi Yêu Cầu
        </Button>
      </Form>
    </div>
  );
}

export default ConsignmentRequestForm;
