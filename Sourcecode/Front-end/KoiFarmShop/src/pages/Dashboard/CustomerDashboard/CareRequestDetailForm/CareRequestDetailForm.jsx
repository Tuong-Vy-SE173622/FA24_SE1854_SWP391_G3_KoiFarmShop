// CareRequestDetailForm.js
import React, { useEffect } from "react";
import { Form, Input, Button, message } from "antd";
import { useNavigate, useParams } from "react-router-dom";
import { createCareRequestDetail } from "../../../../services/CareRequestService";
// import { createCareRequestDetail } from "../../../../services/CareRequestService";

const CareRequestDetailForm = () => {
  const [form] = Form.useForm();
  const navigate = useNavigate();
  const { careRequestID } = useParams(); // Lấy requestId từ URL

  const onFinish = async (values) => {
    try {
      const response = await createCareRequestDetail({
        requestId: careRequestID, // Sử dụng requestId từ URL
        image: 0, // Mặc định là 0
        careMethod: values.careMethod,
        status: values.status,
        note: values.note,
        createdBy: values.createdBy,
      });

      alert("Gửi chi tiết yêu cầu chăm sóc thành công!");
      navigate(`/dashboard/care-request`); // Điều hướng về dashboard hoặc trang khác
      form.resetFields();
    } catch (error) {
      message.error("Có lỗi xảy ra, vui lòng thử lại!");
      console.error(error);
    }
  };

  return (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        padding: "24px",
        backgroundColor: "#f0f2f5",
      }}
    >
      <Form
        form={form}
        layout="horizontal"
        labelCol={{ span: 6 }}
        wrapperCol={{ span: 18 }}
        onFinish={onFinish}
        initialValues={{
          careMethod: 1, // Giá trị mặc định cho phương thức chăm sóc nếu cần
          status: "",
          note: "",
          createdBy: "",
        }}
        style={{
          padding: "24px",
          background: "white",
          borderRadius: "8px",
          boxShadow: "0 4px 12px rgba(0, 0, 0, 0.1)",
          maxWidth: "500px",
          width: "100%",
        }}
      >
        <h1
          style={{
            fontSize: 18,
            fontWeight: 600,
            textAlign: "center",
            marginBottom: 20,
          }}
        >
          Form Chi Tiết Yêu Cầu Chăm Sóc
        </h1>

        <Form.Item
          name="careMethod"
          label="Care Method"
          rules={[{ required: true, message: "Vui lòng nhập Care Method!" }]}
          style={{ marginBottom: 0 }}
        >
          <Input type="number" />
        </Form.Item>

        <Form.Item
          name="status"
          label="Status"
          rules={[{ required: true, message: "Vui lòng nhập Status!" }]}
          style={{ marginBottom: 0 }}
        >
          <Input />
        </Form.Item>

        <Form.Item
          name="note"
          label="Note"
          rules={[{ required: true, message: "Vui lòng nhập Note!" }]}
          style={{ marginBottom: 0 }}
        >
          <Input />
        </Form.Item>

        <Form.Item
          name="createdBy"
          label="Created By"
          rules={[{ required: true, message: "Vui lòng nhập Created By!" }]}
          style={{ marginBottom: 0 }}
        >
          <Input />
        </Form.Item>

        <Form.Item
          wrapperCol={{ offset: 6, span: 18 }}
          style={{ marginBottom: 0 }}
        >
          <Button type="primary" htmlType="submit">
            Gửi Chi Tiết Yêu Cầu
          </Button>
        </Form.Item>
      </Form>
    </div>
  );
};

export default CareRequestDetailForm;
