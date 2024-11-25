// import React, { useState } from "react";
// import "./LoginPage.css";
// import { Button, Checkbox, Form, Input, message, Modal } from "antd";
// import { useNavigate } from "react-router-dom";
// import { getUserInfo, login } from "../../services/authService";

// function LoginPage() {
//   const navigate = useNavigate();
//   const [userName, setUserName] = useState("");
//   const [password, setPassword] = useState("");
//   const [errorMessage, setErrorMessage] = useState("");

//   const handleRegisterPage = () => navigate("/register");
//   const handleHomePage = () => navigate("/");

//   const handleLogin = async () => {
//     try {
//       const data = await login(userName, password);

//       // Gọi getUserInfo với username đã nhập
//       const userData = await getUserInfo(userName);

//       // Chuyển đổi giá trị role
//       let userRole;
//       if (userData.role === "1") {
//         userRole = "Admin";
//       } else if (userData.role === "2") {
//         userRole = "Customer";
//       } else {
//         userRole = "Unknown"; // Nếu không có giá trị role hợp lệ
//       }

//       // Lưu thông tin người dùng vào localStorage
//       localStorage.setItem("accessToken", data.accessToken);
//       localStorage.setItem("refreshToken", data.refreshToken);
//       localStorage.setItem("roles", JSON.stringify(userRole));
//       localStorage.setItem("account", JSON.stringify(userData)); // Lưu role đã chuyển đổi

//       // Modal.success({
//       //   title: "Đăng nhập thành công!",
//       //   content: (
//       //     <div>
//       //       <p>Bạn đã đăng nhập thành công.</p>
//       //     </div>
//       //   ),
//       //   onOk() {
//       //     handleHomePage(); // Chuyển hướng về trang chính khi nhấn OK
//       //   },
//       //   centered: true,
//       // });
//       handleHomePage();
//     } catch (error) {
//       console.error("Error during login: ", error);
//       Modal.error({
//         title: "Đăng nhập thất bại!",
//         content: (
//           <div>
//             <p>Vui lòng kiểm tra thông tin đăng nhập của bạn.</p>
//           </div>
//         ),
//         centered: true,
//       });
//     }
//   };

//   return (
//     <div className="login-container">
//       <div className="login">
//         <div className="login__form">
//           <div className="form-wrapper">
//             <Form
//               className="form"
//               labelCol={{
//                 span: 24,
//               }}
//               onFinish={handleLogin}
//             >
//               <h1>Login</h1>

//               <Form.Item
//                 label="Username"
//                 name="username"
//                 rules={[
//                   { required: true, message: "Please input your username!" },
//                 ]}
//               >
//                 <Input
//                   type="text"
//                   placeholder="Username"
//                   value={userName}
//                   onChange={(e) => setUserName(e.target.value)}
//                   autoComplete="username"
//                 />
//               </Form.Item>

//               <Form.Item
//                 label="Password"
//                 name="password"
//                 rules={[
//                   { required: true, message: "Please input your password!" },
//                 ]}
//               >
//                 <Input
//                   type="password"
//                   placeholder="Password"
//                   value={password}
//                   onChange={(e) => setPassword(e.target.value)}
//                   autoComplete="current-password"
//                 />
//               </Form.Item>

//               <Form.Item>
//                 <Button
//                   type="primary"
//                   style={{ width: "100%", marginTop: "1rem" }}
//                   htmlType="submit"
//                 >
//                   Login
//                 </Button>
//               </Form.Item>

//               <div className="form-nav">
//                 New user?{" "}
//                 <span onClick={handleRegisterPage}>Register here</span>
//               </div>

//               {errorMessage && (
//                 <p style={{ color: "red", marginTop: "1rem" }}>
//                   {errorMessage}
//                 </p>
//               )}
//             </Form>
//           </div>
//         </div>
//       </div>
//     </div>
//   );
// }

// export default LoginPage;

import React, { useState } from "react";
import "./LoginPage.css";
import { Button, Checkbox, Form, Input, message, Modal } from "antd";
import { useNavigate } from "react-router-dom";
import { getUserInfo, login } from "../../services/authService";

function LoginPage() {
  const navigate = useNavigate();
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  const handleRegisterPage = () => navigate("/register");
  const handleHomePage = () => navigate("/");

  const handleLogin = async () => {
    try {
      const data = await login(userName, password);

      // Gọi getUserInfo với username đã nhập
      const userData = await getUserInfo(userName);

      // Chuyển đổi giá trị role
      let userRole;
      if (userData.role === "1") {
        userRole = "Admin";
      } else if (userData.role === "2") {
        userRole = "Customer";
      } else {
        userRole = "Unknown"; // Nếu không có giá trị role hợp lệ
      }

      // Lưu thông tin người dùng vào localStorage
      localStorage.setItem("accessToken", data.accessToken);
      localStorage.setItem("refreshToken", data.refreshToken);
      localStorage.setItem("roles", JSON.stringify(userRole));
      localStorage.setItem("account", JSON.stringify(userData));
      localStorage.setItem("userID", JSON.stringify(userData.userId)); // Lưu role đã chuyển đổi

      // Modal.success({
      //   title: "Đăng nhập thành công!",
      //   content: (
      //     <div>
      //       <p>Bạn đã đăng nhập thành công.</p>
      //     </div>
      //   ),
      //   onOk() {
      //     handleHomePage(); // Chuyển hướng về trang chính khi nhấn OK
      //   },
      //   centered: true,
      // });
      handleHomePage();
    } catch (error) {
      console.error("Error during login: ", error);
      Modal.error({
        title: "Đăng nhập thất bại!",
        content: (
          <div>
            <p>Vui lòng kiểm tra thông tin đăng nhập của bạn.</p>
          </div>
        ),
        centered: true,
      });
    }
  };

  return (
    <div className="login-container">
      <div className="login">
        <div className="login__form">
          <div className="form-wrapper">
            <Form
              className="form"
              labelCol={{
                span: 24,
              }}
              onFinish={handleLogin}
            >
              <h1>Login</h1>

              <Form.Item
                label="Username"
                name="username"
                rules={[
                  { required: true, message: "Please input your username!" },
                ]}
              >
                <Input
                  type="text"
                  placeholder="Username"
                  value={userName}
                  onChange={(e) => setUserName(e.target.value)}
                  autoComplete="username"
                />
              </Form.Item>

              <Form.Item
                label="Password"
                name="password"
                rules={[
                  { required: true, message: "Please input your password!" },
                ]}
              >
                <Input
                  type="password"
                  placeholder="Password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  autoComplete="current-password"
                />
              </Form.Item>

              <Form.Item>
                <Button
                  type="primary"
                  style={{ width: "100%", marginTop: "1rem" }}
                  htmlType="submit"
                >
                  Login
                </Button>
              </Form.Item>

              <div className="form-nav">
                New user?{" "}
                <span onClick={handleRegisterPage}>Register here</span>
              </div>

              {errorMessage && (
                <p style={{ color: "red", marginTop: "1rem" }}>
                  {errorMessage}
                </p>
              )}
            </Form>
          </div>
        </div>
      </div>
    </div>
  );
}

export default LoginPage;
