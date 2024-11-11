import api from "../config/axios";

export const getAllKoi = async (params) => {
  try {
    const res = await api.get("/api/Koi", { params });
    return res.data;
  } catch (err) {
    console.error("Error fetching Koi data:", err);
    throw err;
  }
};

export const getKoiOrigins = async () => {
  try {
    const res = await api.get("/api/Koi/all-origins");
    return res.data;
  } catch (err) {
    console.error("Error fetching Koi Origin Data", err);
    throw err;
  }
};

export const getKoiByID = async (id) => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.get(`/api/Koi/${id}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return res.data;
  } catch (err) {
    console.error("Error fetching Koi Data by ID", err);
    throw err;
  }
};
