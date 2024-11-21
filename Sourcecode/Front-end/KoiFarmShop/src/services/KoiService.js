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

export const getKoiPending = async () => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.get("/api/Koi/admin?Status=PENDING", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return res.data;
  } catch (err) {
    console.error("Error fetching Koi Pending data:", err);
    throw err;
  }
};

export const createKoi = async (koiData) => {
  try {
    const token = localStorage.getItem("accessToken");
    if (!token) {
      throw new Error("Access token not found. Please login first.");
    }

    const formData = new FormData();
    formData.append("Size", koiData.size);
    formData.append("Gender", koiData.gender);
    formData.append("Origin", koiData.origin);
    formData.append("Price", koiData.price);
    formData.append("IsImported", koiData.isImported || "");
    formData.append("Note", koiData.note || "");
    formData.append("Certificate", koiData.certificate); // Dữ liệu tệp
    formData.append("Generation", koiData.generation || "");
    formData.append("Characteristics", koiData.characteristics || "");
    formData.append("KoiTypeId", koiData.KoiTypeId);
    formData.append("Image", koiData.image); // Dữ liệu ảnh
    formData.append("Age", koiData.age);

    const res = await api.post("/api/Koi/for-customer", formData, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "multipart/form-data",
      },
    });

    console.log("Koi created successfully:", res.data);
    return res.data;
  } catch (err) {
    console.error("Error creating koi:", err.response?.data || err.message);
    throw err;
  }
};
export const getKoiByCustomerId = async (userId) => {
  try {
    const token = localStorage.getItem("accessToken");
    if (!token) {
      throw new Error("Access token not found. Please login first.");
    }

    const res = await api.get(`/api/Koi/koi-created-by-user`, {
      params: { userId },
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    console.log("Koi fetched successfully:", res.data);
    return res.data;
  } catch (err) {
    console.error(
      "Error fetching koi by userId:",
      err.response?.data || err.message
    );
    throw err;
  }
};
