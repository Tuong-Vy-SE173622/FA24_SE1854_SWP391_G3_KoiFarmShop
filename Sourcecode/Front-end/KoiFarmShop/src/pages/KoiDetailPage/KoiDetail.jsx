// KoiDetail.jsx
import React from 'react';
import { useParams } from 'react-router-dom';
import './KoiDetail.css';

const sampleKoiLs = [
  {
    koiId: 1,
    koiTypeName: "Koi Đỏ",
    price: 500000,
    size: 30,
    age: 2,
    origin: "Nhật Bản",
    characteristics: "Màu đỏ rực",
    note: "Loại cá phổ biến",
    image: "https://minhxuankoifarm.com/wp-content/uploads/2020/09/Screen-Shot-2020-09-29-at-06.59.58-510x732.png",
  },
  {
    koiId: 2,
    koiTypeName: "Koi Vàng",
    price: 450000,
    size: 28,
    age: 1,
    origin: "Nhật Bản",
    characteristics: "Màu vàng tươi",
    note: "Thích hợp cho bể nhỏ",
    image: "https://minhxuankoifarm.com/wp-content/uploads/2020/09/Screen-Shot-2020-09-29-at-06.59.58-510x732.png",
  },
  // Add more sample objects as needed
];

function KoiDetail() {
  const { koiId } = useParams();
  const koi = sampleKoiLs.find((item) => item.koiId === Number(koiId));

  if (!koi) {
    return <div>Koi fish not found!</div>;
  }

  const handleBuyNow = () => {
    // Get the current cart from local storage or initialize it as an empty array
    const existingCart = JSON.parse(localStorage.getItem("cart")) || [];

    // Add the selected Koi to the cart
    const updatedCart = [...existingCart, koi];

    // Save the updated cart back to local storage
    localStorage.setItem("cart", JSON.stringify(updatedCart));

    // Alert the user
    alert(`Bạn đã thêm ${koi.koiTypeName} vào giỏ hàng!`);
  };

  return (
    <div className="koi-detail-container">
      <img src={koi.image} alt={koi.koiTypeName} className="koi-detail-image" />
      <div className="koi-detail-info">
        <h1>{koi.koiTypeName}</h1>
        <p><strong>Price:</strong> {koi.price} VND</p>
        <p><strong>Size:</strong> {koi.size} cm</p>
        <p><strong>Age:</strong> {koi.age} years</p>
        <p><strong>Origin:</strong> {koi.origin}</p>
        <p><strong>Characteristics:</strong> {koi.characteristics}</p>
        <p><strong>Note:</strong> {koi.note}</p>
        <button onClick={handleBuyNow} className="buy-button">Mua Hàng</button>
      </div>
    </div>
  );
}

export default KoiDetail;
