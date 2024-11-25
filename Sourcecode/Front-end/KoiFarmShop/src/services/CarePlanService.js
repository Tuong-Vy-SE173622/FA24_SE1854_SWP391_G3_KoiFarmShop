import api from "../config/axios";

export const getCarePlan = async () => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.get("/api/CarePlan", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return res.data; // Điều chỉnh nếu cần
  } catch (err) {
    console.error("Error during get CarePlan: ", err);
    throw err;
  }
};
