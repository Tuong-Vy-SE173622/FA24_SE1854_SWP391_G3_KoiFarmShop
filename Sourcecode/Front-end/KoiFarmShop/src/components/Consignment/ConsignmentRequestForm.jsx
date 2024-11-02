import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './ConsignmentRequestForm.css';

function ConsignmentRequestForm() {
    const [request, setRequest] = useState({
        customerId: '',
        paymentMethod: '',
        note: '',
        status: '',
        isOnline: false,
    });

    const navigate = useNavigate();

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setRequest((prevRequest) => ({
            ...prevRequest,
            [name]: type === 'checkbox' ? checked : value,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        // Mock response data for testing
        const mockResponse = {
            consignmentId: '12345', // Sample consignment ID
            message: 'Consignment request submitted successfully',
        };

        // Simulate API success by logging mock response
        console.log("Mock response for consignment request:", mockResponse);

        // Use consignmentId from mock response to navigate
        navigate(`/consignment-detail/${mockResponse.consignmentId}`);
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="number"
                name="customerId"
                placeholder="Mã Khách Hàng"
                value={request.customerId}
                onChange={handleChange}
            />
            <input
                type="text"
                name="paymentMethod"
                placeholder="Phương Thức Thanh Toán"
                value={request.paymentMethod}
                onChange={handleChange}
            />
            <input
                type="text"
                name="note"
                placeholder="Ghi Chú"
                value={request.note}
                onChange={handleChange}
            />
            <input
                type="text"
                name="status"
                placeholder="Trạng Thái"
                value={request.status}
                onChange={handleChange}
            />
            <label>
                Yêu Cầu Trực Tuyến
                <input
                    type="checkbox"
                    name="isOnline"
                    checked={request.isOnline}
                    onChange={handleChange}
                />
            </label>
            <button type="submit">Gửi Yêu Cầu Ký Gửi</button>
        </form>
    );
}

export default ConsignmentRequestForm;
