import React, { useEffect, useState } from "react";
import { Table, Button, Modal, Form, Input } from "antd";
import { PlusOutlined, EditOutlined, DeleteOutlined } from "@ant-design/icons";
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
  const pageSize = 5;

  const openModal = (record = null) => {
    setEditingKoi(record);
    setIsModalOpen(true);
    form.setFieldsValue(record || {});
  };

  const closeModal = () => {
    setIsModalOpen(false);
    form.resetFields();
  };

  const handleDelete = async (id) => {
    try {
      await deleteKoiType(id);
      setKoiTypes(koiTypes.filter((koi) => koi.koiTypeId !== id));
    } catch (error) {
      console.error("Failed to delete koi type:", error);
    }
  };

  const handleSave = () => {
    form.validateFields().then(async (values) => {
      try {
        if (editingKoi) {
          await updateKoiType({ ...editingKoi, ...values });
          setKoiTypes(
            koiTypes.map((koi) =>
              koi.koiTypeId === editingKoi.koiTypeId
                ? { ...editingKoi, ...values }
                : koi
            )
          );
        } else {
          const newKoiType = await createKoiType(values);
          setKoiTypes([...koiTypes, newKoiType]);
        }
      } catch (error) {
        console.error("Failed to save koi type:", error);
      }
      closeModal();
    });
  };

  const columns = [
    { title: "Name", dataIndex: "name", key: "name" },
    {
      title: "Short Description",
      dataIndex: "shortDescription",
      key: "shortDescription",
    },
    {
      title: "Origin History",
      dataIndex: "originHistory",
      key: "originHistory",
    },
    {
      title: "Category Description",
      dataIndex: "categoryDescription",
      key: "categoryDescription",
    },
    { title: "Feng Shui", dataIndex: "fengShui", key: "fengShui" },
    {
      title: "Raising Condition",
      dataIndex: "raisingCondition",
      key: "raisingCondition",
    },
    { title: "Note", dataIndex: "note", key: "note" },
    {
      title: "Created At",
      dataIndex: "createdAt",
      key: "createdAt",
      render: (text) => {
        const date = new Date(text);
        return date.toLocaleString("vi-VN", {
          day: "2-digit",
          month: "2-digit",
          year: "numeric",
        });
      },
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
            onClick={() => handleDelete(record.koiTypeId)}
            danger
          />
        </div>
      ),
    },
  ];

  const fetchKoiTypes = async () => {
    try {
      const data = await getAllKoiType();
      setKoiTypes(data);
    } catch (err) {
      console.error("Failed to fetch Koi Type data", err);
    }
  };

  useEffect(() => {
    fetchKoiTypes();
  }, [koiTypes]);

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
          pagination={{ pageSize }}
          className="custom-table"
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
          <Form.Item label="Created At" name="createdAt">
            <Input type="date" />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default KoiTypePage;
