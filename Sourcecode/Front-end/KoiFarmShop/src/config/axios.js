import axios from "axios";
import { API_URL } from "../constants";

const config = {
  baseURL: API_URL,
  timeout: 30000,
};

const api = axios.create(config);



export default api;
