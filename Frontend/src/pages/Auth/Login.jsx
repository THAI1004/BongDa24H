"use client";

import { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import Loading from "../../components/Loading.jsx";
import { checkEmail, checkStrim } from "../../utils/Validate.js";
import { useGoogleLogin } from "@react-oauth/google";
import { toast } from "sonner";
const BEURL = import.meta.env.VITE_BEURL;

const GoogleIcon = () => (
    <svg className="w-5 h-5" viewBox="0 0 48 48">
        <path
            fill="#FFC107"
            d="M43.611 20.083H42V20H24v8h11.303c-1.649 4.657-6.08 8-11.303 8c-6.627 0-12-5.373-12-12s8.955-20 20-20s20-8.955 20-20c0-1.341-.138-2.65-.389-3.917z"
        ></path>
        <path
            fill="#FF3D00"
            d="M6.306 14.691l6.571 4.819C14.655 15.108 18.961 12 24 12c3.059 0 5.842 1.154 7.961 3.039l5.657-5.657C34.046 6.053 29.268 4 24 4C16.318 4 9.656 8.337 6.306 14.691z"
        ></path>
        <path
            fill="#4CAF50"
            d="M24 44c5.166 0 9.86-1.977 13.409-5.192l-6.19-5.238C29.211 35.091 26.715 36 24 36c-5.202 0-9.619-3.317-11.283-7.946l-6.522 5.025C9.505 39.556 16.227 44 24 44z"
        ></path>
        <path
            fill="#1976D2"
            d="M43.611 20.083H42V20H24v8h11.303c-.792 2.237-2.231 4.166-4.087 5.571l6.19 5.238C42.011 36.397 44 30.841 44 24c0-1.341-.138-2.65-.389-3.917z"
        ></path>
    </svg>
);

const Login = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState({ email: "", password: "" });
    const [isLoading, setLoading] = useState(false);
    const navigate = useNavigate();

    const validate = () => {
        const newError = { email: "", password: "" };
        if (!checkEmail(email)) {
            newError.email = "Email không hợp lệ";
        }
        if (!checkStrim(password, 6)) {
            newError.password = "password không hợp lệ";
        }
        if (newError.email != "" || newError.password != "") {
            setError(newError);
            return false;
        }
        return true;
    };

    const handleGoogleLoginSuccess = async (credentialResponse) => {
        // console.log(credentialResponse);

        const code = credentialResponse.code;

        try {
            const response = await axios.post(`${BEURL}/auth/googleLogin`, {
                code: code,
                redirectUri: import.meta.env.VITE_FRURL,
            });
            const { token, user } = response.data;
            localStorage.setItem("token", token);
            localStorage.setItem("user", JSON.stringify(user));
            toast.success("Đăng nhập bằng Google thành công.");
            navigate("/");
        } catch (error) {
            console.error("Lỗi khi đăng nhập bằng Google:", error);
        }
    };

    const googleLogin = useGoogleLogin({
        onSuccess: handleGoogleLoginSuccess,
        flow: "auth-code",
    });

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!validate()) return;
        setLoading(true);

        try {
            const response = await axios.post(`${BEURL}/auth/login`, {
                email: email,
                PasswordHash: password,
            });
            const { token, user } = response?.data;
            localStorage.setItem("token", token);
            localStorage.setItem("user", JSON.stringify(user));
            console.log(user);

            setLoading(false);
            toast.success("Đăng nhập thành công.");
            navigate("/");
        } catch (error) {
            const errorMessage = error.response?.data?.message || "Đã có lỗi xảy ra. Vui lòng thử lại.";
            setError(errorMessage);
            console.error("Lỗi đăng nhập:", error);
            setLoading(false);
        }
    };

    if (isLoading) return <Loading />;

    return (
        <div className="min-h-screen flex items-center justify-center px-4 py-12">
            <div className="w-full max-w-md">
                {/* Header */}
                <div className="text-center mb-8">
                    <h1 className="text-4xl font-bold text-black mb-3 tracking-tight">Đăng nhập</h1>
                    <p className="text-neutral-400 text-sm leading-relaxed">Chào mừng trở lại! Vui lòng đăng nhập để tiếp tục</p>
                </div>

                {/* Form Card */}
                <div className="bg-white rounded-2xl shadow-2xl p-8">
                    <form onSubmit={handleSubmit} className="space-y-5">
                        {/* Email Field */}
                        <div>
                            <label htmlFor="email" className="block text-sm font-semibold text-neutral-900 mb-2">
                                Email
                            </label>
                            <input
                                id="email"
                                type="text"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                placeholder="example@email.com"
                                className={`w-full px-4 py-3 rounded-lg border-2 transition-all duration-200 text-neutral-900 placeholder:text-neutral-400 focus:outline-none focus:ring-2 focus:ring-offset-2 ${
                                    error.email != ""
                                        ? "border-red-500 focus:border-red-500 focus:ring-red-500"
                                        : "border-neutral-200 focus:border-neutral-900 focus:ring-neutral-900"
                                }`}
                            />
                            {error.email && <p className="text-red-600 text-xs mt-2 font-medium">{error.email}</p>}
                        </div>

                        {/* Password Field */}
                        <div>
                            <label htmlFor="password" className="block text-sm font-semibold text-neutral-900 mb-2">
                                Mật khẩu
                            </label>
                            <input
                                id="password"
                                type="password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                placeholder="••••••••"
                                className={`w-full px-4 py-3 rounded-lg border-2 transition-all duration-200 text-neutral-900 placeholder:text-neutral-400 focus:outline-none focus:ring-2 focus:ring-offset-2 ${
                                    error.password != ""
                                        ? "border-red-500 focus:border-red-500 focus:ring-red-500"
                                        : "border-neutral-200 focus:border-neutral-900 focus:ring-neutral-900"
                                }`}
                            />
                            {error.password && <p className="text-red-600 text-xs mt-2 font-medium">{error.password}</p>}
                        </div>

                        {/* Submit Button */}
                        <button
                            type="submit"
                            className="w-full bg-neutral-900 hover:bg-neutral-800 text-white font-semibold py-3.5 px-4 rounded-lg transition-all duration-200 focus:outline-none focus:ring-2 focus:ring-neutral-900 focus:ring-offset-2 shadow-lg hover:shadow-xl"
                        >
                            Đăng nhập
                        </button>
                    </form>

                    {/* Divider */}
                    <div className="flex items-center gap-4 my-6">
                        <div className="flex-1 h-px bg-neutral-200"></div>
                        <span className="text-xs font-medium text-neutral-500 uppercase tracking-wider">Hoặc</span>
                        <div className="flex-1 h-px bg-neutral-200"></div>
                    </div>

                    {/* Google Login Button */}
                    <button
                        type="button"
                        onClick={() => googleLogin()}
                        className="w-full bg-white hover:bg-neutral-50 text-neutral-900 font-semibold py-3.5 px-4 rounded-lg border-2 border-neutral-200 transition-all duration-200 flex items-center justify-center gap-3 focus:outline-none focus:ring-2 focus:ring-neutral-900 focus:ring-offset-2"
                    >
                        <GoogleIcon />
                        Đăng nhập với Google
                    </button>
                </div>

                {/* Footer */}
                <p className="text-center text-neutral-400 text-sm mt-8">
                    Chưa có tài khoản?{" "}
                    <a
                        href="/register"
                        className="font-semibold text-black text-bold hover:text-neutral-300 transition-colors duration-200 underline underline-offset-4"
                    >
                        Đăng ký ngay
                    </a>
                </p>
            </div>
        </div>
    );
};

export default Login;
