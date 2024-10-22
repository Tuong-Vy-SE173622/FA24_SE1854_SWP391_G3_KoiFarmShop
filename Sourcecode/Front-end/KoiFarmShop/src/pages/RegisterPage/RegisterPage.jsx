// import React from "react";
// import "./RegisterPage.css";
// import "../LoginPage/LoginPage.css";
// import { Button, Form, Input } from "antd";
// import { useNavigate } from "react-router-dom";
// import { useState } from "react";

// function RegisterPage() {
//   const nav = useNavigate();
//   const [form] = Form.useForm();

//   const handleLoginPage = () => nav("/login");
//   const handleHomePage = () => nav("/");

//   const validatePassword = (_, value) => {
//     if (value && value.length >= 5) {
//       return Promise.resolve();
//     }
//     return Promise.reject(new Error("Password must be at least 5 characters."));
//   };

//   const validateConfirmPassword = ({ getFieldValue }) => ({
//     validator(_, value) {
//       if (!value || getFieldValue("password") === value) {
//         return Promise.resolve();
//       }
//       return Promise.reject(new Error("Passwords do not match!"));
//     },
//   });

//   const validatePhone = (_, value) => {
//     const phoneRegex = /^[0-9]{9,11}$/;
//     if (value && phoneRegex.test(value)) {
//       return Promise.resolve();
//     }
//     return Promise.reject(
//       new Error("Phone must be a string of 9 to 11 digits.")
//     );
//   };

//   return (
//     <div className="login-container">
//       <div className="register">
//         <div className="register-form">
//           <div className="form-wrapper">
//             <h1
//               style={{
//                 fontSize: 24,
//                 fontWeight: 600,
//                 marginBottom: ".8rem",
//               }}
//             >
//               Register
//             </h1>
//             <Form
//               form={form}
//               className="form"
//               name="basic"
//               labelCol={{
//                 span: 8,
//               }}
//               style={{
//                 width: 400,
//               }}
//               onFinish={handleHomePage}
//             >
//               <Form.Item
//                 label="Email"
//                 name="email"
//                 hasFeedback
//                 rules={[
//                   {
//                     type: "email",
//                     message: "The input is not valid email!",
//                   },
//                   {
//                     required: true,
//                     message: "Please input your email!",
//                   },
//                 ]}
//               >
//                 <Input type="text" placeholder="Email" autoComplete="email" />
//               </Form.Item>
//               <Form.Item
//                 label="Username"
//                 name="username"
//                 hasFeedback
//                 rules={[
//                   { required: true, message: "Please input your username!" },
//                 ]}
//               >
//                 <Input
//                   type="text"
//                   placeholder="Username"
//                   autoComplete="username"
//                 />
//               </Form.Item>
//               <Form.Item
//                 label="First Name"
//                 name="firstName"
//                 hasFeedback
//                 rules={[
//                   { required: true, message: "Please input your first name!" },
//                 ]}
//               >
//                 <Input
//                   type="text"
//                   placeholder="First Name"
//                   autoComplete="given-name"
//                 />
//               </Form.Item>
//               <Form.Item
//                 label="Last Name"
//                 name="lastName"
//                 hasFeedback
//                 rules={[
//                   { required: true, message: "Please input your last name!" },
//                 ]}
//               >
//                 <Input
//                   type="text"
//                   placeholder="Last Name"
//                   autoComplete="family-name"
//                 />
//               </Form.Item>
//               <Form.Item
//                 label="Phone"
//                 name="phone"
//                 hasFeedback
//                 rules={[
//                   {
//                     required: true,
//                     message: "Please input your phone number!",
//                   },
//                   { validator: validatePhone },
//                 ]}
//               >
//                 <Input type="text" placeholder="Phone" autoComplete="tel" />
//               </Form.Item>

//               <Form.Item
//                 label="Password"
//                 name="password"
//                 hasFeedback
//                 rules={[
//                   { required: true, message: "Please input your password!" },
//                   { validator: validatePassword },
//                 ]}
//               >
//                 <Input
//                   type="password"
//                   placeholder="Password"
//                   autoComplete="new-password"
//                 />
//               </Form.Item>
//               <Form.Item
//                 label="Confirm Password"
//                 name="confirmPassword"
//                 dependencies={["password"]}
//                 hasFeedback
//                 rules={[
//                   { required: true, message: "Please confirm your password!" },
//                   validateConfirmPassword,
//                 ]}
//               >
//                 <Input
//                   type="password"
//                   placeholder="Confirm Password"
//                   autoComplete="new-password"
//                 />
//               </Form.Item>
//               <Form.Item style={{ display: "flex", justifyContent: "center" }}>
//                 <Button
//                   type="primary"
//                   htmlType="submit"
//                   style={{
//                     width: 140,
//                     marginTop: ".56rem",
//                   }}
//                 >
//                   Register
//                 </Button>
//               </Form.Item>
//               <div className="form-nav">
//                 Already have account?{" "}
//                 <span onClick={handleLoginPage}>Login here</span>
//               </div>
//             </Form>
//           </div>
//         </div>
//       </div>
//     </div>
//   );
// }

// export default RegisterPage;

