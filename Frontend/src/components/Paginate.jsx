// src/components/Paginate.jsx
import React from "react";
import { Button } from "@/components/ui/button";

// --------------------------------------------------------
// COMPONENT PHÂN TRANG (PAGINATION) - Bạn đã viết
// --------------------------------------------------------
export function Pagination({ currentPage, totalPages, onPageChange }) {
    // Tạo mảng các số trang
    // Giới hạn số lượng nút trang để tránh quá nhiều nút khi có hàng trăm trang
    const maxVisiblePages = 5;
    let startPage = Math.max(1, currentPage - Math.floor(maxVisiblePages / 2));
    let endPage = Math.min(totalPages, startPage + maxVisiblePages - 1);

    if (endPage - startPage + 1 < maxVisiblePages) {
        startPage = Math.max(1, endPage - maxVisiblePages + 1);
    }

    const pages = Array.from({ length: endPage - startPage + 1 }, (_, i) => startPage + i);

    return (
        <div className="flex items-center justify-end space-x-2 py-4">
            {/* Nút Quay lại */}
            <Button variant="outline" size="sm" onClick={() => onPageChange(currentPage - 1)} disabled={currentPage === 1}>
                Trang trước
            </Button>

            {/* Thêm nút trang đầu nếu cần */}
            {startPage > 1 && (
                <>
                    <Button variant="outline" size="sm" onClick={() => onPageChange(1)}>
                        1
                    </Button>
                    {startPage > 2 && <span className="px-1">...</span>}
                </>
            )}

            {/* Các nút số trang chính */}
            <div className="flex space-x-1">
                {pages.map((page) => (
                    <Button key={page} variant={page === currentPage ? "default" : "outline"} size="sm" onClick={() => onPageChange(page)}>
                        {page}
                    </Button>
                ))}
            </div>

            {/* Thêm nút trang cuối nếu cần */}
            {endPage < totalPages && (
                <>
                    {endPage < totalPages - 1 && <span className="px-1">...</span>}
                    <Button variant="outline" size="sm" onClick={() => onPageChange(totalPages)}>
                        {totalPages}
                    </Button>
                </>
            )}

            {/* Nút Kế tiếp */}
            <Button variant="outline" size="sm" onClick={() => onPageChange(currentPage + 1)} disabled={currentPage === totalPages}>
                Trang sau
            </Button>
        </div>
    );
}

// --------------------------------------------------------
// COMPONENT DATA TABLE (Sẽ dùng trong ListUser)
// --------------------------------------------------------
// Component này chỉ định nghĩa cấu trúc bảng và nhận dữ liệu đã được phân trang
// Tách DataTable ra một file khác thì tốt hơn, nhưng giữ nó ở đây để dễ quản lý.
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";

export function DataTable({ headers, data, renderRow, totalItems, currentPage, totalPages, onPageChange }) {
    if (!data || data.length === 0) {
        return <p className="text-center py-8 text-gray-500">Không có dữ liệu để hiển thị.</p>;
    }

    return (
        <div>
            <div className="rounded-md border">
                <Table>
                    <TableHeader>
                        <TableRow>
                            {headers.map((header, index) => (
                                <TableHead key={index} className={header.className || ""}>
                                    {header.label}
                                </TableHead>
                            ))}
                        </TableRow>
                    </TableHeader>
                    <TableBody>{data.map((item, index) => renderRow(item, index))}</TableBody>
                </Table>
            </div>

            {/* Hiển thị số lượng mục và component phân trang */}
            <div className="flex items-center justify-between px-2 text-sm text-muted-foreground">
                <div className="flex-1">
                    Hiển thị {data.length} trên tổng số {totalItems} mục.
                </div>
                {totalPages > 1 && <Pagination currentPage={currentPage} totalPages={totalPages} onPageChange={onPageChange} />}
            </div>
        </div>
    );
}
