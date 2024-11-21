import React, { useState, useEffect } from "react";
import { Modal, Form, Input, InputNumber, Select, Upload, Button } from "antd";
import { UploadOutlined } from "@ant-design/icons";
import { createKoi } from "../../services/KoiService";
import { getAllKoiType, getKoiType } from "../../services/KoiTypeService";

const { Option } = Select;

const CreateKoiForm = ({ visible, onCancel, onSuccess }) => {
  const [form] = Form.useForm();
  const [loading, setLoading] = useState(false);
  const [koiTypes, setKoiTypes] = useState([]); // Danh sách loại cá Koi

  // Fetch danh sách loại cá Koi khi component được mount
  useEffect(() => {
    const fetchKoiTypes = async () => {
      try {
        const response = await getKoiType();
        if (response && Array.isArray(response)) {
          console.log("Fetched Koi Types:", response); // Debug dữ liệu
          setKoiTypes(response);
        } else {
          console.error("Invalid data format from API");
        }
      } catch (error) {
        console.error("Error fetching koi types:", error);
        Modal.error({
          title: "Lỗi tải loại cá Koi",
          content: "Không thể tải danh sách loại cá Koi. Vui lòng thử lại.",
        });
      }
    };

    fetchKoiTypes();
  }, []);

  const handleSubmit = async (values) => {
    setLoading(true);
    try {
      const formData = new FormData();
      formData.append("KoiTypeId", values.koiTypeId);
      formData.append("Origin", values.origin);
      formData.append("Gender", values.gender);
      formData.append("Age", values.age);
      formData.append("Size", values.size);
      formData.append("Price", values.price);
      formData.append("Characteristics", values.characteristics);
      formData.append("IsImported", values.isImported || "");
      formData.append("Generation", values.generation || "");
      formData.append("Note", values.note || "");
      if (values.image?.file?.originFileObj) {
        formData.append("Image", values.image.file.originFileObj);
      }
      if (values.certificate?.file?.originFileObj) {
        formData.append("Certificate", values.certificate.file.originFileObj);
      }

      await createKoi(formData); // Call API
      Modal.success({
        title: "Tạo Cá Koi Thành Công",
        content: "Cá Koi của bạn đã được tạo và đang chờ duyệt bởi Admin.",
      });
      form.resetFields();
      onSuccess();
    } catch (error) {
      console.error("Error creating koi:", error);
      Modal.error({
        title: "Lỗi Tạo Cá Koi",
        content: "Không thể tạo cá Koi. Vui lòng thử lại.",
      });
    } finally {
      setLoading(false);
    }
  };

  return (
    <Modal open={visible} title="Tạo Cá Koi" onCancel={onCancel} footer={null}>
      <Form form={form} layout="vertical" onFinish={handleSubmit}>
        <Form.Item
          label="Loại Cá Koi (KoiTypeId)"
          name="koiTypeId"
          rules={[{ required: true, message: "Vui lòng chọn loại cá Koi" }]}
        >
          <Select
            placeholder="Chọn loại cá Koi"
            loading={koiTypes.length === 0}
          >
            {koiTypes.map((type) => (
              <Option key={type.koiTypeId} value={type.koiTypeId}>
                {type.name}
              </Option>
            ))}
          </Select>
        </Form.Item>
        <Form.Item
          label="Nguồn Gốc (Origin)"
          name="origin"
          rules={[{ required: true, message: "Vui lòng nhập nguồn gốc" }]}
        >
          <Input placeholder="Ví dụ: Japan" />
        </Form.Item>
        <Form.Item
          label="Giới Tính (Gender)"
          name="gender"
          rules={[{ required: true, message: "Vui lòng chọn giới tính" }]}
        >
          <Select placeholder="Chọn giới tính">
            <Option value={1}>Đực</Option>
            <Option value={2}>Cái</Option>
          </Select>
        </Form.Item>
        <Form.Item
          label="Tuổi (Age)"
          name="age"
          rules={[{ required: true, message: "Vui lòng nhập tuổi cá" }]}
        >
          <InputNumber min={0} style={{ width: "100%" }} />
        </Form.Item>
        <Form.Item
          label="Kích Cỡ (Size)"
          name="size"
          rules={[{ required: true, message: "Vui lòng nhập kích cỡ cá" }]}
        >
          <InputNumber min={0} style={{ width: "100%" }} />
        </Form.Item>
        <Form.Item
          label="Giá (Price)"
          name="price"
          rules={[{ required: true, message: "Vui lòng nhập giá" }]}
        >
          <InputNumber min={0} style={{ width: "100%" }} />
        </Form.Item>
        <Form.Item label="Đặc Điểm (Characteristics)" name="characteristics">
          <Input placeholder="Nhập đặc điểm cá" />
        </Form.Item>
        <Form.Item label="Nhập Khẩu (IsImported)" name="isImported">
          <Select placeholder="Cá Nhập Khẩu?">
            <Option value={true}>Có</Option>
            <Option value={false}>Không</Option>
          </Select>
        </Form.Item>
        <Form.Item label="Thế Hệ (Generation)" name="generation">
          <Input placeholder="Ví dụ: F1" />
        </Form.Item>
        <Form.Item label="Ghi Chú (Note)" name="note">
          <Input.TextArea placeholder="Ghi chú về cá" />
        </Form.Item>
        <Form.Item label="Ảnh Cá (Image)" name="image">
          <Upload maxCount={1} beforeUpload={() => false} listType="picture">
            <Button icon={<UploadOutlined />}>Tải lên ảnh</Button>
          </Upload>
        </Form.Item>
        <Form.Item label="Giấy Chứng Nhận (Certificate)" name="certificate">
          <Upload maxCount={1} beforeUpload={() => false} listType="picture">
            <Button icon={<UploadOutlined />}>Tải lên giấy chứng nhận</Button>
          </Upload>
        </Form.Item>
        <Form.Item>
          <Button type="primary" htmlType="submit" loading={loading}>
            Tạo Cá Koi
          </Button>
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default CreateKoiForm;
