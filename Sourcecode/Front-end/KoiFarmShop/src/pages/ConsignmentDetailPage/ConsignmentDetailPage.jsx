import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import './ConsignmentDetail.css';

function ConsignmentDetail() {
    const { consignmentId } = useParams();
    const [consignmentDetail, setConsignmentDetail] = useState({
        koiId: '',
        consignmentType: '',
        totalMonths: '',
        monthlyConsignmentFee: '',
        soldPrice: '',
        healthDescription: '',
        weight: '',
        status: '',
        isActive: true,
        note: '',
    });

    useEffect(() => {
        // Hardcoded data for testing
        const mockData = {
            koiId: '001',
            consignmentType: 'Long-term',
            totalMonths: 12,
            monthlyConsignmentFee: 200,
            soldPrice: 1500,
            healthDescription: 'Good health, no visible issues',
            weight: 3.5,
            status: 'Active',
            isActive: true,
            note: 'Special care instructions',
        };

        console.log("Mock consignment detail:", mockData);
        setConsignmentDetail(mockData);
    }, [consignmentId]);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setConsignmentDetail((prevDetail) => ({
            ...prevDetail,
            [name]: type === 'checkbox' ? checked : value,
        }));
    };

    const handleUpdate = () => {
        // Mock update functionality
        console.log("Updated consignment detail:", consignmentDetail);
        alert('Cập nhật thông tin chi tiết thành công.');
    };

    return (
        <div className="consignment-detail-container">
            <h2>Chi Tiết Ký Gửi</h2>
            <input
                type="number"
                name="koiId"
                placeholder="Mã Cá Koi"
                value={consignmentDetail.koiId}
                onChange={handleChange}
            />
            <input
                type="text"
                name="consignmentType"
                placeholder="Loại Ký Gửi"
                value={consignmentDetail.consignmentType}
                onChange={handleChange}
            />
            <input
                type="number"
                name="totalMonths"
                placeholder="Tổng Số Tháng"
                value={consignmentDetail.totalMonths}
                onChange={handleChange}
            />
            <input
                type="number"
                name="monthlyConsignmentFee"
                placeholder="Phí Hàng Tháng"
                value={consignmentDetail.monthlyConsignmentFee}
                onChange={handleChange}
            />
            <input
                type="number"
                name="soldPrice"
                placeholder="Giá Bán"
                value={consignmentDetail.soldPrice}
                onChange={handleChange}
            />
            <textarea
                name="healthDescription"
                placeholder="Mô Tả Tình Trạng Sức Khỏe"
                value={consignmentDetail.healthDescription}
                onChange={handleChange}
            />
            <input
                type="number"
                name="weight"
                placeholder="Trọng Lượng"
                value={consignmentDetail.weight}
                onChange={handleChange}
            />
            <input
                type="text"
                name="status"
                placeholder="Trạng Thái"
                value={consignmentDetail.status}
                onChange={handleChange}
            />
            <label>
                Hoạt Động
                <input
                    type="checkbox"
                    name="isActive"
                    checked={consignmentDetail.isActive}
                    onChange={handleChange}
                />
            </label>
            <textarea
                name="note"
                placeholder="Ghi Chú"
                value={consignmentDetail.note}
                onChange={handleChange}
            />
            <button onClick={handleUpdate}>Cập Nhật Chi Tiết Ký Gửi</button>
        </div>
    );
}

export default ConsignmentDetail;
