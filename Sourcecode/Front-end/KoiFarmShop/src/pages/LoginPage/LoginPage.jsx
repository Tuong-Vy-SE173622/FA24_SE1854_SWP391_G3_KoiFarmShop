import React from "react";
import "./LoginPage.css";
import { Button, Checkbox, Form, Input } from "antd";
import { useNavigate } from "react-router-dom";

function LoginPage() {
  const nav = useNavigate();

  const handleRegisterPage = () => nav("/register");
  const handleHomePage = () => nav("/");
  return (
    <div className="login-container">
      {/* <div className="sign-up-page"></div> */}
      {/* <div className="sign-in-page"> */}
      <div className="login">
        <div className="login__form">
          <div className="form-wrapper">
            <Form
              className="form"
              labelCol={{
                span: 24,
              }}
            >
              <h1>Login</h1>
              <Form.Item label="Username" name="username">
                <Input type="text" placeholder="Username" />
              </Form.Item>
              <Form.Item label="Password" name="password">
                <Input type="password" placeholder="Password" />
              </Form.Item>
              <span>
                <Checkbox>Remember me</Checkbox>
                <p>Fogot Password?</p>
              </span>
              <Form.Item>
                <Button
                  type="primary"
                  style={{ width: "100%", marginTop: "1rem" }}
                  onClick={handleHomePage}
                >
                  Login
                </Button>
              </Form.Item>
              <div className="form-nav">
                New user?{" "}
                <span onClick={handleRegisterPage}>Register here</span>
              </div>
            </Form>
          </div>
        </div>
      </div>
    </div>
    // </div>
  );
}

export default LoginPage;
