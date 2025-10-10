import React, { useEffect, useState, useMemo } from "react";
import { Button } from "@/components/ui/button";
import { TableCell, TableRow } from "@/components/ui/table"; // Cần cho renderRow
import axios from "axios";
// Chỉ cần import DataTable và Pagination từ Paginate.jsx
import { DataTable, Pagination } from "@/components/Paginate";
import { Link } from "react-router-dom";

export default function ListTeam() {
    const BEURL = import.meta.env.VITE_BEURL;
    const [loading, setLoading] = useState(false);
    const [team, setTeam] = useState([]);
    console.log("🚀 ~ ListTeam ~ team:", team);
    const token = localStorage.getItem("token");
    const [error, setError] = useState("");

    // ------------------------------------------------
    // LOGIC PHÂN TRANG
    // ------------------------------------------------
    const ITEMS_PER_PAGE = 7; // Có thể đặt là hằng số hoặc dùng useState/Select
    const [currentPage, setCurrentPage] = useState(1);

    // Tính toán tổng số trang
    const totalPages = Math.ceil(team.length / ITEMS_PER_PAGE);

    // Lấy dữ liệu cho trang hiện tại
    const currentItems = useMemo(() => {
        const startIndex = (currentPage - 1) * ITEMS_PER_PAGE;
        const endIndex = startIndex + ITEMS_PER_PAGE;
        return team.slice(startIndex, endIndex);
    }, [currentPage, team]);
    // users được thêm vào dependency vì dữ liệu có thể thay đổi sau khi gọi API

    const handlePageChange = (page) => {
        if (page >= 1 && page <= totalPages) {
            setCurrentPage(page);
        }
    };
    // ------------------------------------------------

    useEffect(() => {
        setLoading(true);
        const fetchTeam = async () => {
            try {
                const response = await axios.get(`${BEURL}/team`, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });
                const teamData = response?.data?.data || [];
                setTeam(teamData);
                if (teamData.length === 0) {
                    setError("Danh sách đội trống.");
                } else {
                    setError("");
                }
            } catch (error) {
                console.error("Lỗi khi tải đội bóng:", error);
                setError("Đã xảy ra lỗi khi tải dữ liệu.");
            } finally {
                setLoading(false);
            }
        };
        fetchTeam();
    }, [BEURL, token]);
    const handleDelete = async (id) => {
        setLoading(true);
        try {
            const response = await axios.delete(`${BEURL}/team/${id}`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
            if (response == null) {
                setLoading(false);
                setError("không thể xóa đội.");
            }
            setTeam((prev) => prev.filter((t) => t.id !== id));
        } catch (error) {
            setError(error);
        } finally {
            setLoading(false);
        }
    };
    // Định nghĩa Header cho bảng
    const teamTableHeaders = [
        { label: "ID", className: "w-[50px]" },
        { label: "Tên đôi", className: "" },
        { label: "Quản lý", className: "" },
        { label: "Số trận đá", className: "" },
        { label: "Số trận thắng", className: "text-center w-[80px]" },
        { label: "Thao tác", className: "text-center w-[150px]" },
    ];

    // Hàm render từng hàng (row)
    const renderTeamRow = (team) => (
        <TableRow key={team.id}>
            <TableCell className="font-medium">{team.id}</TableCell>
            <TableCell>{team.teamName}</TableCell>
            <TableCell>{team.manager.fullName}</TableCell>
            <TableCell>{team.totalMatches}</TableCell>
            <TableCell className="text-center">{team.wins}</TableCell>
            <TableCell className="text-center gap-1 justify-center flex">
                <Link to={`/admin/team/${team.id}`}>
                    <Button className={"bg-blue-500 hover:bg-blue-600 hover:text-white"} variant="ghost" size="sm">
                        Chi tiết
                    </Button>
                </Link>
                <Button
                    handle={() => {
                        handleDelete(team.id);
                    }}
                    className={"bg-red-500 hover:bg-red-600 hover:text-white"}
                    variant="ghost"
                    size="sm"
                >
                    xóa
                </Button>
            </TableCell>
        </TableRow>
    );

    return (
        <div className="p-2">
            <div className="flex justify-between items-center mb-4">
                <h1 className="text-2xl font-sans font-medium">Danh sách đội bóng</h1>
                <Button className="bg-blue-600 hover:bg-blue-700" variant="default">
                    Thêm mới
                </Button>
            </div>

            {loading && <p className="text-center py-4">Đang tải dữ liệu...</p>}
            {error && <p className="text-center py-4 text-red-500">{error}</p>}

            {!loading && !error && team.length > 0 && (
                <DataTable
                    headers={teamTableHeaders}
                    data={currentItems} // Truyền dữ liệu đã được phân trang
                    renderRow={renderTeamRow}
                    totalItems={team.length}
                    currentPage={currentPage}
                    totalPages={totalPages}
                    onPageChange={handlePageChange}
                />
            )}
        </div>
    );
}
