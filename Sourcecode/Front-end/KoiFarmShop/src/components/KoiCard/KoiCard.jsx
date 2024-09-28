import React from "react";
import "./KoiCard.css";
import { IoIosSearch } from "react-icons/io";
import { TiShoppingCart } from "react-icons/ti";
import { FaRegHeart } from "react-icons/fa";
import { FaCodeCompare } from "react-icons/fa6";

function KoiCard() {
  const koiSelectItems = [
    {
      key: 1,
      icon: <IoIosSearch />,
      lable: "Xem nhanh",
    },
    {
      key: 2,
      icon: <TiShoppingCart />,
      lable: "Thêm vào giỏ hàng",
    },
    {
      key: 3,
      icon: <FaRegHeart />,
      lable: "Yêu thích",
    },
    {
      key: 4,
      icon: <FaCodeCompare />,
      lable: "So sánh",
    },
  ];
  return (
    <div className="koi-card-container">
      <img
        src="https://visinhcakoi.com/wp-content/uploads/2021/07/ca-koi-showa-2-600x874-1.jpg"
        alt="koi"
      />
      <div className="koi-title">Koi Showa</div>
      <div className="koi-card-select">
        {koiSelectItems.map((item) => {
          return (
            <div className="koi-card-select-item" key={item.key}>
              {/* <span className="select-item-lable">{item.lable}</span> */}
              <span className="select-item-icon">{item.icon}</span>
            </div>
          );
        })}
      </div>
    </div>
  );
}

export default KoiCard;
