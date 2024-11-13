import React, { useEffect, useState } from "react";
import "./SearchPage.css";
import { FaMinus, FaPlus } from "react-icons/fa";
import { Checkbox, ConfigProvider, Pagination } from "antd";
import KoiCard from "../../components/KoiCard/KoiCard";
import { getAllKoiType } from "../../services/KoiTypeService";
import { getAllKoi, getKoiOrigins } from "../../services/KoiService";

const SearchPage = () => {
  const [selectedFilters, setSelectedFilters] = useState({
    Type: [],
    Origin: [],
    Gender: [],
  });
  const [queryParams, setQueryParams] = useState({});
  const [currentPage, setCurrentPage] = useState(1);
  const [koiTypeLs, setKoiTypeLs] = useState([]);
  const [koiLs, setKoiLs] = useState([]);
  const [totalKoiCount, setTotalKoiCount] = useState(0);
  const [originLs, setOriginLs] = useState([]);
  const [sortOption, setSortOption] = useState("");
  const [activeFilters, setActiveFilters] = useState({
    Type: false,
    Origin: false,
    Gender: false,
  });

  const pageSize = 8;

  const filterOptions = {
    Type: koiTypeLs.map((koi) => ({ label: koi.name, value: koi.name })),
    Origin: originLs.map((origin) => ({
      label: origin.charAt(0).toUpperCase() + origin.slice(1),
      value: origin.charAt(0).toUpperCase() + origin.slice(1),
    })),
    Gender: [
      { label: "Đực", value: 1 },
      { label: "Cái", value: 0 },
    ],
  };

  const handlePageChange = (page) => setCurrentPage(page);

  const handleSortChange = (event) => setSortOption(event.target.value);

  const handleActiveFilter = (filterName) => {
    setActiveFilters((prevFilters) => ({
      ...prevFilters,
      [filterName]: !prevFilters[filterName],
    }));
  };

  const handleFilterChange = (title, selectedValues) => {
    if (title === "Gender" || title === "Type") {
      selectedValues = selectedValues.slice(-1); // Chỉ giữ giá trị cuối cùng
    }

    setSelectedFilters((prevFilters) => ({
      ...prevFilters,
      [title]: selectedValues,
    }));
  };

  const fetchKoiType = async () => {
    try {
      const data = await getAllKoiType();
      setKoiTypeLs(data);
    } catch (err) {
      console.error("Failed to fetch Koi types", err);
    }
  };

  const fetchKoi = async (page) => {
    const params = {
      SearchByTypeName: queryParams.searchData,
      PageNumber: page,
      PageSize: pageSize,
      IsSortedByPrice: sortOption.includes("price"),
      IsAscending: sortOption === "price-desc",
      KoiTypeName: selectedFilters.Type.join(","),
      Gender: selectedFilters.Gender.length
        ? selectedFilters.Gender[0]
        : undefined,
      Origin: selectedFilters.Origin.join(","),
    };

    try {
      const result = await getAllKoi(params);
      console.log("koi search", result);

      setKoiLs(result.data);
      setTotalKoiCount(result.totalRecords);
    } catch (err) {
      console.error("Failed to fetch Koi data", err);
    }
  };

  // const fetchNumberKoi = async () => {
  //   try {
  //     const result = await getAllKoi({
  //       PageSize: 100,
  //       KoiTypeName: selectedFilters.Type.join(","),
  //       Gender: selectedFilters.Gender.length
  //         ? selectedFilters.Gender[0]
  //         : undefined,
  //       Origin: selectedFilters.Origin.join(","),
  //     });
  //     setTotalKoiCount(result.data.length);
  //   } catch (err) {
  //     console.error("Failed to fetch Koi data", err);
  //   }
  // };

  const fetchKoiOrigins = async () => {
    try {
      const result = await getKoiOrigins();
      setOriginLs(result.data);
    } catch (err) {
      console.error("Failed to fetch Koi Origin Data", err);
    }
  };

  useEffect(() => {
    const params = new URLSearchParams(window.location.search);
    const queryObj = {};

    // Duyệt qua các tham số và lưu vào queryObj
    params.forEach((value, key) => {
      queryObj[key] = value;
    });

    // Cập nhật state với các tham số từ URL
    setQueryParams(queryObj);
    console.log("Các tham số từ URL:", queryObj);

    fetchKoiType();
    fetchKoi(currentPage);

    // fetchNumberKoi();
    fetchKoiOrigins();
  }, []); // Run once on mount

  useEffect(() => {
    fetchKoi(currentPage);
    // fetchNumberKoi();
  }, [currentPage, sortOption, selectedFilters, queryParams.searchData]);

  // console.log("koi", koiLs);
  return (
    <div style={{ marginTop: 100 }} className="search-page-container">
      <div className="filter-container">
        <h1 style={{ fontSize: 25, fontWeight: 600, marginLeft: 16 }}>
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
          <h2 style={{ fontSize: 22, fontWeight: 500 }}>
            {totalKoiCount} kết quả
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
            <option value="" disabled>
              -- Select Sort Option --
            </option>
            <option value="views">Xem nhiều</option>
            <option value="price-asc">Giá Cao - Thấp</option>
            <option value="price-desc">Giá Thấp - Cao</option>
          </select>
        </div>
        <div className="search-result-wrapper">
          <div className="search-result-list">
            {koiLs.map((koi) => (
              <KoiCard key={koi.koiId} koi={koi} />
            ))}
          </div>
          <Pagination
            current={currentPage}
            style={{ marginTop: 25 }}
            total={totalKoiCount}
            pageSize={pageSize}
            onChange={handlePageChange}
          />
        </div>
      </div>
    </div>
  );
};

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
            value={selectedFilters || []}
          />
        </ConfigProvider>
      </div>
    </div>
  );
};

export default SearchPage;
