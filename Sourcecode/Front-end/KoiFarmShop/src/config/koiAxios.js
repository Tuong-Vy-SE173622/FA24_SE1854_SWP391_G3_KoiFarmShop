import api from "../config/axios";

export const fetchAllKois = async () => {
  try {
    const response = await api.get("/api/Koi");
    return response.data;
  } catch (err) {
    console.log(err);
  }
};
