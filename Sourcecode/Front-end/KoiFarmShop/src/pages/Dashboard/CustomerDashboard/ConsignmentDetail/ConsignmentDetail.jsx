import React, { useState } from "react";
import { useParams } from "react-router-dom";
import "./ConsignmentDetail.css";
import { createConsignmentRequestDetailForm } from "../../../../services/consignmentRequest";

function ConsignmentDetail() {
  const { consignmentId } = useParams();
  const [consignmentDetail, setConsignmentDetail] = useState({
    consignmentId: consignmentId,
    koiId: "",
    consignmentType: "",
    totalMonths: "",
    monthlyConsignmentFee: "",
    soldPrice: "",
    healthDescription: "",
    weight: "",
    status: "",
    isActive: true,
    note: "",
  });
  const [errors, setErrors] = useState({});

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;

    // Kiểm tra và cập nhật lỗi nếu giá trị nhỏ hơn 1
    if (type === "number" && value < 1) {
      setErrors((prevErrors) => ({
        ...prevErrors,
        [name]: <p style={{ color: "red" }}>Giá trị phải lớn hơn 0</p>,
      }));
    } else {
      setErrors((prevErrors) => ({
        ...prevErrors,
        [name]: null,
      }));
    }

    setConsignmentDetail((prevDetail) => ({
      ...prevDetail,
      [name]: type === "checkbox" ? checked : value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    // Kiểm tra nếu có trường nào nhỏ hơn 1
    const invalidFields = [
      "koiId",
      "totalMonths",
      "monthlyConsignmentFee",
      "soldPrice",
      "weight",
    ].filter((field) => consignmentDetail[field] < 1);

    if (invalidFields.length > 0) {
      alert("Vui lòng nhập tất cả các trường số lớn hơn 0.");
      return;
    }

    const request = {
      consignmentId: parseInt(consignmentId),
      ...consignmentDetail,
    };

    try {
      const data = await createConsignmentRequestDetailForm(request);
      alert("Gửi Form Consignment Request Detail thành công!");
    } catch (error) {
      console.error("Lỗi khi gửi yêu cầu ký gửi:", error);
      alert("Đã xảy ra lỗi trong quá trình gửi yêu cầu ký gửi chi tiết");
    }
  };

  return (
    <div>
      <form
        className="crf-form"
        onSubmit={handleSubmit}
        style={{ marginTop: 50, width: 500 }}
      >
        <h2>Chi Tiết Ký Gửi</h2>
        <input
          type="number"
          name="koiId"
          placeholder="Mã Cá Koi"
          value={consignmentDetail.koiId}
          onChange={handleChange}
          min="1"
        />
        {errors.koiId && <span className="error">{errors.koiId}</span>}

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
          min="1"
        />
        {errors.totalMonths && (
          <span className="error">{errors.totalMonths}</span>
        )}

        <input
          type="number"
          name="monthlyConsignmentFee"
          placeholder="Phí Hàng Tháng"
          value={consignmentDetail.monthlyConsignmentFee}
          onChange={handleChange}
          min="1"
        />
        {errors.monthlyConsignmentFee && (
          <span className="error">{errors.monthlyConsignmentFee}</span>
        )}

        <input
          type="number"
          name="soldPrice"
          placeholder="Giá ký gửi"
          value={consignmentDetail.soldPrice}
          onChange={handleChange}
          min="1"
        />
        {errors.soldPrice && <span className="error">{errors.soldPrice}</span>}

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
          min="1"
        />
        {errors.weight && <span className="error">{errors.weight}</span>}

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

        <button type="submit">Cập Nhật Chi Tiết Ký Gửi</button>
      </form>
    </div>
  );
}

export default ConsignmentDetail;
