// import React from "react";
// import "./Footer.scss";
// import {
//   TikTokOutlined,
//   YoutubeOutlined,
//   InstagramOutlined,
//   TwitterOutlined,
//   GooglePlusOutlined,
// } from "@ant-design/icons";
// import { Button, Dropdown, Menu } from "antd";
// const languageItems = [
//   { key: "en", label: <a href="#en">English</a> },
//   { key: "es", label: <a href="#es">Spanish</a> },
//   { key: "fr", label: <a href="#fr">French</a> },
//   { key: "vn", label: <a href="#vn">Tiếng Việt</a> },
//   { key: "kr", label: <a href="#kr">Korea</a> },
//   { key: "cn", label: <a href="#cn">China</a> },
// ];
// // import { Link, useLocation } from 'react-router-dom'

// const Footer = () => {
//   return (
//     <footer className="footer">
//       <div className="footer-top">
//         <div className="footer-section">
//           <ul>
//             <li>
//               <a href="/">About</a>
//             </li>

//             <li>
//               <a href="/">Press</a>
//             </li>
//           </ul>
//         </div>
//         <div className="footer-section">
//           <ul>
//             <li>
//               <a href="/">Help</a>
//             </li>
//             <li>
//               <a href="/">Advertise</a>
//             </li>
//             <li>
//               <a href="/">Career</a>
//             </li>
//           </ul>
//         </div>
//         <div className="footer-section">
//           <ul>
//             <li>
//               <a href="/">Terms</a>
//             </li>
//             <li>
//               <a href="/">Privacy Policy</a>
//             </li>
//           </ul>
//         </div>
//         <div className="footer-section">
//           <div className="buttons">
//             <Button type="primary" danger size="large">
//               <a href="/">Tech on</a>
//             </Button>
//           </div>
//         </div>
//       </div>
//       <div className="footer-bottom">
//         <p>&copy; 2024 Your Company. All rights reserved.</p>
//         <div className="icon">
//           <TikTokOutlined />
//           <YoutubeOutlined />
//           <InstagramOutlined />
//           <TwitterOutlined />
//           <GooglePlusOutlined />
//         </div>
//       </div>
//     </footer>
//   );
// };

// export default Footer;

import React from "react";
import { FaFacebook, FaInstagram, FaTwitter } from "react-icons/fa";
import "../../index.css";

function Footer() {
  return (
    <footer className="bg-gray-800 text-white py-8">
      <div className="container mx-auto px-4">
        <div className="flex flex-wrap justify-between">
          <div className="w-full md:w-1/4 mb-4 md:mb-0">
            <h3 className="text-xl font-bold mb-2">Koi Farm Shop</h3>
            <p className="text-gray-400">Bringing beauty to your pond</p>
          </div>
          <div className="w-full md:w-1/4 mb-4 md:mb-0">
            <h4 className="text-lg font-semibold mb-2">Quick Links</h4>
            <ul className="space-y-2">
              <li>
                <a href="#" className="text-gray-400 hover:text-white">
                  About Us
                </a>
              </li>
              <li>
                <a href="#" className="text-gray-400 hover:text-white">
                  Contact
                </a>
              </li>
              <li>
                <a href="#" className="text-gray-400 hover:text-white">
                  Privacy Policy
                </a>
              </li>
              <li>
                <a href="#" className="text-gray-400 hover:text-white">
                  Terms of Service
                </a>
              </li>
            </ul>
          </div>
          <div className="w-full md:w-1/4 mb-4 md:mb-0">
            <h4 className="text-lg font-semibold mb-2">Follow Us</h4>
            <div className="flex space-x-4">
              <a href="#" className="text-gray-400 hover:text-white">
                <FaFacebook />
              </a>
              <a href="#" className="text-gray-400 hover:text-white">
                <FaTwitter />
              </a>
              <a href="#" className="text-gray-400 hover:text-white">
                <FaInstagram />
              </a>
            </div>
          </div>
        </div>
        <div className="border-t border-gray-700 mt-8 pt-4 text-center text-gray-400">
          <p>&copy; 2023 Koi Farm Shop. All rights reserved.</p>
        </div>
      </div>
    </footer>
  );
}

export default Footer;
