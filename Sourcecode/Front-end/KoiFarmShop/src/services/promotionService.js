import api from "../config/axios";

export const getPromotion = async () => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.get("/api/Promotion", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return res.data; // Điều chỉnh nếu cần
  } catch (err) {
    console.error("Error during get Promotion: ", err);
    throw err;
  }
};

export const createPromotion = async (promotionData) => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.post("/api/Promotion", promotionData, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return res.data;
  } catch (err) {
    console.error("Error during create Promotion:", err);
    throw err;
  }
};

export const updatePromotion = async (promotionData) => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.put(
      `/api/Promotion?promotionId=${promotionData.promotionId}`,
      promotionData,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );
    return res.data;
  } catch (err) {
    console.error("Error during update Promotion:", err);
    throw err;
  }
};

export const deletePromotion = async (promotionId) => {
  try {
    const token = localStorage.getItem("accessToken");
    await api.delete(`/api/Promotion`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
      data: { promotionId }, // Đưa ID vào body
    });
  } catch (err) {
    console.error("Error during delete Promotion:", err);
    throw err;
  }
};
