// KoiCard.js

import React from 'react';
import { Button } from 'antd';
import { useNavigate } from 'react-router-dom';
import './KoiCard.css';

function KoiCard({ koi, onAddToCart }) {
  const navigate = useNavigate();

  const handleCardClick = () => {
    navigate(`/koi/${koi.koiId}`);
  };

  const handleAddToCart = (e) => {
    e.stopPropagation(); // Dừng sự kiện để không điều hướng khi nhấn vào nút
    onAddToCart(koi);
  };

  return (
    <div className="koi-card-container">
      <div className="koi-card" onClick={handleCardClick}>
        <img src={koi.image} alt={koi.koiTypeName} className="koi-card-image" />
        <h3 className="koi-card-title">{koi.koiTypeName}</h3>
        <p className="koi-card-price">{new Intl.NumberFormat("vi-VN").format(koi.price)} VND</p>
        <Button type="primary" onClick={handleAddToCart}>
          Add to Cart
        </Button>
      </div>
    </div>
  );
}

export default KoiCard;
