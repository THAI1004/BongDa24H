import React, { useEffect, useState } from "react";
import axios from "axios";
import { useParams, Link } from "react-router-dom";

// shadcn/ui components (assume your project has these components available)
import { Card, CardHeader, CardTitle, CardContent, CardFooter } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Avatar, AvatarImage, AvatarFallback } from "@/components/ui/avatar";
import { Separator } from "@/components/ui/separator";
import { Badge } from "@/components/ui/badge";
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from "@/components/ui/tooltip"; // Đảm bảo Tooltip có đủ các phần tử
import { SquareEqual, Trophy, ShieldHalf, Trash2, Users } from "lucide-react"; // Icons mới
import Loading from "@/components/Loading";

export default function TeamDetail() {
    // === Logic: GIỮ NGUYÊN ===
    const { id } = useParams();
    const BEURL = import.meta.env.VITE_BEURL;
    const token = localStorage.getItem("token");

    const [team, setTeam] = useState(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    useEffect(() => {
        const fetchTeam = async () => {
            setLoading(true);
            try {
                const resp = await axios.get(`${BEURL}/team/${id}`, {
                    headers: { Authorization: `Bearer ${token}` },
                });
                const data = resp?.data?.data ?? null;
                setTeam(data);
                if (!data) setError("Không tìm thấy đội bóng");
            } catch (err) {
                console.error(err);
                setError("Đã xảy ra lỗi khi tải dữ liệu.");
            } finally {
                setLoading(false);
            }
        };

        if (id) fetchTeam();
    }, [BEURL, id, token]);

    if (loading) return <Loading />;

    if (error)
        return (
            <div className="p-4">
                <Card>
                    <CardContent>
                        <p className="text-center text-red-600 pt-4">{error}</p>
                    </CardContent>
                </Card>
            </div>
        );

    if (!team)
        return (
            <div className="p-4">
                <Card>
                    <CardContent>
                        <p className="text-center pt-4">Không có dữ liệu đội bóng.</p>
                    </CardContent>
                </Card>
            </div>
        );
    // safe accessors và tính toán
    const manager = team.manager ?? null;
    const totalMatches = team.totalMatches ?? 0;
    const wins = team.wins ?? 0;
    const winRate = totalMatches > 0 ? Math.round((wins / totalMatches) * 100) : 0;
    // === HẾT LOGIC ===

    // === GIAO DIỆN MỚI ===
    return (
        <TooltipProvider>
            <div className="p-4 max-w-7xl mx-auto">
                {/* Header (Giữ nguyên) */}
                <div className="flex items-center justify-between mb-6">
                    <h1 className="text-3xl font-bold tracking-tight">Chi tiết đội: {team.teamName}</h1>
                    {team.isDeleted && (
                        <Badge variant="destructive" className="flex items-center gap-1">
                            <Trash2 className="w-4 h-4" /> Đã xoá
                        </Badge>
                    )}
                </div>

                {/* Main Grid: Responsive (Mobile: 1 cột, Tablet: 2 cột, Desktop: 3 cột) */}
                <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
                    {/* Cột 1: Thông tin tóm tắt và Quản lý (Stacking trên Mobile/Tablet) */}
                    <div className="lg:col-span-1 space-y-6">
                        {/* Thẻ 1: Thông tin Tóm tắt */}
                        <Card>
                            <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                                <CardTitle className="text-lg font-semibold">Tóm tắt</CardTitle>
                                <Badge variant={team.isDeleted ? "destructive" : "default"}>{team.isDeleted ? "Ngừng hoạt động" : "Hoạt động"}</Badge>
                            </CardHeader>
                            <CardContent>
                                <div className="flex flex-col items-center justify-center text-center space-y-3 pt-4">
                                    <Avatar className="w-24 h-24 border-4 border-primary/50 shadow-lg">
                                        {team.image ? (
                                            <AvatarImage src={`${BEURL}${team.image}`} alt={team.teamName} />
                                        ) : (
                                            <AvatarFallback className="text-3xl font-bold bg-gray-200">
                                                {team.teamName?.slice(0, 1) ?? "T"}
                                            </AvatarFallback>
                                        )}
                                    </Avatar>
                                    <h3 className="text-xl font-bold">{team.teamName}</h3>
                                    <p className="text-sm text-muted-foreground">ID: {team.id}</p>
                                </div>
                                <Separator className="my-4" />
                                <div className="space-y-3 text-sm">
                                    <div className="flex justify-between items-center">
                                        <span className="text-muted-foreground flex items-center gap-1">Cấp độ kỹ năng</span>
                                        <Badge variant="outline">{team.skillLevel ?? "Chưa cập nhật"}</Badge>
                                    </div>
                                    <div className="flex justify-between items-center">
                                        <span className="text-muted-foreground flex items-center gap-1">Tổng thành viên</span>
                                        <span className="font-medium">{team.totalMembers ?? "—"}</span>
                                    </div>
                                </div>
                            </CardContent>
                        </Card>

                        {/* Thẻ 2: Quản lý đội */}
                        <Card>
                            <CardHeader>
                                <CardTitle className="text-lg font-semibold">Quản lý đội</CardTitle>
                            </CardHeader>
                            <CardContent>
                                {manager ? (
                                    <div className="flex items-center gap-4">
                                        <Avatar className="w-12 h-12">
                                            {manager.image ? (
                                                <AvatarImage src={`${BEURL}${manager.image}`} alt={manager.fullName} />
                                            ) : (
                                                <AvatarFallback>{manager.fullName?.slice(0, 1) ?? "U"}</AvatarFallback>
                                            )}
                                        </Avatar>
                                        <div>
                                            <div className="flex items-center gap-2">
                                                <h4 className="font-bold">{manager.fullName}</h4>
                                                <Tooltip>
                                                    <TooltipTrigger asChild>
                                                        <Badge variant="secondary" className="cursor-help text-xs">
                                                            Email
                                                        </Badge>
                                                    </TooltipTrigger>
                                                    <TooltipContent>
                                                        <p>Email: {manager.email}</p>
                                                    </TooltipContent>
                                                </Tooltip>
                                            </div>
                                            <p className="text-sm text-muted-foreground">SĐT: {manager.phoneNumber ?? "—"}</p>
                                        </div>
                                    </div>
                                ) : (
                                    <p className="text-muted-foreground">Chưa có quản lý được chỉ định.</p>
                                )}
                            </CardContent>
                            <CardFooter className="pt-4">
                                <Link to={`/admin/user/${manager?.id}`} className="w-full">
                                    <Button variant="outline" size="sm" className="w-full" disabled={!manager}>
                                        Chi tiết hồ sơ quản lý
                                    </Button>
                                </Link>
                            </CardFooter>
                        </Card>
                    </div>

                    {/* Cột 2 & 3: Số liệu chi tiết và Mô tả (Chiếm 2/3 không gian trên Desktop) */}
                    <div className="lg:col-span-2 space-y-6">
                        {/* Thẻ 3: Số liệu Thống kê Chính */}
                        <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
                            {/* Tổng trận */}
                            <Card className="text-center shadow-md">
                                <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                                    <CardTitle className="text-sm font-medium">Tổng trận</CardTitle>
                                    <SquareEqual className="w-4 h-4 text-muted-foreground" />
                                </CardHeader>
                                <CardContent>
                                    <div className="text-3xl font-bold">{totalMatches}</div>
                                </CardContent>
                            </Card>

                            {/* Thắng */}
                            <Card className="text-center shadow-md">
                                <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                                    <CardTitle className="text-sm font-medium">Chiến thắng</CardTitle>
                                    <Trophy className="w-4 h-4 text-green-500" />
                                </CardHeader>
                                <CardContent>
                                    <div className="text-3xl font-bold text-green-600">{wins}</div>
                                </CardContent>
                            </Card>

                            {/* Tỷ lệ thắng */}
                            <Card className="text-center shadow-md">
                                <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                                    <CardTitle className="text-sm font-medium">Tỷ lệ thắng</CardTitle>
                                    <ShieldHalf className="w-4 h-4 text-blue-500" />
                                </CardHeader>
                                <CardContent>
                                    <div className="text-3xl font-bold text-blue-600">{winRate}%</div>
                                </CardContent>
                            </Card>

                            {/* Điểm tích lũy quản lý */}
                            <Card className="text-center shadow-md">
                                <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                                    <CardTitle className="text-sm font-medium">Điểm quản lý</CardTitle>
                                    <Users className="w-4 h-4 text-yellow-600" />
                                </CardHeader>
                                <CardContent>
                                    <div className="text-3xl font-bold text-yellow-700">{manager?.accumulatedPoints ?? 0}</div>
                                </CardContent>
                            </Card>
                        </div>

                        {/* Thẻ 4: Ghi chú / Mô tả */}
                        <Card>
                            <CardHeader>
                                <CardTitle className="text-lg font-semibold">Mô tả chi tiết</CardTitle>
                            </CardHeader>
                            <CardContent>
                                <p className="text-sm text-muted-foreground leading-relaxed">
                                    {team.description ?? "Đội bóng này chưa cung cấp mô tả chi tiết."}
                                </p>
                            </CardContent>
                        </Card>
                    </div>
                </div>
            </div>
        </TooltipProvider>
    );
}
