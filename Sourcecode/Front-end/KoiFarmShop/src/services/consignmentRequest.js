// import api from "../config/axios";

// export const createConsignmentRequestForm = async (form) => {
//   try {
//     const token = localStorage.getItem("accessToken");
//     const res = await api.post("/api/ConsignmentRequest", form, {
//       headers: {
//         Authorization: `Bearer ${token}`,
//       },
//     });
//     return res.data;
//   } catch (err) {
//     console.error("Error during create Consignment Request Form:", err);
//     throw err;
//   }
// };

// export const createConsignmentRequestDetailForm = async (form) => {
//   try {
//     const token = localStorage.getItem("accessToken");
//     const res = await api.post("/api/ConsignmentDetail", form, {
//       headers: {
//         Authorization: `Bearer ${token}`,
//       },
//     });
//     return res.data;
//   } catch (err) {
//     console.error("Error during create Consignment Request Detail Form:", err);
//     throw err;
//   }
// };

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

export const createConsignmentRequest = async (requestData) => {
  try {
    const token = localStorage.getItem("accessToken");
    if (!token) {
      throw new Error("Access token not found. Please login first.");
    }

    const formData = new FormData();
    formData.append("CustomerId", requestData.customerId);
    formData.append("KoiId", requestData.koiId);
    formData.append("ArgredSalePrice", requestData.argredSalePrice);
    formData.append("StartDate", requestData.startDate);
    formData.append("EndDate", requestData.endDate);
    formData.append("Note", requestData.note || "");

    const response = await api.post("/api/ConsignmentRequest", formData, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "multipart/form-data",
      },
    });

    console.log("Consignment request created successfully:", response.data);
    return response.data;
  } catch (error) {
    console.error(
      "Error creating consignment request:",
      error.response?.data || error.message
    );
    throw error;
  }
};

export const approveConsignmentRequest = async (consignmentId, isApproved) => {
  const formData = new FormData();
  formData.append("ConsignmentId", consignmentId);
  formData.append("IsActive", isApproved ? "true" : "false");
  formData.append("Status", isApproved ? "approved" : "rejected");

  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.put(
      "/api/ConsignmentRequest/approve-consignment-request",
      formData,
      {
        headers: {
          accept: "text/plain",
          Authorization: `Bearer ${token}`,
          "Content-Type": "multipart/form-data",
        },
      }
    );
    return res.data;
  } catch (error) {
    console.error("API call failed:", error);
    return null;
  }
};

export const getConsignmentRequestPending = async () => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.get("/api/ConsignmentRequest?Status=pending", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return res.data;
  } catch (err) {
    console.error("Error fetching Consignment Request Pending data:", err);
    throw err;
  }
};
