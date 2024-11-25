import React, { useEffect, useState } from "react";
import { Table, Button, Modal, Form, Input, Switch } from "antd";
import { PlusOutlined, EditOutlined, DeleteOutlined } from "@ant-design/icons";
import {
  createPromotion,
  deletePromotion,
  getPromotion,
  updatePromotion,
} from "../../../../services/promotionService";
// import "./PromotionPage.css";
// import {
//   createPromotion,
//   deletePromotion,
//   getPromotion,
//   updatePromotion,
// } from "../../../services/PromotionService";

const PromotionPage = () => {
  const [promotions, setPromotions] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingPromotion, setEditingPromotion] = useState(null);
  const [form] = Form.useForm();

  const openModal = (record = null) => {
    setEditingPromotion(record);
    setIsModalOpen(true);
    form.setFieldsValue(record || {});
  };

  const closeModal = () => {
    setIsModalOpen(false);
    form.resetFields();
  };

  const handleDelete = async (id) => {
    try {
      await deletePromotion(id); // Gọi API xóa khuyến mãi
      setPromotions(promotions.filter((promo) => promo.promotionId !== id));
    } catch (error) {
      console.error("Failed to delete promotion:", error);
    }
  };

  const handleSave = () => {
    form.validateFields().then(async (values) => {
      try {
        if (editingPromotion) {
          // Gọi API cập nhật khuyến mãi
          await updatePromotion({ ...editingPromotion, ...values });
          setPromotions(
            promotions.map((promo) =>
              promo.promotionId === editingPromotion.promotionId
                ? { ...editingPromotion, ...values }
                : promo
            )
          );
        } else {
          const newPromotion = await createPromotion(values);
          setPromotions([...promotions, newPromotion]);
        }
      } catch (error) {
        console.error("Failed to save promotion:", error);
      }
      closeModal();
    });
  };

  const columns = [
    { title: "ID", dataIndex: "promotionId", key: "promotionId" },
    { title: "Description", dataIndex: "description", key: "description" },
    {
      title: "Discount Percentage",
      dataIndex: "discountPercentage",
      key: "discountPercentage",
    },
    {
      title: "Is Active",
      dataIndex: "isActive",
      key: "isActive",
      render: (text) => <Switch checked={text} disabled />,
    },
    { title: "Note", dataIndex: "note", key: "note" },
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
            onClick={() => handleDelete(record.promotionId)}
            danger
          />
        </div>
      ),
    },
  ];

  const fetchPromotion = async () => {
    try {
      const data = await getPromotion();
      setPromotions(data);
    } catch (err) {
      console.error("Failed to fetch Promotion data", err);
    }
  };

  useEffect(() => {
    fetchPromotion();
  }, [promotions]);

  return (
    <div className="admin-page-container">
      <Button
        type="primary"
        icon={<PlusOutlined />}
        onClick={() => openModal()}
        style={{ marginBottom: 16, marginLeft: 28 }}
      >
        Add Promotion
      </Button>

      <div className="table-style">
        <Table
          dataSource={promotions}
          columns={columns}
          rowKey="promotionId"
          pagination={{ pageSize: 5 }}
          style={{ width: 1200 }}
          className="custom-table"
        />
      </div>

      <Modal
        title={editingPromotion ? "Edit Promotion" : "Add Promotion"}
        open={isModalOpen}
        onCancel={closeModal}
        onOk={handleSave}
        style={{ top: 10 }}
      >
        <Form form={form} layout="vertical">
          <Form.Item
            label="Description"
            name="description"
            rules={[{ required: true, message: "Please enter a description" }]}
          >
            <Input />
          </Form.Item>
          {/* <Form.Item
            label="Discount Percentage"
            name="discountPercentage"
            rules={[
              {
                required: true,
                type: "number",
                message: "Please enter a discount percentage",
              },
            ]}
          >
            <Input type="number" />
          </Form.Item> */}
          <Form.Item
            label="Discount Percentage"
            name="discountPercentage"
            rules={[
              {
                required: true,
                message: "Please enter a discount percentage",
              },
              {
                validator: (_, value) => {
                  if (value && isNaN(value)) {
                    return Promise.reject(
                      new Error("Discount percentage must be a number")
                    );
                  }
                  return Promise.resolve();
                },
              },
            ]}
          >
            <Input type="number" />
          </Form.Item>

          <Form.Item label="Is Active" name="isActive" valuePropName="checked">
            <Switch />
          </Form.Item>
          <Form.Item label="Note" name="note">
            <Input.TextArea />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default PromotionPage;
