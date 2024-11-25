import { jwtDecode } from "jwt-decode";
import api from "../config/axios";

// export const login = async (username, password) => {
//   const params = {
//     userName: username,
//     password: password,
//   };

//   try {
//     const res = await api.post("/api/Authorize/Login", null, {
//       params: params,
//     });

//     return res.data;
//   } catch (err) {
//     console.error("Error during login: ", err);
//     throw err;
//   }
// };

export const login = async (username, password) => {
  const params = {
    userName: username,
    password: password,
  };

  try {
    // 1. Đăng nhập để lấy token
    const res = await api.post("/api/Authorize/Login", null, {
      params: params,
    });

    const accessToken = res.data.token || res.data.accessToken; // Kiểm tra trường token
    if (!accessToken) {
      throw new Error("Token not found in login response.");
    }

    localStorage.setItem("accessToken", accessToken);

    // 2. Giải mã token để lấy thông tin
    const decodedToken = jwtDecode(accessToken);
    const userId = decodedToken.UserId; // Lấy UserId từ payload của token
    const role = decodedToken.Role; // Lấy Role từ payload của token

    if (!userId) {
      throw new Error("UserId not found in token.");
    }

    console.log("UserId:", userId, "Role:", role);

    // Kiểm tra role: nếu là admin (role === 1), bỏ qua bước tạo customer
    if (role === "1") {
      console.log("Admin logged in, skipping customer creation.");
      return res.data; // Chỉ trả về dữ liệu đăng nhập
    }

    // 3. Gọi API tạo customerId, bỏ qua nếu đã tồn tại
    try {
      await api.post(
        "/add-new-customer",
        {
          userId: userId,
          address: "Default Address", // Có thể thay đổi thành thông tin địa chỉ thực tế
        },
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      );

      console.log("Customer created successfully.");
    } catch (error) {
      if (
        error.response &&
        error.response.status === 400 &&
        error.response.data.message === "Customer already exists for this user"
      ) {
        console.log(
          "Customer already exists or no permission to create. Skipping creation."
        );
      } else {
        console.error("Error during customer creation:", error);
        throw error;
      }
    }

    // 4. Lấy customerId từ API /filterByCustomerIdAndUserId
    try {
      const customerResponse = await api.get(
        `/filterByCustomerIdAndUserId?userId=${userId}`,
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      );

      const customerData = customerResponse.data.data;
      if (customerData && customerData.length > 0) {
        const customerId = customerData[0].customerId;
        localStorage.setItem("customerId", customerId);
        console.log("CustomerId saved to localStorage:", customerId);
      } else {
        console.log("No customer found for this user.");
      }
    } catch (error) {
      console.error("Error fetching customerId:", error);
      throw error;
    }

    return res.data;
  } catch (err) {
    console.error("Error during login or customer creation:", err);
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
