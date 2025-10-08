import { useEffect, useState } from "react";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { User, Mail, Phone, Lock, Upload, Check } from "lucide-react";
import Loading from "@/components/Loading";
import { checkPhone, checkStrim } from "@/utils/Validate";
import axios from "axios";
import { toast } from "sonner";

export default function Profile() {
    const token = localStorage.getItem("token");
    const BEURL = import.meta.env.VITE_BEURL;
    const userLocal = JSON.parse(localStorage.getItem("user"));
    const id = userLocal?.id;

    const [userData, setUserData] = useState(null);
    const [formData, setFormData] = useState(null);
    const [imagePreview, setImagePreview] = useState("");
    const [isUpdating, setIsUpdating] = useState(false);
    const [updateSuccess, setUpdateSuccess] = useState(false);
    const [error, setError] = useState({
        confirmPassword: "",
        newPassword: "",
        phoneNumber: "",
        global: "",
    });

    // 🧠 Gọi API lấy thông tin người dùng
    useEffect(() => {
        const fetchUser = async () => {
            try {
                const res = await axios.get(`${BEURL}/auth/${id}`, {
                    headers: { Authorization: `Bearer ${token}` },
                });
                const user = res.data.data;

                setUserData(user);
                setFormData({
                    fullName: user.fullName,
                    phoneNumber: user.phoneNumber || "",
                    currentPassword: "",
                    newPassword: "",
                    confirmPassword: "",
                    imageFile: null,
                });
                setImagePreview(user.image ? `${import.meta.env.VITE_BEURLIMAGE}${user.image}` : "");
            } catch (err) {
                console.error("❌ Lỗi tải thông tin người dùng:", err);
            }
        };
        fetchUser();
    }, [BEURL, id, token]);

    // 🧠 Khi nhập form
    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    // 🖼️ Khi chọn ảnh
    const handleImageChange = (e) => {
        const file = e.target.files?.[0];
        if (file) {
            setFormData((prev) => ({ ...prev, imageFile: file }));
            const reader = new FileReader();
            reader.onloadend = () => setImagePreview(reader.result);
            reader.readAsDataURL(file);
        }
    };

    // 📤 Gửi form cập nhật
    const handleSubmit = async (e) => {
        e.preventDefault();
        setIsUpdating(true);
        setUpdateSuccess(false);
        setError({});

        if (formData.newPassword && formData.newPassword !== formData.confirmPassword) {
            setError((prev) => ({ ...prev, confirmPassword: "Mật khẩu xác nhận không khớp!" }));
            setIsUpdating(false);
            return;
        }

        if (formData.newPassword && !checkStrim(formData.newPassword, 6)) {
            setError((prev) => ({ ...prev, newPassword: "Mật khẩu phải có ít nhất 6 ký tự!" }));
            setIsUpdating(false);
            return;
        }

        if (formData.phoneNumber && !checkPhone(formData.phoneNumber)) {
            setError((prev) => ({ ...prev, phoneNumber: "Số điện thoại không hợp lệ!" }));
            setIsUpdating(false);
            return;
        }

        try {
            const data = new FormData();
            data.append("Id", id);
            data.append("FullName", formData.fullName);
            data.append("Email", userData.email);
            data.append("PhoneNumber", formData.phoneNumber || "");
            data.append("Role", userData.role ?? 0);
            data.append("AccumulatedPoints", userData.accumulatedPoints ?? 0);
            if (formData.imageFile) data.append("ImageFile", formData.imageFile);

            const res = await axios.put(`${BEURL}/auth/${id}`, data, {
                headers: { "Content-Type": "multipart/form-data", Authorization: `Bearer ${token}` },
            });

            const updatedUser = res.data.data;
            localStorage.setItem("user", JSON.stringify(updatedUser));
            toast.success("Cập nhật tài khoản thành công.");
            setUserData(updatedUser);
            setUpdateSuccess(true);
        } catch (err) {
            console.error("❌ Lỗi cập nhật:", err);
            setError((prev) => ({
                ...prev,
                global: "Cập nhật thất bại, vui lòng thử lại sau!",
            }));
        } finally {
            setIsUpdating(false);
        }
    };

    const getInitials = (name) =>
        name
            ?.split(" ")
            .map((n) => n[0])
            .join("")
            .toUpperCase()
            .slice(0, 2);

    if (!userData || !formData) return <Loading />;
    if (isUpdating) return <Loading />;

    return (
        <div className="min-h-screen bg-background py-8 px-4 sm:px-6 lg:px-8">
            <div className="max-w-6xl mx-auto">
                <div className="mb-8">
                    <h1 className="text-3xl font-bold text-foreground">Hồ sơ cá nhân</h1>
                    <p className="text-muted-foreground mt-2">Quản lý thông tin cá nhân và bảo mật tài khoản của bạn</p>
                </div>

                <Card className="shadow-lg">
                    <CardHeader>
                        <CardTitle>Thông tin tài khoản</CardTitle>
                        <CardDescription>Cập nhật thông tin cá nhân và mật khẩu của bạn</CardDescription>
                    </CardHeader>

                    <CardContent>
                        <form onSubmit={handleSubmit} className="space-y-6">
                            {/* Ảnh đại diện */}
                            <div className="flex flex-col items-center gap-4 pb-6 border-b border-border">
                                <Avatar className="h-24 w-24">
                                    <AvatarImage src={imagePreview || "/placeholder.svg"} />
                                    <AvatarFallback className="bg-primary text-primary-foreground text-2xl">
                                        {getInitials(formData.fullName)}
                                    </AvatarFallback>
                                </Avatar>
                                <div className="flex flex-col items-center gap-2">
                                    <Label htmlFor="image-upload" className="cursor-pointer">
                                        <div className="flex items-center gap-2 px-4 py-2 bg-secondary text-secondary-foreground rounded-md hover:bg-secondary/80 transition-colors">
                                            <Upload className="h-4 w-4" />
                                            <span className="text-sm font-medium">Tải ảnh lên</span>
                                        </div>
                                        <Input id="image-upload" type="file" accept="image/*" className="hidden" onChange={handleImageChange} />
                                    </Label>
                                    <p className="text-xs text-muted-foreground">JPG, PNG hoặc GIF (tối đa 2MB)</p>
                                </div>
                            </div>

                            {/* Thông tin cá nhân */}
                            <div className="space-y-4">
                                <div className="space-y-2">
                                    <Label htmlFor="fullName" className="flex items-center gap-2">
                                        <User className="h-4 w-4" />
                                        Họ và tên
                                    </Label>
                                    <Input
                                        id="fullName"
                                        name="fullName"
                                        type="text"
                                        value={formData.fullName}
                                        onChange={handleInputChange}
                                        placeholder="Nhập họ và tên"
                                        required
                                        className="w-full"
                                    />
                                </div>

                                <div className="space-y-2">
                                    <Label htmlFor="email" className="flex items-center gap-2">
                                        <Mail className="h-4 w-4" />
                                        Email
                                    </Label>
                                    <Input id="email" type="email" value={userData.email} disabled className="w-full bg-muted cursor-not-allowed" />
                                    <p className="text-xs text-muted-foreground">Email không thể thay đổi</p>
                                </div>

                                <div className="space-y-2">
                                    <Label htmlFor="phoneNumber" className="flex items-center gap-2">
                                        <Phone className="h-4 w-4" />
                                        Số điện thoại
                                    </Label>
                                    <Input
                                        id="phoneNumber"
                                        name="phoneNumber"
                                        type="tel"
                                        value={formData.phoneNumber}
                                        onChange={handleInputChange}
                                        placeholder="Nhập số điện thoại"
                                    />
                                    {error.phoneNumber && <p className="text-sm text-red-500">{error.phoneNumber}</p>}
                                </div>
                            </div>

                            {/* Đổi mật khẩu */}
                            <div className="space-y-4 pt-6 border-t border-border">
                                <h3 className="text-lg font-semibold text-foreground">Đổi mật khẩu</h3>
                                <p className="text-sm text-muted-foreground">Để trống nếu không muốn thay đổi mật khẩu</p>

                                <div className="space-y-2">
                                    <Label htmlFor="newPassword" className="flex items-center gap-2">
                                        <Lock className="h-4 w-4" />
                                        Mật khẩu mới
                                    </Label>
                                    <Input
                                        id="newPassword"
                                        name="newPassword"
                                        type="password"
                                        value={formData.newPassword}
                                        onChange={handleInputChange}
                                        placeholder="Nhập mật khẩu mới"
                                    />
                                    {error.newPassword && <p className="text-sm text-red-500">{error.newPassword}</p>}
                                </div>

                                <div className="space-y-2">
                                    <Label htmlFor="confirmPassword" className="flex items-center gap-2">
                                        <Lock className="h-4 w-4" />
                                        Xác nhận mật khẩu mới
                                    </Label>
                                    <Input
                                        id="confirmPassword"
                                        name="confirmPassword"
                                        type="password"
                                        value={formData.confirmPassword}
                                        onChange={handleInputChange}
                                        placeholder="Nhập lại mật khẩu mới"
                                    />
                                    {error.confirmPassword && <p className="text-sm text-red-500">{error.confirmPassword}</p>}
                                </div>
                            </div>

                            {/* Nút submit */}
                            <div className="flex items-center gap-4 pt-6">
                                <Button type="submit" disabled={isUpdating} className="w-full sm:w-auto">
                                    {isUpdating ? "Đang cập nhật..." : "Cập nhật thông tin"}
                                </Button>
                                {updateSuccess && (
                                    <div className="flex items-center gap-2 text-green-600 dark:text-green-400">
                                        <Check className="h-5 w-5" />
                                        <span className="text-sm font-medium">Cập nhật thành công!</span>
                                    </div>
                                )}
                            </div>
                        </form>
                    </CardContent>
                </Card>
            </div>
        </div>
    );
}
