"use client";

import { useState } from "react";
import Loading from "../../components/Loading.jsx";
import { checkEmail, checkStrim } from "../../utils/Validate.js";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { toast } from "sonner";
const BEURL = import.meta.env.VITE_BEURL;

const Register = () => {
    const [isloading, setIsLoading] = useState(false);
    const [user, setUser] = useState({ fullname: "", email: "", password: "", confirmPassword: "" });
    const [error, setError] = useState({ fullname: "", email: "", password: "", confirmPassword: "", general: "" });
    const navigate = useNavigate();
    const validate = () => {
        const newError = { fullname: "", email: "", password: "", confirmPassword: "" };
        if (!checkStrim(user.fullname, 5)) {
            newError.fullname = "Họ và tên không hợp lệ";
        }
        if (!checkEmail(user.email)) {
            newError.email = "Email không hợp lệ";
        }
        if (!checkStrim(user.password, 6)) {
            newError.password = "Mật khẩu không hợp lệ";
        }
        if (user.password !== user.confirmPassword) {
            newError.confirmPassword = "Mật khẩu không khớp";
        }
        if (newError.fullname != "" || newError.email != "" || newError.password != "" || newError.confirmPassword != "") {
            setError(newError);
            return false;
        }
        return true;
    };
    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!validate()) return;
        setIsLoading(true);
        try {
            const response = await axios.post(`${BEURL}/auth/register`, {
                FullName: user.fullname,
                Email: user.email,
                PasswordHash: user.password,
            });
            console.log(response);
            setIsLoading(false);
            toast.success("Thêm mới tài khoản thành công.");

            setTimeout(() => {
                navigate("/login");
            }, 1000);
        } catch (err) {
            const errorMessage = err.response?.data || "Đã có lỗi xảy ra. Vui lòng thử lại.";
            console.log(err.response?.data);
            setError((prev) => ({ ...prev, general: errorMessage }));
            toast.error("Thêm mới tài khoản thất bại.");
            setIsLoading(false);
        }
    };
    if (isloading) return <Loading />;
    return (
        <div className="min-h-screen mt-1 flex items-center justify-center  font-sans px-4">
            <div className="w-full max-w-xl">
                <div className="bg-white rounded-2xl shadow-2xl p-8">
                    <div className="text-center mb-4">
                        <h2 className="text-3xl font-bold text-neutral-900 mb-1">Tạo Tài Khoản</h2>
                        <p className="text-neutral-600 text-sm">Điền thông tin để bắt đầu</p>
                    </div>

                    <form onSubmit={handleSubmit} className="space-y-5">
                        <div>
                            <label className="block text-neutral-700 text-sm font-semibold mb-2" htmlFor="fullName">
                                Họ và Tên
                            </label>
                            <input
                                className={`w-full px-4 py-3 rounded-lg border bg-white text-neutral-900 placeholder-neutral-400 transition-all duration-200 focus:outline-none focus:ring-2 focus:ring-neutral-900 focus:border-transparent ${
                                    error.fullname ? "border-red-500 focus:ring-red-500" : "border-neutral-200"
                                }`}
                                id="fullName"
                                type="text"
                                placeholder="Nhập họ và tên của bạn"
                                value={user.fullname}
                                onChange={(e) => setUser({ ...user, fullname: e.target.value })}
                            />
                            {error.fullname && (
                                <p className="text-red-500 text-xs mt-1 flex items-center gap-1">
                                    <span>⚠</span> {error.fullname}
                                </p>
                            )}
                        </div>

                        <div>
                            <label className="block text-neutral-700 text-sm font-semibold mb-2" htmlFor="email">
                                Email
                            </label>
                            <input
                                className={`w-full px-4 py-3 rounded-lg border bg-white text-neutral-900 placeholder-neutral-400 transition-all duration-200 focus:outline-none focus:ring-2 focus:ring-neutral-900 focus:border-transparent ${
                                    error.email || error.general ? "border-red-500 focus:ring-red-500" : "border-neutral-200"
                                }`}
                                id="email"
                                type="text"
                                placeholder="Nhập email của bạn"
                                value={user.email}
                                onChange={(e) => setUser({ ...user, email: e.target.value })}
                            />
                            {error.email && (
                                <p className="text-red-500 text-xs mt-1.5 flex items-center gap-1">
                                    <span>⚠</span> {error.email}
                                </p>
                            )}
                            {error.general && (
                                <p className="text-red-500 text-xs mt-1 flex items-center gap-1">
                                    <span>⚠</span> {error.general}
                                </p>
                            )}
                        </div>

                        <div>
                            <label className="block text-neutral-700 text-sm font-semibold mb-2" htmlFor="password">
                                Mật khẩu
                            </label>
                            <input
                                className={`w-full px-4 py-3 rounded-lg border bg-white text-neutral-900 placeholder-neutral-400 transition-all duration-200 focus:outline-none focus:ring-2 focus:ring-neutral-900 focus:border-transparent ${
                                    error.password ? "border-red-500 focus:ring-red-500" : "border-neutral-200"
                                }`}
                                id="password"
                                type="password"
                                placeholder="Nhập mật khẩu"
                                value={user.password}
                                onChange={(e) => setUser({ ...user, password: e.target.value })}
                            />
                            {error.password && (
                                <p className="text-red-500 text-xs mt-1 flex items-center gap-1">
                                    <span>⚠</span> {error.password}
                                </p>
                            )}
                        </div>

                        <div>
                            <label className="block text-neutral-700 text-sm font-semibold mb-2" htmlFor="confirmPassword">
                                Xác nhận Mật khẩu
                            </label>
                            <input
                                className={`w-full px-4 py-3 rounded-lg border bg-white text-neutral-900 placeholder-neutral-400 transition-all duration-200 focus:outline-none focus:ring-2 focus:ring-neutral-900 focus:border-transparent ${
                                    error.confirmPassword ? "border-red-500 focus:ring-red-500" : "border-neutral-200"
                                }`}
                                id="confirmPassword"
                                type="password"
                                placeholder="Nhập lại mật khẩu"
                                value={user.confirmPassword}
                                onChange={(e) => setUser({ ...user, confirmPassword: e.target.value })}
                            />
                            {error.confirmPassword && (
                                <p className="text-red-500 text-xs mt-1 flex items-center gap-1">
                                    <span>⚠</span> {error.confirmPassword}
                                </p>
                            )}
                        </div>

                        <button
                            className="w-full bg-neutral-900 hover:bg-neutral-800 text-white font-semibold py-3 px-4 rounded-lg transition-all duration-200 shadow-lg hover:shadow-xl mt-2"
                            type="submit"
                        >
                            Đăng Ký
                        </button>
                    </form>

                    <p className="text-center text-neutral-600 text-sm mt-6">
                        Đã có tài khoản?{" "}
                        <a href="/login" className="font-semibold text-neutral-900 hover:underline transition-all">
                            Đăng nhập tại đây
                        </a>
                    </p>
                    <p className="text-center text-neutral-500 text-xs ">Bằng việc đăng ký, bạn đồng ý với điều khoản sử dụng của chúng tôi</p>
                </div>
            </div>
        </div>
    );
};

export default Register;
