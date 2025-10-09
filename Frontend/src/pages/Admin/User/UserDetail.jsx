import axios from "axios";
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

// IMPORT COMPONENTS TỪ SHADCN UI VÀ LUCIDE-REACT
import { Card, CardHeader, CardContent, CardTitle, CardDescription } from "@/components/ui/card";
import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { Badge } from "@/components/ui/badge";
import { Tabs, TabsList, TabsTrigger, TabsContent } from "@/components/ui/tabs";
import { Progress } from "@/components/ui/progress"; // Thêm Progress
import { Skeleton } from "@/components/ui/skeleton"; // Thêm Skeleton cho trạng thái loading
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert"; // Thêm Alert cho trạng thái lỗi
import { Mail, Phone, Star, Zap, Calendar, MessageSquare, AlertTriangle, Loader2 } from "lucide-react";
import Loading from "@/components/Loading";

export default function UserDetail() {
    const token = localStorage.getItem("token");
    // Giả sử VITE_BEURL được cấu hình đúng
    const BEURL = import.meta.env.VITE_BEURL;
    const { id } = useParams(); // Lấy trực tiếp id
    const [loading, setLoading] = useState(true); // Mặc định là true để hiển thị Skeleton
    const [user, setUser] = useState(null); // Khởi tạo là null
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchUser = async () => {
            setLoading(true);
            try {
                const response = await axios.get(`${BEURL}/auth/${id}`, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });

                if (!response?.data?.data) {
                    setError("Không tìm thấy dữ liệu người dùng.");
                } else {
                    setUser(response.data.data);
                }
            } catch (err) {
                console.error(err);
                setError("Đã xảy ra lỗi khi tải dữ liệu.");
            } finally {
                setLoading(false);
            }
        };

        // Kiểm tra token và id trước khi fetch
        if (token && id) {
            fetchUser();
        } else if (!id) {
            setError("Không tìm thấy ID người dùng trong URL.");
            setLoading(false);
        } else {
            setError("Thiếu Token xác thực.");
            setLoading(false);
        }
    }, [id, token, BEURL]);

    // --- RENDER LOGIC ---

    // 1. Trạng thái Loading
    if (loading) return <Loading />;

    // 2. Trạng thái Lỗi
    if (error || !user) {
        return (
            <div className="p-8">
                <Alert variant="destructive">
                    <AlertTitle>Lỗi</AlertTitle>
                    <AlertDescription>{error || "Không thể hiển thị chi tiết người dùng."}</AlertDescription>
                </Alert>
            </div>
        );
    }

    // 3. Trạng thái Thành công (Hiển thị chi tiết)

    // Lấy dữ liệu cần thiết từ object user
    const team = user.teams.length > 0 ? user.teams[0] : null;
    const winRate = team ? Math.round((team.wins / team.totalMatches) * 100) : 0;
    const nameInitial = user.fullName
        .split(" ")
        .map((n) => n[0])
        .join("");

    return (
        <div className="p-8 max-w-7xl mx-auto">
            <h1 className="text-3xl font-bold mb-6">Chi Tiết Người Dùng</h1>

            <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
                {/* Cột 1: Profile và Team */}
                <div className="lg:col-span-1 space-y-6">
                    {/* Thẻ Thông tin Cá nhân */}
                    <Card>
                        <CardHeader className="flex flex-col items-center pt-8">
                            <Avatar className="h-24 w-24 mb-4">
                                {/* Dùng nameInitial nếu image null */}
                                <AvatarFallback className="text-3xl bg-blue-500 text-white">{nameInitial}</AvatarFallback>
                            </Avatar>
                            <CardTitle className="text-2xl">{user.fullName}</CardTitle>
                            <Badge variant="outline">ID: {user.id}</Badge>
                            <Badge className="mt-2" variant="secondary">
                                Vai trò: {user.role === 0 ? "Người chơi" : "Khác"}
                            </Badge>
                        </CardHeader>
                        <CardContent className="space-y-3 pb-6">
                            <div className="flex items-center text-sm">
                                <Star className="w-4 h-4 mr-2 text-yellow-500" />
                                <span className="font-semibold">{user.accumulatedPoints.toLocaleString("vi-VN")}</span> điểm tích lũy
                            </div>
                            <div className="flex items-center text-sm">
                                <Mail className="w-4 h-4 mr-2 text-gray-500" />
                                {user.email}
                            </div>
                            <div className="flex items-center text-sm">
                                <Phone className="w-4 h-4 mr-2 text-gray-500" />
                                {user.phoneNumber}
                            </div>
                        </CardContent>
                    </Card>

                    {/* Thẻ Đội bóng */}
                    {team && (
                        <Card className="border-l-4 border-primary">
                            <CardHeader>
                                <CardTitle className="text-lg flex items-center">
                                    <Zap className="w-5 h-5 mr-2 text-primary" />
                                    Quản Lý Đội: {team.teamName}
                                </CardTitle>
                                <CardDescription>ID Đội: {team.id}</CardDescription>
                            </CardHeader>
                            <CardContent>
                                <div className="text-sm space-y-1">
                                    <p>
                                        Tổng Trận: <span className="font-semibold">{team.totalMatches}</span>
                                    </p>
                                    <p>
                                        Thắng: <span className="font-semibold text-green-600">{team.wins}</span>
                                    </p>
                                    <div className="mt-3">
                                        <p className="mb-1">
                                            Tỷ lệ thắng: <span className="font-semibold">{winRate}%</span>
                                        </p>
                                        <Progress value={winRate} className="h-2" />
                                    </div>
                                </div>
                            </CardContent>
                        </Card>
                    )}
                </div>

                {/* Cột 2: Dữ liệu chi tiết theo Tab */}
                <div className="lg:col-span-2">
                    <Tabs defaultValue="bookings">
                        <TabsList className="grid w-full grid-cols-2 sm:grid-cols-4 lg:grid-cols-4 h-auto">
                            <TabsTrigger value="bookings" className="flex items-center">
                                <Calendar className="w-4 h-4 mr-1" /> Đặt Sân ({user.bookings.length})
                            </TabsTrigger>
                            <TabsTrigger value="requests" className="flex items-center">
                                <Zap className="w-4 h-4 mr-1" /> Yêu Cầu Trận ({user.matchRequests.length})
                            </TabsTrigger>
                            <TabsTrigger value="responses" className="flex items-center">
                                <MessageSquare className="w-4 h-4 mr-1" /> Phản Hồi ({user.matchResponses.length})
                            </TabsTrigger>
                            <TabsTrigger value="reports" className="flex items-center">
                                <AlertTriangle className="w-4 h-4 mr-1" /> Báo Cáo ({user.reports.length})
                            </TabsTrigger>
                        </TabsList>

                        {/* Nội dung Tab Đặt Sân */}
                        <TabsContent value="bookings" className="mt-4">
                            <Card>
                                <CardHeader>
                                    <CardTitle>Chi Tiết Đặt Sân</CardTitle>
                                </CardHeader>
                                <CardContent className="space-y-2">
                                    {user.bookings.length > 0 ? (
                                        user.bookings.map((booking, index) => (
                                            <div key={index} className="flex justify-between items-center p-3 border rounded-md">
                                                <div className="text-sm">
                                                    <p>
                                                        Ngày: <span className="font-medium">{booking.bookingDate}</span>
                                                    </p>
                                                    <p>
                                                        Giờ: <span className="font-medium text-blue-600">{booking.timeSlot}</span>
                                                    </p>
                                                </div>
                                                <Badge variant={booking.status === 1 ? "default" : "secondary"}>
                                                    {booking.status === 1 ? "Đã Xác Nhận" : "Chờ"}
                                                </Badge>
                                            </div>
                                        ))
                                    ) : (
                                        <p className="text-center text-gray-500">Không có lịch đặt sân nào.</p>
                                    )}
                                </CardContent>
                            </Card>
                        </TabsContent>

                        {/* Nội dung Tab Yêu cầu Tìm Trận */}
                        <TabsContent value="requests" className="mt-4">
                            <Card>
                                <CardHeader>
                                    <CardTitle>Chi Tiết Yêu Cầu Tìm Trận</CardTitle>
                                </CardHeader>
                                <CardContent className="space-y-2">
                                    {user.matchRequests.length > 0 ? (
                                        user.matchRequests.map((request, index) => (
                                            <div key={index} className="p-3 border rounded-md">
                                                <p className="text-sm">
                                                    Ngày: <span className="font-medium">{request.matchDate}</span> - Giờ:{" "}
                                                    <span className="font-medium">{request.timeSlot}</span>
                                                </p>
                                                <p className="text-xs text-gray-500">
                                                    Sân ID: {request.pitchId} | Cấp độ: {request.skillLevel}
                                                </p>
                                            </div>
                                        ))
                                    ) : (
                                        <p className="text-center text-gray-500">Không có yêu cầu tìm trận nào.</p>
                                    )}
                                </CardContent>
                            </Card>
                        </TabsContent>

                        {/* Nội dung Tab Phản Hồi Trận đấu */}
                        <TabsContent value="responses" className="mt-4">
                            <Card>
                                <CardHeader>
                                    <CardTitle>Chi Tiết Phản Hồi Trận Đấu</CardTitle>
                                </CardHeader>
                                <CardContent className="space-y-2">
                                    {user.matchResponses.length > 0 ? (
                                        user.matchResponses.map((response, index) => (
                                            <div key={index} className="p-3 border rounded-md">
                                                <p className="text-sm italic">"{response.content}"</p>
                                                <p className="text-xs text-gray-500 mt-1">
                                                    Yêu cầu ID: {response.requestId} | Trạng thái:{" "}
                                                    <Badge variant="outline">{response.status === 0 ? "Chờ" : "Khác"}</Badge>
                                                </p>
                                            </div>
                                        ))
                                    ) : (
                                        <p className="text-center text-gray-500">Không có phản hồi trận đấu nào.</p>
                                    )}
                                </CardContent>
                            </Card>
                        </TabsContent>

                        {/* Nội dung Tab Báo cáo */}
                        <TabsContent value="reports" className="mt-4">
                            <Card>
                                <CardHeader>
                                    <CardTitle>Chi Tiết Báo Cáo</CardTitle>
                                </CardHeader>
                                <CardContent className="space-y-2">
                                    {user.reports.length > 0 ? (
                                        user.reports.map((report, index) => (
                                            <div key={index} className="p-3 border rounded-md">
                                                <p className="text-sm font-medium text-red-600">{report.reason}</p>
                                                <p className="text-xs text-gray-500 mt-1">
                                                    Mục tiêu: {report.targetType} (ID: {report.targetId})
                                                </p>
                                            </div>
                                        ))
                                    ) : (
                                        <p className="text-center text-gray-500">Không có báo cáo nào.</p>
                                    )}
                                </CardContent>
                            </Card>
                        </TabsContent>
                    </Tabs>
                </div>
            </div>
        </div>
    );
}
