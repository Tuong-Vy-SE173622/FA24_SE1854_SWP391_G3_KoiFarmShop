import React from "react";
import KoiCard from "../../../../components/KoiCard/KoiCard";

function KoiBoughtPage() {
  return (
    <div className="dashboard-main-container">
      <div
        className="test"
        style={{
          // marginLeft: 280,
          display: "flex",
          flexWrap: "wrap",
          justifyContent: "space-evenly",
          rowGap: 20,
        }}
      >
        {Array.from({ length: 12 }).map((_, index) => (
          <KoiCard key={index} />
        ))}
      </div>
    </div>
  );
}

export default KoiBoughtPage;
