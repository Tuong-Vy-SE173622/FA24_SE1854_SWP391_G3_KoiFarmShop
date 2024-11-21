import React, { useEffect, useState } from "react";
import { Table, Button, Modal, Form, Input, Upload, message } from "antd";
import {
  PlusOutlined,
  EditOutlined,
  DeleteOutlined,
  UploadOutlined,
} from "@ant-design/icons";
import {
  createKoiType,
  deleteKoiType,
  getAllKoiType,
  updateKoiType,
} from "../../../../services/KoiTypeService";
import "./KoiTypePage.css";

const KoiTypePage = () => {
  const [koiTypes, setKoiTypes] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingKoi, setEditingKoi] = useState(null);
  const [form] = Form.useForm();
  const pageSize = 4;
  const [current, setCurrent] = useState(1);
  const [total, setTotal] = useState(0);
  const [fileList, setFileList] = useState([]);
  const [imageFile, setImageFile] = useState(null);

  const openModal = (record = null) => {
    setEditingKoi(record);
    setIsModalOpen(true);
    form.setFieldsValue(record || {});

    if (record?.image) {
      // Hiển thị ảnh cũ trong danh sách upload
      setFileList([
        {
          uid: "-1",
          name: "Current Image",
          status: "done",
          url: record.image, // URL của ảnh hiện tại
        },
      ]);
    } else {
      setFileList([]);
    }
  };

  const closeModal = () => {
    setIsModalOpen(false);
    form.resetFields();
  };

  const handleDelete = async (id) => {
    try {
      await deleteKoiType(id);
      setKoiTypes(koiTypes.filter((koi) => koi.koiTypeId !== id));
      fetchKoiTypes(current);
    } catch (error) {
      console.error("Failed to delete koi type:", error);
    }
  };

  const handleSave = () => {
    form.validateFields().then(async (values) => {
      try {
        const formData = new FormData();

        // Thêm các trường text vào formData
        Object.entries(values).forEach(([key, value]) => {
          if (value) formData.append(key, value);
        });

        // Thêm file ảnh vào formData
        if (imageFile) {
          formData.append("image", imageFile); // Key 'image' phải trùng với backend
        }

        if (editingKoi) {
          // Gọi API cập nhật
          await updateKoiType(editingKoi.koiTypeId, formData);
          setKoiTypes(
            koiTypes.map((koi) =>
              koi.koiTypeId === editingKoi.koiTypeId
                ? { ...editingKoi, ...values }
                : koi
            )
          );
        } else {
          // Gọi API tạo mới
          const newKoiType = await createKoiType(formData);
          setKoiTypes([...koiTypes, newKoiType]);
        }

        message.success("Koi type saved successfully!");
      } catch (error) {
        console.error("Failed to save koi type:", error);
        message.error("Failed to save koi type.");
      }
      closeModal();
      fetchKoiTypes(current);
    });
  };

  const columns = [
    {
      title: "Image",
      dataIndex: "image",
      width: "10%",
      key: "image",
      render: (image) => (
        <img src={image} alt="Koi" style={{ width: "50px", height: 60 }} />
      ),
    },
    { title: "Name", dataIndex: "name", key: "name", width: "10%" },
    {
      title: "Short Description",
      dataIndex: "shortDescription",
      key: "shortDescription",
      width: "18%",
      render: (text) => <div className="line-clamp-2">{text}</div>,
    },
    // { title: "Created By", dataIndex: "createdBy", key: "createdBy" },

    {
      title: "Origin History",
      dataIndex: "originHistory",
      key: "originHistory",
      width: "18%",
      render: (text) => <div className="line-clamp-2">{text}</div>,
    },
    {
      title: "Category Description",
      dataIndex: "categoryDescription",
      key: "categoryDescription",
      width: "18%",
      render: (text) => <div className="line-clamp-2">{text}</div>,
    },
    {
      title: "Feng Shui",
      dataIndex: "fengShui",
      key: "fengShui",
      width: "18%",
      render: (text) => <div className="line-clamp-2">{text}</div>,
    },
    {
      title: "Raising Condition",
      dataIndex: "raisingCondition",
      key: "raisingCondition",
      width: "18%",
      render: (text) => <div className="line-clamp-2">{text}</div>,
    },
    {
      title: "Actions",
      key: "actions",
      render: (record) => (
        <div style={{ display: "flex", flexWrap: "nowrap" }}>
          <Button
            icon={<EditOutlined />}
            onClick={() => openModal(record)}
            style={{ marginRight: 5 }}
          />
          <Button
            icon={<DeleteOutlined />}
            onClick={() => confirmDelete(record.koiTypeId)}
            danger
          />
        </div>
      ),
    },
  ];

  const beforeUpload = (file) => {
    const isImage = file.type.startsWith("image/");
    if (!isImage) {
      message.error("You can only upload image files!");
    }
    const isLt2M = file.size / 1024 / 1024 < 2;
    if (!isLt2M) {
      message.error("Image must be smaller than 2MB!");
    }
    return isImage && isLt2M;
  };

  const confirmDelete = (id) => {
    Modal.confirm({
      title: "Are you sure delete this koi type?",
      okText: "Yes",
      okType: "danger",
      cancelText: "No",
      onOk: () => handleDelete(id),
    });
  };

  const handleUploadChange = ({ file }) => {
    if (file.status === "removed") {
      setImageFile(null); // Xóa file khi người dùng xóa trong upload
      setFileList([]);
      // } else {
      //   setImageFile(file.originFileObj); // Lưu file gốc
      //   setFileList([file]);
      // }
    } else if (file.status === "done" || file.originFileObj) {
      setImageFile(file.originFileObj);
      setFileList([file]);
    }
  };

  const fetchKoiTypes = async (pageNumber) => {
    try {
      const data = await getAllKoiType(pageNumber, pageSize);
      setKoiTypes(data.data);
      setTotal(data.totalRecords);
    } catch (err) {
      console.error("Failed to fetch Koi Type data", err);
    }
  };

  useEffect(() => {
    fetchKoiTypes(current);
  }, [current]);

  return (
    <div className="admin-page-container">
      <Button
        type="primary"
        icon={<PlusOutlined />}
        onClick={() => openModal()}
        style={{ marginBottom: 16 }}
      >
        Add Koi Type
      </Button>

      <div className="table-style">
        <Table
          dataSource={koiTypes}
          columns={columns}
          rowKey="koiTypeId"
          pagination={{
            total: total,
            pageSize: pageSize,
            current: current,
            onChange: (page) => setCurrent(page),
          }}
          className="custom-table"
          style={{ width: 1200 }}
        />
      </div>

      <Modal
        title={editingKoi ? "Edit Koi Type" : "Add Koi Type"}
        open={isModalOpen}
        onCancel={closeModal}
        onOk={handleSave}
      >
        <Form form={form} layout="vertical">
          <Form.Item
            label="Name"
            name="name"
            rules={[{ required: true, message: "Please enter koi type name" }]}
          >
            <Input />
          </Form.Item>
          <Form.Item label="Short Description" name="shortDescription">
            <Input.TextArea />
          </Form.Item>

          <Form.Item label="Image">
            <Upload
              listType="picture"
              maxCount={1}
              beforeUpload={beforeUpload}
              fileList={fileList}
              onChange={handleUploadChange}
            >
              <Button icon={<UploadOutlined />}>Upload Image</Button>
            </Upload>
          </Form.Item>

          <Form.Item label="Origin History" name="originHistory">
            <Input.TextArea />
          </Form.Item>
          <Form.Item label="Category Description" name="categoryDescription">
            <Input.TextArea />
          </Form.Item>
          <Form.Item label="Feng Shui" name="fengShui">
            <Input.TextArea />
          </Form.Item>
          <Form.Item label="Raising Condition" name="raisingCondition">
            <Input.TextArea />
          </Form.Item>
          <Form.Item label="Note" name="note">
            <Input.TextArea />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default KoiTypePage;
