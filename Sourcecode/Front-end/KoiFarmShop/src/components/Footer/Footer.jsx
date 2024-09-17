import React from "react";
import "./Footer.scss";
import {
  TikTokOutlined,
  YoutubeOutlined,
  InstagramOutlined,
  TwitterOutlined,
  GooglePlusOutlined,
} from "@ant-design/icons";
import { Button, Dropdown, Menu } from "antd";
const languageItems = [
  { key: "en", label: <a href="#en">English</a> },
  { key: "es", label: <a href="#es">Spanish</a> },
  { key: "fr", label: <a href="#fr">French</a> },
  { key: "vn", label: <a href="#vn">Tiếng Việt</a> },
  { key: "kr", label: <a href="#kr">Korea</a> },
  { key: "cn", label: <a href="#cn">China</a> },
];
// import { Link, useLocation } from 'react-router-dom'

const Footer = () => {
  return (
    <footer className="footer">
      <div className="footer-top">
        <div className="footer-section">
          <ul>
            <li>
              <a href="/">About</a>
            </li>

            <li>
              <a href="/">Press</a>
            </li>
          </ul>
        </div>
        <div className="footer-section">
          <ul>
            <li>
              <a href="/">Help</a>
            </li>
            <li>
              <a href="/">Advertise</a>
            </li>
            <li>
              <a href="/">Career</a>
            </li>
          </ul>
        </div>
        <div className="footer-section">
          <ul>
            <li>
              <a href="/">Terms</a>
            </li>
            <li>
              <a href="/">Privacy Policy</a>
            </li>
          </ul>
        </div>
        <div className="footer-section">
          <div className="buttons">
            <Button type="primary" danger size="large">
              <a href="/">Tech on</a>
            </Button>
          </div>
        </div>
      </div>
      <div className="footer-bottom">
        <p>&copy; 2024 Your Company. All rights reserved.</p>
        <div className="icon">
          <TikTokOutlined />
          <YoutubeOutlined />
          <InstagramOutlined />
          <TwitterOutlined />
          <GooglePlusOutlined />
        </div>
      </div>
    </footer>
  );
};

export default Footer;
