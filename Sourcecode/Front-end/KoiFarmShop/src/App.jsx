import Header from "./components/Header/Header";
import "./App.css";
import Footer from "./components/Footer/Footer";
import HomePage from "./pages/homePage/HomePage";
import { RouterProvider } from "react-router-dom";
import { router } from "./routers/routers";

function App() {
  return (
    <>
      <RouterProvider router={router} />
    </>
  );
}

export default App;
