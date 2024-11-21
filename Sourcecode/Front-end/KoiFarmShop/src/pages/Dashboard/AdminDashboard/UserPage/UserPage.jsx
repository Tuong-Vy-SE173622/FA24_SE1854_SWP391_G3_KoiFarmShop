import React, { useEffect, useState } from "react";
import { Table, Button, Modal, Form, Input, Select } from "antd";
import { PlusOutlined, EditOutlined, DeleteOutlined } from "@ant-design/icons";
import moment from "moment";
import {
  createUser,
  deleteUser,
  getUsers,
  updateUser,
} from "../../../../services/userService";
// import {
//   createUser,
//   deleteUser,
//   getUsers,
//   updateUser,
// } from "../../../services/UserService";

const { Option } = Select;

const UserPage = () => {
  const [users, setUsers] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingUser, setEditingUser] = useState(null);
  const [form] = Form.useForm();
  const pageSize = 5;
  const [current, setCurrent] = useState(1);
  const [totalUsers, setTotalUsers] = useState();

  const openModal = (record = null) => {
    setEditingUser(record);
    setIsModalOpen(true);
    form.setFieldsValue(record || {});
  };

  const closeModal = () => {
    setIsModalOpen(false);
    form.resetFields();
  };

  const handleDelete = async (id) => {
    try {
      await deleteUser(id);
      setUsers(users.filter((user) => user.userId !== id));
    } catch (error) {
      console.error("Failed to delete user:", error);
    }
  };

  const handleSave = () => {
    form.validateFields().then(async (values) => {
      try {
        if (editingUser) {
          await updateUser({ ...editingUser, ...values });
          setUsers(
            users.map((user) =>
              user.userId === editingUser.userId
                ? { ...editingUser, ...values }
                : user
            )
          );
        } else {
          const newUser = await createUser(values);
          setUsers([...users, newUser]);
        }
      } catch (error) {
        console.error("Failed to save user:", error);
      }
      closeModal();
    });
  };

  const columns = [
    { title: "Username", dataIndex: "username", key: "username", width: "15%" },
    { title: "Email", dataIndex: "email", key: "email" },
    { title: "First Name", dataIndex: "firstName", key: "firstName" },
    { title: "Last Name", dataIndex: "lastName", key: "lastName" },
    { title: "Phone", dataIndex: "phone", key: "phone" },
    {
      title: "Role",
      dataIndex: "role",
      key: "role",
      render: (role) => (role === "1" ? "Admin" : "Customer"),
    },
    {
      title: "Created At",
      dataIndex: "createdAt",
      key: "createdAt",
      render: (text) => moment(text).format("DD/MM/YYYY"),
    },
    {
      title: "Actions",
      key: "actions",
      render: (record) => (
        <div style={{ display: "flex" }}>
          <Button
            icon={<EditOutlined />}
            onClick={() => openModal(record)}
            style={{ marginRight: 5 }}
          />
          <Button
            icon={<DeleteOutlined />}
            onClick={() => handleDelete(record.userId)}
            danger
          />
        </div>
      ),
    },
  ];

  const fetchUsers = async (pageNumber) => {
    try {
      const data = await getUsers(pageNumber, pageSize);
      setUsers(data.data);
      // setTotalUsers(data.totalCount);
    } catch (err) {
      console.error("Failed to fetch user data", err);
    }
  };

  useEffect(() => {
    fetchUsers(current);
  }, [current]);

  return (
    <div className="admin-page-container" style={{ marginTop: "1.5rem" }}>
      {/* <Button
        type="primary"
        icon={<PlusOutlined />}
        onClick={() => openModal()}
        style={{ marginBottom: 16 }}
      >
        Add User
      </Button> */}

      <Table
        dataSource={users}
        columns={columns}
        rowKey="userId"
        pagination={{
          // total: totalUsers,
          pageSize: pageSize,
          // current: current,
          // onChange: (page) => setCurrent(page),
        }}
        className="custom-table"
        style={{ width: 1200 }}
      />

      <Modal
        title={editingUser ? "Edit User" : "Add User"}
        open={isModalOpen}
        onCancel={closeModal}
        onOk={handleSave}
      >
        <Form form={form} layout="vertical">
          <Form.Item
            label="Username"
            name="username"
            rules={[{ required: true, message: "Please enter username" }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="Email"
            name="email"
            rules={[
              {
                required: true,
                type: "email",
                message: "Please enter a valid email",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="First Name"
            name="firstName"
            rules={[{ required: true, message: "Please enter first name" }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="Last Name"
            name="lastName"
            rules={[{ required: true, message: "Please enter last name" }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="Phone"
            name="phone"
            rules={[{ required: true, message: "Please enter phone number" }]}
          >
            <Input />
          </Form.Item>
          <Form.Item label="Role" name="role">
            <Select placeholder="Select role">
              <Option value="1">Admin</Option>
              <Option value="2">Customer</Option>
            </Select>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default UserPage;
