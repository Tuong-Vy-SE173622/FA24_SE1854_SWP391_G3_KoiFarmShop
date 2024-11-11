import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./ConsignmentRequestForm.css";
import { createConsignmentRequestForm } from "../../../../services/consignmentRequest";

function ConsignmentRequestForm() {
  const [request, setRequest] = useState({
    customerId: "",
    paymentMethod: "",
    note: "",
    status: "",
    isOnline: false,
  });

  const navigate = useNavigate();

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setRequest((prevRequest) => ({
      ...prevRequest,
      [name]: type === "checkbox" ? checked : value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const data = await createConsignmentRequestForm(request);
      console.log("API response for consignment request:", data);
      navigate(`/dashboard/consignment-detail/${data.consignmentId}`);
    } catch (error) {
      console.error("Lỗi khi gửi yêu cầu ký gửi:", error);

      alert("Đã xảy ra lỗi trong quá trình gửi yêu cầu ký gửi");
    }
  };

  return (
    <div>
      <form className="crf-form" onSubmit={handleSubmit}>
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
    </div>
  );
}

export default ConsignmentRequestForm;
