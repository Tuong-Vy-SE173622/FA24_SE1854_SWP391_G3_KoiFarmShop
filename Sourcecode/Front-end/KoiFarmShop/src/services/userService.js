import api from "../config/axios";

export const getUsers = async (pageNumber, pageSize) => {
  try {
    const res = await api.get(
      `/api/User?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
    return res.data;
  } catch (err) {
    console.error("Error during get Users: ", err);
    throw err;
  }
};

export const createUser = async (userData) => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.post("/api/User", userData, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
    return res.data;
  } catch (err) {
    console.error("Error during create User:", err);
    throw err;
  }
};

export const updateUser = async (userData) => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.put(`/api/User/${userData.userId}`, userData, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return res.data;
  } catch (err) {
    console.error("Error during update User:", err);
    throw err;
  }
};

export const deleteUser = async (userId) => {
  try {
    const token = localStorage.getItem("accessToken");
    const res = await api.delete(`/api/User/${userId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return res.data;
  } catch (err) {
    console.error("Error during delete User:", err);
    throw err;
  }
};
