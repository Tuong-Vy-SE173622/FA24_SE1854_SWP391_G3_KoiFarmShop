import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    host: "localhost", // Cố định localhost
    port: 5001, // Bạn có thể thay đổi cổng nếu cần
    open: true, // Tùy chọn để tự động mở trình duyệt
  },
});
