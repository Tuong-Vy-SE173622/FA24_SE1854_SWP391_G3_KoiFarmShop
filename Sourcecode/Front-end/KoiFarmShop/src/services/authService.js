import api from "../config/axios";

export const login = async (username, password) => {
  const params = {
    userName: username,
    password: password,
  };

  try {
    const res = await api.post("/api/Authorize/Login", null, {
      params: params,
    });

    return res.data;
  } catch (err) {
    console.error("Error during login: ", err);
    throw err;
  }
};

export const getUserInfo = async (userName) => {
  try {
    const res = await api.get(`/api/User/${userName}`);
    console.log(res.data.data);
    return res.data.data;
  } catch (err) {
    console.error("Error during get user info: ", err);
    throw err;
  }
};

export const logout = async () => {
  try {
    const accessToken = localStorage.getItem("accessToken");
    console.log("accessToken", accessToken);

    if (!accessToken) {
      throw new Error("No access token found.");
    }

    // Gọi API với header Authorization
    const res = await api.post("/api/Authorize/Logout", null, {
      headers: {
        Authorization: `Bearer ${accessToken}`,
      },
    });

    console.log("response", res.status);

    // Xóa localStorage nếu logout thành công
    if (res.data.isSuccess) {
      localStorage.removeItem("accessToken");
      localStorage.removeItem("refreshToken");
      localStorage.removeItem("roles");
      localStorage.removeItem("account");
      console.log("Logout successful");
    } else {
      console.error("Logout failed: ", res.data.message);
    }

    return res.data;
  } catch (err) {
    console.error("Error during logout: ", err);
    throw err;
  }
};

export const register = async (userData) => {
  try {
    const res = await api.post("/api/v1/account", userData);

    console.log("Response data", res.data); // Log dữ liệu trả về từ API
    return res.data;
  } catch (err) {
    console.error("Error during registration: ", err);
    throw err;
  }
};
