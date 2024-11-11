import api from "../config/axios";

export const createConsignmentRequestForm = async (form) => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.post("/api/ConsignmentRequest", form, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return res.data;
  } catch (err) {
    console.error("Error during create Consignment Request Form:", err);
    throw err;
  }
};

export const createConsignmentRequestDetailForm = async (form) => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.post("/api/ConsignmentDetail", form, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return res.data;
  } catch (err) {
    console.error("Error during create Consignment Request Detail Form:", err);
    throw err;
  }
};
