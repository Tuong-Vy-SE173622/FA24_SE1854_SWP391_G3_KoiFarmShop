// CartPage.js

import React, { useState, useEffect } from 'react';
import { Button } from 'antd';
import { useNavigate } from 'react-router-dom';
import './CartPage.css';

function CartPage() {
    const [cart, setCart] = useState([]);
    const nav = useNavigate();

    // Lấy giỏ hàng từ localStorage khi trang tải
    useEffect(() => {
        const storedCart = JSON.parse(localStorage.getItem('cart')) || [];
        setCart(storedCart);
    }, []);

    // Hàm tính tổng giá trị giỏ hàng
    const calculateTotal = () => {
        return cart.reduce((total, koi) => total + koi.price, 0);
    };

    // Xóa cá khỏi giỏ hàng
    const handleRemoveItem = (koiId) => {
        const updatedCart = cart.filter((koi) => koi.koiId !== koiId);
        setCart(updatedCart);
        localStorage.setItem('cart', JSON.stringify(updatedCart));
    };

    // Chuyển đến trang thanh toán
    const handleCheckout = () => {
        alert("Chuyển đến trang thanh toán...");
        nav('/checkout'); // Chuyển đến trang thanh toán (giả định là bạn đã tạo route này)
    };

    return (
        <section className="cart-page">
            <h1>Giỏ Hàng</h1>
            {cart.length > 0 ? (
                <div className="cart-items-container">
                    {cart.map((koi) => (
                        <div key={koi.koiId} className="cart-item">
                            <img src={koi.image} alt={koi.koiTypeName} className="cart-item-image" />
                            <div className="cart-item-details">
                                <h3>{koi.koiTypeName}</h3>
                                <p>{new Intl.NumberFormat("vi-VN").format(koi.price)} VND</p>
                                <Button onClick={() => handleRemoveItem(koi.koiId)} type="danger">
                                    Xóa
                                </Button>
                            </div>
                        </div>
                    ))}
                    <div className="cart-total">
                        <h2>Tổng Cộng: {new Intl.NumberFormat("vi-VN").format(calculateTotal())} VND</h2>
                    </div>
                    <Button type="primary" onClick={handleCheckout}>
                        Thanh Toán
                    </Button>
                </div>
            ) : (
                <p>Giỏ hàng của bạn đang trống.</p>
            )}
        </section>

    );
}

export default CartPage;
