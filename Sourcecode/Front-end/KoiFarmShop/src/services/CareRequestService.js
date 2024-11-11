import api from "../config/axios";

export const createCareRequest = async (data) => {
  try {
    const res = await api.post("/api/CareRequest", data);
    return res.data;
  } catch (err) {
    console.error("Error during get Koi Type:", err);
    throw err;
  }
};

export const createCareRequestDetail = async (data) => {
  try {
    const res = await api.post("/api/CareRequestDetail", data);
    return res.data;
  } catch (err) {
    console.error("Error during get Koi Type:", err);
    throw err;
  }
};
