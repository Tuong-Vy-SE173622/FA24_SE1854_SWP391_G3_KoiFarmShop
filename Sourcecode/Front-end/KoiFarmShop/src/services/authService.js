import api from "../config/axios";

export const login = async (username, password) => {
  const params = {
    userName: username,
    password: password,
  };
  // const params = {}

  console.log("Preparing to send login request"); // Đảm bảo log này xuất hiện

  try {
    // console.log("API URL: /api/Authorize/Login");
    // console.log("Sent Data: ", loginData); // Xem dữ liệu gửi có đúng không

    console.log("/api/Authorize/Login", {
      params,
    });

    const res = await api.post("/api/Authorize/Login", null, {
      params: params,
    });

    console.log("Response data", res.data); // Log dữ liệu trả về từ API
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
