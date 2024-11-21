import React, { useState } from "react";
import { getKoiPending } from "../../../../services/KoiService";

function NotificationPage() {
  const [koiLs, setKoiLs] = useState([]);
  const [totalKoiCount, setTotalKoiCount] = useState(0);
  const pageSize = 5;

  const fetchKoi = async () => {
    try {
      const res = await getKoiPending();
    } catch (err) {
      console.log("fetch koi to accept fail: ", err);
    }
  };

  return <div>NotificationPage</div>;
}

export default NotificationPage;
