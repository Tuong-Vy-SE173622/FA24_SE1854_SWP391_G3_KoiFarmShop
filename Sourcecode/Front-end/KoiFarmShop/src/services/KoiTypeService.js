import api from "../config/axios";

export const getAllKoiType = async () => {
  try {
    const res = await api.get("/api/KoiType");

    return res.data;
  } catch (err) {
    console.error("Error during login: ", err);
    throw err;
  }
};
