import React, { useRef } from "react";
import "./HomePage.css";
import { LeftOutlined, RightOutlined } from "@ant-design/icons";
import { Button, Carousel } from "antd";
import KoiCard from "../../components/KoiCard/KoiCard";

function HomePage() {
  const KoiTypeIntroduction = [
    {
      key: 1,
      urlImg: "/koi-types/asagi.jpg",
      title: "Koi Asagi",
      info: "Koi Asagi là tổ tiên của Nishikigoi. Nguồn gốc từ cá chép đen suối. Được tiến hóa theo hướng chọn lọc những em Koi có màu trắng, đỏ và xanh dương để nuôi trong hồ.",
    },
    {
      key: 2,
      urlImg: "/koi-types/benigoi.jpg",
      title: "Koi Benigoi",
      info: "Cá koi Benigoi là dòng cá có màu đơn sắc, toàn bộ vảy, vây cá đều mang màu đỏ trông như quả ớt khổng lồ. Nếu thả trong hồ koi thì cá nổi bật hơn hẳn so với dòng koi khác.",
    },
    {
      key: 3,
      urlImg: "/koi-types/karashi.jpg",
      title: "Koi Karashi",
      info: "Koi Karashi là dòng Koi mới tại Nhật nên thừa hưởng nhiều phẩm chất vượt trội. Ưu điểm của Koi Karashi là kích thước phát triển nhanh trong 1 thời gian ngắn. Là Koi dẫn đàn, rất thân thiện, mạnh dạn với con người.",
    },
    {
      key: 4,
      urlImg: "/koi-types/showa-sanshouku.jpg",
      title: "Koi Showa Sanshouku",
      info: "Cá Koi Showa là dòng Gosanke tiêu chuẩn, thuộc dòng cá Koi nhóm AAA của Nhật Koi Showa hấp dẫn người chơi bởi 3 màu đỏ-đen-trắng. Trong đó, màu trắng (Shiroji) là màu nền, tiếp theo là màu đỏ (Hi) và màu đen (Sumi).",
    },
    {
      key: 5,
      urlImg: "/koi-types/asagi.jpg",
      title: "Koi Asagi",
      info: "Koi Asagi là tổ tiên của Nishikigoi. Nguồn gốc từ cá chép đen suối. Được tiến hóa theo hướng chọn lọc những em Koi có màu trắng, đỏ và xanh dương để nuôi trong hồ.",
    },
    {
      key: 6,
      urlImg: "/koi-types/benigoi.jpg",
      title: "Koi Benigoi",
      info: "Cá koi Benigoi là dòng cá có màu đơn sắc, toàn bộ vảy, vây cá đều mang màu đỏ trông như quả ớt khổng lồ. Nếu thả trong hồ koi thì cá nổi bật hơn hẳn so với dòng koi khác.",
    },
    {
      key: 7,
      urlImg: "/koi-types/karashi.jpg",
      title: "Koi Karashi",
      info: "Koi Karashi là dòng Koi mới tại Nhật nên thừa hưởng nhiều phẩm chất vượt trội. Ưu điểm của Koi Karashi là kích thước phát triển nhanh trong 1 thời gian ngắn. Là Koi dẫn đàn, rất thân thiện, mạnh dạn với con người.",
    },
    {
      key: 8,
      urlImg: "/koi-types/showa-sanshouku.jpg",
      title: "Koi Showa Sanshouku",
      info: "Cá Koi Showa là dòng Gosanke tiêu chuẩn, thuộc dòng cá Koi nhóm AAA của Nhật Koi Showa hấp dẫn người chơi bởi 3 màu đỏ-đen-trắng. Trong đó, màu trắng (Shiroji) là màu nền, tiếp theo là màu đỏ (Hi) và màu đen (Sumi).",
    },
    // {
    //   key: 1,
    //   urlImg: '',
    //   title: '',
    //   info: ''
    // },
    // {
    //   key: 1,
    //   urlImg: '',
    //   title: '',
    //   info: ''
    // },
  ];

  const carouselRef = useRef();

  const handlePrev = () => {
    carouselRef.current.prev();
  };

  const handleNext = () => {
    carouselRef.current.next();
  };
  return (
    <main>
      <section
        className="relative  flex items-center justify-center text-white"
        style={{
          height: "calc(100vh - 100px)",
          marginTop: 100,
          // width: "100vw",
        }}
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

      <section className="item-home">
        <div className="item-home-title">
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

      <section className="introduction-koi">
        <div className="introduntion-koi-wrapper">
          <Carousel
            dots={false}
            slidesToScroll={1}
            slidesToShow={5}
            infinite
            draggable
            ref={carouselRef}
            // className="introduction-koi-wrapper"
          >
            {KoiTypeIntroduction.map((koi) => (
              <KoiIntroductionCart
                key={koi.key}
                koi={koi}
                // onClick={() =>
                //     handleItemClick(
                //         course.courseId,
                //     )
                // }
              />
            ))}
          </Carousel>
          <div
            className="carousel__button"
            style={{
              textAlign: "center",
              marginTop: "20px",
            }}
          >
            <Button onClick={handlePrev} icon={<LeftOutlined />} />
            <Button onClick={handleNext} icon={<RightOutlined />} />
          </div>
        </div>
      </section>
      <section className="item-home koi-feature-container">
        <div className="item-home-title">
          <h2>Koi</h2>
          <h1>CÁ KOI NHẬT BẢN</h1>
          <img src="/icons/fish-line.png" alt="" />
        </div>
        <div className="koi-card-list">
          <KoiCard />
          <KoiCard />
          <KoiCard />
          <KoiCard />
          <KoiCard />
          <KoiCard />
          <KoiCard />
          <KoiCard />
        </div>
      </section>

      <section className="item-home testimonial-container">
        <div className="item-home-title">
          <h2>Testimonials</h2>
          <h1>Khách hàng của chúng tôi nói gì?</h1>
          <img src="/icons/fish-line.png" alt="" />
        </div>
      </section>
    </main>
  );
}

const KoiIntroductionCart = ({ koi }) => {
  return (
    <div className="koi-introduction-cart-container">
      <img src={koi.urlImg} alt={koi.title} />
      <div className="content-koi">{koi.info}</div>
    </div>
  );
};

export default HomePage;
