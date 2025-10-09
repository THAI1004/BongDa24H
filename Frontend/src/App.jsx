import React from "react";
import { Route, Routes } from "react-router-dom";
import Home from "./pages/Client/Home";
import HomeAdmin from "./pages/Admin/HomeAdmin.jsx";
import ClientLayout from "./layout/Client/ClientLayout";
import Login from "./pages/Auth/Login";
import Register from "./pages/Auth/Register";
import { Toaster } from "sonner";
import CheckRole from "./components/CheckRole";
import AdminLayout from "./layout/Admin/AdminLayout";
import Trang1 from "./pages/Admin/Trang1";
import Profile from "./pages/Admin/Profile";
import ListUser from "./pages/Admin/User/ListUser";
import UserDetail from "./pages/Admin/User/UserDetail";

function App() {
    return (
        // 2. Xóa thẻ <Router> và </Router> đi
        <div>
            <Routes>
                <Route path="/" element={<ClientLayout />}>
                    <Route index element={<Home />} />
                    <Route path="profile" element={<Profile />} />

                    {/* Ví dụ: Thêm các trang khác cũng dùng layout này */}
                    {/* <Route path="batdoi" element={<BatDoiPage />} /> */}
                    {/* <Route path="tintuc" element={<TinTucPage />} /> */}
                </Route>
                <Route
                    path="/admin"
                    element={
                        <CheckRole role="admin">
                            <AdminLayout />
                        </CheckRole>
                    }
                >
                    <Route index element={<HomeAdmin />} />
                    <Route path="user" element={<ListUser />} />
                    <Route path="user/:id" element={<UserDetail />} />
                    <Route path="profile" element={<Profile />} />
                </Route>
                <Route path="login" element={<Login />} />
                <Route path="register" element={<Register />} />
            </Routes>
            <Toaster position="top-center" />
        </div>
    );
}

export default App;
