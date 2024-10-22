import React, { useEffect, useState } from "react";
import "./SearchPage.css";
// import { fetchAllKois } from "../../config/KoiAxios";
import { FaMinus, FaPlus } from "react-icons/fa";
import { Checkbox, ConfigProvider, Pagination } from "antd";
import KoiCard from "../../components/KoiCard/KoiCard";

function SearchPage() {
  const [activeFilters, setActiveFilters] = useState({
    Type: false,
    Origin: false,
    Generation: false,
  });
  const [koiList, setKoiList] = useState([]);
  const [selectedFilters, setSelectedFilters] = useState({});

  const filterOptions = {
    Type: [
      { label: "Koi  Asagi", value: "Koi  Asagi" },
      { label: "Koi  Karashi", value: "Koi  Karashi" },
      { label: "Koi  Ogon", value: "Koi  Ogon" },
      { label: "Koi  Showa", value: "Koi  Showa" },
    ],
    Generation: [
      { label: "Thuần chủng", value: "Thuần chủng" },
      { label: "Thuần Việt", value: "Thuần Việt" },
      { label: "F1", value: "F1" },
      { label: "F2", value: "F2" },
    ],
    Origin: [
      { label: "Nhật Bản", value: "Nhật Bản" },
      { label: "Việt Nam", value: "Việt Nam" },
    ],
  };

  // const sortOptions = {

  // }

  const [sortOption, setSortOption] = useState(null);

  const handleSortChange = (event) => {
    const value = event.target.value;
    setSortOption(value);
    console.log("Selected Sort Option:", value);
    // Xử lý logic sắp xếp ở đây
  };

  const handleActiveFilter = (filterName) => {
    setActiveFilters((prevFilters) => ({
      ...prevFilters,
      [filterName]: !prevFilters[filterName],
    }));
  };

  const handleFilterChange = (title, selectedValues) => {
    setSelectedFilters((prevFilters) => ({
      ...prevFilters,
      [title]: selectedValues,
    }));
  };

  // useEffect(() => {
  //   const koiData = fetchAllKois();
  //   setKoiList(koiData);
  //   console.log(koiData);
  // }, []);

  return (
    <div style={{ marginTop: 100 }} className="search-page-container">
      <div className="filter-container">
        <h1
          style={{
            fontSize: 25,
            fontWeight: 600,
            marginLeft: 16,
          }}
        >
          Filter
        </h1>
        {Object.entries(filterOptions).map(([filterName, options]) => (
          <FilterComponent
            key={filterName}
            title={filterName}
            options={options}
            activeFilter={activeFilters[filterName]}
            handleActiveFilter={() => handleActiveFilter(filterName)}
            onChange={handleFilterChange}
            selectedFilters={selectedFilters[filterName]}
          />
        ))}
      </div>
      <div className="search-wrapper">
        <div className="nresult-sort">
          <h2
            style={{
              fontSize: 22,
              fontWeight: 500,
            }}
          >
            4 kết quả
          </h2>
          <select
            value={sortOption}
            onChange={handleSortChange}
            style={{
              width: "200px",
              padding: "8px",
              border: "1px solid #0080ff",
              borderRadius: 8,
              outline: "none",
            }}
          >
            <option value="price-asc">Giá Cao - Thấp</option>
            <option value="price-desc">Giá Thấp - Cao</option>
          </select>
        </div>

        <div className="search-result-wrapper">
          {Array.from({ length: 12 }).map((_, index) => (
            <KoiCard key={index} />
          ))}
          <Pagination
            align="center"
            style={{ marginTop: 15 }}
            defaultCurrent={1}
            total={50}
          />
        </div>
      </div>
    </div>
  );
}

const FilterComponent = ({
  title,
  options,
  activeFilter,
  handleActiveFilter,
  onChange,
  selectedFilters,
}) => {
  const handleCheckboxChange = (checkedValues) => {
    onChange(title, checkedValues);
  };

  return (
    <div className="filter">
      <div className="filter-items">
        <div className="item-filter">
          <h3 className="filter-name">{title}</h3>
          <button className="filter-btn" onClick={handleActiveFilter}>
            {activeFilter ? <FaMinus /> : <FaPlus />}
          </button>
        </div>
      </div>
      <div className={`item-checkbox ${activeFilter ? "active" : ""}`}>
        <ConfigProvider
          theme={{
            token: {
              borderRadiusSM: 10,
              paddingXS: 15,
              fontSize: 18,
            },
          }}
        >
          <Checkbox.Group
            options={options}
            onChange={handleCheckboxChange}
            className="checkbox-group"
            value={selectedFilters}
          />
        </ConfigProvider>
      </div>
    </div>
  );
};

export default SearchPage;
