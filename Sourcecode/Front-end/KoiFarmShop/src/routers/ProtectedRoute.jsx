import { Navigate } from "react-router-dom";

const ProtectedRoute = ({ children, allowedRoles }) => {
  const userRole = JSON.parse(localStorage.getItem("roles")); // Lấy role người dùng từ localStorage

  // Kiểm tra xem người dùng đã đăng nhập hay chưa
  if (!userRole) {
    return <Navigate to="/login" replace />; // Nếu chưa đăng nhập, chuyển đến trang login
  }

  // Kiểm tra xem người dùng có vai trò phù hợp với route này không
  if (!allowedRoles.includes(userRole)) {
    return <Navigate to="/not-authorized" replace />; // Nếu không có quyền, chuyển đến trang lỗi
  }

  return children; // Nếu có quyền, render nội dung của trang
};

export default ProtectedRoute;
