import React from "react";
import "./ProfilePage.css";
import KoiCard from "../../../../components/KoiCard/KoiCard";
import { Button, Checkbox, Form, Input, Upload } from "antd";

function ProfilePage() {
  const onFinish = (values) => {
    console.log("Success:", values);
  };
  const onFinishFailed = (errorInfo) => {
    console.log("Failed:", errorInfo);
  };

  return (
    <div className="dashboard-main-container">
      <div className="form-account-wrapper">
        <Form
          name="basic"
          labelCol={{
            span: 4,
          }}
          // wrapperCol={{
          //   span: 16,
          // }}
          style={{
            maxWidth: 600,
          }}
          initialValues={{
            remember: true,
          }}
          onFinish={onFinish}
          onFinishFailed={onFinishFailed}
          autoComplete="off"
          layout="vertical"
        >
          <div className="avatar-wrapper">
            <Form.Item
              // label="Upload"
              valuePropName="fileList"
              // getValueFromEvent={normFile}
            >
              <img
                src="https://avatarfiles.alphacoders.com/173/173714.jpg"
                alt="avatar"
                className="avatar-img"
              />
              <Upload action="/upload.do">
                <button className="btn-upload-img">Upload</button>
              </Upload>
            </Form.Item>
          </div>
          <Form.Item
            label="Username"
            name="username"
            rules={[
              {
                required: true,
                message: "Please input your username!",
              },
            ]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            label="Email"
            name="email"
            rules={[
              {
                required: true,
                message: "Please input your email!",
              },
            ]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            label="FirstName"
            name="firstName"
            // rules={[
            //   {
            //     required: true,
            //     message: "Please input your First Name!",
            //   },
            // ]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            label="LastName"
            name="lastName"
            // rules={[
            //   {
            //     required: true,
            //     message: "Please input your Last Name!",
            //   },
            // ]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            label="Phone"
            name="phone"
            // rules={[
            //   {
            //     required: true,
            //     message: "Please input your Phone!",
            //   },
            // ]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            wrapperCol={{
              offset: 8,
              span: 16,
            }}
            style={{
              marginLeft: 170,
            }}
          >
            <Button type="primary" htmlType="submit">
              Save
            </Button>
          </Form.Item>
        </Form>
      </div>
    </div>
  );
}

export default ProfilePage;
