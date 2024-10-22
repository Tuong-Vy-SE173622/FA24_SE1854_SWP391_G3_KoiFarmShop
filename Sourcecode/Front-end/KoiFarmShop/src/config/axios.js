import axios from "axios";
import { API_URL } from "../constants";

const config = {
  baseURL: API_URL,
  timeout: 30000,
  headers: {
    "Content-Type": "application/json",
  },
};

const api = axios.create(config);

export default api;
