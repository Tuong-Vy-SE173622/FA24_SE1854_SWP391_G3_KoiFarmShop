import { useEffect } from "react";
import { useLocation } from "react-router-dom";

function ScrollToTop() {
  const { pathname } = useLocation();

  useEffect(() => {
    window.scrollTo(0, 0); // Cuộn về vị trí đầu trang
  }, [pathname]); // Lắng nghe sự thay đổi của đường dẫn

  return null;
}

export default ScrollToTop;
