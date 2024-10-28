import React, { useEffect, useState } from "react";
import "./CompareBar.css";
import { FaChevronDown } from "react-icons/fa";
import { MdCompare } from "react-icons/md";
import { IoCloseSharp } from "react-icons/io5";
import { useNavigate } from "react-router-dom";

function CompareBar() {
  const navigate = useNavigate();
  const [compareStatus, setCompareStatus] = useState(false);
  const [compareData, setCompareData] = useState([]);

  const handleCompareStatus = () => setCompareStatus(!compareStatus);

  useEffect(() => {
    const updateCompareData = () => {
      const storedCompareData =
        JSON.parse(localStorage.getItem("Compare")) || [];
      setCompareData(storedCompareData);
    };
    updateCompareData();
    const interval = setInterval(updateCompareData, 1000);
    return () => clearInterval(interval);
  }, []);

  const removeFromCompare = (koi) => {
    let compareList = JSON.parse(localStorage.getItem("Compare")) || [];
    compareList = compareList.filter((element) => element.koiId !== koi.koiId);
    localStorage.setItem("Compare", JSON.stringify(compareList));
    setCompareData(compareList);
  };

  const clearCompare = () => {
    localStorage.removeItem("Compare");
    setCompareData([]);
  };

  const handleComparePage = () => navigate("/compare");

  return (
    <div className="compare-bar-container">
      {!compareStatus ? (
        <div className="compare-collapse-btn" onClick={handleCompareStatus}>
          <MdCompare /> So sánh
        </div>
      ) : (
        <div className="compare-wrapper">
          <div className="compare-collapse" onClick={handleCompareStatus}>
            Thu gọn <FaChevronDown />
          </div>

          {compareData.length > 0
            ? compareData.map((koi, index) => (
                <div className="compare-item" key={index}>
                  <span onClick={() => removeFromCompare(koi)}>
                    <IoCloseSharp size={18} />
                  </span>
                  <img
                    src="https://5.imimg.com/data5/QH/WR/MY-47947495/koi-carp-aquarium-fish.jpg"
                    alt="koi"
                  />
                  <p>{koi.koiTypeName}</p>
                </div>
              ))
            : null}

          {compareData.length < 2 &&
            Array.from({ length: 2 - compareData.length }).map((_, index) => (
              <div className="compare-item" key={`empty-${index}`}>
                {/* <Empty description="No Koi Selected" /> */}
                <div className="empty-koi-wrapper">
                  <img
                    src="https://t4.ftcdn.net/jpg/07/04/77/37/360_F_704773727_BvcnLMqb0lFbrBzFa7710ubt8qZrs1tO.jpg"
                    alt="empty koi"
                  />
                  <p>No Koi Selected</p>
                </div>
              </div>
            ))}

          <div className="compare-item">
            <div
              className={`btn-link-compare ${
                compareData.length === 2 ? "" : "disabled"
              }`}
              onClick={handleComparePage}
            >
              So sánh ngay
            </div>
            <p className="btn-delete-compare-koi" onClick={clearCompare}>
              Xóa tất cả cá Koi
            </p>
          </div>
        </div>
      )}
    </div>
  );
}

export default CompareBar;
