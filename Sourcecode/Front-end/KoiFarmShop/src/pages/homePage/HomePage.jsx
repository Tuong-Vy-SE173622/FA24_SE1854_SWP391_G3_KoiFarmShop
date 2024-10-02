import React, { useRef, useState } from "react";
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

  const TestimonialsList = [
    {
      key: 1,
      avatarImgUrl:
        "https://img.freepik.com/premium-photo/anime-girl-with-long-hair_1108314-51771.jpg",
      userName: "Nguyen Van A",
      testimonial:
        "Dịch vụ chăm sóc cá Koi rất chuyên nghiệp. Tôi gửi nuôi cá trong 6 tháng và chất lượng cá vẫn đảm bảo. Đội ngũ hỗ trợ tận tâm, giá cả hợp lý.",
    },
    {
      key: 2,
      avatarImgUrl:
        "https://img.freepik.com/premium-photo/anime-girl-with-long-hair_1108314-51771.jpg",
      userName: "Le Thi B",
      testimonial:
        "Tôi đã mua 2 con cá Koi từ trang web và chất lượng rất tốt. Cá đẹp, khoẻ mạnh, giao hàng nhanh chóng. Sẽ tiếp tục ủng hộ trong tương lai!",
    },
    {
      key: 3,
      avatarImgUrl:
        "https://img.freepik.com/premium-photo/anime-girl-with-long-hair_1108314-51771.jpg",
      userName: "Tran Van C",
      testimonial:
        "Trang web bán cá Koi uy tín, đa dạng chủng loại cá. Dịch vụ gửi nuôi cũng rất tiện lợi, cá của tôi được chăm sóc tốt, tăng trưởng đều đặn.",
    },
    {
      key: 4,
      avatarImgUrl:
        "https://img.freepik.com/premium-photo/anime-girl-with-long-hair_1108314-51771.jpg",
      userName: "Pham Thi D",
      testimonial:
        "Tôi rất hài lòng với dịch vụ gửi nuôi cá Koi. Cá của tôi được chăm sóc kỹ lưỡng, sạch sẽ. Khi nhận lại cá, chúng vẫn khoẻ và phát triển tốt.",
    },
    // {
    //   key: 5,
    //   avatarImgUrl: "https://example.com/avatar5.jpg",
    //   userName: "Hoang Van E",
    //   testimonial:
    //     "Cửa hàng không chỉ bán cá Koi chất lượng cao mà còn có dịch vụ chăm sóc và nuôi hộ cực kỳ chu đáo. Tôi rất an tâm khi gửi cá tại đây.",
    // },
    // {
    //   key: 6,
    //   avatarImgUrl: "https://example.com/avatar6.jpg",
    //   userName: "Do Thi F",
    //   testimonial:
    //     "Dịch vụ chăm sóc cá của website này thực sự đáng tin cậy. Tôi đã gửi nuôi cá Koi trong 3 tháng và nhận lại cá khỏe mạnh hơn, môi trường rất an toàn.",
    // },
    // {
    //   key: 7,
    //   avatarImgUrl: "https://example.com/avatar7.jpg",
    //   userName: "Vo Van G",
    //   testimonial:
    //     "Tôi mua cá Koi từ trang này và thấy rất hài lòng. Cá rất đẹp, giao hàng nhanh, dịch vụ hỗ trợ rất tận tâm. Tôi cũng đã gửi nuôi và rất yên tâm.",
    // },
    // {
    //   key: 8,
    //   avatarImgUrl: "https://example.com/avatar8.jpg",
    //   userName: "Vu Thi H",
    //   testimonial:
    //     "Trang web bán cá Koi này thực sự có uy tín, cá chất lượng cao và dịch vụ nuôi hộ rất tiện lợi. Tôi đã gửi nuôi trong 4 tháng và rất hài lòng.",
    // },
  ];

  const [posts] = useState([
    {
      id: 1,
      title: "Koi Fish for Sale: Choose the Best for Your Pond",
      author: "Koi Specialist",
      date: "2023-09-10",
      excerpt:
        "Learn how to select healthy and vibrant Koi fish for your home pond.",
      image: "/thumb.png",
    },
    {
      id: 2,
      title: "Koi Care Services: Ensuring Your Fish Thrive",
      author: "Koi Care Expert",
      date: "2023-09-05",
      excerpt:
        "Professional koi fish care services to keep your pond healthy and your fish happy.",
      image: "/thumb.png",
    },
    {
      id: 3,
      title: "Feeding Koi: Best Food for Healthy Growth",
      author: "Koi Nutritionist",
      date: "2023-09-01",
      excerpt:
        "Discover the best Koi food to promote growth, color, and overall health.",
      image: "/thumb.png",
    },
  ]);

  const carouselRef = useRef();

  const chunkArray = (array, chunkSize) => {
    const result = [];
    for (let i = 0; i < array.length; i += chunkSize) {
      result.push(array.slice(i, i + chunkSize));
    }
    return result;
  };

  const chunks = chunkArray(TestimonialsList, 4);

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
            backgroundImage: "url('../../../public/thumb.png')",
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

        <div className="home-testimonial-items">
          {TestimonialsList.map((testimonial, index) => {
            return (
              <div className="testimonial-wrapper" key={index}>
                <img
                  src={testimonial.avatarImgUrl}
                  alt={testimonial.userName}
                  className="customer-testimonial-img"
                />
                <div className="testimonial-info">
                  <p>{testimonial.testimonial}</p>
                  <h3>{testimonial.userName}</h3>
                </div>
              </div>
            );
          })}
        </div>
      </section>

      <section className="item-home">
        <div className="item-home-title">
          <h2>Our Blog News</h2>
          <h1>Tin tức mới nhất</h1>
          <img src="/icons/fish-line.png" alt="" />
        </div>
        {/* <div
          className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6"
          style={{
            width: "75%",
            marginTop: "1.5rem",
          }}
        >
          {blogPosts.map((post) => (
            <div
              key={post.id}
              className="bg-white rounded-lg overflow-hidden shadow-md transition-transform duration-300 hover:scale-105"
            >
              <img
                src={post.image}
                alt={post.title}
                className="w-full h-48 object-cover"
              />
              <div className="p-6">
                <h3 className="text-xl font-semibold mb-2">{post.title}</h3>
                <p className="text-gray-600 mb-4">{post.excerpt}</p>
                <div className="flex justify-between items-center mt-auto">
                  <span className="text-sm text-gray-500">{post.date}</span>
                  <button className="text-blue-600 hover:text-blue-800 transition-colors duration-300">
                    Read More
                  </button>
                </div>
              </div>
            </div>
          ))}
        </div> */}
        <div
          className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 "
          style={{
            marginTop: "1.5rem",
            width: "65%",
          }}
        >
          {posts.map((post) => (
            <div
              key={post.id}
              className="bg-white rounded-lg shadow-md overflow-hidden"
            >
              <img
                src={post.image}
                alt={post.title}
                className="w-full h-48 object-cover"
              />
              <div className="p-4">
                <h3 className="text-xl font-semibold mb-2">{post.title}</h3>
                <p className="text-gray-600 mb-4">{post.excerpt}</p>
                <div className="flex justify-between items-center text-sm text-gray-500">
                  <span>{post.author}</span>
                  <span>{post.date}</span>
                </div>
                <div className="mt-4 flex justify-between items-center">
                  <button className="text-blue-600 hover:text-blue-800">
                    Read More
                  </button>
                  {/* <div className="flex space-x-2">
                    <button className="text-gray-600 hover:text-blue-600">
                      <FaShare />
                    </button>
                  </div> */}
                </div>
              </div>
            </div>
          ))}
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
