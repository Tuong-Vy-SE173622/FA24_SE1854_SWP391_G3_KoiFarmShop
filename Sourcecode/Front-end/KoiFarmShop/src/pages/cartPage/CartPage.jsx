// CartPage.js
import React, { useState, useEffect } from 'react';
import { Button, notification } from 'antd';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import './CartPage.css';

function CartPage() {
    const [cart, setCart] = useState([]);
    const nav = useNavigate();

    useEffect(() => {
        // Load cart from localStorage on component mount
        const storedCart = JSON.parse(localStorage.getItem('cart')) || [];
        setCart(storedCart);
    }, []);

    const calculateTotal = () => {
        return cart.reduce((total, koi) => total + koi.price, 0);
    };

    const handleRemoveItem = (koiId) => {
        const updatedCart = cart.filter((koi) => koi.koiId !== koiId);
        setCart(updatedCart);
        localStorage.setItem('cart', JSON.stringify(updatedCart));
    };

    const handleCheckout = async () => {
        try {
            const totalAmount = calculateTotal();
            if (totalAmount <= 0) {
                notification.error({ message: 'Giỏ hàng trống, không thể thanh toán' });
                return;
            }

            // Attempt to retrieve customer data
            const user = JSON.parse(localStorage.getItem('user'));
            if (!user || !user.id) {
                notification.error({ message: 'Không thể xác định khách hàng. Vui lòng đăng nhập.' });
                nav('/login'); // Redirect to login if not authenticated
                return;
            }

            const customerId = user.id;

            // Create a unique orderId with a 5-digit number
            const uniqueOrderId = Math.floor(10000 + Math.random() * 90000).toString();

            // Prepare order data for the API
            const orderData = {
                customerId,
                orderDate: new Date().toISOString(),
                subAmount: totalAmount,
                vat: 0.1, // 10% VAT
                vatAmount: totalAmount * 0.1,
                promotionAmount: 0, // Adjust for applicable promotions
                totalAmount: totalAmount * 1.1, // Total with VAT
                paymentMethod: 'Online',
                paymentStatus: 'Pending',
                isActive: true,
                note: 'Order placed through checkout process',
                status: 'Processing',
            };

            // Submit order to the API
            const orderResponse = await axios.post('https://localhost:7226/api/Order', orderData);
            if (orderResponse.status !== 200) {
                throw new Error('Đặt hàng thất bại. Vui lòng thử lại.');
            }

            // Prepare payment data for checkout
            const paymentData = {
                amount: totalAmount,
                orderDescription: 'Thanh toán đơn hàng Koi',
                orderId: uniqueOrderId,
            };

            const paymentResponse = await axios.post('https://localhost:7226/api/Payment/create-payment', paymentData);

            if (paymentResponse.status === 200 && paymentResponse.data.paymentUrl) {
                window.location.href = paymentResponse.data.paymentUrl;
            } else {
                throw new Error('Lỗi tạo liên kết thanh toán.');
            }
        } catch (error) {
            console.error('Lỗi trong quá trình thanh toán:', error);
            notification.error({
                message: 'Thanh toán thất bại',
                description: 'Đã xảy ra lỗi trong quá trình thanh toán. Vui lòng thử lại.',
            });
        }
    };

    return (
        <section className="cart-page">
            <h1>Giỏ Hàng</h1>
            {cart.length > 0 ? (
                <div className="cart-items-container">
                    {cart.map((koi, index) => (
                        <div key={`${koi.koiId}-${index}`} className="cart-item">
                            <img src={koi.image} alt={koi.koiTypeName} className="cart-item-image" />
                            <div className="cart-item-details">
                                <h3>{koi.koiTypeName}</h3>
                                <p>{new Intl.NumberFormat('vi-VN').format(koi.price)} VND</p>
                                <Button onClick={() => handleRemoveItem(koi.koiId)} type="danger">
                                    Xóa
                                </Button>
                            </div>
                        </div>
                    ))}

                    <div className="cart-total">
                        <h2>Tổng Cộng: {new Intl.NumberFormat('vi-VN').format(calculateTotal())} VND</h2>
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
