import React from "react";
import { Navigate } from "react-router-dom";

export default function CheckRole({ children, role }) {
    const user = JSON.parse(localStorage.getItem("user"));

    // Nếu chưa đăng nhập
    if (!user) {
        return <Navigate to="/login" replace />;
    }

    // Nếu có role yêu cầu mà user.role không đúng
    if (role && user.role !== 1) {
        return <Navigate to="/" replace />;
    }

    // Nếu đúng quyền thì cho vào
    return children;
}
