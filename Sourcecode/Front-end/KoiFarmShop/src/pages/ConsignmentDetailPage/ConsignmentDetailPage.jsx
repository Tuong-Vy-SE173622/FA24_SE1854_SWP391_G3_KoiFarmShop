import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import './ConsignmentDetail.css'; // Import CSS cho ConsignmentDetail

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
        const fetchConsignmentDetail = async () => {
            try {
                const response = await fetch(`https://localhost:7226/api/ConsignmentDetail/${consignmentId}`);
                if (!response.ok) throw new Error('Không lấy được thông tin chi tiết');

                const data = await response.json();
                setConsignmentDetail(data);
            } catch (error) {
                console.error("Lỗi khi lấy thông tin chi tiết consignment:", error);
            }
        };

        fetchConsignmentDetail();
    }, [consignmentId]);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setConsignmentDetail((prevDetail) => ({
            ...prevDetail,
            [name]: type === 'checkbox' ? checked : value,
        }));
    };

    const handleUpdate = async () => {
        try {
            const response = await fetch(`https://localhost:7226/api/ConsignmentDetail/${consignmentId}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(consignmentDetail),
            });

            if (!response.ok) throw new Error('Cập nhật thông tin chi tiết không thành công');

            alert('Cập nhật thông tin chi tiết thành công.');
        } catch (error) {
            console.error("Lỗi khi cập nhật thông tin chi tiết:", error);
        }
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
