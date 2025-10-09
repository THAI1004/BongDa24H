import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./App.jsx";
import { BrowserRouter } from "react-router-dom";
import { GoogleOAuthProvider } from "@react-oauth/google";
const googleClientId = "346888899163-cdkl3ldp5fc3oj6av1n11iu9hoqh3trr.apps.googleusercontent.com";
createRoot(document.getElementById("root")).render(
    <BrowserRouter>
        <GoogleOAuthProvider clientId={googleClientId}>
            {/* <StrictMode> */}
            <App />
            {/* </StrictMode> */}
        </GoogleOAuthProvider>
    </BrowserRouter>
);
