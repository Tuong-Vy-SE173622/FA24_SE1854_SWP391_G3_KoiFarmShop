import React from "react";
import { Line, Pie } from "@ant-design/plots";
import { Col, Row } from "antd";
import "./AdminDashboardPage.css";

function AdminDashboardPage() {
  // Dữ liệu cho biểu đồ doanh thu
  const revenueData = [
    { month: "Tháng 10", revenue: 1000000000 },
    { month: "Tháng 11", revenue: 1200000000 },
    { month: "Tháng 12", revenue: 1500000000 },
  ];

  // const revenueData = [
  //   { month: "Tháng 10", revenue: "1000000000" },
  //   { month: "Tháng 11", revenue: "1200000000" },
  //   { month: "Tháng 12", revenue: "1500000000" },
  // ];

  const revenueConfig = {
    data: revenueData,
    xField: "month",
    yField: "revenue",
    // label: {
    //   position: "top",
    //   style: { fill: "#595959", fontWeight: 600 },
    //   formatter: ({ revenue }) =>
    //     `${Intl.NumberFormat("vi-VN").format(revenue)} VNĐ`,
    // },
    // tooltip: {
    //   formatter: () => ({
    //     name: "Doanh thu",
    //     // value: `${Intl.NumberFormat("vi-VN").format(item.revenue)} VNĐ`,
    //   }),
    // },
    color: "#1890ff",
    smooth: true,
    xAxis: {
      title: { text: "Tháng", style: { fontWeight: 600 } },
    },
    yAxis: {
      title: { text: "Doanh thu (VNĐ)", style: { fontWeight: 600 } },
      label: {
        formatter: (value) =>
          `${Intl.NumberFormat("vi-VN").format(Number(value))}`,
      },
    },
    animation: {
      appear: {
        animation: "path-in",
        duration: 1000,
      },
    },
    point: {
      size: 5,
      shape: "circle",
    },
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
    label: {
      type: "outer",
      content: "{name} ({percentage})",
      style: {
        fontSize: 14,
      },
    },
    tooltip: {
      formatter: (item) => ({
        name: item.type,
        value: `${item.value}%`,
      }),
    },
    interactions: [{ type: "element-active" }],
    color: ["#ff7875", "#ffa940", "#36cfc9"],
  };

  return (
    <div
      style={{
        display: "flex",
        gap: 10,
        flexDirection: "column",
        padding: "1rem",
        paddingTop: 0,
        // backgroundColor: "lightskyblue",
      }}
    >
      <div>
        <Row gutter={23} className="sd-achievements">
          <Col span={6} className="responsive-col">
            <div className="sd-achievement-item">
              <p className="achievement-title">Total Koi</p>
              <div className="achievement-info">
                <div className="achievement-info-left">
                  <p className="achievement-number">100</p>
                  <span
                    style={{
                      backgroundColor: "#52C41A",
                    }}
                  >
                    New
                  </span>
                </div>

                <div className="achievement-icon">
                  <img src="/icons/koi-fish.png" alt="achievement-icon" />
                </div>
              </div>
            </div>
          </Col>
          <Col span={6} className="responsive-col">
            <div className="sd-achievement-item">
              <p className="achievement-title">Total Customer</p>
              <div className="achievement-info">
                <div className="achievement-info-left">
                  <p className="achievement-number">30</p>
                  <span
                    style={{
                      backgroundColor: "#CCA1FF",
                    }}
                  >
                    New
                  </span>
                </div>

                <div className="achievement-icon">
                  <img src="/icons/user.png" alt="achievement-icon" />
                </div>
              </div>
            </div>
          </Col>
          {/* <Col span={12} className="responsive-col-full">
                            <div className="sd-achievement-item">
                                <p className="achievement-title">Achievement</p>
                                <div
                                    style={{
                                        display: 'flex',
                                        justifyContent: 'center',
                                    }}
                                >
                                    <div className="achievement-icon-goals">
                                        {achievementItem.map((goal) => (
                                            <div
                                                key={goal.id}
                                                style={{
                                                    display: 'flex',
                                                    alignItems: 'center',
                                                    flexDirection: 'column',
                                                }}
                                            >
                                                <img
                                                    className={
                                                        goal.status === 'true'
                                                            ? ''
                                                            : 'not-achievement'
                                                    }
                                                    src={goal.icon}
                                                    alt="achievement-item"
                                                />
                                                <p>{goal.label}</p>
                                            </div>
                                        ))}
                                    </div>
                                </div>
                            </div>
                        </Col> */}
        </Row>
      </div>
      <div
        style={{
          height: "100%",
          padding: "15px 10px",
          backgroundColor: "#f5f5f5",
          borderRadius: "8px",
        }}
      >
        <h2 style={{ textAlign: "center" }}>Biểu đồ doanh thu</h2>
        <Line {...revenueConfig} height={340} />
      </div>
      {/* <div
        style={{
          backgroundColor: "#f5f5f5",
          padding: "15px 10px",
          borderRadius: "8px",
        }}
      >
        <h2 style={{ textAlign: "center" }}>Biểu đồ loại cá koi bán chạy</h2>
        <Pie {...koiTypeConfig} height={350} />
      </div> */}
    </div>
  );
}

export default AdminDashboardPage;
