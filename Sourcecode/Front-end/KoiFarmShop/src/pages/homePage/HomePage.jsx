import React from "react";
import "./HomePage.css";

function HomePage() {
  return (
    <main>
      <section
        className="relative  flex items-center justify-center text-white"
        style={{ height: "calc(100vh - 100px)", marginTop: 100 }}
      >
        <div
          className="absolute inset-0 bg-cover bg-center z-0"
          style={{
            backgroundImage:
              "url('https://images2.alphacoders.com/134/thumb-1920-1348333.png')",
          }}
        ></div>
        <div className="absolute inset-0 bg-black opacity-50 z-10"></div>
        <div className="z-20 text-center">
          <h1 className="text-5xl font-bold mb-4">Welcome to Koi Farm Shop</h1>
          <p className="text-xl mb-8">Discover the beauty of Koi fish</p>
          <button className="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full">
            Explore Now
          </button>
        </div>
      </section>

      <section className="about-us">
        <div className="about-us-title">
          <h2>About Us</h2>
          <h1>Who we are</h1>
          <img src="/icons/fish-line.png" alt="" />
          <p>
            Koi Farm Shop tự hào là đơn vị hàng đầu cung cấp và nuôi dưỡng giống
            cá Koi chất lượng cao. Chúng tôi luôn cam kết mang đến cho khách
            hàng những dịch vụ tốt nhất, từ việc chọn giống đến chăm sóc cá một
            cách toàn diện.
          </p>
        </div>
        <div className="about-us-info">
          <div className="info-item">
            <span className="item-icon-web">
              <img src="/icons/koi-fish.png" alt="koi-fish" />
            </span>
            <h3 className="item-title">Nguồn gốc uy tín</h3>
            <p className="item-info">
              Cá Koi chuẩn Nhật, sức khỏe bền bỉ, màu sắc đẹp như tranh vẽ,
              khẳng định đẳng cấp hồ cá của bạn
            </p>
          </div>
          <div className="info-item">
            <span className="item-icon-web">
              <img src="/icons/pond.png" alt="pond" />
            </span>

            <h3 className="item-title">Môi trường hoàn hảo</h3>
            <p className="item-info">
              Nơi cá Koi không chỉ sống mà còn phát triển rực rỡ, tạo nên sự
              thịnh vượng trong không gian của bạn
            </p>
          </div>
          <div className="info-item">
            <span className="item-icon-web">
              <img src="/icons/best-price.png" alt="best-price" />
            </span>
            <h3 className="item-title">Giá trị xứng đáng</h3>
            <p className="item-info">
              Chất lượng cá Koi hoàn hảo, giá cả hợp lý, biến đam mê của bạn
              thành hiện thực dễ dàng hơn bao giờ hết
            </p>
          </div>
          <div className="info-item">
            <span className="item-icon-web">
              <img src="/icons/heart.png" alt="heart" />
            </span>
            <h3 className="item-title">Dịch vụ tận tâm</h3>
            <p className="item-info">
              Chăm sóc cá Koi từ A đến Z, bảo hành sức khỏe toàn diện để cá luôn
              đẹp và khỏe theo thời gian
            </p>
          </div>
        </div>
      </section>
    </main>
  );
}

export default HomePage;
