import React, { useEffect, useState, useMemo } from "react";
import { Button } from "@/components/ui/button";
import { TableCell, TableRow } from "@/components/ui/table"; // Cần cho renderRow
import axios from "axios";
// Chỉ cần import DataTable và Pagination từ Paginate.jsx
import { DataTable, Pagination } from "@/components/Paginate";
import { Link } from "react-router-dom";

export default function ListUser() {
    const BEURL = import.meta.env.VITE_BEURL;
    const [loading, setLoading] = useState(false);
    const [users, setUsers] = useState([]);
    const token = localStorage.getItem("token");
    const [error, setError] = useState("");

    // ------------------------------------------------
    // LOGIC PHÂN TRANG
    // ------------------------------------------------
    const ITEMS_PER_PAGE = 5; // Có thể đặt là hằng số hoặc dùng useState/Select
    const [currentPage, setCurrentPage] = useState(1);

    // Tính toán tổng số trang
    const totalPages = Math.ceil(users.length / ITEMS_PER_PAGE);

    // Lấy dữ liệu cho trang hiện tại
    const currentItems = useMemo(() => {
        const startIndex = (currentPage - 1) * ITEMS_PER_PAGE;
        const endIndex = startIndex + ITEMS_PER_PAGE;
        return users.slice(startIndex, endIndex);
    }, [currentPage, users]);
    // users được thêm vào dependency vì dữ liệu có thể thay đổi sau khi gọi API

    const handlePageChange = (page) => {
        if (page >= 1 && page <= totalPages) {
            setCurrentPage(page);
        }
    };
    // ------------------------------------------------

    useEffect(() => {
        setLoading(true);
        const fetchUser = async () => {
            try {
                const response = await axios.get(`${BEURL}/auth`, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });
                const usersData = response?.data?.data || [];
                setUsers(usersData);
                if (usersData.length === 0) {
                    setError("Danh sách người dùng rỗng.");
                } else {
                    setError("");
                }
            } catch (error) {
                console.error("Lỗi khi tải người dùng:", error);
                setError("Đã xảy ra lỗi khi tải dữ liệu.");
            } finally {
                setLoading(false);
            }
        };
        fetchUser();
    }, [BEURL, token]);

    // Định nghĩa Header cho bảng
    const userTableHeaders = [
        { label: "ID", className: "w-[50px]" },
        { label: "Tên đầy đủ", className: "" },
        { label: "Email", className: "" },
        { label: "Số điện thoại", className: "" },
        { label: "Vai trò", className: "text-center w-[80px]" },
        { label: "Thao tác", className: "text-right w-[150px]" },
    ];

    // Hàm render từng hàng (row)
    const renderUserRow = (user) => (
        <TableRow key={user.id}>
            <TableCell className="font-medium">{user.id}</TableCell>
            <TableCell>{user.fullName}</TableCell>
            <TableCell>{user.email}</TableCell>
            <TableCell>{user.phoneNumber}</TableCell>
            <TableCell className="text-center">{user.role === 1 ? "Admin" : "User"}</TableCell>
            <TableCell className="text-right">
                <Link to={`/admin/user/${user.id}`}>
                    <Button variant="ghost" size="sm">
                        Xem
                    </Button>
                </Link>
            </TableCell>
        </TableRow>
    );

    return (
        <div className="p-2">
            <div className="flex justify-between items-center mb-4">
                <h1 className="text-2xl font-sans font-medium">Danh sách người dùng</h1>
                <Button className="bg-blue-600 hover:bg-blue-700" variant="default">
                    Thêm mới
                </Button>
            </div>

            {loading && <p className="text-center py-4">Đang tải dữ liệu...</p>}
            {error && <p className="text-center py-4 text-red-500">{error}</p>}

            {!loading && !error && users.length > 0 && (
                <DataTable
                    headers={userTableHeaders}
                    data={currentItems} // Truyền dữ liệu đã được phân trang
                    renderRow={renderUserRow}
                    totalItems={users.length}
                    currentPage={currentPage}
                    totalPages={totalPages}
                    onPageChange={handlePageChange}
                />
            )}
        </div>
    );
}
