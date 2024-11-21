// import React from "react";
// import { Line, Pie } from "@ant-design/plots";

// const Test = () => {
//   // Dữ liệu cho biểu đồ doanh thu
//   const revenueData = [
//     { month: "Tháng 10", revenue: 1000000000 },
//     { month: "Tháng 11", revenue: 1200000000 },
//     { month: "Tháng 12", revenue: 1500000000 },
//   ];

//   const revenueConfig = {
//     data: revenueData,
//     xField: "month",
//     yField: "revenue",
//     label: {
//       position: "middle",
//       style: { fill: "#595959", opacity: 0.6 },
//     },
//     point: {
//       size: 5,
//       shape: "diamond",
//     },
//     tooltip: {
//       formatter: (item) => ({
//         name: "Doanh thu",
//         value: `${item.revenue.toLocaleString()} VNĐ`,
//       }),
//     },
//     color: "#1890ff",
//     smooth: true,
//   };

//   // Dữ liệu cho biểu đồ loại cá koi bán chạy
//   const koiTypeData = [
//     { type: "Shusui", value: 45 },
//     { type: "Kohaku", value: 35 },
//     { type: "Showa", value: 20 },
//   ];

//   const koiTypeConfig = {
//     appendPadding: 10,
//     data: koiTypeData,
//     angleField: "value",
//     colorField: "type",
//     radius: 0.9,
//     label: {
//       type: "inner",
//       offset: "-30%",
//       content: "{value}",
//       style: {
//         textAlign: "center",
//         fontSize: 14,
//       },
//     },
//     tooltip: {
//       formatter: (item) => ({
//         name: item.type,
//         value: `${item.value}%`,
//       }),
//     },
//     interactions: [{ type: "element-active" }],
//   };

//   return (
//     <div
//       style={{
//         marginTop: 120,
//         height: 400,
//         display: "flex",
//         padding: "1rem",
//       }}
//     >
//       <div style={{ width: 800, height: "100%", padding: "15px 20px" }}>
//         <h2>Biểu đồ doanh thu</h2>
//         <Line {...revenueConfig} />
//       </div>
//       <div style={{ backgroundColor: "lightblue", width: 400 }}>
//         <h2>Biểu đồ loại cá koi bán chạy</h2>
//         <Pie {...koiTypeConfig} height={350} />
//       </div>
//     </div>
//   );
// };

// export default Test;

import React from "react";
import { Line, Pie } from "@ant-design/plots";

const Test = () => {
  // Dữ liệu cho biểu đồ doanh thu
  const revenueData = [
    { month: "Tháng 10", revenue: 1000000000 },
    { month: "Tháng 11", revenue: 1200000000 },
    { month: "Tháng 12", revenue: 1500000000 },
  ];

  const revenueConfig = {
    data: revenueData,
    xField: "month",
    yField: "revenue",
    label: {
      position: "top", // Thay "middle" bằng "top" hoặc một giá trị hợp lệ
      style: { fill: "#595959", opacity: 0.6 },
    },
    // tooltip: {
    //   formatter: (item) => ({
    //     name: "Doanh thu",
    //     value: `${item.revenue.toLocaleString()} VNĐ`,
    //   }),
    // },
    color: "#1890ff",
    smooth: true,
  };

  // Dữ liệu cho biểu đồ loại cá koi bán chạy
  const koiTypeData = [
    { type: "Shusui", value: 45 },
    { type: "Kohaku", value: 35 },
    { type: "Showa", value: 20 },
  ];

  const koiTypeConfig = {
    appendPadding: 10,
    data: koiTypeData,
    angleField: "value",
    colorField: "type",
    radius: 0.9,
    // label: {
    //   type: "inner",
    //   offset: "-30%",
    //   content: "{value}", // Sử dụng {percentage} để hiển thị tỷ lệ phần trăm
    //   style: {
    //     textAlign: "center",
    //     fontSize: 14,
    //   },
    // },
    // tooltip: {
    //   formatter: (item) => ({
    //     name: item.type,
    //     value: `${item.value}%`,
    //   }),
    // },
    interactions: [{ type: "element-active" }],
  };

  return (
    <div
      style={{
        marginTop: 120,
        height: 400,
        display: "flex",
        padding: "1rem",
      }}
    >
      <div style={{ width: 800, height: "100%", padding: "15px 20px" }}>
        <h2>Biểu đồ doanh thu</h2>
        <Line {...revenueConfig} />
      </div>
      <div style={{ backgroundColor: "lightblue", width: 400 }}>
        <h2>Biểu đồ loại cá koi bán chạy</h2>
        <Pie {...koiTypeConfig} height={350} />
      </div>
    </div>
  );
};

export default Test;
