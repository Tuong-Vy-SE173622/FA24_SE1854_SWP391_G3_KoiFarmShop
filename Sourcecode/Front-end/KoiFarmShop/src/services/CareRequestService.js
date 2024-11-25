import api from "../config/axios";

// export const createCareRequest = async (data) => {
//   try {
//     const res = await api.post("/api/CareRequest", data);
//     return res.data;
//   } catch (err) {
//     console.error("Error during get Koi Type:", err);
//     throw err;
//   }
// };

export const createCareRequestDetail = async (data) => {
  try {
    const res = await api.post("/api/CareRequestDetail", data);
    return res.data;
  } catch (err) {
    console.error("Error during get Koi Type:", err);
    throw err;
  }
};

// import api from "../config/axios";

export const createCareRequest = async (requestData) => {
  try {
    const token = localStorage.getItem("accessToken");
    if (!token) {
      throw new Error("Access token not found. Please login first.");
    }

    const formData = new FormData();
    formData.append("CustomerId", requestData.customerId);
    formData.append("KoiId", requestData.koiId);
    formData.append("CarePlanId", requestData.carePlanId);
    formData.append("Note", requestData.note || "");

    const response = await api.post("/api/CareRequest", formData, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "multipart/form-data",
      },
    });

    return response.data;
  } catch (error) {
    console.error(
      "Error creating care request:",
      error.response?.data || error.message
    );
    throw error;
  }
};

// export const getCareRequestPending

export const getCareRequestPending = async () => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.get("/api/CareRequest?Status=PendingApproval", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return res.data;
  } catch (err) {
    console.error("Error fetching Care Request Pending data:", err);
    throw err;
  }
};

export const approveCareRequest = async (careRequestId) => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.put(
      `/api/CareRequest/approve?careRequestId=${careRequestId}`,
      null,
      {
        headers: {
          Authorization: `Bearer ${token}`,
          Accept: "text/plain",
        },
      }
    );
    return res.data;
  } catch (err) {
    console.error("Error Care Request approve:", err);
    throw err;
  }
};

export const rejectCareRequest = async (careRequestId) => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.put(
      `/api/CareRequest/reject?careRequestId=${careRequestId}`,
      null,
      {
        headers: {
          Authorization: `Bearer ${token}`,
          Accept: "text/plain",
        },
      }
    );
  } catch (err) {
    console.error("Error Care Request reject:", err);
    throw err;
  }
};