import React, { useState } from "react";
import "./RegisterPage.css";
import "../LoginPage/LoginPage.css";
import { Button, Form, Input, message, Modal } from "antd"; // Import Ant Design message để hiện thông báo
import { useNavigate } from "react-router-dom";
import { register } from "../../services/authService";
// import { register } from "../services/auth"; // Import hàm đăng ký

function RegisterPage() {
  const nav = useNavigate();
  const [form] = Form.useForm();

  const handleLoginPage = () => nav("/login");
  // const handleHomePage = () => nav("/");

  // Gọi API đăng ký
  const handleRegister = async (values) => {
    try {
      const requestData = {
        ...values,
        role: 2, // Set role to 2 by default
      };
      const response = await register(requestData);

      if (response.isSuccess) {
        message.success("Registration successful!");
        Modal.success({
          title: "Đăng nhập thành công!",
          content: (
            <div>
              <p>Bạn đã đăng ký thành công.</p>
            </div>
          ),
          onOk() {
            handleLoginPage();
          },
          centered: true,
        });
      } else {
        message.error(response.data.message || "Registration failed.");
      }
    } catch (error) {
      message.error("An error occurred during registration.");
    }
  };

  // Validation các trường
  const validatePassword = (_, value) => {
    if (value && value.length >= 5) {
      return Promise.resolve();
    }
    return Promise.reject(new Error("Password must be at least 5 characters."));
  };

  const validateConfirmPassword = ({ getFieldValue }) => ({
    validator(_, value) {
      if (!value || getFieldValue("password") === value) {
        return Promise.resolve();
      }
      return Promise.reject(new Error("Passwords do not match!"));
    },
  });

  const validatePhone = (_, value) => {
    const phoneRegex = /^[0-9]{9,11}$/;
    if (value && phoneRegex.test(value)) {
      return Promise.resolve();
    }
    return Promise.reject(
      new Error("Phone must be a string of 9 to 11 digits.")
    );
  };

  return (
    <div className="login-container">
      <div className="register">
        <div className="register-form">
          <div className="form-wrapper">
            <h1
              style={{
                fontSize: 24,
                fontWeight: 600,
                marginBottom: ".8rem",
              }}
            >
              Register
            </h1>
            <Form
              form={form}
              className="form"
              name="basic"
              labelCol={{
                span: 8,
              }}
              style={{
                width: 400,
              }}
              onFinish={handleRegister} // Gọi API khi form submit
            >
              <Form.Item
                label="Email"
                name="email"
                hasFeedback
                rules={[
                  {
                    type: "email",
                    message: "The input is not valid email!",
                  },
                  {
                    required: true,
                    message: "Please input your email!",
                  },
                ]}
              >
                <Input type="text" placeholder="Email" autoComplete="email" />
              </Form.Item>
              <Form.Item
                label="Username"
                name="username"
                hasFeedback
                rules={[
                  { required: true, message: "Please input your username!" },
                  {
                    pattern: /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$/,
                    message:
                      "Username must be at least 6 characters and contain both letters and numbers.",
                  },
                ]}
              >
                <Input
                  type="text"
                  placeholder="Username"
                  autoComplete="username"
                />
              </Form.Item>
              <Form.Item
                label="First Name"
                name="firstName"
                hasFeedback
                rules={[
                  { required: true, message: "Please input your first name!" },
                ]}
              >
                <Input
                  type="text"
                  placeholder="First Name"
                  autoComplete="given-name"
                />
              </Form.Item>
              <Form.Item
                label="Last Name"
                name="lastName"
                hasFeedback
                rules={[
                  { required: true, message: "Please input your last name!" },
                ]}
              >
                <Input
                  type="text"
                  placeholder="Last Name"
                  autoComplete="family-name"
                />
              </Form.Item>
              <Form.Item
                label="Phone"
                name="phone"
                hasFeedback
                rules={[
                  {
                    required: true,
                    message: "Please input your phone number!",
                  },
                  { validator: validatePhone },
                ]}
              >
                <Input type="text" placeholder="Phone" autoComplete="tel" />
              </Form.Item>

              <Form.Item
                label="Password"
                name="password"
                hasFeedback
                rules={[
                  { required: true, message: "Please input your password!" },
                  { validator: validatePassword },
                ]}
              >
                <Input
                  type="password"
                  placeholder="Password"
                  autoComplete="new-password"
                />
              </Form.Item>
              <Form.Item
                label="Confirm Password"
                name="confirmPassword"
                dependencies={["password"]}
                hasFeedback
                rules={[
                  { required: true, message: "Please confirm your password!" },
                  validateConfirmPassword,
                ]}
              >
                <Input
                  type="password"
                  placeholder="Confirm Password"
                  autoComplete="new-password"
                />
              </Form.Item>
              <Form.Item style={{ display: "flex", justifyContent: "center" }}>
                <Button
                  type="primary"
                  htmlType="submit"
                  style={{
                    width: 140,
                    marginTop: ".56rem",
                  }}
                >
                  Register
                </Button>
              </Form.Item>
              <div className="form-nav">
                Already have account?{" "}
                <span onClick={handleLoginPage}>Login here</span>
              </div>
            </Form>
          </div>
        </div>
      </div>
    </div>
  );
}

export default RegisterPage;
