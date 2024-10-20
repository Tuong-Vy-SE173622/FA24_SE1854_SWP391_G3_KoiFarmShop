import "./App.css";
import { RouterProvider } from "react-router-dom";
import { router } from "./routers/routers";
// import ScrollToTop from "./components/ScrollTop/ScrollToTop";

function App() {
  return (
    <>
      <RouterProvider router={router} />
    </>
  );
}

export default App;
