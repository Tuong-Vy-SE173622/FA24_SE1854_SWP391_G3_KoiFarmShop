import React from "react";
import "./RegisterPage.css";
import "../LoginPage/LoginPage.css";
import { Button, Checkbox, Form, Input } from "antd";
import { useNavigate } from "react-router-dom";

function RegisterPage() {
  const nav = useNavigate();

  const handleLoginPage = () => nav("/login");
  const handleHomePage = () => nav("/");
  return (
    <div className="login-container">
      <div className="register">
        <div className="register-form">
          <div className="form-wrapper">
            <Form
              className="form"
              labelCol={{
                span: 24,
              }}
            >
              <h1>Register</h1>
              <Form.Item label="Email" name="emal">
                <Input type="text" placeholder="Email" />
              </Form.Item>
              <Form.Item label="Username" name="username">
                <Input type="text" placeholder="Username" />
              </Form.Item>
              <Form.Item label="Password" name="password">
                <Input type="password" placeholder="Password" />
              </Form.Item>
              <span>
                <Checkbox>Remember me</Checkbox>
                {/* <p>Fogot Password?</p> */}
              </span>
              <Form.Item>
                <Button
                  type="primary"
                  style={{ width: "100%", marginTop: "1rem" }}
                  onClick={handleHomePage}
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
  // </div>;
}

export default RegisterPage;
