import api from "../config/axios"; // Config Axios

// Gọi API tạo Order
export const createOrder = async (customerId) => {
  try {
    // Lấy token từ localStorage
    const accessToken = localStorage.getItem("accessToken");
    if (!accessToken) {
      throw new Error("Access token not found. Please login first.");
    }

    // Gửi yêu cầu POST tới API
    const res = await api.post(
      "/api/Order",
      {
        customerId: customerId, // ID của Customer
      },
      {
        headers: {
          Authorization: `Bearer ${accessToken}`, // Đính kèm token trong header
        },
      }
    );

    console.log("Order created successfully:", res.data);
    return res.data; // Trả về dữ liệu từ API
  } catch (error) {
    console.error("Error during order creation:", error);
    throw error;
  }
};
// Gọi API tạo OrderItem
export const createOrderItem = async (orderId, koiId, amount = 1, price) => {
    try {
      // Lấy token từ localStorage
      const accessToken = localStorage.getItem("accessToken");
      if (!accessToken) {
        throw new Error("Access token not found. Please login first.");
      }
  
      // Gửi yêu cầu POST tới API
      const res = await api.post(
        "/api/OrderItem",
        [
          {
            orderId: orderId,
            koiId: koiId,
            amount: amount, // Số lượng
            price: price, // Giá
          },
        ],
        {
          headers: {
            Authorization: `Bearer ${accessToken}`, // Đính kèm token trong header
          },
        }
      );
  
      console.log("OrderItem created successfully:", res.data);
      return res.data; // Trả về dữ liệu từ API
    } catch (error) {
      console.error("Error during order item creation:", error);
      throw error;
    }
  };
  export const getOrders = async () => {
    try {
      // Lấy token từ localStorage
      const accessToken = localStorage.getItem("accessToken");
      if (!accessToken) {
        throw new Error("Access token not found. Please login first.");
      }
  
      // Gửi yêu cầu GET tới API
      const res = await api.get("/api/Order", {
        headers: {
          Authorization: `Bearer ${accessToken}`, // Đính kèm token trong header
        },
      });
  
      console.log("Orders retrieved successfully:", res.data);
      return res.data; // Trả về danh sách Order từ API
    } catch (error) {
      console.error("Error retrieving orders:", error);
      throw error;
    }
  };
  // Gọi API lấy danh sách OrderItem
export const getOrderItems = async () => {
  
  try {
    // Lấy token từ localStorage
    const accessToken = localStorage.getItem("accessToken");
    if (!accessToken) {
      throw new Error("Access token not found. Please login first.");
    }
      const res = await api.get("/api/OrderItem", {
        headers: {
          Authorization: `Bearer ${accessToken}`, // Đính kèm token trong header
        },
      });
      console.log("Order items retrieved successfully:", res.data);
      return res.data; // Trả về danh sách OrderItem
    } catch (error) {
      console.error("Error fetching order items:", error);
      throw error;
    }
  };   
  
  // Gọi API tạo Payment
  export const createPayment = async (amount, orderDescription, orderId) => {
    try {
      // Lấy token từ localStorage
      const accessToken = localStorage.getItem("accessToken");
      if (!accessToken) {
        throw new Error("Access token not found. Please login first.");
      }
  
      // Đảm bảo orderId là chuỗi
      const paymentData = {
        amount: amount, // Tổng tiền cần thanh toán
        orderDescription: orderDescription, // Mô tả đơn hàng
        orderId: String(orderId), // Chuyển orderId sang chuỗi
      };
  
      // Gửi yêu cầu POST tới API
      const res = await api.post("/api/Payment/create-payment", paymentData, {
        headers: {
          Authorization: `Bearer ${accessToken}`, // Đính kèm token trong header
          "Content-Type": "application/json", // Định dạng JSON
        },
      });
  
      console.log("Payment created successfully:", res.data);
      return res.data; // Trả về URL thanh toán
    } catch (error) {
      console.error("Error during payment creation:", error.response?.data || error.message);
      throw error;
    }
  };
  // Gọi API cập nhật trạng thái đơn hàng sau khi thanh toán
export const updateOrderStatus = async (orderId, paymentStatus, isActive = true) => {
  try {
    const accessToken = localStorage.getItem("accessToken");
    if (!accessToken) {
      throw new Error("Access token not found. Please login first.");
    }

    const data = {
      paymentStatus: paymentStatus, // Trạng thái thanh toán: "Paid" hoặc "Failed"
      isActive: isActive, // Trạng thái đơn hàng (có hiệu lực hay không)
    };

    // Gửi yêu cầu PUT tới API
    const res = await api.put(`/api/Order/${orderId}/update-order-status-after-payment`, data, {
      headers: {
        Authorization: `Bearer ${accessToken}`, // Đính kèm token trong header
        "Content-Type": "application/json", // Định dạng JSON
      },
    });

    console.log("Order status updated successfully:", res.data);
    return res.data;
  } catch (error) {
    console.error("Error updating order status:", error.response?.data || error.message);
    throw error;
  }
};
// Gọi API lấy chi tiết Order theo OrderId
export const getOrderById = async (orderId) => {
  try {
    const accessToken = localStorage.getItem("accessToken");
    if (!accessToken) {
      throw new Error("Access token not found. Please login first.");
    }

    const res = await api.get(`/api/Order/${orderId}`, {
      headers: {
        Authorization: `Bearer ${accessToken}`,
      },
    });

    console.log(`Order with ID ${orderId} retrieved successfully:`, res.data);
    return res.data;
  } catch (error) {
    console.error(`Error fetching order with ID ${orderId}:`, error);
    throw error;
  }
};
