// import api from "../config/axios";

// export const getAllKoiType = async () => {
//   try {
//     const res = await api.get("/api/KoiType");

//     return res.data;
//   } catch (err) {
//     console.error("Error during login: ", err);
//     throw err;
//   }
// };

import api from "../config/axios";

export const getAllKoiType = async () => {
  try {
    const res = await api.get("/api/KoiType");
    return res.data;
  } catch (err) {
    console.error("Error during get Koi Type:", err);
    throw err;
  }
};

export const createKoiType = async (koiTypeData) => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.post("/api/KoiType", koiTypeData, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return res.data;
  } catch (err) {
    console.error("Error during create Koi Type:", err);
    throw err;
  }
};

export const updateKoiType = async (koiTypeData) => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.put("/api/KoiType", koiTypeData, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return res.data;
  } catch (err) {
    console.error("Error during update Koi Type:", err);
    throw err;
  }
};

export const deleteKoiType = async (koiTypeId) => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.delete(`/api/KoiType/${koiTypeId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return res.data;
  } catch (err) {
    console.error("Error during delete Koi Type:", err);
    throw err;
  }
};
