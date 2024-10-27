import React, { useState } from "react";
import "./CompareBar.css";
import { FaChevronDown } from "react-icons/fa";
import { MdCompare } from "react-icons/md";
import { IoCloseSharp } from "react-icons/io5";

function CompareBar() {
  const [compareStatus, setCompareStatus] = useState(false);

  const handleCompareStatus = () => {
    return setCompareStatus(!compareStatus);
  };
  return (
    <div className="compare-bar-container">
      {!compareStatus ? (
        <div className="compare-collapse-btn " onClick={handleCompareStatus}>
          <MdCompare /> So sánh
        </div>
      ) : (
        <div className="compare-wrapper">
          <div className="compare-collapse" onClick={handleCompareStatus}>
            Thu gọn <FaChevronDown />
          </div>
          <div className="compare-item">
            <span>
              <IoCloseSharp size={18} />
            </span>
          </div>
          <div className="compare-item">
            <span>
              <IoCloseSharp size={18} />
            </span>
          </div>
          <div className="compare-item"></div>
        </div>
      )}
    </div>
  );
}

export default CompareBar;
